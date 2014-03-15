using System;
using Gos.SimpleObjectStore;
using Newtonsoft.Json;

namespace ImageGallery.Infrastructire.Data
{
    public class ImageData : IEntity
    {
        [Identifier]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        public DateTime UploadDate { get; set; }

        public bool IsValid { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }
    }
}