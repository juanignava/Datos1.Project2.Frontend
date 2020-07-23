using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookTime.Models
{
    /*
     * This class is the model of a recipe image 
     */
    public class RecipeImage
    {
        [JsonProperty(PropertyName = "recipeName")]
        public string RecipeName { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }
    }
}
