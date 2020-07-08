using P2Front.Constants;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace P2Front.TabbedClasses
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddRecipePage : ContentPage
    {
        public AddRecipePage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            BackgroundColor = ColorsFonts.BackgroundColor;
            entry_recipeName.TextColor = ColorsFonts.goldColor;
            entry_recipeName.PlaceholderColor = ColorsFonts.goldColor;
            entry_recipeName.FontFamily = ColorsFonts.contentFont;

            entry_author.TextColor = ColorsFonts.goldColor;
            entry_author.PlaceholderColor = ColorsFonts.goldColor;
            entry_author.FontFamily = ColorsFonts.contentFont;

            entry_amountOfPortions.TextColor = ColorsFonts.goldColor;
            entry_amountOfPortions.PlaceholderColor = ColorsFonts.goldColor;
            entry_amountOfPortions.FontFamily = ColorsFonts.contentFont;

            editor_ingridients.TextColor = ColorsFonts.goldColor;
            editor_ingridients.PlaceholderColor = ColorsFonts.goldColor;
            editor_ingridients.FontFamily = ColorsFonts.contentFont;

            editor_instructions.TextColor = ColorsFonts.goldColor;
            editor_instructions.PlaceholderColor = ColorsFonts.goldColor;
            editor_instructions.FontFamily = ColorsFonts.contentFont;

            editor_diet.TextColor = ColorsFonts.goldColor;
            editor_diet.PlaceholderColor = ColorsFonts.goldColor;
            editor_diet.FontFamily = ColorsFonts.contentFont;

            entry_price.TextColor = ColorsFonts.goldColor;
            entry_price.PlaceholderColor = ColorsFonts.goldColor;
            entry_price.FontFamily = ColorsFonts.contentFont;

            button_dishType.BackgroundColor = ColorsFonts.wineColor;
            button_difficulty.BackgroundColor = ColorsFonts.wineColor;
            button_preparingTime.BackgroundColor = ColorsFonts.wineColor;

            button_selectImage.BackgroundColor = ColorsFonts.wineColor;
            button_selectImage.BackgroundColor = ColorsFonts.wineColor;
            button_selectImage.BackgroundColor = ColorsFonts.wineColor;


        }

        async void selectDifficulty (object sender, EventArgs e)
        {
            String difficulty = await DisplayActionSheet ("Selec difficulty level", "Cancel", null, "Easy", "Meduim", "Hard");
            if ( difficulty != "Cancel") 
                (sender as Button).Text = "Difficulty: " + difficulty;
        }

        async void selectPreparingTime(object sender, EventArgs e)
        {
            String preparingTime = await DisplayActionSheet("Select Preparing Time", "Cancel", null, "Less than 1 hour", "1 to 3 hours", "3 hours to 1 day", "More than 1 day");
            if (preparingTime != "Cancel")
                (sender as Button).Text = "Preparing Time: " + preparingTime;
        }

        async void selectDishType(object sender, EventArgs e)
        {
            String dishType = await DisplayActionSheet("Select dish type", "Cancel", null, "Breakfast", "Lunch", "Dinner", "Brunch", "Snack");
            if (dishType != "Cancel")
                (sender as Button).Text = "Dish Type: " + dishType;
        }

        async void selectImageFromGallery(object sender, EventArgs e)
        {
            //!Added using Plugin.Media
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Not supported", "Your device doesn´t currently support this functionality", "Ok");
                return;
            }
            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };

            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);
            if (selectedImage == null)
            {
                await DisplayAlert("Error", "Could not get the image, please try again", "Ok");
                return;
            }

            selectedImage.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());
        }

        async void publishRecipe (object source, EventArgs e)
        {
            bool publishRecipe = await DisplayAlert("Publish recipe", "Do you want you to publish this recipe ?", "Yes", "No");
        }
    }
}