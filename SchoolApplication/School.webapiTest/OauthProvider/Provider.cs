using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using School.webapiTest.App_Start;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace School.webapiTest.OauthProvider
{
    public class Provider : OAuthAuthorizationServerProvider
    {

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            return Task.Factory.StartNew(() =>
            {
                var username = context.UserName;
                var password = context.Password;
                //  var userRepository = new UserRolesRepository(_context);
                // var user = _repo.ValidateUser(username, password);
                // var user = _repo.ValidateUser(username, password);
                if (username == "b761")
                {
                    var claims = new List<Claim>()
                          {
                               new Claim(ClaimTypes.Sid,"1"),
                               new Claim(ClaimTypes.Name, "sarfraz"),
                               new Claim(ClaimTypes.NameIdentifier, "blawal")
                          };
                    //foreach (var role in user.roles)
                    //{
                    //    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    //}

                    var props = new AuthenticationProperties(new Dictionary<string, string>
                         {
                           {"Id","1"},
                           {"UserName","blawal" },
                           {"Password","b761"},
                         });
                    ClaimsIdentity oAutIdentity = new ClaimsIdentity(claims, Startup.OAuthOptions.AuthenticationType);
                    context.Validated(new AuthenticationTicket(oAutIdentity, props));
                }
                else
                {
                    context.SetError("invalid_grant", "Error");
                }
            });
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}