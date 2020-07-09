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

        /*
         * This method defines some of the main characteristics of the objects from the XAML
         */
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

        /*
         * To avoid different answers than the wanted ones the difficulty is chosen from a Display
         */
        async void selectDifficulty (object sender, EventArgs e)
        {
            String difficulty = await DisplayActionSheet ("Selec difficulty level", "Cancel", null, "Easy", "Meduim", "Hard");
            if ( difficulty != "Cancel") 
                (sender as Button).Text = "Difficulty: " + difficulty;
        }

        /*
         * To avoid different answers than the wanted ones the Preparing Time is chosen from a Display
         */
        async void selectPreparingTime(object sender, EventArgs e)
        {
            String preparingTime = await DisplayActionSheet("Select Preparing Time", "Cancel", null, "Less than 1 hour", "1 to 3 hours", "3 hours to 1 day", "More than 1 day");
            if (preparingTime != "Cancel")
                (sender as Button).Text = "Preparing Time: " + preparingTime;
        }

        /*
         * To avoid different answers than the wanted ones the select dish type option is chosen from a Display
         */
        async void selectDishType(object sender, EventArgs e)
        {
            String dishType = await DisplayActionSheet("Select dish type", "Cancel", null, "Breakfast", "Lunch", "Dinner", "Brunch", "Snack");
            if (dishType != "Cancel")
                (sender as Button).Text = "Dish Type: " + dishType;
        }

        /*
         * In this method the image is uploaded from the gallery and changed with the befault image of the recipe (the camera shown)
         */
        async void selectImageFromGallery(object sender, EventArgs e)
        {
            //!Added using Plugin.Media (install the nugget)

            await CrossMedia.Current.Initialize(); //Asks if the gallery access is allowed for this app

            if (!CrossMedia.Current.IsPickPhotoSupported)  //If it is not allowed gives an alert
            {
                await DisplayAlert("Not supported", "Your device doesn´t currently support this functionality", "Ok");
                return;
            }

            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium //The picture size is changed 
            };

            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);
            if (selectedImage == null) //If there´s not selected image then theres a display message
            {
                await DisplayAlert("Error", "Could not get the image, please try again", "Ok");
                return;
            }

            selectedImage.Source = ImageSource.FromStream(() => selectedImageFile.GetStream()); //Loads the selected image into the frame difined in the XAML
        }

        /*
         * Actions done after presing the publish button
         */
        async void publishRecipe (object source, EventArgs e)
        {
            bool publishRecipe = await DisplayAlert("Publish recipe", "Do you want you to publish this recipe ?", "Yes", "No");
        }
    }
}