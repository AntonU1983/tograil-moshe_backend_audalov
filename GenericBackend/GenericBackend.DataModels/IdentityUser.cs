using System;
using System.Collections.Generic;
using GenericBackend.Core;

namespace GenericBackend.DataModels
{
    public class IdentityUser : MongoEntityBase
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string SecurityStamp { get; set; }
        public string PasswordHash { get; set; }
        public IEnumerable<string> Roles { get; set; } 
    }
}
