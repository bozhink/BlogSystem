namespace M101DotNet.WebApp.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Data;
    using Data.Contracts;
    using Data.Models;

    using Data.Common.Repositories;
    using Data.Common.Repositories.Contracts;

    using Services;
    using Services.Contracts;
    using Services.Models;

    using MongoDB.Driver;
    using ViewModels.Account;



    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUsersDataService service;

        public AccountController()
        {
            var provider = new BlogDatabaseProvider();
            var repository = new GenericRepository<User>(provider);
            this.service = new UsersDataService(repository);
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var model = new LoginModel
            {
                ReturnUrl = returnUrl
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            //var blogContext = new BlogContext();
            //var user = await blogContext.Users.Find(x => x.Email == model.Email).SingleOrDefaultAsync();
            var user = await this.service.GetUser(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Email address has not been registered.");
                return this.View(model);
            }

            var identity = new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email)
                },
                "ApplicationCookie");

            var context = Request.GetOwinContext();
            var authManager = context.Authentication;

            authManager.SignIn(identity);

            return this.Redirect(this.GetRedirectUrl(model.ReturnUrl));
        }

        [HttpPost]
        public ActionResult Logout()
        {
            var context = Request.GetOwinContext();
            var authManager = context.Authentication;

            authManager.SignOut("ApplicationCookie");
            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return this.View(new RegisterModel());
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = new UserServiceModel
            {
                Name = model.Name,
                Email = model.Email
            };

            await this.service.AddNewUser(user);

            //var blogContext = new BlogContext();
            //var user = new User
            //{
            //    Name = model.Name,
            //    Email = model.Email
            //};

            //await blogContext.Users.InsertOneAsync(user);
            return this.RedirectToAction("Index", "Home");
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("index", "home");
            }

            return returnUrl;
        }
    }
}