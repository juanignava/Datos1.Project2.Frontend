using P2Front.Constants;
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

            NavigationPage.SetHasNavigationBar(this, false);
        }

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

        public void SignUpProcedure(object sender, EventArgs e)
        {
            User user = new User(entry_email.Text, entry_password.Text);
            if (user.CheckLogInInformation())
            {
                DisplayAlert("Login", "Sign up Successful", "Ok");
            }
            else
            {
                DisplayAlert("Login", "Sign up incorrect, something is missing", "Ok");
            }
        }
        
        async void backToLogin (object source, EventArgs e)
        {
            await Navigation.PopAsync(true);

        }
        
    }
}