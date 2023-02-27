using CryptoApp.Models;
using CryptoApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CryptoApp.Views.UserControls
{
    public partial class CryptoBarUserControl : UserControl
    {
        public CryptoCurrency OriginalCurrency { get; set; }
        public CryptoCurrencyUI CryptoCurrencyUI { get; set; }

        public CryptoBarUserControl()
        {
            CryptoCurrencyUI = new CryptoCurrencyUI() { 
                                                        Name=String.Empty, 
                                                        PriceUsd = String.Empty, 
                                                        Rank = String.Empty, 
                                                        VolumeUsdEra = String.Empty};
            InitializeComponent();
        }

        public CryptoBarUserControl(CryptoCurrency cryptoCurrency)
        {
            OriginalCurrency = cryptoCurrency;
            CryptoCurrencyUI = new CryptoCurrencyUI(OriginalCurrency);
            InitializeComponent();
        }

        public CryptoBarUserControl(bool test)
        {
            if(test)
            {
                OriginalCurrency = new CryptoCurrency
                {
                    Id = "bitcoin",
                    Rank = Convert.ToByte(new Random().Next(0, 255)),
                    Symbol = new[] { 'b', 't', 'c' },
                    Name = "Bitcoin",
                    Supply = 17193925.0000000000000000M,
                    MaxSupply = 25112525412.125124125M,
                    MarketCupUsd = 25125412.125124125M,
                    VolumeUsdEra = 2927959461.1750323310959460M,
                    PriceUsd = 6929.8217756835584756M,
                    ChangePecentEra = 25125412.125124125M, 
                    Vwap24Era = 7175.0663247679233209M,
                };
                CryptoCurrencyUI = new CryptoCurrencyUI(OriginalCurrency);
            }
            
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            App.GlobalCurrency = OriginalCurrency;
        }
    }
}
