using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using CookTime.Constants;
using CookTime.Views;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using CookTime.Services;
using CookTime.Models;
using Plugin.Media.Abstractions;
using CookTime.FileHelpers;

namespace CookTime.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {

        #region ATTRIBUTES

        //TEXT
        private string textName;

        private string textBirthday;

        private string textEmail;

        private string textPassword;

        private string textConfirmPassword;


        //BACKGROUND COLOR
        private string bCName;

        private string bCBirthday;

        private string bCEmail;

        private string bCPassword;

        private string bCConfirmPassword;

        //OTHERS

        private Regex emailRegex;

        private Match match;

        private DatePicker actualDate;

        private bool isRunning;

        private MediaFile file;

        #endregion


        #region PROPERTIES

        //TEXT
        public string TextName
        {
            get { return this.textName; }
            set { SetValue(ref this.textName, value); }
        }

        public string TextBirthday
        {
            get { return this.textBirthday; }
            set { SetValue(ref this.textBirthday, value); }
        }

        public string TextAge { get; set; }

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

        public string TextConfirmPassword
        {
            get { return this.textConfirmPassword; }
            set { SetValue(ref this.textConfirmPassword, value); }
        }

        //BACKGROUND COLOR
        public string BCName
        {
            get { return this.bCName; }
            set { SetValue(ref this.bCName, value); }
        }

        public string BCBirthday
        {
            get { return this.bCBirthday; }
            set { SetValue(ref this.bCBirthday, value); }
        }

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

        public string BCConfirmPassword
        {
            get { return this.bCConfirmPassword; }
            set { SetValue(ref this.bCConfirmPassword, value); }
        }

        //COMMAND
        public ICommand CreateAccountCommand
        {
            get { return new RelayCommand(CreateAccount); }
        }

        public ICommand CompanyCommand
        {
            get { return new RelayCommand(Company); }
        }

        //ACTIVITY INDICATOR

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        #endregion


        #region CONSTRUCTOR

        public SignUpViewModel()
        {
            this.emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            this.actualDate = new DatePicker();

        }

        #endregion


        #region COMMAND METHODS

        //CLASS METHODS

        /*
         * gets the age based in the birth date
         */
        private bool IsOverSixteen()
        {
            string textBirthdayDay = this.TextBirthday.Substring(3, 2);
            string textBirthdayMonth = this.TextBirthday.Substring(0, 2);
            string textBirthdayYear = this.TextBirthday.Substring(6, 4);

            int years = actualDate.Date.Year - int.Parse(textBirthdayYear);

            if (years >= 16)
            {
                int months = actualDate.Date.Month - int.Parse(textBirthdayMonth);

                if (months < 0)// The birthday month isn't yet
                {
                    years -= 1;
                    months += 12;
                }

                else
                {
                    int days = actualDate.Date.Day - int.Parse(textBirthdayDay);

                    if (days < 0) // The birthday day isn't yet
                    {
                        if (months == 0)
                        {
                            years -= 1;
                            months = 11;
                        }
                        else
                        {
                            months -= 1;
                        }
                    }
                }
            }

            if (years >= 16)
            {
                this.TextAge = years.ToString();
                return true;
            }

            return false;


        }

        //COMMAND METHODS

        /*
         *Validates every single information given in the form defined in the SignUpPage.XAML
         *and if everything is correct creates an account
         */
        private async void CreateAccount()
        {

            //Activates activity indicador
            IsRunning = true;

            if (string.IsNullOrEmpty(this.TextName))
            {
                IsRunning = false;
                BCName = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a name", "Ok");
                BCName = ColorsFonts.BackGround;
                return;
            }

            if (string.IsNullOrEmpty(this.TextBirthday))
            {
                IsRunning = false;
                BCBirthday = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a birth date", "Ok");
                BCBirthday = ColorsFonts.BackGround;
                return;
            }

            if (string.IsNullOrEmpty(this.TextEmail))
            {
                IsRunning = false;
                BCEmail = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter an Email", "Ok");
                BCEmail = ColorsFonts.BackGround;
                return;
            }

            if (string.IsNullOrEmpty(this.TextPassword))
            {
                IsRunning = false;
                BCPassword = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a password", "Ok");
                BCPassword = ColorsFonts.BackGround;
                return;
            }

            if (string.IsNullOrEmpty(this.TextConfirmPassword))
            {
                IsRunning = false;
                BCConfirmPassword = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a confirmation password", "Ok");
                BCConfirmPassword = ColorsFonts.BackGround;
                return;
            }

            //Makes sure that the user is over 16 years old
            if (!(IsOverSixteen()))
            {
                IsRunning = false;
                BCBirthday = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", $"You must be over 16 years old", "Ok");
                BCBirthday = ColorsFonts.BackGround;
                return;
            }

            //Makes sure that the email written has the format of an email
            this.match = emailRegex.Match(TextEmail);
            if (!(match.Success))
            {
                IsRunning = false;
                BCEmail = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a valid Email", "Ok");
                BCEmail = ColorsFonts.BackGround;
                return;
            }

            //Validates that the password and confirmation password match
            if (this.TextPassword != this.TextConfirmPassword)
            {
                IsRunning = false;
                BCPassword = ColorsFonts.ErrorColor;
                BCConfirmPassword = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "The confirmation password doesn't match", "Ok");
                BCPassword = ColorsFonts.BackGround;
                BCConfirmPassword = ColorsFonts.BackGround;
                return;
            }

            //Checks the internet connection before interacting with the server
            Response checkConnection = await ApiService.CheckConnection();
            if (!checkConnection.IsSuccess)
            {
                IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Check your internet connection",
                    "Accept");
                return;
            }

            

            //Changes the spacing in the user name
            this.TextName = ReadStringConverter.ChangePostString(this.TextName);

            //Creates the user account with the data given 
            User user = new User
            {
                Email = this.TextEmail,
                Name = this.TextName,
                Age = this.TextAge,
                Password = this.TextPassword,
            };

            string controller = "/users/" + this.TextEmail; //Asking for the account information
            Response checkEmail = await ApiService.Get<User>( //Tries to get the account information
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                controller);

            //Checks if the email already exists (it doesn't have to)
            if (checkEmail.IsSuccess)
            {
                IsRunning = false;
                this.BCEmail = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "This email is already registered",
                    "Accept");
                this.BCEmail = ColorsFonts.BackGround;
                return;
            }

            //Generates the query url
            string queryUrl = "/users?email=" + this.TextEmail + "&password=" + this.TextPassword
                + "&name=" + this.TextName + "&age=" + this.TextAge;

            //Posts the account
            Response response = await ApiService.Post<User>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                queryUrl,
                user,
                true);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Something was wrong",
                    "Accept");
                return;
            }

            User loggedUser = (User)response.Result;

            this.TextName = string.Empty;
            this.TextEmail = string.Empty;
            this.TextPassword = string.Empty;
            this.TextConfirmPassword = string.Empty;

            //The new user has been created, it can enter to the tabbed page 
            MainViewModel.getInstance().TabbedHome = new TabbedHomeViewModel(loggedUser);
            await Application.Current.MainPage.Navigation.PushAsync(new TabbedHomePage());

        }

        private async void Company()
        {
            this.TextName = string.Empty;
            this.TextEmail = string.Empty;
            this.TextPassword = string.Empty;
            this.TextConfirmPassword = string.Empty;

            MainViewModel.getInstance().CompanySignUp = new CompanySignUpViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new CompanySignUpPage());
        }

        #endregion
    }
}
