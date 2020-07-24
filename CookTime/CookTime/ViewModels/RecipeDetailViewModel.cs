
using CookTime.Constants;
using CookTime.FileHelpers;
using CookTime.Models;
using CookTime.Services;
using CookTime.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CookTime.ViewModels
{
    public class RecipeDetailViewModel: BaseViewModel
    {
        #region ATTRIBUTES

        //TEXT
        private string likeSourse;

        private string textComment;

        private string bCComment;

        //VALUE
        private int punctuation;

        //BOOLEAN
        private bool isRefreshingComments;


        //USER
        private User loggedUser;

        // LIST OBJECTS
        private ObservableCollection<Comment> comments;

        private List<Comment> commentsList;


        #endregion


        #region Properties

        //SPECIFIC RECIPE
        public Recipe UserRecipe { get; set; }

        //TEXT
        public string LikeSourse
        {
            get { return this.likeSourse; }
            set { SetValue(ref this.likeSourse, value); }
        }

        public string TextComment
        {
            get { return this.textComment; }
            set
            {
                SetValue(ref this.textComment, value);
                this.BCComment = ColorsFonts.backGround;

            }
        }

        //BACKGROUND COLOR
        public string BCComment
        {
            get { return this.bCComment; }
            set { SetValue(ref this.bCComment, value); }
        }

        //VALUE
        public int Punctuation
        {
            get { return this.punctuation; }
            set { SetValue(ref this.punctuation, value); }
        }

        //BOOLEAN
        public bool IsRefreshingComments
        {
            get { return this.isRefreshingComments; }
            set { SetValue(ref this.isRefreshingComments, value); }
        }

        //LIST OBJECTS
        public ObservableCollection<Comment> Comments
        {
            get { return this.comments; }
            set { SetValue(ref this.comments, value); }
        }

        //COMMAND
        public ICommand LikeCommand
        {
            get { return new RelayCommand(Like); }
        }

        public ICommand RefreshCommentsCommand
        {
            get { return new RelayCommand(loadComments); }
        }

        public ICommand CommentCommand
        {
            get { return new RelayCommand(SendComment); }
        }

        #endregion


        #region CONSTRUCTOR

        public RecipeDetailViewModel(Recipe recipe)
        {
            this.loggedUser = TabbedHomeViewModel.getUserInstance();
            this.UserRecipe = recipe;
            this.Punctuation = UserRecipe.Punctuation;

            this.LikeSourse = loadLikeButton();

            this.loadComments();

        }

        #endregion


        #region COMMAND METHODS

        private string loadLikeButton()
        {
            if (UserRecipe.Likers != null)
            {
                foreach (string item in UserRecipe.Likers)
                {
                    if (item.Equals(this.loggedUser.Email))
                    {
                        return "LikedIcon";
                    }

                }
            }
            return "UnLikedIcon";

        }

        private async void Like()
        {
            if (this.LikeSourse.Equals("UnLikedIcon"))
            {

                Response checkConnection = await ApiService.CheckConnection();
                if (!checkConnection.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Check your internet connection",
                        "Accept");
                    return;
                }

                string recipeName = ReadStringConverter.ChangePostString(this.UserRecipe.Name);

                string queryUrl = $"/users/new_notif?emisorUser={loggedUser.Email}&recieverUser={UserRecipe.Author}&notifType=3&recipe={recipeName}";

                Notification notification = new Notification
                {
                    EmisorUser = loggedUser.Email,
                    RecieverUser = UserRecipe.Author,
                    NotifType = 3,
                    RecipeName = recipeName    
                };

                Response response = await ApiService.Post<Notification>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                queryUrl,
                notification,
                false);

                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Action can't be done",
                        "Ok");
                    return;
                }

                this.LikeSourse = "LikedIcon";
                this.Punctuation += 1;

                string queryUrl2 = $"/users/new_notif?emisorUser={loggedUser.Email}&recieverUser={UserRecipe.Author}&notifType=5&recipe={recipeName}";

                Notification notification2 = new Notification
                {
                    EmisorUser = loggedUser.Email,
                    RecieverUser = UserRecipe.Author,
                    NotifType = 5,
                    RecipeName = recipeName
                };

                Response response2 = await ApiService.Post<Notification>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                queryUrl2,
                notification2,
                false);

                if (!response2.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Action can't be done",
                        "Ok");
                    return;
                }

            }

            else
            {
                Response checkConnection = await ApiService.CheckConnection();
                if (!checkConnection.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Check your internet connection",
                        "Accept");
                    return;
                }

                string recipeName = ReadStringConverter.ChangePostString(this.UserRecipe.Name);

                string queryUrl = $"/users/new_notif?emisorUser={loggedUser.Email}&recieverUser={UserRecipe.Author}&notifType=4&recipe={recipeName}";

                Notification notification = new Notification
                {
                    EmisorUser = loggedUser.Email,
                    RecieverUser = UserRecipe.Author,
                    NotifType = 4,
                    RecipeName = recipeName
                };

                Response response = await ApiService.Post<Notification>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                queryUrl,
                notification,
                false);

                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Action can't be done",
                        "Ok");
                    return;
                }

                this.LikeSourse = "UnLikedIcon";
                this.Punctuation -= 1;

                string queryUrl2 = $"/users/new_notif?emisorUser={loggedUser.Email}&recieverUser={UserRecipe.Author}&notifType=6&recipe={recipeName}";

                Notification notification2 = new Notification
                {
                    EmisorUser = loggedUser.Email,
                    RecieverUser = UserRecipe.Author,
                    NotifType = 6,
                    RecipeName = recipeName
                };

                Response response2 = await ApiService.Post<Notification>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                queryUrl2,
                notification2,
                false);

                if (!response2.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Action can't be done",
                        "Ok");
                    return;
                }
            }

        }

        private async void loadComments()
        {
            this.IsRefreshingComments = true;

            //Checking internet connection before asking for the server
            var connection = await ApiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshingComments = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Check your internet connection",
                    "Accept");
                return;
            }

            var response = await ApiService.GetList<Recipe>(
                "http://localhost:8080/CookTime.BackEnd",
                "/api",
                "/recipes");

            if (!response.IsSuccess)
            {
                this.IsRefreshingComments = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Action can't be done",
                    "Accept");
            }

            this.IsRefreshingComments = false;

            this.commentsList = this.UserRecipe.Comments;

            ChangeStringSpaces();

            await LoadUserProfilePic();

            this.Comments = new ObservableCollection<Comment>(this.commentsList);
        }

        public void ChangeStringSpaces()
        {
            foreach (Comment comment in this.commentsList)
            {
                comment.UserComment = ReadStringConverter.ChangeGetString(comment.UserComment);

            }
        }

        private async Task<Response> LoadUserProfilePic()
        {
            foreach (Comment comment in this.commentsList)
            {
                string controller = $"/users/{comment.User}";
                Response checkEmail = await ApiService.Get<User>(
                    "http://localhost:8080/CookTime.BackEnd",
                    "/api",
                    controller);
                User userX = (User)checkEmail.Result;

                comment.UserImage = userX.UserImage;

            }
            return null;
        }


        private async void SendComment()
        {
            if (string.IsNullOrEmpty(this.TextComment))
            {
                this.BCComment = ColorsFonts.errorColor;
                return;
            }

            Response checkConnection = await ApiService.CheckConnection();
            if (!checkConnection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Check your internet connection",
                    "Accept");
                return;
            }

            string recipeName = ReadStringConverter.ChangePostString(this.UserRecipe.Name);
            string recipeComment = ReadStringConverter.ChangePostString(this.TextComment);

            string queryUrl = $"/users/new_notif?emisorUser={loggedUser.Email}&recieverUser={UserRecipe.Author}&notifType=0&recipe={recipeName}&newComment={recipeComment}";

            Notification notification = new Notification
            {
                EmisorUser = loggedUser.Email,
                RecieverUser = UserRecipe.Author,
                NotifType = 0,
                RecipeName = recipeName,
                NewComment = recipeComment
            };

            Response response = await ApiService.Post<Notification>(
            "http://localhost:8080/CookTime.BackEnd",
            "/api",
            queryUrl,
            notification,
            false);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Action can't be done",
                    "Ok");
                return;
            }

            this.loadComments();
        }

        #endregion
    }
}
