using P2Front.Constants;
using P2Front.TabbedClasses;
using Pyecto2Datos1Fontend.ConstantModels;
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
    public partial class CompanySignUp : ContentPage
    {
        public CompanySignUp()
        {
            InitializeComponent();
            Init();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        void Init()
        {
            BackgroundColor = ColorsFonts.BackgroundColor;

            entry_companyName.PlaceholderColor = ColorsFonts.goldColor;
            entry_companyName.TextColor = ColorsFonts.goldColor;
            entry_companyName.FontFamily = ColorsFonts.contentFont;

            entry_CompanyContactMethod.PlaceholderColor = ColorsFonts.goldColor;
            entry_CompanyContactMethod.TextColor = ColorsFonts.goldColor;
            entry_CompanyContactMethod.FontFamily = ColorsFonts.contentFont;

            entry_serviceHours.PlaceholderColor = ColorsFonts.goldColor;
            entry_serviceHours.TextColor = ColorsFonts.goldColor;
            entry_serviceHours.FontFamily = ColorsFonts.contentFont;

            entry_companyName.Completed += (s, e) => entry_CompanyContactMethod.Focus();
            entry_CompanyContactMethod.Completed += (s, e) => entry_serviceHours.Focus();
            entry_serviceHours.Completed += (s, e) => SignUpProcedure(s, e);

        }

        async void SignUpProcedure(object sender, EventArgs e)
        {
            Company company = new Company(entry_companyName.Text, entry_CompanyContactMethod.Text);
            if (company.CheckCompanySignUpInformation())
            {
                await Navigation.PushAsync(new TabbedHomePage());
            }
            else
            {
                DisplayAlert("Company sign up", "Sign up incorrect, something is missing", "Ok");
            }
        }

        async void backToSignUp(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }
    }
}