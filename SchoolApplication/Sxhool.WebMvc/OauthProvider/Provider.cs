using IRepository;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using School.Repository;
using Sxhool.WebMvc.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Sxhool.WebMvc.OauthProvider
{
    public class Provider : OAuthAuthorizationServerProvider
    {
        private IUser _repo;
        public Provider(IUser Cont)
        {
            _repo = Cont;
        }
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var username = context.UserName;
                var password = context.Password;

                var user = _repo.ValidateUser(username, password);
                if (user != null)
                {
                    var claims = new List<Claim>()
                    { new Claim(ClaimTypes.Sid, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.FullName),
                        new Claim("UserID", user.UserName)
                    };
                    foreach (var role in user.roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                    ClaimsIdentity oAutIdentity = new ClaimsIdentity(claims, Startup.OAuthOptions.AuthenticationType);
                    context.Validated(new AuthenticationTicket(oAutIdentity, new AuthenticationProperties() { }));
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
    }
}