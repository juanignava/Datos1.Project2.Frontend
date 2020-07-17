
using CookTime.Models;
using CookTime.Views;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookTime.ViewModels
{
    public class RecipeDetailViewModel
    {
        
        #region Properties
        //This property will be passed by parameter, due to the connection with the NewsFeed page
        public Recipe Recipe 
        { 
            get;
            set;
        }
        #endregion

        #region CONSTRUCTOR
        public RecipeDetailViewModel(Recipe recipe)
        {
            this.Recipe = recipe;
        } 
        #endregion
    }
}
