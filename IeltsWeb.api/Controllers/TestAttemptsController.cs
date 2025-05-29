using IeltsWeb.api.models;
using IeltsWeb.api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IeltsWeb.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestAttemptsController : ControllerBase
{
    private readonly ITestAttemptService _service;
    public TestAttemptsController(ITestAttemptService service) { _service = service; }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TestAttempt>>> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<TestAttempt>> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<TestAttempt>> Create(TestAttempt attempt)
    {
        var created = await _service.CreateAsync(attempt);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TestAttempt attempt)
    {
        if (id != attempt.Id) return BadRequest();
        await _service.UpdateAsync(attempt);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
} 