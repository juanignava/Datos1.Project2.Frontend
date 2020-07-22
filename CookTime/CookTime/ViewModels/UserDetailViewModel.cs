using System;
using CookTime.Models;

namespace CookTime.ViewModels
{
    public class UserDetailViewModel : BaseViewModel
    {
        #region ATTRIBUTES

        //VALUE
        private int following;

        private int followers;

        #endregion


        #region Properties

        //This property will be passed by parameter, due to the connection with the NewsFeed page
        public User User
        {
            get;
            set;
        }

        //VALUE
        public int Following
        {
            get { return this.following; }
            set { SetValue(ref this.following, value); }
        }

        public int Followers
        {
            get { return this.followers; }
            set { SetValue(ref this.followers, value); }
        }

        #endregion


        #region CONSTRUCTOR

        public UserDetailViewModel(User user)
        {
            this.User = user;
            this.Following = User.UsersFollowing.Length;
            this.Followers = User.Followers.Length;
        }

        #endregion

        
    }
}
