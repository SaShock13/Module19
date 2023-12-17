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
        IdentityRole role1 = new IdentityRole { Name = "admin" };
        IdentityRole role2 = new IdentityRole { Name = "user" };

        public AccountController(UserManager<User> userManager, SignInManager<User> signManager, RoleManager<IdentityRole> roleMan)
        {
            this.userManager = userManager;
            this.signManager = signManager;
            this.roleManager = roleMan;


            //регистрируем роли
            roleManager.CreateAsync(role1);
            roleManager.CreateAsync(role2);
        }

       
        

        //// добавляем роли в бд
        //      roleManager.Create(role1);
        //    roleManager.Create(role2);
 
        //    // создаем пользователей
        //    var admin = new ApplicationUser { Email = "somemail@mail.ru", UserName = "somemail@mail.ru" };
        //      string password = "ad46D_ewr3";
        //      var result = userManager.Create(admin, password);
 
        //    // если создание пользователя прошло успешно
        //    if(result.Succeeded)
        //    {
        //        // добавляем для пользователя роль
        //        userManager.AddToRole(admin.Id, role1.Name);
        //        userManager.AddToRole(admin.Id, role2.Name);
        //    }








    public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {

            LogInUser user = new LogInUser();
            user.ReturnUrl = returnUrl;
            return View(user);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LogInUser user)
        {
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

                    await userManager.AddToRoleAsync(user, role1.Name);
                    await signManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "People");
                    
                }
                else//иначе
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


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signManager.SignOutAsync();
            return RedirectToAction("Index", "People");
        }

        
    }
}
