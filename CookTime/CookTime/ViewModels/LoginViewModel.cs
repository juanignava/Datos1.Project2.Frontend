using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using CookTime.Views;
using Xamarin.Forms;
using CookTime.Constants;


namespace CookTime.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region ATTRIBUTES

        //TEXT
        private string textEmail;

        private string textPassword;

        //BACKGROUND COLOR
        private string bCEmail;

        private string bCPassword;

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
        
            if (string.IsNullOrEmpty(this.TextEmail))
            {
                BCEmail = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter an Email", "Ok");
                BCEmail = ColorsFonts.BackGround;
                return;
            }

            if (string.IsNullOrEmpty(this.TextPassword))
            {
                BCPassword = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a password", "Ok");
                BCPassword = ColorsFonts.BackGround;
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
