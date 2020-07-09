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
        /*
         *The Init method defines some of the attributes of the components in the XAML file
         */
        void Init()
        {
            BackgroundColor = ColorsFonts.BackgroundColor;
            entry_email.PlaceholderColor = ColorsFonts.goldColor;
            entry_email.TextColor = ColorsFonts.goldColor;
            entry_email.FontFamily = ColorsFonts.contentFont;
            entry_password.PlaceholderColor = ColorsFonts.goldColor;
            entry_password.TextColor = ColorsFonts.goldColor;
            entry_password.FontFamily = ColorsFonts.contentFont;

            //With the following code the user can automaticaly change entry everytime he finished the previous one
            entry_email.Completed += (s, e) => entry_password.Focus();
            entry_password.Completed += (s, e) => SignInProcedure(s, e);
        }

        /*
         * Actions done when the user tabs the login button
         */
        async void SignInProcedure(object sender, EventArgs e)
        {
            User user = new User(entry_email.Text, entry_password.Text); //An user is created (from the User class) based in what's written on the entrys
            if (user.CheckLogInInformation()) //This method checks if the information provided is correct (on the User class)
            {
                await Navigation.PushAsync(new TabbedHomePage());
                //ToDo: Search in the server if the user is already registered
            }
            else
            {
                await DisplayAlert("Login", "Login not correct; empty username or password", "Ok");
            }

        }

        /*
         * Actions done when the user tabs the create new account button
         */
        async void newAccountProcedure(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp(), true);
        }

        /*
         * Actions done when the user tabs the create new company button
         */
        async void newCompanyProcedure(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CompanySignUp(), true);
        }
    }
}