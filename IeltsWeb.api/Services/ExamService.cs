using IeltsWeb.api.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IeltsWeb.api.Interfaces;
namespace IeltsWeb.api.Services
{
    public class ExamService : IExamService
    {
        private readonly MyDbContext _context;

        public ExamService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Exam>> GetAllExamsAsync()
        {
            return await _context.Exams.ToListAsync();
        }

        public async Task<Exam?> GetExamByIdAsync(int id)
        {
            return await _context.Exams.FindAsync(id);
        }

        public async Task<IEnumerable<Exam>> SearchExamsByTitleAsync(string keyword)
        {
            return await _context.Exams
                .Where(e => e.Title.Contains(keyword))
                .ToListAsync();
        }

        public async Task<Exam> CreateExamAsync(Exam exam)
        {
            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();
            return exam;
        }

        public async Task UpdateExamAsync(Exam exam)
        {
            _context.Entry(exam).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExamAsync(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam != null)
            {
                _context.Exams.Remove(exam);
                await _context.SaveChangesAsync();
            }
        }
    }
}
