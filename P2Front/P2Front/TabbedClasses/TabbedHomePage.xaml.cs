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
            BarBackgroundColor = P2Front.Constants.ColorsFonts.BackgroundColor;
            SelectedTabColor = P2Front.Constants.ColorsFonts.goldColor;
            BarTextColor = Color.Black;
            UnselectedTabColor = Color.Black;

            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);

        }
    }
}