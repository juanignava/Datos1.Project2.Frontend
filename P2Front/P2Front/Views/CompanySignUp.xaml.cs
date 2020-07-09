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

            NavigationPage.SetHasNavigationBar(this, false); //Disables the navigation bar
        }

        /*
        * The Init method defines some of the attributes of the components in the XAML file
         */
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

        /*
         * Actions done when the user tabs the create account button
         */
        async void SignUpProcedure(object sender, EventArgs e)
        {
            Company company = new Company(entry_companyName.Text, entry_CompanyContactMethod.Text); //Defines a Company object with the attributes given
            if (company.CheckCompanySignUpInformation()) //Checks if the user completes the information
            {
                await Navigation.PushAsync(new TabbedHomePage());
                //ToDo: Register a new compapny account
            }
            else
            {
                await DisplayAlert("Company sign up", "Sign up incorrect, something is missing", "Ok");
            }
        }

        /*
         * Actions done when the user tabs the go back button
         */
        async void backToSignUp(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true); //Pops this page (the heriarchical navigation works like a stack)
        }
    }
}