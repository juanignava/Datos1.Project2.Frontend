using System;
using Newtonsoft.Json;

namespace CookTime.Models
{
    public class ChefQuery
    {
        
        [JsonProperty(PropertyName = "user")]
        public string User { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        
    }
}
