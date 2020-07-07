using P2Front.Constants;
using Pyecto2Datos1Fontend.ViewsModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace P2Front
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LogIn());
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
