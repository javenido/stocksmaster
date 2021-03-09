using Microsoft.AspNetCore.Mvc;
using StocksMasterClient.Models;
using StocksMasterClient.Models.ViewModels;
using StocksMasterClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksMasterClient.Controllers
{
    public class HomeController : Controller
    {
        private IStocksMasterAPIClient client;

        // Constructor
        public HomeController(IStocksMasterAPIClient apiClient)
        {
            client = apiClient;
        }

        // Actions
        public async Task<ViewResult> Index()
        {
            var allCompanies = await client.GetAllCompanies();
            var allStockData = await client.GetAllStocksData();

            List<CompanyWithLinearRegression> companies = new List<CompanyWithLinearRegression>();
            foreach (var company in allCompanies)
            {
                companies.Add(new CompanyWithLinearRegression
                {
                    Company = company,
                    StocksData = allStockData.Where(data => data.CompanyId == company.CompanyId)
                                                .OrderByDescending(data => data.Date)
                                                .Take(100)
                });
            }

            return View(new IndexViewModel { AllCompanies = companies.OrderByDescending(c => c.Slope) });
        }
    }
}