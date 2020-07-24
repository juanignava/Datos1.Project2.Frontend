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
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadRecipes);
            }
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

        private async void LoadRecipes()
        {
            this.IsRefreshing = true;
            
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

            var response = await ApiService.GetList<Recipe>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                "/recipes");

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
            }

            this.IsRefreshing = false;

            //Copies the list loaded from the server to the list in the attributes
            this.recipesList = (List<Recipe>)response.Result;

            var variable = await LoadUserProfilePic();

            ChangeStringSpaces();

            //Creates the observable collection with the method 
            this.Recipes = new ObservableCollection<RecipeItemViewModel>(this.ToRecipeItemViewModel());

           // AddRecipeImages();
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
                Likers = r.Likers,
                Price = r.Price,
                Difficulty = r.Difficulty,
                Punctuation = r.Punctuation,
                Shares = r.Shares,
                RecipeImage = SelectImage(r.Image),
                UserImage = r.UserImage
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

        private async Task<Response> LoadUserProfilePic()
        {
            foreach (Recipe recipe in this.recipesList)
            {
                string controller = "/users/" + recipe.Author; //Asking for the account information
                Response checkEmail = await ApiService.Get<User>( //Tries to get the account information
                    "http://localhost:8080/CookTime.BackEnd",
                    "/api",
                    controller);
                User loggedUser = (User)checkEmail.Result;
                string imageString = loggedUser.ProfilePic;
                if (imageString == null)
                {
                    recipe.UserImage = "SignUpIcon";
                    continue;
                }

                byte[] backToArray = Convert.FromBase64String(imageString);
                MemoryStream ms = new MemoryStream(backToArray);
                recipe.UserImage = ImageSource.FromStream(() =>
                {
                    return ms;
                });
            }
            return new Response
            {
                IsSuccess = true
            };
        }
        #endregion
    }
}