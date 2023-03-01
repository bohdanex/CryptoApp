using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Models
{
    public class MarketModelFromApi
    {
            public string exchangeId { get; set; }
            public string rank { get; set; }
            public string baseSymbol { get; set; }
            public string baseId { get; set; }
            public string quoteSymbol { get; set; }
            public string quoteId { get; set; }
            public string priceQuote { get; set; }
            public string priceUsd { get; set; }
            public string volumeUsd24Hr { get; set; }
            public string percentExchangeVolume { get; set; }
            public string tradesCount24Hr { get; set; }
            public string updated { get; set; }

        public Marketplace ToMarketplace()
        {
            return new Marketplace
            {
                ExchangeId = exchangeId,
                Rank = int.Parse(rank ??"0"),
                BaseSymbol = baseSymbol,
                BaseId = baseId,
                QuoteSymbol= quoteSymbol,
                QuoteId= quoteId,
                PriceQuote=int.Parse(priceQuote ?? "0"),
                PriceUsd=int.Parse(priceUsd ?? "0"),
                VolumeUsd24Hr=int.Parse(volumeUsd24Hr ?? "0"),
                PercentExchangeVolume=int.Parse(percentExchangeVolume ?? "0"),
                TradesCount24Hr=int.Parse(tradesCount24Hr ?? "0")
            };
        }
    }
}
