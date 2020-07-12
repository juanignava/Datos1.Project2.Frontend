using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using CookTime.Constants;
using CookTime.Views;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace CookTime.ViewModels
{
    public class SignUpViewModel: BaseViewModel
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

        private bool IsOverSixteen()
        {
            string textBirthdayDay = this.TextBirthday.Substring(3, 2);
            string textBirthdayMonth = this.TextBirthday.Substring(0, 2);
            string textBirthdayYear = this.TextBirthday.Substring(6, 4);

            int years = actualDate.Date.Year - int.Parse(textBirthdayYear);

            if (years < 16) return false;

            if (years > 16) return true;

            int months = actualDate.Date.Month - int.Parse(textBirthdayMonth);

            if (months < 0) return false;

            if (months > 0) return true;

            int days = actualDate.Date.Day - int.Parse(textBirthdayDay);

            if (days < 0) return false;

            else return true;

        }

        //COMMAND METHODS
        private async void CreateAccount()
        {

            if (string.IsNullOrEmpty(this.TextName))
            {
                BCName = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a name", "Ok");
                BCName = ColorsFonts.BackGround;
                return;
            }

            if (string.IsNullOrEmpty(this.TextBirthday))
            {
                BCBirthday = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a birth date", "Ok");
                BCBirthday = ColorsFonts.BackGround;
                return;
            }

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

            if (string.IsNullOrEmpty(this.TextConfirmPassword))
            {
                BCConfirmPassword = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a confirmation password", "Ok");
                BCConfirmPassword = ColorsFonts.BackGround;
                return;
            }

            if (!(IsOverSixteen()))
            {
                BCBirthday = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", $"You must be over 16 years old", "Ok");
                BCBirthday = ColorsFonts.BackGround;
                return;
            }

            this.match = emailRegex.Match(TextEmail);

            if (!(match.Success))
            {
                BCEmail = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a valid Email", "Ok");
                BCEmail = ColorsFonts.BackGround;
                return;
            }

            if (this.TextPassword != this.TextConfirmPassword)
            {
                BCPassword = ColorsFonts.ErrorColor;
                BCConfirmPassword = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "The password confirmation doesn't match", "Ok");
                BCPassword = ColorsFonts.BackGround;
                BCConfirmPassword = ColorsFonts.BackGround;
                return;
            }

            this.TextName = string.Empty;
            this.TextEmail = string.Empty;
            this.TextPassword = string.Empty;
            this.TextConfirmPassword = string.Empty;

            MainViewModel.getInstance().TabbedHome = new TabbedHomeViewModel();
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
