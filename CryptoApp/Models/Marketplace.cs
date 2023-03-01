using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Models
{
    public class Marketplace
    {
        public string ExchangeId { get; set; }
        public int Rank { get; set; }
        public string BaseSymbol { get; set; }
        public string BaseId { get; set; }
        public string QuoteSymbol { get; set; }
        public string QuoteId { get; set; }
        public decimal PriceQuote { get; set; }
        public decimal PriceUsd { get; set; }
        public decimal VolumeUsd24Hr { get; set; }
        public decimal PercentExchangeVolume { get; set; }
        public decimal TradesCount24Hr { get; set; }
    }
}
