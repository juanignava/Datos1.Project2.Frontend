using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Input;
using CookTime.FileHelpers;
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

        //OBSERVABLE COLLECTION
        private ObservableCollection<Notification> notifications;

        //LIST
        private List<Notification> notificationsList;

        #endregion


        #region PROPERTIES

        //TEXT
        public string TextNotification
        {
            get { return this.textNotification; }
            set { SetValue(ref this.textNotification, value); }
        }

        //OBSERVABLE COLLECTION
        public ObservableCollection<Notification> Notifications
        {
            get { return this.notifications; }
            set { SetValue(ref this.notifications, value); }
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
                try
                {
                    string controller = $"/users/get_notif?observerUser={loggedUser.Email}";

                    Response notificationResponse = await ApiService.GetList<Notification>(
                        "http://localhost:8080/CookTime.BackEnd",
                        "/api",
                        controller);

                    if (!notificationResponse.IsSuccess)
                    {
                        break;
                    }

                    this.notificationsList = (List<Notification>)notificationResponse.Result;

                    ChangeStringSpaces();

                    this.Notifications = new ObservableCollection<Notification>(this.notificationsList);
                }
                catch (System.Reflection.TargetInvocationException)
                {
                    break;
                }
                

            }
            RefreshNotifications();

        }

        public void ChangeStringSpaces()
        {
            foreach (Notification notification in this.notificationsList)
            {
                notification.MessageType = ReadStringConverter.ChangeGetString(notification.MessageType);
            }
        }
        #endregion
    }
}
