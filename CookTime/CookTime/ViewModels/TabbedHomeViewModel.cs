
using CookTime.Models;

namespace CookTime.ViewModels
{
    public class TabbedHomeViewModel
    {

        public TabbedHomeViewModel(User user)
        {
            InstanceTabbedPages(user);
        }

        private void InstanceTabbedPages(User user)
        {
            MainViewModel.getInstance().NewsFeed = new NewsFeedViewModel();
            MainViewModel.getInstance().Search = new SearchViewModel();
            MainViewModel.getInstance().AddRecipe = new AddRecipeViewModel(user);
            MainViewModel.getInstance().Notification = new NotificationViewModel();
            MainViewModel.getInstance().MyMenu = new MyMenuViewModel(user);

        }
    }
}
