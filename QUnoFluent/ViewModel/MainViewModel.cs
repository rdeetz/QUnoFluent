// <copyright file="MainViewModel.cs" company="Mooville">
//   Copyright © 2024 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Fluent.ViewModel
{
    using System;
    using Mooville.QUno.Model;
    using Mooville.QUno.ViewModel;

    public class MainViewModel : ViewModelBase
    {
        private Game game;
        private bool isGameInProgress;

        public MainViewModel()
        {
            this.isGameInProgress = false;
        }

        public Game Game
        {
            get
            {
                return this.game;
            }
        }

        public bool IsGameInProgress
        {
            get
            {
                return this.isGameInProgress;
            }

            set
            {
                this.isGameInProgress = value;
                this.OnPropertyChanged(nameof(this.IsGameInProgress));
            }
        }

        public void OpenGame(Game game)
        {
            this.game = game;
            this.IsGameInProgress = true;
            this.OnPropertyChanged(String.Empty);

            return;
        }
    }
}
