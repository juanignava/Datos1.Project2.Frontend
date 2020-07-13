


namespace CookTime.Models
{
    using Newtonsoft.Json;

    /*
     * This class is the model of an user, it has all its characteristics
     */
    class User
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
        public object ProfilePic { get; set; }

        [JsonProperty(PropertyName = "usersFollowing")]
        public object UsersFollowing { get; set; }

        [JsonProperty(PropertyName = "followers")]
        public object Followers { get; set; }

        [JsonProperty(PropertyName = "profile")]
        public object Profile { get; set; }
    }
}
