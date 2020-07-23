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
using System.IO;
using System.Threading.Tasks;

namespace CookTime.ViewModels
{
    public class NewsFeedViewModel : BaseViewModel
    {
        #region ATTRIBUTES

        // LIST OBJECTS
        private ObservableCollection<RecipeItemViewModel> recipes; //Recipe collection (is not a list beacuse lists don´t match the binding objects)
                                                                    //Every recipe is saved in this attribute and this observable collection is Binded un the NeesFeedPage.XAML

        private List<Recipe> recipesList; //The list of recipes loaded from teh server is copied in this attribute 
                                             //It is just used to transfer the data from the one the server loads to the observable collection 

        //ACTIVITY INDICADOR
        private bool isRefreshing;

        //IMAGES

        private ImageSource userProfilePic;

        #endregion


        #region PROPERTIES

        //LIST OBJECTS
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

        //IMAGES
        public ImageSource UserProfilePic
        {
            get { return this.userProfilePic; }
            set { SetValue(ref this.userProfilePic, value); }
        }

        #endregion


        #region CONSTRUCTOR

        public NewsFeedViewModel()
        {
            this.LoadRecipes();
            this.IsRefreshing = false;
        }

        #endregion

        #region METHODS
        /**
         * Loads the recipes saved in the server and saves them into tha obsservable collections that
         * is displayed in the NewsFeedPagePage
         */
        private async void LoadRecipes()
        {

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

            this.IsRefreshing = false;
            //Copies the list loaded from the server to the list in the attributes
            this.recipesList = (List<Recipe>)response.Result;

            //Creats the observable collection with the method 
            this.Recipes = new ObservableCollection<RecipeItemViewModel>(
                this.ToRecipeItemViewModel());

           // AddRecipeImages();
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
                EatingTime = r.EatingTime,
                Tags = r.Tags,
                Image = r.Image,
                Ingredients = r.Ingredients,
                Steps = r.Steps,
                Comments = r.Comments,
                Price = r.Price,
                Difficulty = r.Difficulty,
                Punctuation = r.Punctuation,
                Shares = r.Shares,
                RecipeImage = SelectImage(r.Image)
            });
        }

        private ImageSource SelectImage(string image)
        {
            byte[] backToArray = Convert.FromBase64String(image);
            //recipe.RecipeImageStream = new MemoryStream(backToArray);
            MemoryStream ms = new MemoryStream(backToArray);
            var imagesource = ImageSource.FromStream(() =>
            {
                return ms;
            });
            return imagesource;
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
                this.IsRefreshing = true;
                //It loads the list again (just as it did on  the first time)
                return new RelayCommand(LoadRecipes);
            }
        }

        #endregion
    }
}
