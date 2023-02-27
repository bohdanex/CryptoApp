using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Models
{
    public class CryptoCurrency
    {
        public string Id { get; set; }
        public int Rank { get; set; }
        public char[] Symbol { get; set; }
        public string Name { get; set; }
        public decimal Supply { get; set; }
        public decimal MaxSupply { get; set; }
        public decimal MarketCupUsd { get; set; }
        public decimal VolumeUsdEra { get; set; }
        public decimal PriceUsd { get; set; }
        public decimal ChangePecentEra { get; set; }
        public decimal Vwap24Era { get; set; }
        public ICollection<CryptoMarket> Markets { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object? obj)
        {
            return this.PriceUsd.Equals((obj as CryptoCurrency)?.PriceUsd);
        }

        public override int GetHashCode()
        {
            return this.PriceUsd.GetHashCode();
        }
    }
}
