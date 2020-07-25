using System;
namespace CookTime.ViewModels
{
    public class MainViewModel
    {
        #region ATTRIBUTES

        private static MainViewModel instance;

        #endregion


        #region VIEWMODELS
        public LoginViewModel Login { get; set; }

        public SignUpViewModel SignUp { get; set; }

        public TabbedHomeViewModel TabbedHome { get; set; }

        public NewsFeedViewModel NewsFeed { get; set; }

        public SearchViewModel Search { get; set; }

        public AddRecipeViewModel AddRecipe { get; set; }

        public NotificationViewModel Notification { get; set; }

        public MyMenuViewModel MyMenu { get; set; }

        public UserDetailViewModel UserDetail { get; set; }

        public RecipeDetailViewModel RecipeDetail { get; set; }

        public ChefQueryViewModel ChefQuery { get; set; }

        public AddAdminViewModel AddAdmin { get; set; }

        #endregion


        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
        }

        //SINGLETON (instance is never null)
        public static MainViewModel getInstance()
        {
            return instance;
        }
    }
}
