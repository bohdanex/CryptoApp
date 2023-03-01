using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.ViewModels
{
    public class AppSettingsViewModel : INotifyPropertyChanged
    {
        private string _selectedTheme;
        public string SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                _selectedTheme = value;
                App.Theme = value;
            }
        }

        private string _selectedLanguage;

        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                _selectedLanguage = value;
                App.Localization = value;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string callerName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(callerName)));
        }

        public AppSettingsViewModel()
        {
            SelectedTheme= App.Theme;
            SelectedLanguage= App.Localization;
            OnPropertyChanged(nameof(SelectedTheme));
            OnPropertyChanged(nameof(SelectedLanguage));
        }
    }
}
