using AutoMapper;
using LoggingTest.Identity.Models;
using LoggingTest.Identity.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;

        public UsersController(UserManager<ApiUser> userManager, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegistrationResource model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userIdentity = _mapper.Map<ApiUser>(model);
            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return new OkObjectResult("Account created");
        }
    }
}
