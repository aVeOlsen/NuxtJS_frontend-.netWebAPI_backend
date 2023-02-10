using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using wellbeing_api.Models;
using wellbeing_api.Services;

namespace wellbeing_api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly DepartmentService _departmentService;

    public DepartmentController(DepartmentService departmentService) =>
        _departmentService = departmentService;

    [HttpGet]
    public async Task<List<Department>> Get() =>
        await _departmentService.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Department>> Get(string id)
    {
        var department = await _departmentService.GetAsync(id);

        if (department is null)
        {
            return NotFound();
        }

        return department;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Department newDepartment)
    {
        await _departmentService.CreateAsync(newDepartment);

        return CreatedAtAction(nameof(Get), new { id = newDepartment.Title }, newDepartment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Department updatedDepartment)
    {
        var department = await _departmentService.GetAsync(id);

        if (department is null)
        {
            return NotFound();
        }

        updatedDepartment.Title = department.Title;

        await _departmentService.UpdateAsync(id, updatedDepartment);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var department = await _departmentService.GetAsync(id);

        if (department is null)
        {
            return NotFound();
        }

        await _departmentService.RemoveAsync(id);

        return NoContent();
    }
}