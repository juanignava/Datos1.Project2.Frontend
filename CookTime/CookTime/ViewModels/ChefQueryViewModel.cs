using System;
using System.Windows.Input;
using CookTime.Constants;
using CookTime.FileHelpers;
using CookTime.Models;
using CookTime.Services;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace CookTime.ViewModels
{
    public class ChefQueryViewModel: BaseViewModel
    {
        #region ATTRIBUTES

        //TEXT
        private string textChef;

        //BACKGROUND COLOR
        private string bCChef;

        //BOOLEAN
        private bool isRunning;

        private bool isChecked;

        //OTHER
        private User loggedUser;

        #endregion


        #region PROPERTIES

        //TEXT
        public string TextChef
        {
            get { return this.textChef; }
            set
            {
                SetValue(ref this.textChef, value);
                this.BCChef = ColorsFonts.backGround;
            }
        }

        //BACKGROUND COLOR
        public string BCChef
        {
            get { return this.bCChef; }
            set { SetValue(ref this.bCChef, value); }
        }

        //BOOLEAN

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsChecked
        {
            get { return this.isChecked; }
            set { SetValue(ref this.isChecked, value); }
        }

        //COMMAND
        public ICommand SendQueryCommand
        {
            get { return new RelayCommand(SendQuery); }
        }

        #endregion

        #region CONSTRUCTOR

        public ChefQueryViewModel()
        {
            this.loggedUser = TabbedHomeViewModel.getUserInstance();
        }
        #endregion


        #region COMMAND METHODS

        private async void SendQuery()
        {
            this.IsRunning = true;

            if (string.IsNullOrEmpty(this.TextChef))
            {
                this.IsRunning = false;
                this.BCChef = ColorsFonts.errorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter an explanation", "Ok");
                return;
            }

            if (!this.IsChecked)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert("Error", "You must accept", "Ok");
                return;
            }

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

            string text = ReadStringConverter.ChangePostString(this.TextChef);

            ChefQuery chefQuery = new ChefQuery
            {
                User = this.loggedUser.Email,
                Text = text
            };

            var responseImage = await ApiService.Post<ChefQuery>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                "/users/chef_request",
                chefQuery,
                false);

            if (!responseImage.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    responseImage.Message,
                    "Accept");
                return;
            }

            this.IsRunning = false;

        }

        #endregion
    }
}
