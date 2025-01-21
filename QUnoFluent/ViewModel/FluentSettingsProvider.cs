// <copyright file="FluentSettingsProvider.cs" company="Mooville">
//   Copyright © 2025 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Fluent.ViewModel
{
    using System;
    using Mooville.QUno.ViewModel;
    using Windows.Storage;

    public class FluentSettingsProvider : ISettingsProvider
    {
        private const string DefaultHumanPlayerNameKey = @"DefaultHumanPlayerName";
        private const string DefaultComputerPlayersKey = @"DefaultComputerPlayers";
        private const string FluentlDefaultHumanPlayerName = @"Player 1";
        private const int FluentDefaultComputerPlayers = 3;

        private string defaultHumanPlayerName;
        private int defaultComputerPlayers;

        public FluentSettingsProvider()
        {
        }

        #region ISettingsProvider Members

        public string DefaultHumanPlayerName
        {
            get
            {
                return this.defaultHumanPlayerName;
            }

            set
            {
                this.defaultHumanPlayerName = value;
            }
        }

        public int DefaultComputerPlayers
        {
            get
            {
                return this.defaultComputerPlayers;
            }

            set
            {
                this.defaultComputerPlayers = value;
            }
        }

        public bool AutoCheckForUpdates
        {
            get
            {
                return false;
            }

            set
            {
                return;
            }
        }

        public void LoadSettings()
        {
            this.defaultHumanPlayerName = this.GetValue<string>(FluentSettingsProvider.DefaultHumanPlayerNameKey, FluentSettingsProvider.FluentlDefaultHumanPlayerName);
            this.defaultComputerPlayers = this.GetValue<int>(FluentSettingsProvider.DefaultComputerPlayersKey, FluentSettingsProvider.FluentDefaultComputerPlayers);

            return;
        }

        public void SaveSettings()
        {
            this.SetValue(FluentSettingsProvider.DefaultHumanPlayerNameKey, this.defaultHumanPlayerName);
            this.SetValue(FluentSettingsProvider.DefaultComputerPlayersKey, this.defaultComputerPlayers);

            return;
        }

        #endregion

        private T GetValue<T>(string key, T defaultValue)
        {
            object value = ApplicationData.Current.RoamingSettings.Values[key];

            if (value != null)
            {
                return (T)value;
            }
            else
            {
                return defaultValue;
            }
        }

        private void SetValue(string key, object value)
        {
            ApplicationData.Current.RoamingSettings.Values[key] = value;

            return;
        }
    }
}
