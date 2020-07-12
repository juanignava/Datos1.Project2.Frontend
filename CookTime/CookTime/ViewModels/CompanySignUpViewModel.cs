using System;
using System.Windows.Input;
using CookTime.Constants;
using CookTime.Views;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace CookTime.ViewModels
{
    public class CompanySignUpViewModel: BaseViewModel
    {
        #region ATTRIBUTES

        //TEXT
        private string textCompanyName;

        private string textContactMethods;

        private string textAddress;

        private string textServiceHours;

        //BACKGROUND COLOR
        private string bCCompanyName;

        private string bCContactMethods;

        private string bCAddress;

        private string bCServiceHours;

        #endregion


        #region PROPERTIES

        //TEXT
        public string TextCompanyName
        {
            get { return this.textCompanyName; }
            set { SetValue(ref this.textCompanyName, value); }
        }

        public string TextContactMethods
        {
            get { return this.textContactMethods; }
            set { SetValue(ref this.textContactMethods, value); }
        }

        public string TextAddress
        {
            get { return this.textAddress; }
            set { SetValue(ref this.textAddress, value); }
        }

        public string TextServiceHours
        {
            get { return this.textServiceHours; }
            set { SetValue(ref this.textServiceHours, value); }
        }

        //BACKGROUND COLOR
        public string BCCompanyName
        {
            get { return this.bCCompanyName; }
            set { SetValue(ref this.bCCompanyName, value); }
        }

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
        public ICommand CreateCompanyCommand
        {
            get { return new RelayCommand(CreateCompany); }
        }

        #endregion

        #region COMMAND METHODS

        private async void CreateCompany()
        {
            if (string.IsNullOrEmpty(this.TextCompanyName))
            {
                BCCompanyName = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a company name", "Ok");
                BCCompanyName = ColorsFonts.BackGround;
                return;
            }

            if (string.IsNullOrEmpty(this.TextContactMethods))
            {
                BCContactMethods = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a contact method", "Ok");
                BCContactMethods = ColorsFonts.BackGround;
                return;
            }

            if (string.IsNullOrEmpty(this.TextAddress))
            {
                BCAddress = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter an address", "Ok");
                BCAddress = ColorsFonts.BackGround;
                return;
            }

            if (string.IsNullOrEmpty(this.TextServiceHours))
            {
                BCServiceHours = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter service hours", "Ok");
                BCServiceHours = ColorsFonts.BackGround;
                return;
            }

            this.TextCompanyName = string.Empty;
            this.TextContactMethods = string.Empty;
            this.TextAddress = string.Empty;
            this.TextServiceHours = string.Empty;

            MainViewModel.getInstance().TabbedHome = new TabbedHomeViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new TabbedHomePage());
        }

        #endregion
    }
}
