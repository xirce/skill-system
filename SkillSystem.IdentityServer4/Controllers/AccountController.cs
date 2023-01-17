using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillSystem.IdentityServer4.Data.Entities;
using SkillSystem.IdentityServer4.Models.Account;

namespace SkillSystem.IdentityServer4.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly IIdentityServerInteractionService interactionService;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IIdentityServerInteractionService interactionService
    )
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.interactionService = interactionService;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await userManager.FindByEmailAsync(model.Email);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "User not found");
            return View(model);
        }

        var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
        if (!signInResult.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Incorrect password");
            return View(model);
        }

        return Redirect(model.ReturnUrl);
    }

    [HttpGet]
    public IActionResult Register(string returnUrl)
    {
        return View(new RegisterViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = new ApplicationUser
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Patronymic = model.Patronymic,
            Email = model.Email,
            UserName = model.Email
        };
        var identityResult = await userManager.CreateAsync(user, model.Password);
        if (!identityResult.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Registration failed");
            return View(model);
        }

        await signInManager.PasswordSignInAsync(user, model.Password, false, false);

        return Redirect(model.ReturnUrl);
    }

    [HttpGet]
    public async Task<IActionResult> Logout(string logoutId)
    {
        var context = await interactionService.GetLogoutContextAsync(logoutId);
        if (context?.PostLogoutRedirectUri is null)
            return Redirect("/Account/Login");
        await signInManager.SignOutAsync();

        return Redirect(context.PostLogoutRedirectUri);
    }
}