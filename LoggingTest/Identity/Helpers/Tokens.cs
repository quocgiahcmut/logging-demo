using LoggingTest.Identity.Auth;
using LoggingTest.Identity.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoggingTest.Identity.Helpers
{
    public class Tokens
    {
        public static async Task<JwtToken> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            var token = new JwtToken
            {
                Id = identity.Claims.Single(c => c.Type == "id").Value,
                AuthToken = await jwtFactory.GenerateEncodedToken(userName, identity),
                ExpiresIn = (int)jwtOptions.ValidFor.TotalSeconds
            };
            return token;
        }
    }
}
