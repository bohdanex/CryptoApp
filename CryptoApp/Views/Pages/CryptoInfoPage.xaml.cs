using CryptoApp.Models;
using CryptoApp.Views.UserControls;
using System;
using System.Collections.Generic;
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

namespace CryptoApp.Pages
{
    public partial class CryptoInfoPage : Page
    {
        private List<CryptoCurrency> _currencyList;
        public CryptoInfoPage()
        {
            InitializeComponent();
        }

        async private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _currencyList = (List<CryptoCurrency>)await CryptoCurrencyRepository.GetCryptoCurrenciesAsync();

            foreach (CryptoCurrency crypto in _currencyList)
            {
                stackPanelCrypto.Children.Add(new CryptoBarUserControl(crypto));
            }
        }
    }
}
