using P2Front;
using P2Front.Constants;
using P2Front.TabbedClasses;
using P2Front.Views;
using Pyecto2Datos1Fontend.ConstantModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pyecto2Datos1Fontend.ViewsModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogIn : ContentPage
    {
        

        public LogIn()
        {
           
            InitializeComponent(); //Initialize the UI components defined in teh Xaml file
            Init();

            NavigationPage.SetHasBackButton(this, false);

            NavigationPage.SetHasNavigationBar(this, false);
        }

        void Init()
        {
            BackgroundColor = ColorsFonts.BackgroundColor;
            entry_email.PlaceholderColor = ColorsFonts.goldColor;
            entry_email.TextColor = ColorsFonts.goldColor;
            entry_email.FontFamily = ColorsFonts.contentFont;
            entry_password.PlaceholderColor = ColorsFonts.goldColor;
            entry_password.TextColor = ColorsFonts.goldColor;
            entry_password.FontFamily = ColorsFonts.contentFont;

            entry_email.Completed += (s, e) => entry_password.Focus();
            entry_password.Completed += (s, e) => SignInProcedure(s, e);



        }

        async void SignInProcedure(object sender, EventArgs e)
        {
            User user = new User(entry_email.Text, entry_password.Text);
            if (user.CheckLogInInformation())
            {
                await Navigation.PushAsync(new TabbedHomePage());
            }
            else
            {
                await DisplayAlert("Login", "Login not correct; empty username or password", "Ok");
            }

        }

        async void newAccountProcedure(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp(), true);
        }

        async void newCompanyProcedure(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CompanySignUp(), true);
        }
    }
}