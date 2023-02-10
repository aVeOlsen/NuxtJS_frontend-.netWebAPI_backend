using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
// using AutoMapper;
// using AutoMapper.QueryableExtensions;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using wellbeing_api.Data;
using wellbeing_api.Models;
using wellbeing_api.Services;
namespace wellbeing_api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AppUserController : ControllerBase
{
    private readonly AppUserService _userService;
    private UserManager<ApplicationUser> _userManager;
    private RoleManager<ApplicationRole> _roleManager;
    private SignInManager<ApplicationUser> _signInManager;
    public AppUserController(AppUserService userService,
                            UserManager<ApplicationUser> userManager,
                            RoleManager<ApplicationRole> roleManager,
                            SignInManager<ApplicationUser> signInManager)
    {
        _userService = userService;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;

    }

    [HttpGet]
    public async Task<List<ApplicationUser>> Get()
    {
        return await _userService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApplicationUser>> Get(Guid id)
    {
        var user = await _userService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpPost]
    public async Task<IActionResult> Post(User newUser)
    {
        ApplicationUser appUser = new ApplicationUser()
        {
            UserName = newUser.Mail,
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            Email = newUser.Mail,
            DepartmentTitle = newUser.DepartmentTitle,
            Role = newUser.role
        };
        await _userManager.CreateAsync(appUser, newUser.Password);
        if (appUser.Role == 0)
        {
            await _userManager.AddToRoleAsync(appUser, "admin");
        }
        if (appUser.Role.Equals(1))
        {
            await _userManager.AddToRoleAsync(appUser, "manager");
        }
        else
        {
            await _userManager.AddToRoleAsync(appUser, "user");
        }

        return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
    }

    [HttpPost("{name}")]
    public async Task<IActionResult> CreateRole([Required] string name)
    {
        if (ModelState.IsValid)
        {
            IdentityResult result = await _roleManager.CreateAsync(new ApplicationRole() { Name = name });
            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ApplicationUser updatedUser)
    {
        var user = await _userService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        updatedUser.Id = user.Id;
        await _userManager.UpdateAsync(updatedUser);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var user = await _userService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        await _userService.RemoveAsync(id);

        return NoContent();
    }
    [HttpPost]
    [AllowAnonymous]
    [Route("authenticate")]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        if (ModelState.IsValid)
        {
            ApplicationUser appUser = await _userManager.FindByEmailAsync(user.Email);
            if (appUser != null)
            {
                await _signInManager.SignOutAsync();
                user.Password = Decryptor.DecryptStringAES(user.Password);
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, user.Password, false, false);
                if (result.Succeeded)
                {
                    appUser.Token = _userService.Authenticate(user.Email);
                    await Update(appUser.Id, appUser);
                    return Ok(new { appUser });
                }
            }
            ModelState.AddModelError(nameof(user.Email), "Login Failed: Invalid Email or Password");
            return Unauthorized();
        }
        return Ok();

    }

}