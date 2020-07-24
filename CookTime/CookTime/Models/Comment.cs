using System;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CookTime.Models
{
    public class Comment
    {
        [JsonProperty(PropertyName = "user")]
        public string User { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string UserComment { get; set; }

        public ImageSource UserImage { get; set; }

    }
}
