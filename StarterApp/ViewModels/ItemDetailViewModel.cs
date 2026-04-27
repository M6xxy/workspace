using StarterApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using StarterApp.Database.Models;
using CommunityToolkit.Mvvm.Input;

namespace StarterApp.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;
        
        /// @brief Gets a placeholder message
        /// @return A message indicating this is a placeholder page
        public string Title => "Item Detail";
        public string? Message;

        private Item? currItem;
        public Item? CurrItem
        {
            get => currItem;
            set => SetProperty(ref currItem, value);
        }

        //Constrcutor
        public ItemDetailViewModel(ApiService apiService) { 
            _apiService = apiService;
        }

        public async Task loadItemInfoAsync(int id) {
            CurrItem = await _apiService.GetItemInfoAsync(id);
        }

        
    }
}
