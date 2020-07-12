using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace CookTime.ViewModels
{
    public class SearchViewModel: BaseViewModel
    {
        #region ATTRIBUTES

        //TEXT
        private string searchText;

        #endregion


        #region PROPERITES

        //TEXT
        public string SearchText
        {
            get { return this.searchText; }
            set { SetValue(ref this.searchText, value); }
        }

        //COMMAND
        public ICommand SearchCommand
        {
            get { return new RelayCommand(Search); }
        }


        public ICommand FilterCommand
        {
            get { return new RelayCommand(Filter); }
        }


        #endregion


        #region METHODS

        private async void Filter()
        {
            string filter = await Application.Current.MainPage.DisplayActionSheet("Filter", "Cancel", null, "All", "Companies", "Recipes", "Users");

            switch (filter)
            {
                //Todo: Hacer que se apliquen correctamente los filtros

                case "All":
                    await Application.Current.MainPage.DisplayAlert(":)", "Elegiste all", "Ok");
                    break;

                case "Companies":
                    await Application.Current.MainPage.DisplayAlert(":)", "Elegiste companies", "Ok");
                    break;

                case "Recipes":
                    await Application.Current.MainPage.DisplayAlert(":)", "Elegiste recipes", "Ok");
                    break;

                case "Users":
                    await Application.Current.MainPage.DisplayAlert(":)", "Elegiste users", "Ok");
                    break;

            }
        }

        private async void Search()
        {
            //ToDo: Buscar correctamente los solicitado
            await Application.Current.MainPage.DisplayAlert(":)", $"Estas buscando {SearchText}", "Ok");
        }
        #endregion
    }
}
