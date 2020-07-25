using CookTime.Models;
using CookTime.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace CookTime.ViewModels
{
    public class AddAdminViewModel: BaseViewModel
    {
        #region ATTRIBUTES

        private bool isRunning;

        private string textChef;

        private List<string> adminsList;

        private string email;

        #endregion

        #region PROPERTIES

        public bool IsRunning 
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public string TextChef 
        {
            get { return this.textChef; }
            set { SetValue(ref this.textChef, value); }
        }
        #endregion

        #region COMMANDS

        public ICommand AddAdminCommand { get { return new RelayCommand(AddNewAdmin); } }
        #endregion

        #region CONSTRUCTOR

        public AddAdminViewModel(List<string> listAdmins, string email)
        {
            this.adminsList = listAdmins;
            this.email = email;
            this.IsRunning = false;
        }
        #endregion

        #region METHODS

        private async void AddNewAdmin()
        {
            this.IsRunning = true;
            var newAdmin = this.TextChef;
            
            foreach (string admin in this.adminsList)
            {
                
                if (newAdmin == admin)
                {
                    this.IsRunning = false;
                    this.TextChef = string.Empty;
                    await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "This user is already an admin of this company",
                    "Accept");
                    return;
                }
            }

            var connection = await ApiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.TextChef = string.Empty;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Accept");
                return;
            }

            //Creates the url needed to getthe information
            var url = "/companies/" + this.email + "?email=" + newAdmin;

            //Asks the server for the list of recipes
            //Posts the Company
            Response response = await ApiService.Post<Company>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                url,
                null,
                false);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert( //if something goes wrong the page displays a message
                    "Error",
                    "Error uploading the user as an admin",
                    "Accept");
                return;
            }
            this.IsRunning = false;
            await Application.Current.MainPage.DisplayAlert( //if something goes wrong the page displays a message
                    "New Admin",
                    "Admin succesfully added",
                    "Accept");

            await Application.Current.MainPage.Navigation.PopAsync();
        }
        #endregion
    }
}
