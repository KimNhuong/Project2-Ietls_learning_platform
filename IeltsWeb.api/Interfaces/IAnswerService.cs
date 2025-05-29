using IeltsWeb.api.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IeltsWeb.api.Interfaces;

public interface IAnswerService
{
    Task<IEnumerable<Answer>> GetAllAsync();
    Task<Answer?> GetByIdAsync(int id);
    Task<Answer> CreateAsync(Answer answer);
    Task UpdateAsync(Answer answer);
    Task DeleteAsync(int id);
} 