using CryptoApp.Models;
using CryptoApp.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CryptoApp.ViewModels
{
    class CurrencyViewModel : INotifyPropertyChanged
    {
        public bool IsContentLoaded { get; set; } = true; //false
        public Uri CurrencyImageUri { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;


        public ObservableCollection<CryptoMarket> CurrencyMarkets { get; set; }
        public ObservableCollection<CryptoCurrency> CurrenciesList { get; set; }

        private CryptoCurrency _currency;
        public CryptoCurrency Currency 
        { 
            get => _currency; 
            set 
            {
                IsContentLoaded = true; //false
                _currency = value; 
                OnPropertyChanged(nameof(Currency));
                if(value != null && value.Symbol != null && value.Name != "Undefined")
                {
                    SetImage(new Uri($"https://assets.coincap.io/assets/icons/{value.Symbol.ToLower()}@2x.png"));
                }
                
            }
        }

        public CurrencyViewModel()
        {
            CurrencyImageUri = new Uri("../../StaticFiles/Images/loading.png", UriKind.Relative);
            Currency = new CryptoCurrency();
            CurrencyMarkets = new ObservableCollection<CryptoMarket>();
            App.CryptoCurrencies_Loaded += LoadCryptocurrencies;    
        }

        private void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void LoadCryptocurrencies()
        {
            CurrenciesList = new(App.TopTenCurrenciesList);
            OnPropertyChanged(nameof(CurrenciesList));
        }

        private void SetImage(Uri uri)
        {
            CurrencyImageUri = uri;
            OnPropertyChanged(nameof(CurrencyImageUri));
            IsContentLoaded = true;
            OnPropertyChanged(nameof(IsContentLoaded));
        }
    }
}
