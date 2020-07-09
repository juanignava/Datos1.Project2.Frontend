using P2Front.Constants;
using P2Front.TabbedClasses;
using Pyecto2Datos1Fontend.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace P2Front.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        public SignUp()
        {
            InitializeComponent();
            Init();

            NavigationPage.SetHasNavigationBar(this, false); //Disables the navigation bar
        }

        /*
         * The Init method defines some of the attributes of the components in the XAML file
         */
        void Init()
        {
            BackgroundColor = ColorsFonts.BackgroundColor;

            entry_name.PlaceholderColor = ColorsFonts.goldColor;
            entry_name.TextColor = ColorsFonts.goldColor;
            entry_name.FontFamily = ColorsFonts.contentFont;

            entry_age.PlaceholderColor = ColorsFonts.goldColor;
            entry_age.TextColor = ColorsFonts.goldColor;
            entry_age.FontFamily = ColorsFonts.contentFont;

            entry_email.PlaceholderColor = ColorsFonts.goldColor;
            entry_email.TextColor = ColorsFonts.goldColor;
            entry_email.FontFamily = ColorsFonts.contentFont;

            entry_password.PlaceholderColor = ColorsFonts.goldColor;
            entry_password.TextColor = ColorsFonts.goldColor;
            entry_password.FontFamily = ColorsFonts.contentFont;

            entry_name.Completed += (s, e) => entry_age.Focus();
            entry_age.Completed += (s, e) => entry_email.Focus();
            entry_email.Completed += (s, e) => entry_password.Focus();
            entry_password.Completed += (s, e) => SignUpProcedure(s, e);
        }

        /*
         * Actions done when the user tabs the create account button
         */
        async public void SignUpProcedure(object sender, EventArgs e)
        {
            User user = new User(entry_email.Text, entry_password.Text); //Defines an user object with the attributes given
            if (user.CheckLogInInformation()) //Checks if the user completes the information 
            {
                await Navigation.PushAsync(new TabbedHomePage());
                //ToDo: Register the new user in the server
            }
            else
            {
                await DisplayAlert("Login", "Sign up incorrect, something is missing", "Ok");
            }
        }

        /*
         * Actions done when the user tabs the go back button
         */
        async void backToLogin (object source, EventArgs e)
        {
            await Navigation.PopAsync(true); //Pops this page (the heriarchical navigation works like a stack)
        }
        
    }
}