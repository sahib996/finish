using Maxim.Models;
using Maxim.Views.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Maxim.Controllers
{

        public class AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager) : Controller
        {

		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginVM signIn, string ReturnUrl)
		{
			AppUser user;
			if (signIn.UserNameOrEmail.Contains("@"))
			{
				user = await _userManager.FindByEmailAsync(signIn.UserNameOrEmail);
			}
			else
			{
				user = await _userManager.FindByNameAsync(signIn.UserNameOrEmail);
			}
			if (user == null)
			{
				ModelState.AddModelError("", "Login ve ya parol yalnisdir");
				return View(signIn);
			}
			var result = await
			_signInManager.PasswordSignInAsync(user, signIn.Password, signIn.RememberMe, true);
			if (!result.Succeeded)
			{
				ModelState.AddModelError("", "Login ve ya parol yalnisdir");
				return View(signIn);
			}
			if (ReturnUrl != null) return LocalRedirect(ReturnUrl);
			return RedirectToAction(nameof(HomeController.Index), "Home");

		}
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM register)
		{
			if (!ModelState.IsValid) return View();
			AppUser newUser = new AppUser
			{
				Email = register.Email,
				UserName = register.UserName
			};
			IdentityResult result = await _userManager.CreateAsync(newUser, register.Password);
			if (!result.Succeeded)
			{
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError("", item.Description);
				}
			}
			return RedirectToAction(nameof(HomeController.Index), "Home");
		}
		public async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
	}
}
