using CryptoApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CryptoApp.ViewModels
{
    class CurrencyViewModel : INotifyPropertyChanged
    {
        private CryptoCurrency _currency;
        public CryptoCurrency Currency 
        { 
            get => _currency; 
            set { _currency = value; 
                    OnPropertyChanged(nameof(Currency));
                } 
        }

        

        public Uri CurrencyImageUri { get; set; }
        public string Symbol { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public CurrencyViewModel()
        {
            Currency = new CryptoCurrency();
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
            CurrencyImageUri = new Uri(String.Format(@"https://assets.coincap.io/assets/icons/{0}@2x.png", 
                                                     new string(Currency.Symbol).ToLower()));
            Symbol = new string(Currency.Symbol).ToUpper();
            
            OnPropertyChanged(nameof(CurrencyImageUri));
            OnPropertyChanged(nameof(Symbol));
        }
    }
}
