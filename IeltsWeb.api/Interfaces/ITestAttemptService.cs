using IeltsWeb.api.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IeltsWeb.api.Interfaces;

public interface ITestAttemptService
{
    Task<IEnumerable<TestAttempt>> GetAllAsync();
    Task<TestAttempt?> GetByIdAsync(int id);
    Task<TestAttempt> CreateAsync(TestAttempt attempt);
    Task UpdateAsync(TestAttempt attempt);
    Task DeleteAsync(int id);
} 