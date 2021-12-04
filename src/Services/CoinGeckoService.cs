using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using CoinGecko.Clients;
using CoinGecko.Entities.Response.Coins;
using CoinGecko.Interfaces;
using CoinGecko.Parameters;

namespace CryptoBotnet.Services
{
    public class CoinGeckoService
    {
        private readonly HttpClient _http;
        private readonly ICoinGeckoClient _client;

        public CoinGeckoService(HttpClient http)
        {
            _http = http;
            _client = CoinGeckoClient.Instance;
        }

        public async Task<Stream> GetCryptocurrencyInformation(string abrv)
        {
            var resp = await _http.GetAsync("https://cataas.com/cat");
            return await resp.Content.ReadAsStreamAsync();
        }

        public Dictionary<string, string> ListCoins()
        {
            var dic = new Dictionary<string, string>();
            var coinList = await _client.CoinsClient.GetCoinList();
            for (var i = 0; i < 20; ++i)
            {
                var result = await _client.CoinsClient.GetHistoryByCoinId(coinList[i].Id, "01-12-2018", "false");
                dic.Add(coinList[i].Name, result.Name);
            }
        }
    }
}
