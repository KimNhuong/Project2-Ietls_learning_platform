using IeltsWeb.api.models;
using IeltsWeb.api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IeltsWeb.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;
    public RolesController(IRoleService roleService) { _roleService = roleService; }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Role>>> GetAll() => Ok(await _roleService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<Role>> GetById(int id)
    {
        var role = await _roleService.GetByIdAsync(id);
        if (role == null) return NotFound();
        return Ok(role);
    }

    [HttpPost]
    public async Task<ActionResult<Role>> Create(Role role)
    {
        var created = await _roleService.CreateAsync(role);
        return CreatedAtAction(nameof(GetById), new { id = created.RoleId }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Role role)
    {
        if (id != role.RoleId) return BadRequest();
        await _roleService.UpdateAsync(role);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _roleService.DeleteAsync(id);
        return NoContent();
    }
} 