// <copyright file="MainWindow.xaml.cs" company="Mooville">
//   Copyright © 2024 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Fluent
{
    using Microsoft.UI.Xaml;
    using System;
    using Windows.Storage.Pickers;
    using WinRT.Interop;

    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.ExtendsContentIntoTitleBar = true;

            this.buttonOpen.Click += this.ButtonOpen_Click;
        }

        private async void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            var fileOpenPicker = new FileOpenPicker();

            var hWnd = WindowNative.GetWindowHandle(this);
            InitializeWithWindow.Initialize(fileOpenPicker, hWnd);

            fileOpenPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            fileOpenPicker.ViewMode = PickerViewMode.Thumbnail;
            fileOpenPicker.FileTypeFilter.Add(@".quno");

            var file = await fileOpenPicker.PickSingleFileAsync();

            if (file != null)
            {
                textFile.Text = file.Path;
            }
            else
            {
                textFile.Text = "Operation cancelled.";
            }

            return;
        }
    }
}
