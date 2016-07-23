using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using GenericBackend.Core.Utils;
using GenericBackend.Identity;
using Account = GenericBackend.Identity.Core.IdentityUser;
using GenericBackend.Identity.Identity;
using GenericBackend.Models;
using GenericBackend.UnitOfWork.GoodNightMedical;

namespace GenericBackend.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _userManager = new ApplicationUserManager(new UserStore<Account>(MongoUtil<Account>.GetDefaultConnectionString()));
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(UserModel.GetUserInfo(User));
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IHttpActionResult> Registration([FromBody]RegistrationModel model)
        {
            var roles = new List<string>();
            roles.Add("SuperUser");
            var identityUser = new Account { UserName = model.Email, Roles = roles};

            var result = await _userManager.CreateAsync(identityUser, model.Password);

            return Ok();
        }

        [HttpGet]
        [Route("users")]
        [Authorize(Roles = "SuperUser")]
        public IHttpActionResult GetUsersList()
        {
            return Ok(_unitOfWork.Users.EndUsers());
        }
    }
}
