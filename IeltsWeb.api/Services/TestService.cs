using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IeltsWeb.api.models;

namespace IeltsWeb.Services
{
    public class TestService : IeltsWeb.api.Interfaces.ITestService
    {
        private readonly MyDbContext _context;

        public TestService(MyDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int id)
        {
            var test = await _context.Tests.FindAsync(id);
            if (test != null)
            {
                _context.Tests.Remove(test);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Test>> GetByLessonIdAsync(int lessonId)
        {
            return await _context.Tests.Where(t => t.LessonId == lessonId).ToListAsync();
        }

        public async Task<IEnumerable<Test>> GetAllAsync()
        {
            return await _context.Tests.ToListAsync();
        }

        public async Task<Test?> GetByIdAsync(int id)
        {
            return await _context.Tests.FindAsync(id);
        }

        public async Task<Test> CreateAsync(Test test)
        {
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
            return test;
        }

        public async Task UpdateAsync(Test test)
        {
            _context.Tests.Update(test);
            await _context.SaveChangesAsync();
        }
    }
}