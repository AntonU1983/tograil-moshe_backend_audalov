using System.Web.Http;
using System.Web.Http.Controllers;
using GenericBackend.Core.Utils;
using GenericBackend.Identity;
using GenericBackend.Identity.Core;
using GenericBackend.Identity.Identity;

namespace GenericBackend.Helpers
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    { 
        public string AccessLevel { get; set; }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var userName = actionContext.RequestContext.Principal.Identity.Name;
            if (string.IsNullOrEmpty(userName))
            {
                HandleUnauthorizedRequest(actionContext);
            }
            else
            {
                using (var repo = new ApplicationUserManager(new UserStore<IdentityUser>(MongoUtil<IdentityUser>.GetDefaultConnectionString())))
                {
                    var user = repo.FindByNameAsync(userName).Result;

                    if (user == null)
                    {
                        HandleUnauthorizedRequest(actionContext);
                    }
                    else
                    {
                        if (user.Roles.Contains(AccessLevel))
                        {
                            base.OnAuthorization(actionContext);
                        }
                        else
                        {
                            HandleUnauthorizedRequest(actionContext);
                        }
                    }
                }
            }
        } 
    }
}