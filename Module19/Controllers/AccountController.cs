using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Module19.Authorization;
using System.Data;
using System.Runtime.CompilerServices;

namespace Module19.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signManager;
        private  RoleManager<IdentityRole>roleManager { get; }
        
        // создаем роли
        IdentityRole roleAdmin = new IdentityRole { Name = "admin" };
        IdentityRole roleUser = new IdentityRole { Name = "user" };

        public AccountController(   UserManager<User> userManager, SignInManager<User> signManager, 
                                    RoleManager<IdentityRole> roleMan, ILoggerFactory loggerFactory, 
                                    ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signManager = signManager;
            this.roleManager = roleMan;

            logger.LogCritical("Сработал Контроллер Аккаунтов");
            
            //CreateRoles();
            
            //регистрируем роли
            
        }
        //ЧТО-ТО ЗДЕСЬ МЕШАЕТ призапуске в конструкторе
        async Task CreateRoles()
        {
           
            await roleManager.CreateAsync(roleUser);
            await roleManager.CreateAsync(roleAdmin);
        }

    public IActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {

            LogInUser user = new LogInUser();
            if (returnUrl!=null)
            {
                user.ReturnUrl=returnUrl;
            }             
            else user.ReturnUrl = "~/";
                



            
            return View(user);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LogInUser user)
        {
            


            if (User.Identity.IsAuthenticated)
            {
                return View("Аутентифицирован");
            }
            if (ModelState.IsValid)
            {

                var loginResult = await signManager.PasswordSignInAsync(user.LoginProp,
                    user.Password,
                    false,
                    lockoutOnFailure: false);

                if (loginResult.Succeeded)
                {

                    if (Url.IsLocalUrl(user.ReturnUrl))
                    {
                        return Redirect(user.ReturnUrl);
                    }

                    return RedirectToAction("Index", "People");
                }
                


            }
            
                ModelState.AddModelError("", "Пользователь не найден");
                return View(user);
            
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegUser());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegUser regUser)
        {
            
            if (ModelState.IsValid)
            {


                var user = new User { UserName = regUser.LoginProp };

                var createResult = await userManager.CreateAsync(user, regUser.Password);

                if (createResult.Succeeded)
                {

                    await userManager.AddToRoleAsync(user, roleUser.Name);
                    await signManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "People");
                    
                }
                else
                {
                    foreach (var identityError in createResult.Errors)
                    {
                        ModelState.AddModelError("", identityError.Description);
                    }
                    return View(ModelState);
                }
            }
            else return View(regUser);
        }


        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await signManager.SignOutAsync();
            //HttpContext.SignOutAsync();
            return RedirectToAction("Index", "People");
        }

        public async Task<IActionResult> AccessDenied()
        {
            
            return View();
        }


    }
}
