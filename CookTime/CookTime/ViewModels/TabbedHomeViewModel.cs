using System;
namespace CookTime.ViewModels
{
    public class TabbedHomeViewModel
    {
        public TabbedHomeViewModel()
        {
            InstanceTabbedPages();
        }

        private void InstanceTabbedPages()
        {
            MainViewModel.getInstance().NewsFeed = new NewsFeedViewModel();
            MainViewModel.getInstance().Search = new SearchViewModel();
            MainViewModel.getInstance().AddRecipe = new AddRecipeViewModel();
            MainViewModel.getInstance().Notification = new NotificationViewModel();
            MainViewModel.getInstance().MyMenu = new MyMenuViewModel();

        }
    }
}
