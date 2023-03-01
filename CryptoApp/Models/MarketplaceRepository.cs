using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace CryptoApp.Models
{
    public class MarketplaceRepository
    {
        async public static Task<IEnumerable<CryptoMarket>> GetCryptoMarketsAsync(object cryptoId, byte limit = 10)
        {
            try
            {
                string baseId = cryptoId.ToString().ToLower();

                string uri = $"https://api.coincap.io/v2/assets/{baseId}/markets?limit={limit}";
                using HttpClient client = new HttpClient();
                using Stream readJsonStream = await client.GetStreamAsync(uri);
                using JsonDocument responseJson = await JsonDocument.ParseAsync(readJsonStream);
                JsonElement dataElement = responseJson.RootElement.GetProperty("data");
                List<CryptoMarketFromApi> dataToList =
                    JsonSerializer.Deserialize<List<CryptoMarketFromApi>>(dataElement);

                List<CryptoMarket> result = new();
                foreach (CryptoMarketFromApi market in dataToList)
                {
                    result.Add(CryptoMarket.ParseFromApi(market));
                }
                return result;
            }
            catch(HttpRequestException)
            {
                return null;
            }
        }

        async public static Task<IEnumerable<Marketplace>> GetMarketAsync(byte limit = 40)
        {
            try
            {
                string url = $"https://api.coincap.io/v2/markets?limit={limit}";
                HttpClient client = new HttpClient();
                using Stream stream = await client.GetStreamAsync(url);
                using JsonDocument dataJson = await JsonDocument.ParseAsync(stream);
                JsonElement dataElement = dataJson.RootElement.GetProperty("data");
                List<MarketModelFromApi> dataFromApi = JsonSerializer.Deserialize<List<MarketModelFromApi>>(dataElement);
                List<Marketplace> originalMarket = new();
                foreach (MarketModelFromApi market in dataFromApi)
                {
                    originalMarket.Add(market.ToMarketplace());
                }
                return originalMarket;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}
