
using CookTime.FileHelpers;
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

        //USER
        private User loggedUser;

        #endregion


        #region Properties

        //SPECIFIC RECIPE
        public Recipe UserRecipe { get; set; }

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
            this.UserRecipe = recipe;
            this.Punctuation = UserRecipe.Punctuation;

            this.LikeSourse = loadLikeButton();
        }

        #endregion


        #region COMMAND METHODS

        private string loadLikeButton()
        {
            if (UserRecipe.Likers != null)
            {
                foreach (string item in UserRecipe.Likers)
                {
                    if (item.Equals(this.loggedUser.Email))
                    {
                        return "LikedIcon";
                    }

                }
            }
            return "UnLikedIcon";

        }

        private async void Like()
        {
            if (this.LikeSourse.Equals("UnLikedIcon"))
            {

                Response checkConnection = await ApiService.CheckConnection();
                if (!checkConnection.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Check your internet connection",
                        "Accept");
                    return;
                }

                string recipeName = ReadStringConverter.ChangePostString(this.UserRecipe.Name);

                string queryUrl = $"/users/new_notif?emisorUser={loggedUser.Email}&recieverUser={UserRecipe.Author}&notifType=3&recipe={recipeName}";

                Notification notification = new Notification
                {
                    EmisorUser = loggedUser.Email,
                    RecieverUser = UserRecipe.Author,
                    NotifType = 3,
                    RecipeName = recipeName    
                };

                Response response = await ApiService.Post<Notification>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                queryUrl,
                notification,
                false);

                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Action can't be done",
                        "Ok");
                    return;
                }

                this.LikeSourse = "LikedIcon";
                this.Punctuation += 1;

            }

            else
            {
                Response checkConnection = await ApiService.CheckConnection();
                if (!checkConnection.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Check your internet connection",
                        "Accept");
                    return;
                }

                string recipeName = ReadStringConverter.ChangePostString(this.UserRecipe.Name);

                string queryUrl = $"/users/new_notif?emisorUser={loggedUser.Email}&recieverUser={UserRecipe.Author}&notifType=4&recipe={recipeName}";

                Notification notification = new Notification
                {
                    EmisorUser = loggedUser.Email,
                    RecieverUser = UserRecipe.Author,
                    NotifType = 4,
                    RecipeName = recipeName
                };

                Response response = await ApiService.Post<Notification>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                queryUrl,
                notification,
                false);

                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Action can't be done",
                        "Ok");
                    return;
                }

                this.LikeSourse = "UnLikedIcon";
                this.Punctuation -= 1;
            }

        }

        #endregion
    }
}
