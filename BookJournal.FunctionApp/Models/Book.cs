using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BookJournal.FunctionApp.Models
{
    public class Book
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("upn")]
        public string Upn { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("isbn")]
        public string Isbn { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("publicationYear")]
        public string PublicationYear { get; set; }
        [JsonProperty("publisher")] 
        public string Publisher { get; set; }
        [JsonProperty("rating")]
        public string Rating { get; set; }
        [JsonProperty("review")]
        public string Review { get; set; }
        [JsonProperty("notes")]
        public string Notes { get; set; }
        [JsonProperty("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

    }
}
