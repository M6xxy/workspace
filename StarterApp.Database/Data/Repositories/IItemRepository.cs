using StarterApp.Database.Models;

namespace StarterApp.Database.Data.Repositories;

public interface IItemRepository
{
    //Method for getting page content, Asynchronous returns a list of page content via API (Returns List<Item>)
    Task<List<Item>> GetPageAsync(int page, int pageSize);

    //Method for getting an item by ID, Asynchrous returns a Item or Null via API (Returns <Item> || Null)
    Task<Item?> GetByIdAsync(int id);

    //Method for creating a an item Asynchrous via API (Returns bool)
    Task<bool> CreateAsync(Item item, string token);

    //Method for updating existing item info via API (Returns bool)
    Task<bool> UpdateAsync(int id, Item item, string token);
}