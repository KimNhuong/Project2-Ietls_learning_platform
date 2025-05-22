using Microsoft.AspNetCore.Mvc;

namespace IeltsWeb.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlashcardsController : ControllerBase
{
    [HttpPost]
    public IActionResult AddFlashcard([FromBody] FlashcardDto dto)
    {
        // TODO: Thêm flashcard vào DB
        return Ok(new { Message = "Flashcard added", Flashcard = dto });
    }

    // GET: /api/flashcards
    [HttpGet]
    public IActionResult GetFlashcards()
    {
        // TODO: Lấy danh sách flashcard từ DB
        return Ok(new[] { new { Id = 1, Word = "example", Meaning = "ví dụ" } });
    }

    // PUT: /api/flashcards/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateFlashcard(int id, [FromBody] FlashcardDto dto)
    {
        // TODO: Cập nhật flashcard trong DB
        return Ok(new { Message = "Flashcard updated", Id = id, Flashcard = dto });
    }

    // DELETE: /api/flashcards/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteFlashcard(int id)
    {
        // TODO: Xóa flashcard khỏi DB
        return Ok(new { Message = "Flashcard deleted", Id = id });
    }

    public class FlashcardDto
    {
        public string Word { get; set; } = "";
        public string Meaning { get; set; } = "";
        // Thêm các trường khác nếu cần
    }
}
