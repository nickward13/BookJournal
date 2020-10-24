using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BookJournal.FunctionApp.Models
{
    class Book
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("upn")]
        public string Upn { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("isbn")]
        public string Isbn { get; set; }
        [JsonPropertyName("author")]
        public string Author { get; set; }
        [JsonPropertyName("publicationYear")]
        public string PublicationYear { get; set; }
        [JsonPropertyName("publisher")] 
        public string Publisher { get; set; }
        [JsonPropertyName("rating")]
        public string Rating { get; set; }
        [JsonPropertyName("review")]
        public string Review { get; set; }
        [JsonPropertyName("notes")]
        public string Notes { get; set; }
        [JsonPropertyName("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

    }
}
