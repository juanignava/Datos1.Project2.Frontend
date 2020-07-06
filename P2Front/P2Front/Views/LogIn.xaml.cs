using P2Front.Constants;
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
            InitializeComponent();
        }

        void SignInProcedure(object sender, EventArgs e)
        {
            User user = new User(entry_email.Text, entry_password.Text);
            if (user.CheckLogInInformation())
            {
                DisplayAlert("Login", "Login Success", "Ok");
            }
            else
            {
                DisplayAlert("Login", "Login not correct; empty username or password", "Ok");
            }
        }
    }
}