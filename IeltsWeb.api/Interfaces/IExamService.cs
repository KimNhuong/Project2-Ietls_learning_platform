using IeltsWeb.api.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IeltsWeb.api.Interfaces;

public interface IExamService
{
    Task<IEnumerable<Exam>> GetAllExamsAsync();
    Task<Exam?> GetExamByIdAsync(int id);
    Task<IEnumerable<Exam>> SearchExamsByTitleAsync(string keyword);
    Task<Exam> CreateExamAsync(Exam exam);
    Task UpdateExamAsync(Exam exam);
    Task DeleteExamAsync(int id);
}
