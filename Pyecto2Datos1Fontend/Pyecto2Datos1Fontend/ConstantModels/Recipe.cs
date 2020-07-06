using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Pyecto2Datos1Fontend.ConstantModels
{
    /**
     * Defines objects with all the attributes needed in a recipe (specified in the intructions)
     * Not every attribute is needed to create a recipe, we will change the constructor
     * when implementing the server classes made in java
     */
    class Recipe
    {
        public String Name { get; set; }
        public String Author { get; set; }
        public String Meal { get; set; }
        public int Portions { get; set; }
        public String PreparingTime { get; set; }
        public String Difficulty { get; set; }
        public String DietTags { get; set; }
        public Image RecipeImage { get; set; }
        public String Instructions { get; set; }
        public float Price { get; set; }

        public Recipe() { }

        public Recipe(String name, String author, String meal, int portions, String preparingTime,
             String difficulty, String dietTags, Image recipeImage, String instructions, float price)
        {
            this.Name = name;
            this.Author = author;
            this.Meal = meal;
            this.Portions = portions;
            this.PreparingTime = preparingTime;
            this.Difficulty = difficulty;
            this.DietTags = dietTags;
            this.RecipeImage = recipeImage;
            this.Instructions = instructions;
            this.Price = price;
        }
        

    }
}
