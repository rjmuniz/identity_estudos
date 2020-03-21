using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WithoutIdentity.Models;
using WithoutIdentity.Models.ManagerViewModel;

namespace WithoutIdentity.Controllers
{
    public class ManagerController : Controller
    {
        public readonly UserManager<ApplicationUser> userManager;
        public readonly SignInManager<ApplicationUser> signInManager;
        public ManagerController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
                throw new ApplicationException($"Não foi possível carregar o usuário com o ID '{userManager.GetUserId(User)}'");

            var model = new IndexViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage

            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.GetUserAsync(User);

            if (user == null)
                throw new ApplicationException($"Não foi possível carregar o usuário com o ID '{userManager.GetUserId(User)}'");

            try
            {
                if (user.Email != model.Email)
                {
                    var setEmail = await userManager.SetEmailAsync(user, model.Email);
                    if (!setEmail.Succeeded)
                        throw new ApplicationException($"Erro ao atribuir email ao usuário com o ID '{userManager.GetUserId(User)}'");
                }
                if (user.PhoneNumber != model.PhoneNumber)
                {
                    var setPhoneNumber = await userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                    if (!setPhoneNumber.Succeeded)
                        throw new ApplicationException($"Erro ao atribuir PhoneNumber ao usuário com o ID '{userManager.GetUserId(User)}'");
                }
                StatusMessage = "Perfil atualizado com sucesso";
            }
            catch (Exception e)
            {
                StatusMessage = e.Message;
            }


            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
                throw new ApplicationException($"Não foi possível carregar o usuário com o ID '{userManager.GetUserId(User)}'");

            var model = new ChangePasswordViewModel { StatusMessage = StatusMessage };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await userManager.GetUserAsync(User);

            if (user == null)
                throw new ApplicationException($"Não foi possível carregar o usuário com o ID '{userManager.GetUserId(User)}'");


            var changePasswordResult = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }
                return View(model);
            }

            StatusMessage = "Senha alterada com Sucesso!";

            return RedirectToAction(nameof(ChangePassword));

        }
    }
}