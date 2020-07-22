


namespace CookTime.Models
{
    using CookTime.FileHelpers;
    using Newtonsoft.Json;

    /*
     * This class is the model of an user, it has all its characteristics
     */
    public class User
    {
        #region ATTRIBUTES

        private string name;

        #endregion

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get { return this.name; } set { this.name = ChangeStringSpaces(value); } }

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


        #region METHODS

        public string ChangeStringSpaces(string property)
        {
            return ReadStringConverter.ChangeGetString(property);
        }

        #endregion
    }
}
