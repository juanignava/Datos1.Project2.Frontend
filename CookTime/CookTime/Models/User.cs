


namespace CookTime.Models
{
    using System.Collections.Generic;
    using CookTime.FileHelpers;
    using Newtonsoft.Json;

    /*
     * This class is the model of an user, it has all its characteristics
     */
    public class User
    {
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

    }
}
