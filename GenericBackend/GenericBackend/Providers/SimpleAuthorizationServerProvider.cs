using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using GenericBackend.Core.Utils;
using GenericBackend.DataModels;
using GenericBackend.Identity;
using GenericBackend.Identity.Core;
using GenericBackend.Identity.Identity;
using GenericBackend.Repository.Admin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace GenericBackend.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            /*context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Origin, X-Requested-With, Content-Type, Accept" });
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "GET,POST,OPTIONS,PUT" });*/

            using (var repo =new ApplicationUserManager(new UserStore<Identity.Core.IdentityUser>(MongoUtil<Identity.Core.IdentityUser>.GetDefaultConnectionString())))
            {
                var user = await repo.FindAsync(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
               
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                foreach (var role in user.Roles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                context.Validated(identity);
            }
        }

        public static AuthenticationProperties CreateProperties(Identity.Core.IdentityUser user)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", user.UserName }
            };
            return new AuthenticationProperties(data);
        }
    }
}