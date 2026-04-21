using System;
using System.Collections.Generic;
using System.Text;

namespace StarterApp.Database.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemTitle { get; set; } = "";
        public string ItemDescription { get; set; } = "";
        public decimal ItemRate { get; set; }
        public string ItemCategory { get; set; } = "";
        public string ItemOwnerName { get; set; } = "";
    }
}
