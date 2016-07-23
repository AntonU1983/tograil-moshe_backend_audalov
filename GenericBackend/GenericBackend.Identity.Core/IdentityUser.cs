using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace GenericBackend.Identity.Core
{
    public class IdentityUser : IUser<string>
    {
        public IdentityUser()
        {
            Id = Guid.NewGuid().ToString("D");
            Roles = new List<string>();
        }

        public string Id { get; }
        public string UserName { get; set; }
        public virtual string Email { get; set; }
        public virtual string SecurityStamp { get; set; }
        public virtual string PasswordHash { get; set; }

        public List<string> Roles { get; set; }

        public virtual void AddRole(string role)
        {
            Roles.Add(role);
        }

        public virtual void RemoveRole(string role)
        {
            Roles.Remove(role);
        }

        public virtual bool HasPassword()
        {
            return false;
        }
    }
}
