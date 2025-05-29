using IeltsWeb.api.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IeltsWeb.api.Interfaces;

public interface IUserBookmarkService
{
    Task<IEnumerable<UserBookmark>> GetAllAsync();
    Task<UserBookmark?> GetByIdAsync(int id);
    Task<UserBookmark> CreateAsync(UserBookmark bookmark);
    Task UpdateAsync(UserBookmark bookmark);
    Task DeleteAsync(int id);
} 