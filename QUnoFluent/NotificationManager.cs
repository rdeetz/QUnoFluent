// <copyright file="NotificationManager.cs" company="Mooville">
//   Copyright © 2025 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Fluent
{
    using System;
    using System.Diagnostics;
    using Microsoft.Windows.AppNotifications;
    using Microsoft.Windows.AppNotifications.Builder;

    internal class NotificationManager
    {
        private bool isRegistered;

        public NotificationManager()
        {
            this.isRegistered = false;
        }

        ~NotificationManager()
        {
            this.Unregister();
        }

        public void Register()
        {
            AppNotificationManager.Default.NotificationInvoked += this.OnNotificationInvoked;
            AppNotificationManager.Default.Register();
            this.isRegistered = true;

            return;
        }

        public void Unregister()
        {
            if (this.isRegistered)
            {
                AppNotificationManager.Default.Unregister();
                this.isRegistered = false;
            }

            return;
        }

        public static bool SendNotification()
        {
            var appNotificationBuilder = new AppNotificationBuilder();
            appNotificationBuilder
                .AddArgument("action", "ToastClick")
                .AddText("Celebrate")
                .AddText("Player 1 is the winner")
                .AddButton(new AppNotificationButton("Celebrate")
                    .AddArgument("action", "OpenApp"));
            var appNotification = appNotificationBuilder.BuildNotification();

            AppNotificationManager.Default.Show(appNotification);

            return (appNotification.Id != 0);
        }

        public void ProcessActivatedEventArgs(AppNotificationActivatedEventArgs args)
        {
            Debug.WriteLine("App launched from notification");

            return;
        }

        private void OnNotificationInvoked(AppNotificationManager sender, AppNotificationActivatedEventArgs args)
        {
            var action = args.Arguments["action"];

            if (action == "OpenApp")
            {
                (App.Current as App).BringToFront();
            }

            return;
        }
    }
}
