
namespace CookTime.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using CookTime.Views;
    using Xamarin.Forms;
    using CookTime.Constants;
    using Services;
    using Models;
    using Encryptor;
    using System;
    using System.Collections.Generic;

    public class LoginViewModel : BaseViewModel
    {

        #region SERVICES
        private ApiService apiService;

        #endregion

        #region ATTRIBUTES

        //TEXT
        private string textEmail;

        private string textPassword;

        private string askedPassword; 

        //BACKGROUND COLOR
        private string bCEmail;

        private string bCPassword;

        //Activity indicator
        private bool isRunning;

        #endregion


        #region PROPERTIES

        //TEXT
        public string TextEmail
        {
            get { return this.textEmail; }
            set { SetValue(ref this.textEmail, value); }
        }

        public string TextPassword
        {
            get { return this.textPassword; }
            set { SetValue(ref this.textPassword, value); }
        }

        //BACKGROUND COLOR
        public string BCEmail
        {
            get { return this.bCEmail; }
            set { SetValue(ref this.bCEmail, value); }
        }

        public string BCPassword
        {
            get { return this.bCPassword; }
            set { SetValue(ref this.bCPassword, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        //COMMAND
        public ICommand LoginCommand
        {
            get { return new RelayCommand(Login); }
        }

        public ICommand AccountCommand
        {
            get { return new RelayCommand(Account); }
        }

        

        #endregion

        #region COMMAND METHODS

        private async void Login()
        {
            this.IsRunning = true;
        
            if (string.IsNullOrEmpty(this.TextEmail))
            {
                BCEmail = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter an Email", "Ok");
                BCEmail = ColorsFonts.BackGround;
                this.IsRunning = false;
                return;
            }

            if (string.IsNullOrEmpty(this.TextPassword))
            {
                this.IsRunning = false;
                BCPassword = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a password", "Ok");
                BCPassword = ColorsFonts.BackGround;
                return;
            }

            var connection = await ApiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Accept");
                return;
            }

            string controller = "/users/" + this.TextEmail;
            var response = await ApiService.GetUser<User>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                controller);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                this.TextEmail = string.Empty;
                return;
            }

            this.askedPassword = (string)response.Result;

            var encryptedPassword = MD5encryptor.MD5Hash(this.TextPassword);

            if (encryptedPassword != this.askedPassword)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Login error",
                    "Incorrect password",
                    "Accept");
                this.TextPassword = string.Empty;
                return;
            }

            this.TextEmail = string.Empty;
            this.TextPassword = string.Empty;
            MainViewModel.getInstance().TabbedHome = new TabbedHomeViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new TabbedHomePage());
        }

        private async void Account()
        {
            this.TextEmail = string.Empty;
            this.TextPassword = string.Empty;
            MainViewModel.getInstance().SignUp = new SignUpViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new SignUpPage());

        }

        #endregion

    }
}
