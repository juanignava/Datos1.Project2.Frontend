using System;
using System.Windows.Input;
using CookTime.Models;
using CookTime.Views;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace CookTime.ViewModels
{
    public class UserItemViewModel : User
    {
        public ICommand SelectUserCommand
        {
            get
            {
                return new RelayCommand(selectUser);
            }
        }

        private async void selectUser()
        {
            MainViewModel.getInstance().UserDetail = new UserDetailViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new UserDetailPage());
        }
    }
}
