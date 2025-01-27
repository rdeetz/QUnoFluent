// <copyright file="App.xaml.cs" company="Mooville">
//   Copyright © 2025 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Fluent
{
    using System;
    using System.Diagnostics;
    using Microsoft.UI.Xaml;
    using Microsoft.Windows.AppLifecycle;
    using Microsoft.Windows.AppNotifications;
    using Windows.Win32;
    using Windows.Win32.Foundation;
    using WinRT.Interop;

    public partial class App : Application
    {
        private Window window;
        private NotificationManager notificationManager;

        public App()
        {
            this.InitializeComponent();
            this.notificationManager = new NotificationManager();

            AppDomain.CurrentDomain.ProcessExit += this.OnProcessExit;
        }

        public void BringToFront()
        {
            if (this.window != null)
            {
                var hWnd = WindowNative.GetWindowHandle(this.window);
                PInvoke.SwitchToThisWindow((HWND)hWnd, false);
            }

            return;
        }   

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            this.window = new MainWindow();
            this.notificationManager.Register();

            var appInstance = AppInstance.GetCurrent();
            var activatedEventArgs = appInstance.GetActivatedEventArgs();

            if (activatedEventArgs.Kind == ExtendedActivationKind.File)
            {
                var fileActivatedEventArgs = activatedEventArgs.Data as Windows.ApplicationModel.Activation.FileActivatedEventArgs;
                var file = fileActivatedEventArgs.Files[0];

                Debug.WriteLine("File activation with path: " + file.Path);

                (this.window as MainWindow).LoadGameFromPathAsync(file.Path);
            }
            else if (activatedEventArgs.Kind == ExtendedActivationKind.AppNotification)
            {
                var appNotificationActivatedEventArgs = activatedEventArgs.Data as AppNotificationActivatedEventArgs;
                this.notificationManager.ProcessActivatedEventArgs(appNotificationActivatedEventArgs);
            }

            this.window.Activate();

            return;
        }
        
        private void OnProcessExit(object sender, EventArgs e)
        {
            this.notificationManager.Unregister();

            return;
        }
    }
}
