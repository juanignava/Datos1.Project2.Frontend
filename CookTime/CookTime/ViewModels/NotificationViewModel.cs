using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Input;
using CookTime.Models;
using CookTime.Services;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace CookTime.ViewModels
{
    public class NotificationViewModel: BaseViewModel
    {
        #region ATTRIBUTES

        //TEXT
        private string textEmisorUser;

        private string textNewComment;

        private string textRecipeName;

        private string textNotification;

        //VALUE
        private int notifType;

        //OTHER
        private User loggedUser;

        #endregion


        #region PROPERTIES

        //TEXT
        public string TextEmisorUser
        {
            get { return this.textEmisorUser; }
            set { SetValue(ref this.textEmisorUser, value); }
        }

        public string TextNewComment
        {
            get { return this.textNewComment; }
            set { SetValue(ref this.textNewComment, value); }
        }

        public string TextRecipeName
        {
            get { return this.textRecipeName; }
            set { SetValue(ref this.textRecipeName, value); }
        }

        public string TextNotification
        {
            get { return this.textNotification; }
            set { SetValue(ref this.textNotification, value); }
        }

        //VALUE
        public int NotifType
        {
            get { return this.notifType; }
            set { SetValue(ref this.notifType, value); }
        }

        #endregion


        #region CONSTRUCTOR

        public NotificationViewModel()
        {
            this.loggedUser = TabbedHomeViewModel.getUserInstance();

            Thread t = new Thread(RefreshNotifications);
            t.Start();

        }

        #endregion


        #region METHODS

        private async void RefreshNotifications()
        {

            while (true)
            {
                string controller = $"/users/get_notif?observerUser={loggedUser.Email}";

                Response notificationResponse = await ApiService.Get<Notification>(
                    "http://localhost:8080/CookTime.BackEnd",
                    "/api",
                    controller);

                if (!notificationResponse.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        notificationResponse.Message,
                        "Accept");
                    return;
                }

                Notification notification = (Notification)notificationResponse.Result;

                this.TextEmisorUser = notification.EmisorUser;
                this.NotifType = notification.NotifType;

                switch (this.NotifType)
                {
                    case 0:
                        this.TextNotification = "commented a publication";
                        break;

                    case 1:
                        this.TextNotification = "followed you";
                        break;

                    case 2:
                        this.TextNotification = "qualified a recipe";
                        break;
                    case 3:
                        this.TextNotification = "share a recipe";
                        break;
                }

            }

        }
        #endregion
    }
}
