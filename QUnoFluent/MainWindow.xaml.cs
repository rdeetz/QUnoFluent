// <copyright file="MainWindow.xaml.cs" company="Mooville">
//   Copyright © 2024 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Fluent
{
    using System;
    using Microsoft.UI.Xaml;
    using Windows.Storage.Pickers;
    using WinRT.Interop;
    using Mooville.QUno.Fluent.Model;
    using Mooville.QUno.Fluent.ViewModel;

    public sealed partial class MainWindow : Window
    {
        private readonly MainViewModel viewModel;

        public MainWindow()
        {
            this.InitializeComponent();
            this.ExtendsContentIntoTitleBar = true;
            this.viewModel = new MainViewModel();

            this.buttonOpen.Click += this.ButtonOpen_Click;
        }

        public MainViewModel ViewModel
        {
            get
            {
                return this.viewModel;
            }
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
                var serializer = new FluentGameSerializer();
                var game = await serializer.LoadFromFileAsync(file);
                this.viewModel.OpenGame(game);
            }
            else
            {
                textFile.Text = "No file selected.";
            }

            return;
        }
    }
}
