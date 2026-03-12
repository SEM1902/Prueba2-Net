using Microsoft.AspNetCore.Mvc;
using PruebaNet.Services;

namespace PruebaNet.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly ExchangeRateService _exchangeRateService;

        public CurrencyController(ExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        public async Task<IActionResult> Index()
        {
            var rates = await _exchangeRateService.GetLatestRatesAsync();
            return View(rates);
        }
    }
}
