using IeltsWeb.api.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IeltsWeb.api.Interfaces;

public interface ITestService
{
    Task<IEnumerable<Test>> GetAllAsync();
    Task<Test?> GetByIdAsync(int id);
    Task<Test> CreateAsync(Test test);
    Task UpdateAsync(Test test);
    Task DeleteAsync(int id);
    Task<IEnumerable<Test>> GetByLessonIdAsync(int lessonId);
} 