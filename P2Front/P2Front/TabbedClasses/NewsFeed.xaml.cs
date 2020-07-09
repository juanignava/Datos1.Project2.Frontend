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
    public partial class NewsFeed : ContentPage
    {
        public NewsFeed()
        {
            InitializeComponent();
            BackgroundColor = P2Front.Constants.ColorsFonts.BackgroundColor;
        }

        //ToDo: A method that loads the list of recipes in date order from the server (most recent first)
    }
}