using GenericBackend.Identity.Core;
using Microsoft.AspNet.Identity;

namespace GenericBackend.Identity.Identity
{
    public class ApplicationRoleManager : RoleManager<IdentityRole>, IApplicationRoleManager
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> store) 
            : base(store)
        {
        }
    }
}