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
    /// <summary>
    /// Initializes a new instance of the <see cref="ItemDetailViewModel"/> class.
    /// </summary>
    /// <param name="itemRepository">Repository used for item data operations.</param>
    public ItemDetailViewModel(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    // ----------------- METHODS -------------------------


    // Method for loading item info based on ID via API
    /// <summary>
    /// Loads detailed information for a specific item.
    /// </summary>
    /// <param name="id">Unique identifier of the item to load.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task LoadItemInfoAsync(int id)
    {
        CurrItem = await _itemRepository.GetByIdAsync(id);
    }
}