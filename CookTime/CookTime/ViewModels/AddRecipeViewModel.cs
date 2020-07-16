using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;
using CookTime.Constants;
using CookTime.FileHelpers;
using CookTime.Models;
using CookTime.Services;
using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace CookTime.ViewModels
{
    public class AddRecipeViewModel: BaseViewModel
    {

        #region ATTRIBUTES

        //ACTUAL USER

        private User actualUser;

        //TEXT
        private string textRecipeName;

        private string textAuthor;

        private string textDishType;

        private string textDishTime;

        private string textIngredients;

        private string textInstructions;

        private string textTags;

        private string textPrice;

        //BACKGROUND COLOR
        private string bCRecipeName;

        private string bCAuthor;

        private string bCIngredients;

        private string bCInstructions;

        private string bCTags;

        private string bCPrice;

        //VALUE
        private int durationHourValue;

        private int durationMinutesValue;

        private string preparationTime;

        private int portionsValue;

        private int difficultyValue;

        //IMAGESOURCE

        private ImageSource addImageSource;

        //MEDIAFILE
        private MediaFile file;

        //IMAGE BYTE ARRAY
        private Byte[] imageByteArray;

        #endregion


        #region PROPERTIES

        //TEXT
        public string TextRecipeName
        {
            get { return this.textRecipeName; }
            set { SetValue(ref this.textRecipeName, value); }
        }

        public string TextAuthor
        {
            get { return this.textAuthor; }
            set { SetValue(ref this.textAuthor, value); }
        }

        public string TextDishType
        {
            get { return this.textDishType; }
            set { SetValue(ref this.textDishType, value); }
        }

        public string TextDishTime
        {
            get { return this.textDishTime; }
            set { SetValue(ref this.textDishTime, value); }
        }

        public string TextIngredients
        {
            get { return this.textIngredients; }
            set { SetValue(ref this.textIngredients, value); }
        }

        public string TextInstructions
        {
            get { return this.textInstructions; }
            set { SetValue(ref this.textInstructions, value); }
        }

        public string TextTags
        {
            get { return this.textTags; }
            set { SetValue(ref this.textTags, value); }
        }

        public string TextPrice
        {
            get { return this.textPrice; }
            set { SetValue(ref this.textPrice, value); }
        }

        //BACKGROUND COLOR
        public string BCRecipeName
        {
            get { return this.bCRecipeName; }
            set { SetValue(ref this.bCRecipeName, value); }
        }

        public string BCAuthor
        {
            get { return this.bCAuthor; }
            set { SetValue(ref this.bCAuthor, value); }
        }

        public string BCIngredients
        {
            get { return this.bCIngredients; }
            set { SetValue(ref this.bCIngredients, value); }
        }

        public string BCInstructions
        {
            get { return this.bCInstructions; }
            set { SetValue(ref this.bCInstructions, value); }
        }

        public string BCTags
        {
            get { return this.bCTags; }
            set { SetValue(ref this.bCTags, value); }
        }

        public string BCPrice
        {
            get { return this.bCPrice; }
            set { SetValue(ref this.bCPrice, value); }
        }

        //VALUE
        public int DurationHourValue
        {
            get { return this.durationHourValue; }
            set { SetValue(ref this.durationHourValue, value); }
        }

        public int DurationMinutesValue
        {
            get { return this.durationMinutesValue; }
            set { SetValue(ref this.durationMinutesValue, value); }
        }

        public int PortionsValue
        {
            get { return this.portionsValue; }
            set { SetValue(ref this.portionsValue, value); }
        }

        public int DifficultyValue
        {
            get { return this.difficultyValue; }
            set { SetValue(ref this.difficultyValue, value); }
        }

        //COMMAND
        public ICommand DurationHourCommand
        {
            get { return new RelayCommand(DurationHour); }
        }

        public ICommand DurationMinutesCommand
        {
            get { return new RelayCommand(DurationMinutes); }
        }

        public ICommand DishTypeCommand
        {
            get { return new RelayCommand(DishType); }
        }

        public ICommand DishTimeCommand
        {
            get { return new RelayCommand(DishTime); }
        }

        public ICommand AddImageCommand
        {
            get { return new RelayCommand(AddImage); }
        }

        public ICommand ShareCommand
        {
            get { return new RelayCommand(Share); }
        }

        //IMAGESOURCE

        public ImageSource AddImageSource
        {
            get { return this.addImageSource; }
            set { SetValue(ref this.addImageSource, value); }
        }

        #endregion


        #region CONSTRUCTOR

        public AddRecipeViewModel(User actualUser)
        {
            this.actualUser = actualUser;

            this.DurationHourValue = 0;
            this.DurationMinutesValue = 10;
            this.TextDishType = "Breakfast";
            this.PortionsValue = 1;
            this.TextDishTime = "Appetizer";
            this.DifficultyValue = 3;
            this.AddImageSource = "AddImageIcon";

            this.TextAuthor = "Author: " + actualUser.Email;
        }

        #endregion


        #region COMMAND METHODS

        private async void DurationHour()
        {
            int DurationHourValueCopy = DurationHourValue;
            try
            {
                DurationHourValue = int.Parse(await Application.Current.MainPage.DisplayActionSheet("Hour", "Cancel", null, "0", "1", "2", "3", "4", "5",
                                                                                                    "6", "7", "8", "9", "10", "11", "12"));
            }
            catch (FormatException)
            {
                DurationHourValue = DurationHourValueCopy;
            }
            catch (ArgumentNullException)
            {
                DurationHourValue = DurationHourValueCopy;
            }

        }

        private async void DurationMinutes()
        {
            int DurationMinutesValueCopy = DurationMinutesValue;

            try
            {
                DurationMinutesValue = int.Parse(await Application.Current.MainPage.DisplayActionSheet("Minutes", "Cancel", null, "0", "10", "20", "30", "40", "50", "60"));
            }
            catch (FormatException)
            {
                DurationMinutesValue = DurationMinutesValueCopy;
            }
            catch (ArgumentNullException)
            {
                DurationMinutesValue = DurationMinutesValueCopy;
            }

        }

        private async void DishType()
        {
            string textDishTypeCopy = TextDishType;

            TextDishType = await Application.Current.MainPage.DisplayActionSheet("Dish type", "Cancel", null, "Breakfast", "Brunch", "Lunch", "Snack", "Dinner");

            if (string.IsNullOrEmpty(TextDishType) || TextDishType.Equals("Cancel")) TextDishType = textDishTypeCopy;

        }

        private async void DishTime()
        {
            string textDishTimeCopy = TextDishTime;

            TextDishTime = await Application.Current.MainPage.DisplayActionSheet("Dish time", "Cancel", null, "Appetizer", "Entry", "Main Course", "Alcoholic drinks",
                                                                                "Cold drinks", "Hot drinks", "Dessert");

            if (string.IsNullOrEmpty(TextDishTime) || TextDishTime.Equals("Cancel")) TextDishTime = textDishTimeCopy;

        }

        private async void AddImage()
        {
            await CrossMedia.Current.Initialize();

            string source = await Application.Current.MainPage.DisplayActionSheet("Image source", "Cancel", null, "Take picture", "Open Gallery");

            if (string.IsNullOrEmpty(source) || source.Equals("Cancel"))
            {
                this.file = null;
                return;
            }

            if (source.Equals("Take picture"))
            {
                this.file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg",
                    PhotoSize = PhotoSize.Medium,
                }
                );
            }

            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (this.file != null)
            {
                this.AddImageSource = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });

                this.imageByteArray = FileHelper.ReadFully(this.file.GetStream());
            }

        }

        private async void Share()
        {
            if (string.IsNullOrEmpty(this.TextRecipeName))
            {
                BCRecipeName = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter the recipe name", "Ok");
                BCRecipeName = ColorsFonts.BackGround;
                return;
            }

            if (string.IsNullOrEmpty(this.TextAuthor))
            {
                BCAuthor = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter the author(s)", "Ok");
                BCAuthor = ColorsFonts.BackGround;
                return;
            }

            if (string.IsNullOrEmpty(this.TextIngredients))
            {
                BCIngredients = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter the ingredients", "Ok");
                BCIngredients = ColorsFonts.BackGround;
                return;
            }

            if (string.IsNullOrEmpty(this.TextInstructions))
            {
                BCInstructions = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter the instructions", "Ok");
                BCInstructions = ColorsFonts.BackGround;
                return;
            }

            if (string.IsNullOrEmpty(this.TextTags))
            {
                BCTags = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter some tags", "Ok");
                BCTags = ColorsFonts.BackGround;
                return;
            }

            if (string.IsNullOrEmpty(this.TextPrice))
            {
                BCPrice = ColorsFonts.ErrorColor;
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter the price", "Ok");
                BCPrice = ColorsFonts.BackGround;
                return;
            }

            var checkConnection = await ApiService.CheckConnection();
            //Checks the internet connection before interacting with the server
            if (!checkConnection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Check your internet connection",
                    "Accept");
                return;
            }

            //Changes the spacing on every string that is being uploaded

            this.TextRecipeName = ReadStringConverter.ChangePostString(this.TextRecipeName);
            this.preparationTime = this.DurationHourValue.ToString() +
                " h and" + this.DurationMinutesValue + " min";
            this.preparationTime = ReadStringConverter.ChangePostString(this.preparationTime);
            this.TextDishTime = ReadStringConverter.ChangePostString(this.TextDishTime);
            this.TextIngredients = ReadStringConverter.ChangePostString(this.TextIngredients);
            this.TextInstructions = ReadStringConverter.ChangePostString(this.TextInstructions);
            this.TextTags = ReadStringConverter.ChangePostString(this.TextTags);

            //Creates the recipe with the information given
   

            //string arrayConverted = Convert.ToBase64String(this.imageByteArray);

            //string arrayConvertedPost = ReadStringConverter.Base64toString(arrayConverted);

          //  ArrayConverted = Convert.ToBase64String () this.imageByteArray.

        

            var recipe = new Recipe
            {
                Name = this.TextRecipeName,
                Author = this.actualUser.Email,
                Type = this.TextDishType,
                CookingSpan = this.preparationTime,
                Portions = this.PortionsValue,
                EatingTime = this.TextDishTime,
                Tags = this.TextTags,
                //Image = arrayConvertedPost,
                Ingredients = this.TextIngredients,
                Steps = this.TextInstructions,
                //Comments
                Price = this.TextPrice,
                Difficulty = this.DifficultyValue,

                Punctuation = 0
            };

            //Generates the query url

            var queryUrl = "/recipes?name=" + this.TextRecipeName + "&author=" + this.actualUser.Email + "&type=" + this.TextDishType +
                "&cookingSpan=" + this.preparationTime + "&portions=" + this.PortionsValue + "&eatingTime=" + this.TextDishTime +
                "&tags=" + this.TextTags + "&ingredients=" + this.TextIngredients + "&steps=" + this.TextInstructions + "&price=" +
                this.TextPrice + "&diffiulty=" + this.DifficultyValue + "&punctuation=0";

            //Posts the recipe
            var response = await ApiService.Post<Recipe>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                queryUrl,
                recipe);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }



            // Redifines the addRecipe entries and editors
            this.TextRecipeName = string.Empty;
            this.TextAuthor = string.Empty;
            this.DurationHourValue = 0;
            this.DurationMinutesValue = 10;
            this.TextDishType = "Breakfast";
            this.PortionsValue = 1;
            this.TextDishTime = "Appetizer";
            this.TextIngredients = string.Empty;
            this.TextInstructions = string.Empty;
            this.TextTags = string.Empty;
            this.TextPrice = string.Empty;
            this.DifficultyValue = 3;
            this.AddImageSource = "AddImageIcon";

            await Application.Current.MainPage.DisplayAlert("Alert", "Recipe succesfully posted", "Accept");

        }

        #endregion
    }
}
