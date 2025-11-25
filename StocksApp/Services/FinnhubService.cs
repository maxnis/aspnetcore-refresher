using StocksApp.ServiceContracts;
using System.Text.Json;

namespace StocksApp.Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _token = string.Empty;

        public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _token = configuration["FINNHUB_API_KEY"]?.ToString() ?? string.Empty;
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            var uri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_token}");
            return await GetDataInternal(stockSymbol, uri.ToString());
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            var uri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_token}");
            return await GetDataInternal(stockSymbol, uri.ToString());
        }

        private async Task<Dictionary<string, object>?> GetDataInternal(string stockSymbol, string queryUri) 
        {
            using HttpClient httpClient = _httpClientFactory.CreateClient();
            HttpRequestMessage httpRequestMessage = new()
            {
                RequestUri = new Uri(queryUri),
                Method = HttpMethod.Get
            };

            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            Stream stream = httpResponseMessage.Content.ReadAsStream();

            StreamReader streamReader = new(stream);

            string response = streamReader.ReadToEnd();

            Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response) ?? throw new InvalidOperationException("No response from finnhub server");

            if (responseDictionary.TryGetValue("error", out object? value))
                throw new InvalidOperationException(Convert.ToString(value));

            return responseDictionary;
        }
    }
}
