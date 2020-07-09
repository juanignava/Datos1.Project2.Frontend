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

        /*
         * This method defines some of the main characteristics of the objects from the XAML
         */
        void Init()
        {
            BackgroundColor = ColorsFonts.BackgroundColor;
            searchBar.BackgroundColor = ColorsFonts.BackgroundColor;
            searchBar.CancelButtonColor = ColorsFonts.wineColor;
            searchBar.PlaceholderColor = ColorsFonts.wineColor;
            searchBar.TextColor = ColorsFonts.wineColor;

            SearchByLabel.TextColor = ColorsFonts.wineColor;
            SearchByLabel.BackgroundColor = ColorsFonts.BackgroundColor;
        }
        //ToDo: a method that allows the search bar to search in the list that is shown

        //ToDo: Load a recomendations list to show some predefined list in the serach view

        //ToDo: Load  a diferent list of users if the users button is pressed

        //ToDo: Load a different list of recipes is the users button is pressed

        //ToDo: load a different list of companies if the users button is pressed
    }
}