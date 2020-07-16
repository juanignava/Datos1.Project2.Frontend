
namespace CookTime.ViewModels
{
    using CookTime.Models;
    using CookTime.Views;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class RecipeItemViewModel : Recipe
    {
        public ICommand SelectRecipeCommand
        {
            get
            {
                return new RelayCommand(selectRecipe);
            }
        }

        private async void selectRecipe()
        {
            MainViewModel.getInstance().RecipeDetail = new RecipeDetailViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new RecipeDetailPage());
        }
    }
}
