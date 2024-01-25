// <copyright file="App.xaml.cs" company="Mooville">
//   Copyright © 2024 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Fluent
{
    using Microsoft.UI.Xaml;

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
            this.window.Activate();

            return;
        }
    }
}
