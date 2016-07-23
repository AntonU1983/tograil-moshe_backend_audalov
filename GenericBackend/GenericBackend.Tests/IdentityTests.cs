using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericBackend.Core.Utils;
using GenericBackend.Identity;
using GenericBackend.Identity.Core;
using GenericBackend.Identity.Identity;
using GenericBackend.Repository;
using NUnit.Framework;

namespace GenericBackend.Tests
{
    [TestFixture]
    public class IdentityTests
    {
        [SetUp]
        public void Init()
        {

        }

        [Test]
        public async Task Add_ForUserManager_ShouldAddUserWiyhoutRoles_Test()
        {
            var userManager = new ApplicationUserManager(new UserStore<IdentityUser>(MongoUtil<IdentityUser>.GetDefaultConnectionString()));
            var password = "11Password";
            var userName = "testIdentity@test.com";
            var identityUser = new IdentityUser
            {
                UserName = userName,
                Email = "testIdentity@test.com"
            };

            await userManager.CreateAsync(identityUser, password);

            Assert.That(await userManager.FindByNameAsync(userName) != null);
        }

        [Test]
        public async Task Add_ForRoleManager_ShouldAddRole_Test()
        {
            var roleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(MongoUtil<IdentityUser>.GetDefaultConnectionString()));
            var role = "TestRole";
            

            await roleManager.CreateAsync(new IdentityRole(role));

            Assert.That(await roleManager.FindByNameAsync(role) != null);
        }

        [Test]
        public async Task Add_ForUserManager_ShouldAddUserWithRoles_Test()
        {
            var userManager = new ApplicationUserManager(new UserStore<IdentityUser>(MongoUtil<IdentityUser>.GetDefaultConnectionString()));
            var password = "11Password";
            var userName = "testIdentity2@test.com";
            var testRole = "testRole 123";

            var identityUser = new IdentityUser
            {
                UserName = userName,
                Email = "testIdentity@test.com"
            };

            identityUser.Roles.Add(testRole);

            await userManager.CreateAsync(identityUser, password);
            var user = await userManager.FindByNameAsync(userName);

            Assert.That(user != null);
            Assert.That(user.Roles.Count == 1);
        }
    }
}
