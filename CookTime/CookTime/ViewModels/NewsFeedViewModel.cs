using CookTime.Models;
using System;
using System.Collections.ObjectModel;
using CookTime.Services;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using CookTime.FileHelpers;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace CookTime.ViewModels
{
    public class NewsFeedViewModel : BaseViewModel
    {
        #region ATTRIBUTES

        //Recipe collection (is not a list beacuse lists don´t match the binding objects)
        //Every recipe is saved in this attribute and this observable collection is Binded un the NeesFeedPage.XAML
        private ObservableCollection<RecipeItemViewModel> recipes;

        //The list of recipes loaded from teh server is copied in this attribute 
        //It is just used to transfer the data from the one the server loads to the observable collection 
        private List<Recipe> recipesList;

        //Activity indicator when the list is refreshing
        private bool isRefreshing;

        #endregion


        #region PROPERTIES

        public ObservableCollection<RecipeItemViewModel> Recipes
        {
            get { return this.recipes; }
            set { SetValue(ref this.recipes, value); }
        }

        public bool IsRefreshing 
        {
            get { return this.isRefreshing;  }
            set { SetValue(ref this.isRefreshing, value); }
        }

        #endregion


        #region CONSTRUCTOR

        public NewsFeedViewModel()
        {
            this.LoadRecipes();
        }

        #endregion

        #region METHODS
        /**
         * Loads the recipes saved in the server and saves them into tha obsservable collections that
         * is displayed in the NewsFeedPagePage
         */
        private async void LoadRecipes()
        {
            this.IsRefreshing = true;
            this.IsRefreshing = false;

            //Checking internet connection before asking for the server
            var connection = await ApiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Accept");
                return;
            }

            //Asks the server for the list of recipes 
            var response = await ApiService.GetList<Recipe>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                "/recipes");

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert( //If something goes wrong the page displays a message
                    "Error",
                    response.Message,
                    "Accept");
            }
            
            //Copies the list loaded from the server to the list in the attributes
            this.recipesList = (List<Recipe>)response.Result;

            //If there are recipes with a null at the image propertie, then it loads a generic image for that object
            this.recipesList = ChangeNullImages(this.recipesList);

            //Change the strings in the recipes with spaces
            ChangeStringSpaces();

            //Creats the observable collection with the method 
            this.Recipes = new ObservableCollection<RecipeItemViewModel>(
                this.ToRecipeItemViewModel());
            
        }

        /*
         * This method changes the recipe list into a Recipe Item View Model
         * This is necessary for loading an specific recipe later if the user tabs in one of the recipes 
         */
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
                Difficulty = r.Difficulty,
                Punctuation = r.Punctuation,
                Shares = r.Shares
            });
        }

        /*
         * establishes a default Image for the objects loaded that have no image
         */
        public List<Recipe> ChangeNullImages (List<Recipe> list)
        {
            foreach (Recipe recipe in list)
            {
                if (recipe.Image == null)
                {
                    recipe.Image = "DefaultRecipeIcon";
                }
            }

            return list;
        }

        /*
         * This method gets alll teh string properties of the recipe and changes the '_ 'characters into 
         * spaces due to that it was impossible to save them with spaces
         */
        public void ChangeStringSpaces()
        {
            foreach (Recipe recipe in this.recipesList)
            {
                recipe.Name = ReadStringConverter.ChangeGetString(recipe.Name);
                recipe.CookingSpan = ReadStringConverter.ChangeGetString(recipe.CookingSpan);
                recipe.EatingTime = ReadStringConverter.ChangeGetString(recipe.EatingTime);
                recipe.Ingredients = ReadStringConverter.ChangeGetString(recipe.Ingredients);
                recipe.Steps = ReadStringConverter.ChangeGetString(recipe.Steps);
                recipe.Tags = ReadStringConverter.ChangeGetString(recipe.Tags);
            }
        }

        #endregion


        #region COMMANDS

        /*
         * Command used when the user pulls the list to refresh
         */
        public ICommand RefreshCommand
        {
            get
            {
                //It loads the list again (just as it did on in the first time)
                return new RelayCommand(LoadRecipes);
            }
        }

        #endregion
    }
}
