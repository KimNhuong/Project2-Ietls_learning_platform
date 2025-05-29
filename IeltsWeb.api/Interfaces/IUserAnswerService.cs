using IeltsWeb.api.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IeltsWeb.api.Interfaces;

public interface IUserAnswerService
{
    Task<IEnumerable<UserAnswer>> GetAllAsync();
    Task<UserAnswer?> GetByIdAsync(int id);
    Task<UserAnswer> CreateAsync(UserAnswer answer);
    Task UpdateAsync(UserAnswer answer);
    Task DeleteAsync(int id);
} 