// <copyright file="MainWindow.xaml.cs" company="Mooville">
//   Copyright © 2024 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Fluent
{
    using Microsoft.UI.Xaml;

    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.Title = "QUno for Windows App SDK";
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            this.myButton.Content = "Clicked";

            return;
        }
    }
}
