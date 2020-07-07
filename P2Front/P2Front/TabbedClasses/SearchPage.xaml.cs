using P2Front.Constants;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace P2Front.TabbedClasses
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            searchBar.BackgroundColor = ColorsFonts.BackgroundColor;
            searchBar.CancelButtonColor = ColorsFonts.wineColor;
            searchBar.PlaceholderColor = ColorsFonts.wineColor;
            searchBar.TextColor = ColorsFonts.wineColor;

            SearchByLabel.TextColor = ColorsFonts.wineColor;
            SearchByLabel.BackgroundColor = ColorsFonts.BackgroundColor;
        }
    }
}