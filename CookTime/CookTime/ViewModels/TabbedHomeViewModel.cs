
using CookTime.Models;

namespace CookTime.ViewModels
{
    public class TabbedHomeViewModel
    {
        #region ATTRIBUTES

        private static User loggedUser;

        #endregion


        #region CONSTRUCTOR

        public TabbedHomeViewModel(User user)
        {
            loggedUser = user;
            InstanceTabbedPages();
        }

        #endregion


        #region METHODS

        private void InstanceTabbedPages()
        {
            MainViewModel.getInstance().NewsFeed = new NewsFeedViewModel();
            MainViewModel.getInstance().Search = new SearchViewModel();
            MainViewModel.getInstance().AddRecipe = new AddRecipeViewModel();
            MainViewModel.getInstance().Notification = new NotificationViewModel();
            MainViewModel.getInstance().MyMenu = new MyMenuViewModel();

        }

        public static User getUserInstance()
        {
            return loggedUser;
        }

        #endregion
    }
}
