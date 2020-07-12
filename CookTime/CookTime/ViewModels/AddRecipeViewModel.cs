using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
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

        //VALUE
        private int durationHourValue;

        private int durationMinutesValue;

        private int portionsValue;

        private int priceValue;

        private int difficultyValue;


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

        public int PriceValue
        {
            get { return this.priceValue; }
            set { SetValue(ref this.priceValue, value); }
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

        #endregion


        #region CONSTRUCTOR

        public AddRecipeViewModel()
        {
            DurationHourValue = 0;
            DurationMinutesValue = 10;
            PortionsValue = 1;
            TextDishType = "Breakfast";
            TextDishTime = "Appetizer";
            DifficultyValue = 3;

        }

        #endregion

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

            TextDishTime = await Application.Current.MainPage.DisplayActionSheet("Dish time", "Cancel", null, "Appetizer", "Entry", "Main Course", "Alcoholic drinks", "Cold drinks", "Hot drinks", "Dessert");

            if (string.IsNullOrEmpty(TextDishTime) || TextDishTime.Equals("Cancel")) TextDishTime = textDishTimeCopy;

        }
    }
}
