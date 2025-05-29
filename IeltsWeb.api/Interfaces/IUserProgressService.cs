using IeltsWeb.api.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IeltsWeb.api.Interfaces;

public interface IUserProgressService
{
    Task<IEnumerable<UserProgress>> GetAllAsync();
    Task<UserProgress?> GetByIdAsync(int id);
    Task<UserProgress> CreateAsync(UserProgress progress);
    Task UpdateAsync(UserProgress progress);
    Task DeleteAsync(int id);
} 