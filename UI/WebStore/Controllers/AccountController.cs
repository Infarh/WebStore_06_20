using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels.Identity;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<AccountController> _Logger;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager, ILogger<AccountController> Logger)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _Logger = Logger;
        }

        #region Процесс регистрации нового пользвоателя

        public IActionResult Register() => View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            _Logger.LogInformation("Начало процесса регистрации нового пользователя {0}", Model.UserName);

            var user = new User
            {
                UserName = Model.UserName
            };

            var registration_result = await _UserManager.CreateAsync(user, Model.Password);
            if (registration_result.Succeeded)
            {
                _Logger.LogInformation("Пользователь {0} успешно зарегистрирован", user.UserName);

                await _UserManager.AddToRoleAsync(user, Role.User);

                _Logger.LogInformation("Пользователь {0} наделён ролью {1}", user.UserName, Role.User);

                await _SignInManager.SignInAsync(user, false);

                _Logger.LogInformation("Пользователь {0} автоматически вошёл в систему после регистрации", user.UserName);

                return RedirectToAction("Index", "Home");
            }

            _Logger.LogWarning("Ошибка при регистрации нового пользователя {0}\r\n",
                Model.UserName,
                string.Join(Environment.NewLine, registration_result.Errors.Select(error => error.Description)));

            //_Logger.Log(LogLevel.Information, new EventId(5), registration_result, null, (result, _) => string.Join(Environment.NewLine, result.Errors.Select(error => error.Description)));

            foreach (var error in registration_result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(Model);
        }

        #endregion

        #region Процесс входа пользователя в систему

        public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl });

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            var login_result = await _SignInManager.PasswordSignInAsync(
                Model.UserName,
                Model.Password,
                Model.RememberMe,
                false);

            _Logger.LogInformation("Попытка входа пользователя {0} в систему", Model.UserName);

            if (login_result.Succeeded)
            {
                _Logger.LogInformation("Пользователь {0} успешно вошёл в систему", Model.UserName);

                if (Url.IsLocalUrl(Model.ReturnUrl))
                    return Redirect(Model.ReturnUrl);
                return RedirectToAction("Index", "Home");
            }

            _Logger.LogWarning("Ошибка имени пользователя, или пароля при попытке входа {0}", Model.UserName);

            ModelState.AddModelError(string.Empty, "Неверное имя пользователя или пароль!");

            return View(Model);
        } 

        #endregion

        public async Task<IActionResult> Logout()
        {
            var user_name = User.Identity.Name;
            await _SignInManager.SignOutAsync();
            _Logger.LogInformation("Пользователь {0} вышел из системы", user_name);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied() => View();
    }
}
