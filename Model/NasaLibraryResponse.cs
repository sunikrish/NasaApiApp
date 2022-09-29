using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NasaApiApp.Model
{
    public class NasaResponse
    {
        [JsonProperty("collection")]
        public Collection Collections { get; set; }
    }

    public class Collection
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("links")]
        public List<Link> Links { get; set; }
    }

    public class Item
    {
        [JsonProperty("data")]
        public List<Data> NasaData { get; set; }

        [JsonProperty("href")]
        public Uri href { get; set; }

        [JsonProperty("links")]
        public List<Link> Links { get; set; }
    }

    public class Data
    {
        [JsonProperty("center")]
        public string Center { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("photographer")]
        public string Photographer { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("nasa_id")]
        public string NasaId { get; set; }

        [JsonProperty("media_type")]
        public string MediaType { get; set; }

        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class Link
    {
        [JsonProperty("href")]
        public Uri Uri { get; set; }

        [JsonProperty("rel")]
        public string Rel { get; set; }

    }
}
