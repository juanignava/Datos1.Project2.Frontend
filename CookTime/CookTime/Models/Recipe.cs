using CookTime.FileHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace CookTime.Models
{
    /*
     * This class is the model of a recipe, it has all its characteristics
     */
    public class Recipe
    {

        #region ATTRIBUTES

        private string name;

        private string cookingSpan;

        private string eatingTime;

        private string tags;

        private string ingredients;

        private string steps;

        private string image;

        #endregion

        [JsonProperty(PropertyName = "name")]
        public string Name { get { return this.name; } set { this.name = ChangeStringSpaces(value); } }

        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "portions")]
        public int Portions { get; set; }

        [JsonProperty(PropertyName = "cookingSpan")]
        public string CookingSpan { get { return this.cookingSpan; } set { this.cookingSpan = ChangeStringSpaces(value); } }

        [JsonProperty(PropertyName = "eatingTime")]
        public string EatingTime { get { return this.eatingTime; } set { this.eatingTime = ChangeStringSpaces(value); } }

        [JsonProperty(PropertyName = "tags")]
        public string Tags { get { return this.tags; } set { this.tags = ChangeStringSpaces(value); } }

        [JsonProperty(PropertyName = "image")]
        public string Image { get { return this.image; } set { this.image = ChangeNullImages(value); } }

        public ImageSource RecipeImage { get; set; } 

        public string UserImage { get; set; }

        public Stream RecipeImageStream { get; set; }

        [JsonProperty(PropertyName = "ingredients")]
        public string Ingredients { get { return this.ingredients; } set { this.ingredients = ChangeStringSpaces(value); } }

        [JsonProperty(PropertyName = "steps")]
        public string Steps { get { return this.steps; } set { this.steps = ChangeStringSpaces(value); } }

        [JsonProperty(PropertyName = "comments")]
        public object Comments { get; set; }

        [JsonProperty(PropertyName = "price")]
        public object Price { get; set; }

        [JsonProperty(PropertyName = "difficulty")]
        public int Difficulty { get; set; }

        [JsonProperty(PropertyName = "punctuation")]
        public int Punctuation { get; set; }

        [JsonProperty(PropertyName = "shares")]
        public int Shares { get; set; }


        #region METHODS

        public string ChangeStringSpaces(string property)
        {
            return ReadStringConverter.ChangeGetString(property);
        }

        private string ChangeNullImages(string imageSource)
        {
            if (string.IsNullOrEmpty(imageSource))
            {
                return "DefaultRecipeIcon";
            }

            else
            {
                return imageSource;
            }

        }
        #endregion
    }
}
