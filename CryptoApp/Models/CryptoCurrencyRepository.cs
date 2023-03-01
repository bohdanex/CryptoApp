using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoApp.Models
{
    public class CryptoCurrencyRepository
    {
        /// <summary>
        /// Perform operations using System.Net.Http, System.Text.Json namespaces
        /// </summary>
        /// <typeparam name="T"> variable that implements IFormatProvider</typeparam>
        /// <param name="cryptoId">Cryptocurrency identifier</param>
        /// <returns>CryptoCurrency</returns>
        async public static Task<CryptoCurrency> GetCryptoAsync<T>(T cryptoId)
        {
            try
            {
                HttpClient client = new HttpClient();
                string request = $"https://api.coincap.io/v2/assets?search={cryptoId}";
                using Stream getStream = await client.GetStreamAsync(request);
                using JsonDocument jsonDocument = await JsonDocument.ParseAsync(getStream);
                JsonElement dataElement = jsonDocument.RootElement.GetProperty("data");
                CryptoCurrencyFromApi mockCurrency = JsonSerializer.Deserialize<CryptoCurrencyFromApi>(dataElement[0]);
                List<CryptoMarket> currencyMarkets = (List<CryptoMarket>) await MarketplaceRepository.GetCryptoMarketsAsync(mockCurrency.id);
                return new CryptoCurrency()
                {
                    Id = mockCurrency.id ?? "Undefined",
                    Symbol = mockCurrency.symbol ?? "###",
                    Name = mockCurrency.name ?? "Undefined",
                    Rank = int.Parse(mockCurrency.rank ?? "0"),
                    PriceUsd = decimal.Parse(mockCurrency.priceUsd ?? "0"),
                    ChangePecentEra = Convert.ToDecimal(mockCurrency.changePercent24Hr ?? "0"),
                    MarketCupUsd = decimal.Parse(mockCurrency.marketCapUsd ?? "0"),
                    MaxSupply = decimal.Parse(mockCurrency.maxSupply ?? "0"),
                    Supply = decimal.Parse(mockCurrency.supply ?? "0"),
                    VolumeUsdEra = decimal.Parse(mockCurrency.volumeUsd24Hr ?? "0"),
                    Vwap24Era = decimal.Parse(mockCurrency.vwap24Hr ?? "0"),
                    Markets= currencyMarkets ?? new List<CryptoMarket>(),
                }
                ?? new CryptoCurrency();
            }
            catch(HttpRequestException)
            {
                return null;
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }

        /// <summary>
        /// Perform operations using System.Net.Http, System.Text.Json namespaces
        /// </summary>
        /// <param name="cryptoId">Cryptocurrency identifier</param>
        /// <returns>CryptoCurrency</returns>
        async public static Task<CryptoCurrency> GetCryptoAsync(object cryptoId)
        {

            try
            {
                HttpClient client = new HttpClient();
                string request = $"https://api.coincap.io/v2/assets?search={cryptoId}";
                using Stream getStream = await client.GetStreamAsync(request);
                using JsonDocument jsonDocument = await JsonDocument.ParseAsync(getStream);
                JsonElement dataElement = jsonDocument.RootElement.GetProperty("data");
                CryptoCurrencyFromApi mockCurrency = JsonSerializer.Deserialize<CryptoCurrencyFromApi>(dataElement[0]);
                List<CryptoMarket> currencyMarkets = (List<CryptoMarket>)await MarketplaceRepository.GetCryptoMarketsAsync(mockCurrency.id);
                return new CryptoCurrency()
                {
                    Id = mockCurrency.id ?? "Undefined",
                    Symbol = mockCurrency.symbol ?? "###",
                    Name = mockCurrency.name ?? "Undefined",
                    Rank = int.Parse(mockCurrency.rank ?? "0"),
                    PriceUsd = decimal.Parse(mockCurrency.priceUsd ?? "0"),
                    ChangePecentEra = Convert.ToDecimal(mockCurrency.changePercent24Hr ?? "0"),
                    MarketCupUsd = decimal.Parse(mockCurrency.marketCapUsd ?? "0"),
                    MaxSupply = decimal.Parse(mockCurrency.maxSupply ?? "0"),
                    Supply = decimal.Parse(mockCurrency.supply ?? "0"),
                    VolumeUsdEra = decimal.Parse(mockCurrency.volumeUsd24Hr ?? "0"),
                    Vwap24Era = decimal.Parse(mockCurrency.vwap24Hr ?? "0"),
                    Markets = currencyMarkets ?? new List<CryptoMarket>(),
                }
                ?? new CryptoCurrency();
            }
            catch (HttpRequestException)
            {
                return null;
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the IEnumareble<CryptoCurrency> collection
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Limit must be more than 1</exception>
        /// <exception cref="NullReferenceException">Stream is null</exception>
        async public static Task<IEnumerable<CryptoCurrency>> GetCryptoCurrenciesAsync(byte limit = 10)
        {
            try
            {
                if (limit < 1)
                {
                    throw new ArgumentOutOfRangeException("limit", "must be more than 1");
                }

                HttpClient client = new();
                string request = $"https://api.coincap.io/v2/assets?limit={limit}";
                using Stream getStream = await client.GetStreamAsync(request);

                if (getStream == null)
                {
                    throw new NullReferenceException("The stream is null");
                }

                using JsonDocument jsonDocument = await JsonDocument.ParseAsync(getStream);
                JsonElement dataElement = jsonDocument.RootElement.GetProperty("data");
                var deserialized = JsonSerializer.Deserialize<List<CryptoCurrencyFromApi>>(dataElement);

                List<CryptoCurrency> finalCryptoList = new();
                foreach (CryptoCurrencyFromApi mockCurrency in deserialized)
                {
                    List<CryptoMarket> currencyMarkets = (List<CryptoMarket>)await MarketplaceRepository.GetCryptoMarketsAsync(mockCurrency.id);
                    finalCryptoList.Add(
                        new CryptoCurrency
                        {
                            Id = mockCurrency.id ?? "Undefined",
                            Symbol = mockCurrency.symbol ?? "###",
                            Name = mockCurrency.name ?? "Undefined",
                            Rank = int.Parse(mockCurrency.rank ?? "0"),
                            PriceUsd = decimal.Parse(mockCurrency.priceUsd ?? "0"),
                            ChangePecentEra = Convert.ToDecimal(mockCurrency.changePercent24Hr ?? "0"),
                            MarketCupUsd = decimal.Parse(mockCurrency.marketCapUsd ?? "0"),
                            MaxSupply = decimal.Parse(mockCurrency.maxSupply ?? "0"),
                            Supply = decimal.Parse(mockCurrency.supply ?? "0"),
                            VolumeUsdEra = decimal.Parse(mockCurrency.volumeUsd24Hr ?? "0"),
                            Vwap24Era = decimal.Parse(mockCurrency.vwap24Hr ?? "0"),
                            Markets = currencyMarkets ?? new List<CryptoMarket>(),
                        });
                }
                return finalCryptoList ?? new List<CryptoCurrency>();
            }
            catch(HttpRequestException)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the IEnumareble<CryptoCurrency> collection
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Limit must be more than 1</exception>
        /// <exception cref="NullReferenceException">Stream is null</exception>
        public static IEnumerable<CryptoCurrency> GetCryptoCurrencies(byte limit = 10)
        {
            try
            {
                if (limit < 1)
                {
                    throw new ArgumentOutOfRangeException("limit", "must be more than 1");
                }

                HttpClient client = new();
                string request = $"https://api.coincap.io/v2/assets?limit={limit}";
                using Stream getStream = client.GetStreamAsync(request).Result;

                if (getStream == null)
                {
                    throw new NullReferenceException("The stream is null");
                }

                using JsonDocument jsonDocument = JsonDocument.Parse(getStream);
                JsonElement dataElement = jsonDocument.RootElement.GetProperty("data");
                var deserialized = JsonSerializer.Deserialize<List<CryptoCurrencyFromApi>>(dataElement);

                List<CryptoCurrency> finalCryptoList = new();
                foreach (CryptoCurrencyFromApi mockCurrency in deserialized)
                {
                    List<CryptoMarket> currencyMarkets = (List<CryptoMarket>) MarketplaceRepository.GetCryptoMarketsAsync(mockCurrency.id).Result;
                    finalCryptoList.Add(
                        new CryptoCurrency
                        {
                            Id = mockCurrency.id ?? "Undefined",
                            Symbol = mockCurrency.symbol ?? "###",
                            Name = mockCurrency.name ?? "Undefined",
                            Rank = int.Parse(mockCurrency.rank ?? "0"),
                            PriceUsd = decimal.Parse(mockCurrency.priceUsd ?? "0"),
                            ChangePecentEra = Convert.ToDecimal(mockCurrency.changePercent24Hr ?? "0"),
                            MarketCupUsd = decimal.Parse(mockCurrency.marketCapUsd ?? "0"),
                            MaxSupply = decimal.Parse(mockCurrency.maxSupply ?? "0"),
                            Supply = decimal.Parse(mockCurrency.supply ?? "0"),
                            VolumeUsdEra = decimal.Parse(mockCurrency.volumeUsd24Hr ?? "0"),
                            Vwap24Era = decimal.Parse(mockCurrency.vwap24Hr ?? "0"),
                            Markets = currencyMarkets ?? new List<CryptoMarket>(),
                        });
                }
                return finalCryptoList ?? new List<CryptoCurrency>();
            }
            catch(HttpRequestException)
            {
                return null;
            }
        }
    }
}
