using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Models
{
    public class CryptoMarket
    { 
        public string ExchangeId { get; set; } = String.Empty;
        public string BaseId { get; set; } = String.Empty;
        public string QuoteId { get; set; } = String.Empty;
        public string BaseSymbol { get; set; } = String.Empty;
        public string QuoteSymbol { get; set; } = String.Empty;
        public decimal VolumeUsd24Hr { get; set; } = default;
        public decimal PriceUsd { get; set; } = default;
        public decimal VolumePercent { get; set; } = default;

        public static CryptoMarket ParseFromApi(CryptoMarketFromApi market)
        {
            return new CryptoMarket
            {
                ExchangeId = market.exchangeId,
                BaseId = market.baseId,
                QuoteId = market.quoteId,
                BaseSymbol = market.baseSymbol,
                QuoteSymbol = market.quoteSymbol,
                VolumeUsd24Hr = Decimal.Parse(market.volumeUsd24Hr ?? "0"),
                PriceUsd = Decimal.Parse(market.priceUsd ?? "0"),
                VolumePercent = Decimal.Parse(market.volumePercent ?? "0"),
            };
        }

        public override string ToString()
        {
            return $"Base: {BaseId}, Exchange: {ExchangeId}";
        }

        public override bool Equals(object? obj)
        {
            return ((CryptoMarket)obj).ExchangeId.Equals(this.ExchangeId)
                & ((CryptoMarket)obj).BaseId.Equals(this.BaseId);
        }

        public override int GetHashCode()
        {
            return ExchangeId.GetHashCode() & BaseId.GetHashCode();
        }
    }
}
