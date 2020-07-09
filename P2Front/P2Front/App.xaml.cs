using P2Front.Constants;
using Pyecto2Datos1Fontend.ViewsModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace P2Front
{
    public partial class App : Application
    {
        /*
         * Nuggets needed: Xam.Plugin.Media (5.0.1)
         * Xamarin.Essensials (1.5.3.2)
         * Xamarin.Forms (4.5.0.495)
         */
        public App()
        {
            InitializeComponent();

            //The App begins with the view in the Loginpage
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
