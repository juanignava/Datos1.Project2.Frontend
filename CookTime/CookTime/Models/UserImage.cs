using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookTime.Models
{
    /*
     * This class is the model of an user image 
     */
    public class UserImage
    {
        [JsonProperty(PropertyName = "user")]
        public string User { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }
    }
}
