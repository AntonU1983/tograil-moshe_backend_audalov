using System;
using Microsoft.AspNet.Identity;

namespace GenericBackend.Identity.Core
{
    public class IdentityRole : IRole<string>
    {
        public IdentityRole()
        {
            Id = Guid.NewGuid().ToString("D");
        }

        public IdentityRole(string roleName) : this()
        {
            Name = roleName;
        }

        public string Id { get; }

        public string Name { get; set; }
    }
}
