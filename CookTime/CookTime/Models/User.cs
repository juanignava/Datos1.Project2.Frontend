


namespace CookTime.Models
{
    using System;
    using System.IO;
    using Newtonsoft.Json;
    using Xamarin.Forms;

    /*
     * This class is the model of an user, it has all its characteristics
     */
    public class User
    {
        private ImageSource userImage;

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "age")]
        public string Age { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "profilePic")]
        public string ProfilePic { get; set; }

        [JsonProperty(PropertyName = "usersFollowing")]
        public string[] UsersFollowing { get; set; }

        [JsonProperty(PropertyName = "followers")]
        public string[] Followers { get; set; }

        [JsonProperty(PropertyName = "chef")]
        public bool Chef { get; set; }


        //INTERNAL PROPERTIES
        public ImageSource UserImage { get { return ConvertImage(ProfilePic); } set { userImage = value; } }

        #region METHODS

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
        #endregion

    }
}
