using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace P2Front.TabbedClasses
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedHomePage : TabbedPage
    {
        public TabbedHomePage()
        {
            InitializeComponent();
            
            //Defining the button main characteristics
            BarBackgroundColor = P2Front.Constants.ColorsFonts.BackgroundColor;
            SelectedTabColor = P2Front.Constants.ColorsFonts.goldColor;
            BarTextColor = Color.Black;
            UnselectedTabColor = Color.Black;

            NavigationPage.SetHasBackButton(this, false); //Disables the navigation back button to avoid going back to the login page
            NavigationPage.SetHasNavigationBar(this, false); //Disabled the navigation bar
        }
    }
}