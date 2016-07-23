using System.Linq;
using System.Threading.Tasks;
using GenericBackend.Identity.Core;
using Microsoft.AspNet.Identity;

namespace GenericBackend.Identity.Identity
{
    public interface IApplicationRoleManager
    {
        void Dispose();
        Task<IdentityResult> CreateAsync(IdentityRole role);
        Task<IdentityResult> UpdateAsync(IdentityRole role);
        Task<IdentityResult> DeleteAsync(IdentityRole role);
        Task<bool> RoleExistsAsync(string roleName);
        Task<IdentityRole> FindByIdAsync(string roleId);
        Task<IdentityRole> FindByNameAsync(string roleName);
        IIdentityValidator<IdentityRole> RoleValidator { get; set; }
        IQueryable<IdentityRole> Roles { get; }
    }
}