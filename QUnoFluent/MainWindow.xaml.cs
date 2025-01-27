// <copyright file="MainWindow.xaml.cs" company="Mooville">
//   Copyright © 2025 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Fluent
{
    using System;
    using Microsoft.UI.Xaml;
    using Microsoft.Windows.ApplicationModel.Resources;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using WinRT.Interop;
    using Mooville.QUno.Fluent.Model;
    using Mooville.QUno.Fluent.ViewModel;

    public sealed partial class MainWindow : Window
    {
        private readonly MainViewModel viewModel;
        private readonly ResourceLoader resourceLoader;

        public MainWindow()
        {
            this.InitializeComponent();
            this.ExtendsContentIntoTitleBar = true;
            this.Title = "QUno for Windows App SDK";
            this.resourceLoader = new ResourceLoader();
            this.viewModel = new MainViewModel();

            this.buttonNew.Click += this.ButtonNew_Click;
            this.buttonOpen.Click += this.ButtonOpen_Click;
        }

        public MainViewModel ViewModel
        {
            get
            {
                return this.viewModel;
            }
        }

        public async void LoadGameFromPathAsync(string path)
        {
            var file = await StorageFile.GetFileFromPathAsync(path);
            var serializer = new FluentGameSerializer();
            var game = await serializer.LoadFromFileAsync(file);
            this.viewModel.OpenGame(game);

            return;
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            NotificationManager.SendNotification();

            return;
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
                var noFileSelected = this.resourceLoader.GetString("FileNoFileSelected");
                textFile.Text = noFileSelected;
            }

            return;
        }
    }
}
