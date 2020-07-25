using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
        private bool isRefreshing;

        private bool isVisibleUsers;

        private bool isVisibleRecipes;

        private bool isVisibleChefs;

        //OBSERVABLE COLLECTION
        private ObservableCollection<UserItemViewModel> users;

        private ObservableCollection<RecipeItemViewModel> recipes;

        private ObservableCollection<UserItemViewModel> chefs;

        //LIST
        private List<Recipe> recipesList;

        private List<User> usersList;

        private List<User> chefsList;
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
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
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

        public bool IsVisibleChefs
        {
            get { return this.isVisibleChefs; }
            set { SetValue(ref this.isVisibleChefs, value); }
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

        public ObservableCollection<UserItemViewModel> Chefs
        {
            get { return this.chefs; }
            set { SetValue(ref this.chefs, value); }
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

        public ICommand RefreshChefsCommand
        {
            get { return new RelayCommand(loadChefs); }
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

            this.IsVisibleUsers = true;

            this.IsRefreshing = false;


        }

        #endregion


        #region METHODS

        private async void loadUsers()
        {
            this.IsRefreshing = true;

            this.chefsList = new List<User>();

            Response userResponse = await ApiService.GetList<User>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                "/users");

            if (!userResponse.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    userResponse.Message,
                    "Accept");
                return;
            }

            this.IsRefreshing = false;

            this.usersList = (List<User>)userResponse.Result;

            ChangeStringSpaces(1);

            this.Users = new ObservableCollection<UserItemViewModel>(this.ToUserItemViewModel(this.usersList));


        }

        private async void loadRecipes()
        {
            this.IsRefreshing = true;

            Response recipeResponse = await ApiService.GetList<Recipe>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                "/recipes");

            if (!recipeResponse.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    recipeResponse.Message,
                    "Accept");
                return;
            }

            this.IsRefreshing = false;

            this.recipesList = (List<Recipe>)recipeResponse.Result;

            ChangeStringSpaces(2);

            this.Recipes = new ObservableCollection<RecipeItemViewModel>(this.ToRecipeItemViewModel());

            

        }

        private void loadChefs()
        {
            this.IsRefreshing = true;

            this.loadUsers();

            foreach (User user in this.usersList)
            {
                if (user.Chef)
                {
                    this.chefsList.Add(user);
                }
            }

            this.IsRefreshing = false;

            if (this.chefsList != null)
            {
                this.Chefs = new ObservableCollection<UserItemViewModel>(this.ToUserItemViewModel(this.chefsList));
            }

        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.TextSearch))
            {
                this.Users = new ObservableCollection<UserItemViewModel>(this.ToUserItemViewModel(this.usersList));

                this.Recipes = new ObservableCollection<RecipeItemViewModel>(this.ToRecipeItemViewModel());

                this.Chefs = new ObservableCollection<UserItemViewModel>(this.ToUserItemViewModel(this.chefsList));

            }
            else
            {
                this.Users = new ObservableCollection<UserItemViewModel>(this.ToUserItemViewModel(this.usersList).Where(u => u.Name.ToLower().Contains(this.TextSearch.ToLower())
                                                                                        || u.Email.ToLower().Contains(this.TextSearch.ToLower())));

                this.Recipes = new ObservableCollection<RecipeItemViewModel>(this.ToRecipeItemViewModel().Where(r => r.Name.ToLower().Contains(this.TextSearch.ToLower())
                                                                                        || r.Author.ToLower().Contains(this.TextSearch.ToLower())));

                this.Chefs = new ObservableCollection<UserItemViewModel>(this.ToUserItemViewModel(this.chefsList).Where(c => c.Name.ToLower().Contains(this.TextSearch.ToLower())
                                                                                        || c.Email.ToLower().Contains(this.TextSearch.ToLower())));

            }
        }

        private async void Filter()
        {
            string filter = await Application.Current.MainPage.DisplayActionSheet("Filter", "Cancel", null, "Chefs", "Recipes", "Users");

            switch (filter)
            {

                case "Chefs":
                    this.loadChefs();
                    this.IsVisibleUsers = false;
                    this.IsVisibleRecipes = false;
                    this.IsVisibleChefs = true;
                    break;

                case "Recipes":
                    this.loadRecipes();
                    this.IsVisibleUsers = false;
                    this.IsVisibleRecipes = true;
                    this.IsVisibleChefs = false;
                    break;

                case "Users":
                    this.loadUsers();
                    this.IsVisibleUsers = true;
                    this.IsVisibleRecipes = false;
                    this.IsVisibleChefs = false;
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

        private IEnumerable<UserItemViewModel> ToUserItemViewModel(List<User> list)
        {
            return list.Select(u => new UserItemViewModel
            {
                Email = u.Email,
                Name = u.Name,
                Age = u.Age,
                Password = u.Password,
                ProfilePic = u.ProfilePic,
                UsersFollowing = u.UsersFollowing,
                Followers = u.Followers,
                Chef = u.Chef,
                IsCompany = u.IsCompany
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
                Image = r.Image,
                Ingredients = r.Ingredients,
                Steps = r.Steps,
                Comments = r.Comments,
                Likers = r.Likers,
                Price = r.Price,
                Difficulty = r.Difficulty,
                Punctuation = r.Punctuation,
                Shares = r.Shares,
                RecipeImage = r.RecipeImage,
                UserImage = r.UserImage
            });
        }
        #endregion

    }
}