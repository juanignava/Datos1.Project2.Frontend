using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace CookTime.Models
{
    public class Company
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "contact")]
        public string Contact { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "image")]
        public string Image { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "serviceSchedule")]
        public string ServiceSchedule { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "location")]
        public double[] Location { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "punctuation")]
        public int Punctuation { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "sortingType")]
        public int SortingType { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "admins")]
        public List<string> Admins { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "usersFollowing")]
        public List<string> UsersFollowing { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "followers")]
        public List<string> Followers { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "likers")]
        public List<string> Likers { get; set; }

        //[Newtonsoft.Json.JsonProperty(PropertyName = "recipes")]
        //public List<object> Recipes { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "notifications")]
        public List<object> Notifications { get; set; }

        //INTERNAL PROPERTIES
        public ImageSource CompanyImage { get { return ConvertImage(Image); } set { CompanyImage = value; } }

        public ImageSource ConvertImage(string imageValue)
        {

            ImageSource retSource = "SignUpIcon";

            if (!string.IsNullOrEmpty(imageValue))
            {
                try
                {
                    byte[] imageAsBytes = Convert.FromBase64String(imageValue);
                    retSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
                }
                catch (Exception)
                {
                    return retSource;
                }

            }
            return retSource;
        }
    }
}
