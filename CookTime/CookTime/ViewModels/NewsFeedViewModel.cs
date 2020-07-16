using CookTime.Models;
using System;
using System.Collections.ObjectModel;
using CookTime.Services;
using CookTime.Models;
using Xamarin.Forms;
using System.Collections.Generic;

namespace CookTime.ViewModels
{
    public class NewsFeedViewModel : BaseViewModel
    {
        #region ATTRIBUTES
        private ObservableCollection<Recipe> recipes;
        #endregion

        #region PROPERTIES
        public ObservableCollection<Recipe> Recipes
        {
            get { return this.recipes; }
            set { SetValue(ref this.recipes, value); }
        }
        #endregion

        #region CONSTRUCTOR
        public NewsFeedViewModel()
        {
            //this.LoadRecipes();
        }
        #endregion

        #region METHODS
        private async void LoadRecipes()
        {
            var response = await ApiService.GetList<Recipe>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                "/recipes");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
            }

            var list = (List<Recipe>)response.Result;

            this.Recipes = new ObservableCollection<Recipe>(list);
        }
        #endregion
    }
}
