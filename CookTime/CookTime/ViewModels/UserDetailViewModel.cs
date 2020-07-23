using System;
using System.Windows.Input;
using CookTime.Models;
using CookTime.Services;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace CookTime.ViewModels
{
    public class UserDetailViewModel : BaseViewModel
    {
        #region ATTRIBUTES
        //TEXT
        private string textFollow;
        //VALUE
        private int following;

        private int followers;

        //OTHER
        private User loggedUser;

        #endregion


        #region Properties

        //This property will be passed by parameter, due to the connection with the NewsFeed page
        public User User
        {
            get;
            set;
        }

        public string TextFollow
        {
            get { return this.textFollow; }
            set { SetValue(ref this.textFollow, value); }
        }

        //VALUE
        public int Following
        {
            get { return this.following; }
            set { SetValue(ref this.following, value); }
        }

        public int Followers
        {
            get { return this.followers; }
            set { SetValue(ref this.followers, value); }
        }

        //COMMAND
        public ICommand FollowCommand
        {
            get { return new RelayCommand(Follow); }
        }

        #endregion


        #region CONSTRUCTOR

        public UserDetailViewModel(User user)
        {
            this.loggedUser = TabbedHomeViewModel.getUserInstance();

            this.User = user;
            this.Following = User.UsersFollowing.Length;
            this.Followers = User.Followers.Length;

            this.TextFollow = this.loadFollowButton();


        }

        #endregion

        #region COMMAND METHODS

        private string loadFollowButton()
        {
            if (User.Followers != null)
            {
                foreach (string item in User.Followers)
                {
                    if (item.Equals(this.loggedUser.Email))
                    {
                        return "Following";
                    }

                }
            }
            
            return "Follow";
        }

        private async void Follow()
        {
            if (this.TextFollow.Equals("Follow"))
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

                string queryUrl = $"/users/new_notif?emisorUser={loggedUser.Email}&recieverUser={User.Email}&notifType=1";

                Notification notification = new Notification
                {
                    EmisorUser = loggedUser.Email,
                    RecieverUser = User.Email,
                    NotifType = 1,
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

                this.TextFollow = "Following";
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

                string queryUrl = $"/users/new_notif?emisorUser={loggedUser.Email}&recieverUser={User.Email}&notifType=2";

                Notification notification = new Notification
                {
                    EmisorUser = loggedUser.Email,
                    RecieverUser = User.Email,
                    NotifType = 2,
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
                        "Check your internet connection",
                        "Ok");
                    return;
                }

                this.TextFollow = "Follow";
            }

            
        }

        #endregion
    }
}
