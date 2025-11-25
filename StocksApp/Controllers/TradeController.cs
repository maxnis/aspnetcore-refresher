using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksApp.Models;
using StocksApp.ServiceContracts;

namespace StocksApp.Controllers
{
    public class TradeController : Controller
    {
        private readonly IFinnhubService _finnhubService;
        private readonly TradingOptions _tradingOptions;

        public TradeController(IFinnhubService finnhubService, IOptions<TradingOptions> tradingOptions)
        {
            _finnhubService = finnhubService;
            _tradingOptions = tradingOptions.Value;
        }

        [Route("Trade/Index")]
        public async Task<IActionResult> Index()
        {
            _tradingOptions.DefaultStockSymbol ??= "MSFT";

            Dictionary<string, object>? companyProfile = await _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);

            if (companyProfile == null)
            {
                return NotFound();
            }

            var stockSymbol = companyProfile["ticker"]?.ToString() ?? "MSFT";

            Dictionary<string, object>? responseDictionary = await _finnhubService.GetStockPriceQuote(stockSymbol);

            var stock = new StockTrade()
            {
                StockSymbol = companyProfile?["ticker"].ToString(),
                StockName = companyProfile?["name"].ToString(),
                Price = Convert.ToDouble(responseDictionary?["c"].ToString()),
                Quantity = 1 // Convert.ToUInt32(responseDictionary?["v"].ToString())
            };

            return View(stock);
        }
    }
}