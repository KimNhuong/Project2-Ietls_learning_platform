using IeltsWeb.api.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IeltsWeb.api.Interfaces;

public interface IQuestionService
{
    Task<IEnumerable<Question>> GetAllAsync();
    Task<Question?> GetByIdAsync(int id);
    Task<Question> CreateAsync(Question question);
    Task UpdateAsync(Question question);
    Task DeleteAsync(int id);
    Task<IEnumerable<Question>> GetByTestIdAsync(int testId);
}