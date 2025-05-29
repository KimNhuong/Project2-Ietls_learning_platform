using IeltsWeb.api.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IeltsWeb.api.Interfaces;

public interface IQuestionMediaService
{
    Task<IEnumerable<QuestionMedia>> GetAllAsync();
    Task<QuestionMedia?> GetByIdAsync(int questionId, int mediaId);
    Task<QuestionMedia> CreateAsync(QuestionMedia entity);
    Task DeleteAsync(int questionId, int mediaId);
} 