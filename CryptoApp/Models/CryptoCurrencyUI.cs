using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Models
{
    public class CryptoCurrencyUI
    {
        public string Rank { get; set; }
        public string Name { get; set; }
        public string VolumeUsdEra { get; set; }
        public string PriceUsd { get; set; }

        public CryptoCurrencyUI()
        {
            Rank = "";
            Name= "";
            VolumeUsdEra = "";
            PriceUsd = "";
        }

        public CryptoCurrencyUI(CryptoCurrency cryptoCurrency)
        {
            string volume = cryptoCurrency.VolumeUsdEra.ToString("F4");
            string priceusd = cryptoCurrency.PriceUsd.ToString("F4");

            Rank = cryptoCurrency.Rank.ToString();
            Name= cryptoCurrency.Name;
            VolumeUsdEra = volume;
            PriceUsd = priceusd;
        }
    }
}
