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
    public partial class myMenuPage : ContentPage
    {
        public myMenuPage()
        {
            InitializeComponent();
        }

        //ToDo:  Load the a picture from the active user and  the ammount of followers from the server

        //ToDo: Load a list with all the  MyMenu recipes (my publicaitons, the shared and commented ones)

        //ToDo: Create a view of the my MyMenu from one user that is not the active user (like when you visit an user profile) this one will be similar to the active user myMenu page

        //ToDo: Create a page in wich the active user can send a chaf request to the administration of the app
    }
}