// <copyright file="App.xaml.cs" company="Mooville">
//   Copyright © 2024 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Fluent
{
    using System.Diagnostics;
    using Microsoft.UI.Xaml;
    using Microsoft.Windows.AppLifecycle;

    public partial class App : Application
    {
        private Window window;

        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            this.window = new MainWindow();

            var appInstance = AppInstance.GetCurrent();
            var activatedEventArgs = appInstance.GetActivatedEventArgs();

            if (activatedEventArgs.Kind == ExtendedActivationKind.File)
            {
                var fileActivatedEventArgs = activatedEventArgs.Data as Windows.ApplicationModel.Activation.FileActivatedEventArgs;
                var file = fileActivatedEventArgs.Files[0];

                Debug.WriteLine("File activation with path: " + file.Path);

                (this.window as MainWindow).LoadGameFromPathAsync(file.Path);
            }

            this.window.Activate();

            return;
        }
    }
}
