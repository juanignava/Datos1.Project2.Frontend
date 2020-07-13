using System;
using System.Windows.Input;
using CookTime.Constants;
using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace CookTime.ViewModels
{
    public class AddRecipeViewModel: BaseViewModel
    {

        #region ATTRIBUTES

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

        private int portionsValue;

        private int difficultyValue;

        //IMAGESOURCE

        private ImageSource addImageSource;

        //MEDIAFILE
        private MediaFile file;

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

        public AddRecipeViewModel()
        {
            this.DurationHourValue = 0;
            this.DurationMinutesValue = 10;
            this.TextDishType = "Breakfast";
            this.PortionsValue = 1;
            this.TextDishTime = "Appetizer";
            this.DifficultyValue = 3;
            this.AddImageSource = "AddImageIcon";

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
                    PhotoSize = PhotoSize.Small,
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

        }

        #endregion
    }
}
