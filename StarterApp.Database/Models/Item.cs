using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StarterApp.Database.Models
{
    public class Item
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string ItemTitle { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string ItemDescription { get; set; } = string.Empty;

        [JsonPropertyName("dailyRate")]
        public decimal ItemRate { get; set; }

        [JsonPropertyName("categoryId")]
        public int CategoryId { get; set; }

        [JsonPropertyName("category")]
        public string ItemCategory { get; set; } = string.Empty;

        [JsonPropertyName("ownerId")]
        public int OwnerId { get; set; }

        [JsonPropertyName("ownerName")]
        public string ItemOwnerName { get; set; } = string.Empty;

        [JsonPropertyName("ownerRating")]
        public double? OwnerRating { get; set; }

        [JsonPropertyName("isAvailable")]
        public bool IsAvailable { get; set; }

        [JsonPropertyName("averageRating")]
        public double? AverageRating { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
