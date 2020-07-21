
using CookTime.Models;
using CookTime.Services;
using CookTime.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookTime.ViewModels
{
    public class RecipeDetailViewModel: BaseViewModel
    {
        #region ATTRIBUTES

        //TEXT
        private string likeSourse;

        //VALUE
        private int punctuation;

        //BOOLEAN
        private bool isRunning;

        //USER
        private User loggedUser;

        #endregion


        #region Properties

        //SPECIFIC RECIPE
        public Recipe Recipe { get; set; }

        //TEXT
        public string LikeSourse
        {
            get { return this.likeSourse; }
            set { SetValue(ref this.likeSourse, value); }
        }

        //VALUE
        public int Punctuation
        {
            get { return this.punctuation; }
            set { SetValue(ref this.punctuation, value); }
        }

        //BOOLEAN
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        //COMMAND
        public ICommand LikeCommand
        {
            get { return new RelayCommand(Like); }
        }

        #endregion


        #region CONSTRUCTOR

        public RecipeDetailViewModel(Recipe recipe)
        {
            this.loggedUser = TabbedHomeViewModel.getUserInstance();
            this.Recipe = recipe;
            this.Punctuation = Recipe.Punctuation;

            this.LikeSourse = "UnLikedIcon";
        }

        #endregion


        #region COMMAND METHODS

        private async void Like()
        {
            if (this.LikeSourse.Equals("UnLikedIcon"))
            {
                this.IsRunning = true;

                this.LikeSourse = "LikedIcon";
                this.Punctuation += 1;

                Response checkConnection = await ApiService.CheckConnection();
                if (!checkConnection.IsSuccess)
                {
                    this.IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Check your internet connection",
                        "Accept");
                    return;
                }

                string queryUrl = $"/users/new_notif?emisorUser={loggedUser.Email}&recieverUser={Recipe.Author}&notifType=2&recipe={Recipe.Name}";

                Response response = await ApiService.Post<Recipe>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                queryUrl,
                this.Recipe,
                false);

                if (!response.IsSuccess)
                {
                    this.IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Something was wrong",
                        "Accept");
                    return;
                }

                this.IsRunning = false;

            }

            else
            {
                this.LikeSourse = "UnLikedIcon";
                this.Punctuation -= 1;
            }

        }

        #endregion
    }
}
