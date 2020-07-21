using System;
using Newtonsoft.Json;

namespace CookTime.Models
{
    public class Notification
    {
        [JsonProperty(PropertyName = "emisorUser")]
        public string EmisorUser { get; set; }

        [JsonProperty(PropertyName = "recieverUser")]
        public string RecieverUser { get; set; }

        [JsonProperty(PropertyName = "notifType")]
        public int NotifType { get; set; }

        [JsonProperty(PropertyName = "newComment")] 
        public string NewComment { get; set; }

        [JsonProperty(PropertyName = "recipe")] 
        public string RecipeName { get; set; }
    }
}
