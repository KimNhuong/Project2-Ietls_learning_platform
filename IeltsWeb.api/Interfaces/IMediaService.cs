using IeltsWeb.api.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IeltsWeb.api.Interfaces;

public interface IMediaService
{
    Task<IEnumerable<Media>> GetAllAsync();
    Task<Media?> GetByIdAsync(int id);
    Task<Media> CreateAsync(Media media);
    Task UpdateAsync(Media media);
    Task DeleteAsync(int id);
} 