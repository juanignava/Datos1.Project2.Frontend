using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Pyecto2Datos1Fontend.ConstantModels
{
    class Company
    {
        public String Name { get; set; }
        public Image Logo { get; set; }
        public String ContactMethod { get; set; }
        public String Schedule { get; set; }
        public String Location { get; set; } //This has to be changed (not a string)

        public Company() { }
        public Company(String name, Image logo, String contactMethod, String schedule, String location)
        {
            this.Name = name;
            this.Logo = logo;
            this.ContactMethod = contactMethod;
            this.Schedule = schedule;
            this.Location = location;
        }
    }
}
