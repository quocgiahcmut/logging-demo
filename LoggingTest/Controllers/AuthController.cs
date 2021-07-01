using AutoMapper;
using LoggingTest.Domain.Communication;
using LoggingTest.Identity.Auth;
using LoggingTest.Identity.Helpers;
using LoggingTest.Identity.Models;
using LoggingTest.Identity.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoggingTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IMapper _mapper;

        public AuthController(UserManager<ApiUser> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, IMapper mapper)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CredentialsResource credentials)
        {
            if (!ModelState.IsValid)
            {
                var errorDetails = string.Join('\n', ModelState.Values.Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage));
                var error = Error.BadRequest("Login.BadFormat", errorDetails);
                return BadRequest(error);
            }
            var identity = await GetClaimsIdentity(credentials.Username, credentials.Password);
            if (identity == null)
            {
                var error = Error.BadRequest("Login.Invalid", "Username or password is invalid");
                return BadRequest(error);
            }
            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, credentials.Username, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            var user = await _userManager.FindByNameAsync(credentials.Username);
            var employee = _mapper.Map<ApiUser, EmployeeResource>(user);
            var loginResource = new LoginResource()
            {
                Token = jwt,
                Employee = employee
            };
            return new OkObjectResult(loginResource);

        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }
            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
