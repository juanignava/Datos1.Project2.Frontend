using Pyecto2Datos1Fontend.ConstantModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace P2Front.Constants
{
    /**
     * Defines objects with all the attributes needed in an user (specified in the intructions)
     * Not every attribute is needed to create an user, we will change the constructor
     * when implementing the server classes made in java
     */
    class User
    {
        public String Email { get; set; }
        public String Name { get; set; }
        public int Age { get; set; }
        public String Password { get; set; }
        public String Username { get; set; }
        public Image UserPicture { get; set; }
        public HashSet<Recipe> MyMenu { get; set; } //I'm not sure of this
        public List<String> Following { get; set; }
        public List<String> Followers { get; set; }
        public List<Recipe> SharedRecipes { get; set; }

        public User() { }

        public User(String email, String password)
        {
            if (email == null)
                email = "";
            else if (password == null)
                password = "";
            this.Email = email;
            this.Password = password;
        }

        public User(String email, String name, int Age, String password, String username, Image userPicture,
            HashSet<Recipe> myMenu, List<String> following, List<String> followers, List<Recipe> sharedRecipes)
        {
            this.Email = email;
            this.Name = name;
            this.Age = Age;
            this.Password = password;
            this.Username = username;
            this.UserPicture = userPicture;
            this.MyMenu = myMenu;
            this.Following = following;
            this.Followers = followers;
            this.SharedRecipes = sharedRecipes;
        }

        public bool CheckLogInInformation()
        {
            if (!this.Email.Equals("") && !this.Password.Equals(""))
                return true;
            else
                return false;
        }
    }
}
