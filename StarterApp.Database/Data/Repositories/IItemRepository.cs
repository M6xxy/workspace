using StarterApp.Database.Models;

namespace StarterApp.Database.Data.Repositories;

public interface IItemRepository
{
    Task<List<Item>> GetPageAsync(int page, int pageSize);
    Task<Item?> GetByIdAsync(int id);
    Task<bool> CreateAsync(Item item, string token);
    Task<bool> UpdateAsync(int id, Item item, string token);
}