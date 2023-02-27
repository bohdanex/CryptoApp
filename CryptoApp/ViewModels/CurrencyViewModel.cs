using CryptoApp.Models;
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
        public bool LoadContent { get; set; } = false;
        public ObservableCollection<CryptoMarket> CurrencyMarkets { get; set; }

        private CryptoCurrency _currency;
        public CryptoCurrency Currency 
        { 
            get => _currency; 
            set { _currency = value; 
                    OnPropertyChanged(nameof(Currency));
                } 
        }

        public Uri CurrencyImageUri { get; set; } = 
            new Uri("pack://application:,,,/CryptoApp;component/StaticFiles/Images/loading.png",UriKind.Absolute);
        public string Symbol { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public CurrencyViewModel()
        {
            Currency = new CryptoCurrency();
            CurrencyMarkets = new ObservableCollection<CryptoMarket>();
            App.OnTrigger += SourceUpdated;
        }

        private void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void SourceUpdated()
        {
            Currency = App.GlobalCurrency;
            OnPropertyChanged(nameof(Currency));
            CurrencyMarkets = new ObservableCollection<CryptoMarket>(App.GlobalCurrency.Markets) ??
                new ObservableCollection<CryptoMarket>();
            OnPropertyChanged(nameof(CurrencyMarkets));
            CurrencyImageUri =new Uri(String.Format(@"https://assets.coincap.io/assets/icons/{0}@2x.png", 
                                        new string(Currency.Symbol).ToLower()));
            Symbol = new string(Currency.Symbol).ToUpper();
            
            OnPropertyChanged(nameof(CurrencyImageUri));
            OnPropertyChanged(nameof(Symbol));
            LoadContent= true;
            OnPropertyChanged(nameof(LoadContent));
        }
    }
}
