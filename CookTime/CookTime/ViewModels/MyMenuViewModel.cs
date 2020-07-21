using CookTime.FileHelpers;
using CookTime.Models;
using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookTime.ViewModels
{
    public class MyMenuViewModel : BaseViewModel
    {
        #region ATTRIBUTES

        //VALUE
        private int followers;

        private int following;

        //TEXT
        private string name;

        private string email;

        private string age;

        //OTHER
        private User loggedUser;

        private Image profilePic;

        private MediaFile file;

        private ImageSource addImageSource;

        private Byte[] ImageByteArray;

        #endregion


        #region PROPERTIES

        //VALUE
        public int Followers
        {
            get { return this.followers; }
            set { SetValue(ref this.followers, value); }
        }

        public int Following
        {
            get { return this.following; }
            set { SetValue(ref this.following, value); }
        }

        //TEXT
        public string Name 
        {
            get { return name; }
            set { SetValue(ref this.name, value); }
        }

        public string Email 
        {
            get { return  this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string Age 
        {
            get { return this.age; }
            set { SetValue(ref this.age, value); }
        }

        //COMMANDS
        public ICommand CheffQueryCommand 
        {
            get { return new RelayCommand(CheffQuery); }
        }

        public ICommand SortDateCommand 
        {
            get { return new RelayCommand(SortDate); }
        }

        public ICommand SortRateCommand
        {
            get { return new RelayCommand(SortRate); }
        }

        public ICommand SortDifficultyCommand
        {
            get { return new RelayCommand(SortDifficulty); }
        }

        public ICommand ChangePictureCommand
        {
            get { return new RelayCommand(ChangePicture); }
        }

        //IMAGE SOURCE
        public ImageSource AddImageSource
        {
            get { return this.addImageSource; }
            set { SetValue(ref this.addImageSource, value); }
        }
        #endregion


        #region CONSTRUCTOR

        public MyMenuViewModel()
        {
            this.loggedUser = TabbedHomeViewModel.getUserInstance();

            init();
        }
        #endregion


        #region METHODS

        private void init()
        {
            this.Name = ReadStringConverter.ChangeGetString(loggedUser.Name);
            this.Email = loggedUser.Email;
            this.Age = loggedUser.Age;
            this.Followers = loggedUser.Followers;
            this.Following = loggedUser.UsersFollowing.Length;

            if (loggedUser.ProfilePic == null)
            {
                this.AddImageSource = "SignUpIcon";
            }
            //ToDo: Load a predefined profile picture when one user is registered, then load it from here, the image should be saved in AddImageSource
        }

        private void CheffQuery()
        {
            //ToDo: Command that creates the cheff query navigation page
        }

        private void SortDate()
        {
            //ToDo: Command that sorts the menu by date
        }

        private void SortRate()
        {
            //ToDo: Command that sorts the menu by rate
        }

        private void SortDifficulty()
        {
            //ToDo: Command  that sorts the menu by difficulty
        }

        private async void ChangePicture()
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

                this.ImageByteArray = FileHelper.ReadFully(this.file.GetStream());
                Console.WriteLine(this.ImageByteArray);
            }
        }
        #endregion
    }
}
