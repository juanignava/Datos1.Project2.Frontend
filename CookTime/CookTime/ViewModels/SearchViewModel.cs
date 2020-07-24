using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CookTime.FileHelpers;
using CookTime.Models;
using CookTime.Services;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace CookTime.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        #region ATTRIBUTES

        //TEXT
        private string textSearch;

        //BOOLEAN
        private bool isRefreshingUsers;

        private bool isRefreshingRecipes;

        private bool isVisibleUsers;

        private bool isVisibleRecipes;

        //OBSERVABLE COLLECTION
        private ObservableCollection<UserItemViewModel> users;

        private ObservableCollection<RecipeItemViewModel> recipes;

        //LIST
        private List<Recipe> recipesList;

        private List<User> usersList;
        #endregion


        #region PROPERITES

        //TEXT
        public string TextSearch
        {
            get { return this.textSearch; }
            set
            {
                SetValue(ref this.textSearch, value);
                this.Search();
            }
        }

        //BOOLEAN
        public bool IsRefreshingUsers
        {
            get { return this.isRefreshingUsers; }
            set { SetValue(ref this.isRefreshingUsers, value); }
        }

        public bool IsRefreshingRecipes
        {
            get { return this.isRefreshingRecipes; }
            set { SetValue(ref this.isRefreshingRecipes, value); }
        }

        public bool IsVisibleUsers
        {
            get { return this.isVisibleUsers; }
            set { SetValue(ref this.isVisibleUsers, value); }
        }

        public bool IsVisibleRecipes
        {
            get { return this.isVisibleRecipes; }
            set { SetValue(ref this.isVisibleRecipes, value); }
        }

        //OBSERVABLE COLLECTION
        public ObservableCollection<UserItemViewModel> Users
        {
            get { return this.users; }
            set { SetValue(ref this.users, value); }
        }

        public ObservableCollection<RecipeItemViewModel> Recipes
        {
            get { return this.recipes; }
            set { SetValue(ref this.recipes, value); }
        }

        //COMMAND
        public ICommand RefreshUsersCommand
        {
            get { return new RelayCommand(loadUsers); }
        }

        public ICommand RefreshRecipesCommand
        {
            get { return new RelayCommand(loadRecipes); }
        }

        public ICommand SearchCommand
        {
            get { return new RelayCommand(Search); }
        }

        public ICommand FilterCommand
        {
            get { return new RelayCommand(Filter); }
        }

        #endregion


        #region CONSTRUCTOR

        public SearchViewModel()
        {
            this.loadUsers();
            this.loadRecipes();

            this.IsVisibleUsers = true;

            this.IsRefreshingUsers = false;

        }

        #endregion


        #region METHODS

        private async void loadUsers()
        {
            this.IsRefreshingUsers = true;

            Response userResponse = await ApiService.GetList<User>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                "/users");

            if (!userResponse.IsSuccess)
            {
                this.IsRefreshingUsers = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    userResponse.Message,
                    "Accept");
                return;
            }

            this.usersList = (List<User>)userResponse.Result;

            ChangeStringSpaces(1);

            this.IsRefreshingUsers = false;

            this.Users = new ObservableCollection<UserItemViewModel>(this.ToUserItemViewModel());


        }

        private async void loadRecipes()
        {
            this.IsRefreshingRecipes = true;

            Response recipeResponse = await ApiService.GetList<Recipe>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                "/recipes");

            if (!recipeResponse.IsSuccess)
            {
                this.IsRefreshingRecipes = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    recipeResponse.Message,
                    "Accept");
                return;
            }

            this.recipesList = (List<Recipe>)recipeResponse.Result;

            ChangeStringSpaces(2);

            this.IsRefreshingRecipes = false;

            this.Recipes = new ObservableCollection<RecipeItemViewModel>(this.ToRecipeItemViewModel());

            

        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.TextSearch))
            {
                this.Users = new ObservableCollection<UserItemViewModel>(this.ToUserItemViewModel());

                this.Recipes = new ObservableCollection<RecipeItemViewModel>(this.ToRecipeItemViewModel());
            }
            else
            {
                this.Users = new ObservableCollection<UserItemViewModel>(this.ToUserItemViewModel().Where(u => u.Name.ToLower().Contains(this.TextSearch.ToLower())
                                                                                        || u.Email.ToLower().Contains(this.TextSearch.ToLower())));

                this.Recipes = new ObservableCollection<RecipeItemViewModel>(this.ToRecipeItemViewModel().Where(r => r.Name.ToLower().Contains(this.TextSearch.ToLower())
                                                                                        || r.Author.ToLower().Contains(this.TextSearch.ToLower())));
            }
        }

        private async void Filter()
        {
            string filter = await Application.Current.MainPage.DisplayActionSheet("Filter", "Cancel", null, "Companies", "Recipes", "Users");

            switch (filter)
            {

                case "Companies":
                    await Application.Current.MainPage.DisplayAlert(":)", "Elegiste companies", "Ok");
                    break;

                case "Recipes":
                    this.IsVisibleUsers = false;
                    this.IsVisibleRecipes = true;
                    break;

                case "Users":
                    this.IsVisibleUsers = true;
                    this.IsVisibleRecipes = false;
                    break;

            }
        }

        public void ChangeStringSpaces(byte option)
        {

            switch (option)
            {
                case 1:
                    foreach (User user in this.usersList)
                    {
                        user.Name = ReadStringConverter.ChangeGetString(user.Name);
                    }

                    break;

                case 2:
                    foreach (Recipe recipe in this.recipesList)
                    {
                        recipe.Name = ReadStringConverter.ChangeGetString(recipe.Name);
                        recipe.CookingSpan = ReadStringConverter.ChangeGetString(recipe.CookingSpan);
                        recipe.EatingTime = ReadStringConverter.ChangeGetString(recipe.EatingTime);
                        recipe.Ingredients = ReadStringConverter.ChangeGetString(recipe.Ingredients);
                        recipe.Steps = ReadStringConverter.ChangeGetString(recipe.Steps);
                        recipe.Tags = ReadStringConverter.ChangeGetString(recipe.Tags);
                    }
                    break;
            }
        }

        private IEnumerable<UserItemViewModel> ToUserItemViewModel()
        {
            return this.usersList.Select(u => new UserItemViewModel
            {
                Email = u.Email,
                Name = u.Name,
                Age = u.Age,
                Password = u.Password,
                ProfilePic = u.ProfilePic,
                UsersFollowing = u.UsersFollowing,
                Followers = u.Followers,
                Chef = u.Chef

            });
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
                Image = r.Tags,
                Ingredients = r.Ingredients,
                Steps = r.Steps,
                Comments = r.Comments,
                Likers = r.Likers,
                Price = r.Price,
                Difficulty = r.Difficulty,
                Punctuation = r.Punctuation,
                Shares = r.Shares

            });
        }
        #endregion

    }
}