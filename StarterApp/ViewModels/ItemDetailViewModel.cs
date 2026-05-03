using StarterApp.Database.Data.Repositories;
using StarterApp.Database.Models;

namespace StarterApp.ViewModels;

public class ItemDetailViewModel : BaseViewModel
{
    // --------------- VARIBLES --------------------
    private readonly IItemRepository _itemRepository;

    public string Title => "Item Detail";

    private Item? currItem;
    public Item? CurrItem
    {
        get => currItem;
        set => SetProperty(ref currItem, value);
    }
    
    // ----------------- CONSTRUCTOR -------------------------

    public ItemDetailViewModel(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    // ----------------- METHODS -------------------------


    // Method for loading item info based on ID via API
    public async Task LoadItemInfoAsync(int id)
    {
        CurrItem = await _itemRepository.GetByIdAsync(id);
    }
}