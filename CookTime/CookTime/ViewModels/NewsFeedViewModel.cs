using CookTime.Models;
using System;
using System.Collections.ObjectModel;
using CookTime.Services;
using CookTime.Models;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace CookTime.ViewModels
{
    public class NewsFeedViewModel : BaseViewModel
    {
        #region ATTRIBUTES
        private ObservableCollection<RecipeItemViewModel> recipes;

        private List<Recipe> recipesList;
        #endregion

        #region PROPERTIES
        public ObservableCollection<RecipeItemViewModel> Recipes
        {
            get { return this.recipes; }
            set { SetValue(ref this.recipes, value); }
        }
        #endregion

        #region CONSTRUCTOR
        public NewsFeedViewModel()
        {
            this.LoadRecipes();
        }
        #endregion

        #region METHODS
        private async void LoadRecipes()
        {
            var response = await ApiService.GetList<Recipe>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                "/recipes");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
            }
            

            this.recipesList = (List<Recipe>)response.Result;
            this.recipesList = ChangeNullImages(this.recipesList);

            this.Recipes = new ObservableCollection<RecipeItemViewModel>(
                this.ToRecipeItemViewModel());
        }


        private IEnumerable<RecipeItemViewModel> ToRecipeItemViewModel()
        {
            return this.recipesList.Select(r => new RecipeItemViewModel
            {
                Name = r.Name,
                Author = r.Author,
                Type = r.Type,
                Portions = r.Portions,
                CookingSpan = r.CookingSpan,
                EatingTime =r.EatingTime,
                Tags = r.Tags,
                Image = r.Tags,
                Ingredients = r.Ingredients,
                Steps = r.Steps,
                Comments = r.Comments,
                Price = r.Price,
                PointerToNodeInList = r.PointerToNodeInList,
                Difficulty = r.Difficulty,
                Id = r.Id,
                Punctuation = r.Punctuation
            });
        }


        public List<Recipe> ChangeNullImages (List<Recipe> list)
        {
            foreach (var recipe in list)
            {
                if (recipe.Image == null)
                {
                    recipe.Image = "defaultRecipeIcon";
                }
            }

            return list;
        }
        #endregion
    }
}
