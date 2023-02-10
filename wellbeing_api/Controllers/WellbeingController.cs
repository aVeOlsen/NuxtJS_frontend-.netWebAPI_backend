using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using wellbeing_api.Models;
using wellbeing_api.Services;

namespace wellbeing_api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class WellbeingController : ControllerBase
{
    private readonly WellbeingService _wellbeingService;
    private readonly IEmailSender _emailSender;
    private readonly IDatabaseService<ApplicationUser> _userService;

    public WellbeingController(WellbeingService wellbeingService, IEmailSender emailSender, AppUserService userService)
    {

        _wellbeingService = wellbeingService;
        _emailSender = emailSender;
        _userService = userService;
    }

    [HttpGet]
    public async Task<List<Wellbeing>> Get() =>
        await _wellbeingService.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Wellbeing>> Get(Guid id)
    {
        var wellbeing = await _wellbeingService.GetAsync(id);

        if (wellbeing is null)
        {
            return NotFound();
        }

        return wellbeing;
    }
    [HttpGet("department/{id}")]
    public async Task<ActionResult<List<Wellbeing>>> GetDepartment(string id)
    {
        var wellbeing = await _wellbeingService.GetDepartmentAsync(id);

        if (wellbeing is null)
        {
            return NotFound();
        }

        return wellbeing;
    }
    [HttpGet("user/{id}")]
    public async Task<ActionResult<List<Wellbeing>>> GetUser(Guid id)
    {
        var wellbeing = await _wellbeingService.GetUserAsync(id);

        if (wellbeing is null)
        {
            return NotFound();
        }

        return wellbeing;
    }
    [HttpGet("quistionnaire/{id}")]
    public async Task<ActionResult<List<Wellbeing>>> GetQuistionnaire(string id)
    {
        var wellbeing = await _wellbeingService.GetQuistionnaireAsync(id);

        if (wellbeing is null)
        {
            return NotFound();
        }

        return wellbeing;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Wellbeing newWellbeing)
    {
        var wellbeings = new List<Wellbeing>();
        var users = await _userService.GetAllAsync();
        string subject = "Trivselsundersøgelse fra onlineplus";
        string msg = "Ny trivselsundersøgelse fra Onlineplus til din afdeling ";
        foreach (var user in users)
        {
            if (user.DepartmentTitle == newWellbeing.DepartmentTitle)
            {
                wellbeings.Add(new Wellbeing()
                {
                    Id = newWellbeing.Id,
                    QuestionId = newWellbeing.QuestionId,
                    Title = newWellbeing.Title,
                    WellbeingLevel = newWellbeing.WellbeingLevel,
                    Subsections = newWellbeing.Subsections,
                    Primary = newWellbeing.Primary,
                    DepartmentTitle = newWellbeing.DepartmentTitle,
                    UserID = user.Id
                });
            }
        }

        if (wellbeings.Count > 0)
        {
            await _wellbeingService.CreateAsync(wellbeings);

            foreach (var wellbeing in wellbeings)
            {
                var user = users.FirstOrDefault(u => u.Id == wellbeing.UserID);
                if (user != null)
                {
                    await _emailSender.SendEmailAsync(user.Email, subject, msg + user.DepartmentTitle + "\n http://graceful-elf-716eec.netlify.app/");
                }
            }
        }

        return CreatedAtAction(nameof(Get), new { id = newWellbeing.Id }, newWellbeing);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Wellbeing updatedWellbeing)
    {
        var wellbeing = await _wellbeingService.GetAsync(id);

        if (wellbeing is null)
        {
            return NotFound();
        }
        updatedWellbeing.Id = id;

        await _wellbeingService.UpdateAsync(id, updatedWellbeing);

        return NoContent();
    }
    [HttpPut("quistionnaire/{id}")]
    public async Task<ActionResult<List<Wellbeing>>> SetPrimary(string id)
    {
        var wellbeing = await _wellbeingService.GetQuistionnaireAsync(id);
        if (wellbeing is null)
        {
            return NotFound();
        }
        await _wellbeingService.SetPrimeAsync(id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var wellbeing = await _wellbeingService.GetAsync(id);

        if (wellbeing is null)
        {
            return NotFound();
        }

        await _wellbeingService.RemoveAsync(id);

        return NoContent();
    }
}