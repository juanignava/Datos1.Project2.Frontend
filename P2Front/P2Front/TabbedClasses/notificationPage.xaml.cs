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
    public partial class notificationPage : ContentPage
    {
        public notificationPage()
        {
            InitializeComponent();
        }

        //ToDo: Load a list of notification of the active user from the server
    }
}