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

    }
}
