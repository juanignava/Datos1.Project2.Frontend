
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

        //ACTIVITY INDICADOR
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


        #region Constructor

        public LoginViewModel()
        {
            this.TextEmail = "andres@gmail.com";
            this.TextPassword = "123";
        }

        #endregion


        #region COMMAND METHODS

        private async void Login()
        {
            //The activivty indicator is enabled once the button is pressed
            this.IsRunning = true;
        
            if (string.IsNullOrEmpty(this.TextEmail)) // No email written
            {
                BCEmail = ColorsFonts.errorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter an Email", "Ok");
                BCEmail = ColorsFonts.backGround;
                this.IsRunning = false;
                return;
            }

            if (string.IsNullOrEmpty(this.TextPassword)) //No Password written
            {
                this.IsRunning = false;
                BCPassword = ColorsFonts.errorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a password", "Ok");
                BCPassword = ColorsFonts.backGround;
                return;
            }

            //Checking internet connection before asking for the server
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

            string controller = "/users/" + this.TextEmail; //Asking for the account information
            var response = await ApiService.Get<User>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                controller);

            //If the email isn't found then the account is not registered yet
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

            User loggedUser = ((User) response.Result);

            this.askedPassword = loggedUser.Password; //The answer given is the real encrypted password

            var encryptedPassword = MD5encryptor.MD5Hash(this.TextPassword); //To compare them the written password has to be encrypted

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

            this.IsRunning = false;

            //If the passwords match then the login credentials were correct, the user will get to the tabbed page
            this.TextEmail = string.Empty;
            this.TextPassword = string.Empty;
            MainViewModel.getInstance().TabbedHome = new TabbedHomeViewModel(loggedUser);
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
