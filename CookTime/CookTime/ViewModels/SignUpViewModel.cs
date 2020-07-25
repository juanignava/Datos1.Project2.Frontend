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
using System.Linq;

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

        //companyText

        private string textServiceHours;

        private string textContactMethods;

        private string textAddressLat;

        private string textAddressLon; 
        //BACKGROUND COLOR
        private string bCName;

        private string bCBirthday;

        private string bCEmail;

        private string bCPassword;

        private string bCConfirmPassword;

        //background color company

        private string bCServiceHours;

        private string bCContactMethods;

        private string bCAddress;

        //OTHERS

        private Regex emailRegex;

        private Match match;

        private DatePicker actualDate;

        private bool isRunning;

        private MediaFile file;

        //COMPANY
        private bool isCompany;

        private ImageSource signUpIcon;

        private string companyText;


        #endregion


        #region PROPERTIES

        //COMPANY
        public bool IsCompany
        {
            get { return this.isCompany; }
            set { SetValue(ref this.isCompany, value); }
        }

        public ImageSource SignUpIcon
        {
            get { return this.signUpIcon; }
            set { SetValue(ref this.signUpIcon, value); }
        }
        public string CompanyText 
        {
            get { return this.companyText; }
            set { SetValue(ref this.companyText, value); }
        }

        //TEXT
        public string TextName
        {
            get { return this.textName; }
            set
            {
                SetValue(ref this.textName, value);
                BCName = ColorsFonts.backGround;
            }
        }

        public string TextBirthday
        {
            get { return this.textBirthday; }
            set
            {
                SetValue(ref this.textBirthday, value);
                BCBirthday = ColorsFonts.backGround;
            }
        }

        public string TextAge { get; set; }

        public string TextEmail
        {
            get { return this.textEmail; }
            set
            {
                SetValue(ref this.textEmail, value);
                BCEmail = ColorsFonts.backGround;
            }
        }

        public string TextPassword
        {
            get { return this.textPassword; }
            set
            {
                SetValue(ref this.textPassword, value);
                BCPassword = ColorsFonts.backGround;
            }
        }

        public string TextConfirmPassword
        {
            get { return this.textConfirmPassword; }
            set
            {
                SetValue(ref this.textConfirmPassword, value);
                BCConfirmPassword = ColorsFonts.backGround;
            }
        }

        //text company 

        public string TextContactMethods 
        {
            get { return this.textContactMethods; }
            set { SetValue(ref this.textContactMethods, value); }
        }

        public string TextAddressLat
        {
            get { return this.textAddressLat; }
            set { SetValue(ref this.textAddressLat, value); }
        }

        public string TextAddressLon
        {
            get { return this.textAddressLon; }
            set { SetValue(ref this.textAddressLon, value); }
        }

        public string TextServiceHours
        {
            get { return this.textServiceHours; }
            set { SetValue(ref this.textServiceHours, value); }
        }

        //BACKGROUND COLOR
        public string BCName
        {
            get { return this.bCName; }
            set
            {
                SetValue(ref this.bCName, value);
            }
        }

        public string BCBirthday
        {
            get { return this.bCBirthday; }
            set
            {
                SetValue(ref this.bCBirthday, value);
            }
        }

        public string BCEmail
        {
            get { return this.bCEmail; }
            set
            {
                SetValue(ref this.bCEmail, value);
            }
        }

        public string BCPassword
        {
            get { return this.bCPassword; }
            set
            {
                SetValue(ref this.bCPassword, value);
            }
        }

        public string BCConfirmPassword
        {
            get { return this.bCConfirmPassword; }
            set
            {
                SetValue(ref this.bCConfirmPassword, value);
            }
        }

        //background color companies

        public string BCContactMethods
        {
            get { return this.bCContactMethods; }
            set { SetValue(ref this.bCContactMethods, value); }
        }

        public string BCAddress
        {
            get { return this.bCAddress; }
            set { SetValue(ref this.bCAddress, value); }
        }

        public string BCServiceHours
        {
            get { return this.bCServiceHours; }
            set { SetValue(ref this.bCServiceHours, value); }
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
            this.SignUpIcon = "SignUpIcon";
            this.CompanyText = "Sign Up as company";
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
                BCName = ColorsFonts.errorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a name", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(this.TextBirthday))
            {
                IsRunning = false;
                BCBirthday = ColorsFonts.errorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a birth date", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(this.TextEmail))
            {
                IsRunning = false;
                BCEmail = ColorsFonts.errorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter an Email", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(this.TextPassword))
            {
                IsRunning = false;
                BCPassword = ColorsFonts.errorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a password", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(this.TextConfirmPassword))
            {
                IsRunning = false;
                BCConfirmPassword = ColorsFonts.errorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a confirmation password", "Ok");
                return;
            }

            //Makes sure that the user is over 16 years old
            if (!(IsOverSixteen()))
            {
                IsRunning = false;
                BCBirthday = ColorsFonts.errorColor;
                await Application.Current.MainPage.DisplayAlert("Error", $"You must be over 16 years old", "Ok");
                //BCBirthday = ColorsFonts.backGround;
                return;
            }

            //Makes sure that the email written has the format of an email
            this.match = emailRegex.Match(TextEmail);
            if (!(match.Success))
            {
                IsRunning = false;
                BCEmail = ColorsFonts.errorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a valid Email", "Ok");
                return;
            }

            //Validates that the password and confirmation password match
            if (this.TextPassword != this.TextConfirmPassword)
            {
                IsRunning = false;
                BCPassword = ColorsFonts.errorColor;
                BCConfirmPassword = ColorsFonts.errorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "The confirmation password doesn't match", "Ok");
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

            string controller = "/users/" + this.TextEmail; //Asking for the account information
            Response checkEmail = await ApiService.Get<User>( //Tries to get the account information
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                controller);

            //Checks if the email already exists (it doesn't have to)
            if (checkEmail.IsSuccess)
            {
                IsRunning = false;
                this.BCEmail = ColorsFonts.errorColor;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "This email is already registered",
                    "Accept");
                this.BCEmail = ColorsFonts.backGround;
                return;
            }

            

            if (this.IsCompany == true)
            {
                if (string.IsNullOrEmpty(this.TextContactMethods))
                {
                    IsRunning = false;
                    BCContactMethods = ColorsFonts.errorColor;
                    await Application.Current.MainPage.DisplayAlert("Error", "You must enter the company contact methods", "Ok");
                    BCContactMethods = ColorsFonts.backGround;
                    return;
                }

                if (string.IsNullOrEmpty(this.TextAddressLat))
                {
                    IsRunning = false;
                    BCAddress = ColorsFonts.errorColor;
                    await Application.Current.MainPage.DisplayAlert("Error", "You must enter the company adress latitud", "Ok");
                    BCAddress = ColorsFonts.backGround;
                    return;
                }

                if (string.IsNullOrEmpty(this.TextAddressLon))
                {
                    IsRunning = false;
                    BCAddress = ColorsFonts.errorColor;
                    await Application.Current.MainPage.DisplayAlert("Error", "You must enter the company adress longitud", "Ok");
                    BCAddress = ColorsFonts.backGround;
                    return;
                }

                if (string.IsNullOrEmpty(this.TextServiceHours))
                {
                    IsRunning = false;
                    BCServiceHours = ColorsFonts.errorColor;
                    await Application.Current.MainPage.DisplayAlert("Error", "You must enter the company service hours", "Ok");
                    BCServiceHours = ColorsFonts.backGround;
                    return;
                }

                this.TextContactMethods = ReadStringConverter.ChangePostString(this.TextContactMethods);
                this.TextServiceHours = ReadStringConverter.ChangePostString(this.TextServiceHours);

                var address2 = this.TextAddressLat + "," + this.TextAddressLon;

                Company company = new Company
                {
                    Email = this.TextEmail,
                    Name = this.TextName,
                    Password = this.TextPassword,
                    Contact = this.TextContactMethods,
                    ServiceSchedule = this.TextServiceHours,
                   // Location = address2
                };

                string queryUrlC = "/companies?email=" + this.TextEmail + "&password=" + this.TextPassword
                    + "&name=" + this.TextName + "&location=" + address2 + "&contact=" + this.TextContactMethods + "&serviceSchedule=" + this.TextServiceHours;

                //Posts the Company
                Response responseC = await ApiService.Post<Company>(
                    "http://localhost:8080/CookTime.BackEnd",
                    "/api",
                    queryUrlC,
                    company,
                    true);

                if (!responseC.IsSuccess)
                {
                    IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Something was wrong",
                        "Accept");
                    return;
                }
            }

            //Creates the user account with the data given 
            User user = new User
            {
                Email = this.TextEmail,
                Name = this.TextName,
                Age = this.TextAge,
                Password = this.TextPassword,
            };

            //Generates the query url
            
            string queryUrl = "/users?email=" + this.TextEmail + "&password=" + this.TextPassword
                + "&name=" + this.TextName + "&age=" + this.TextAge;

            if (this.IsCompany == true)
            {
                queryUrl += "&company=true";
            }

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

            IsRunning = false;

            //The new user has been created, it can enter to the tabbed page 
            MainViewModel.getInstance().TabbedHome = new TabbedHomeViewModel(loggedUser);
            await Application.Current.MainPage.Navigation.PushAsync(new TabbedHomePage());



        }

        private async System.Threading.Tasks.Task createCompany()
        {
            var address = this.TextAddressLat + "," + this.TextAddressLon;
            Company company = new Company
            {
                Email = this.TextEmail,
                Name = this.TextName,
                Password = this.TextPassword,
                Contact = this.TextContactMethods,
                ServiceSchedule = this.TextServiceHours,
                //Location = address
            };

            string queryUrl = "/companies?email=" + this.TextEmail + "&password=" + this.TextPassword
                + "&name=" + this.TextName + "&location=" + address + "&contact=" + this.TextContactMethods + "&serviceSchedule=" + this.TextServiceHours;

            //Posts the Company
            Response response = await ApiService.Post<Company>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                queryUrl,
                company,
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

            return;
        }

        private async void Company()
        {
            if (this.IsCompany == false)
            {
                this.IsCompany = true;
                this.SignUpIcon = "CompanySignUpIcon";
                this.CompanyText = "Sign Up as an user";
            }
            else
            {
                this.IsCompany = false;
                this.SignUpIcon = "SignUpIcon";
                this.CompanyText = "Sign Up as a company";
            }
        }

        #endregion
    }
}
