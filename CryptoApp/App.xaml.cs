using CryptoApp.Models;
using CryptoApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CryptoApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static CryptoCurrency _currency;
        public static CryptoCurrency GlobalCurrency { get => _currency; set { _currency = value; if(value != null) Update();} }

        public delegate void Trigger();
        public static event Trigger OnTrigger;

        private static void Update()
        {
            OnTrigger?.Invoke();
        }
    }
}
