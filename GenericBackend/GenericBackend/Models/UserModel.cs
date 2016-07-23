using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace GenericBackend.Models
{
    public class UserModel
    {
        public static UserModel GetUserInfo(IPrincipal user)
        {
            var userModel = new UserModel
            {
                Name = user.Identity.Name,
                IsSuperUser = user.IsInRole(UserModel.SuperuserRole)
            };
            return userModel;
        }
        public const string SuperuserRole = "SuperUser";
        public string Name { get; set; }
        public string Role { get; set; }
        public bool IsSuperUser { get; set; }
    }
}