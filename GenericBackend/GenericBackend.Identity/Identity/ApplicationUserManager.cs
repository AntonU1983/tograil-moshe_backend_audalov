using GenericBackend.Identity.Core;
using Microsoft.AspNet.Identity;

namespace GenericBackend.Identity.Identity
{
    public class ApplicationUserManager : UserManager<IdentityUser>, IApplicationUserManager
    {
        public ApplicationUserManager(IUserStore<IdentityUser> store) 
            : base(store)
        {
        }
    }
}