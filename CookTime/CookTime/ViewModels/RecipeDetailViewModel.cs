
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
