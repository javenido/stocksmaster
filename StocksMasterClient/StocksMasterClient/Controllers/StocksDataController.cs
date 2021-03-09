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
    public class StocksDataController : Controller
    {
        private IStocksMasterAPIClient client;
        private int PAGE_SIZE = 25;

        public StocksDataController(IStocksMasterAPIClient apiClient)
        {
            client = apiClient;
        }

        public async Task<ViewResult> List(int companyId = 0, int page = 1)
        {
            var model = await client.GetAllStocksData();
            var companies = await client.GetAllCompanies();
            ViewBag.companies = companies;

            if (companyId > 0)
            {
                model = model.Where(data => data.CompanyId == companyId);
                TempData["company"] = companies.First(c => c.CompanyId == companyId).CompanyName;
            }

            model = model.OrderBy(data => data.CompanyId).ThenByDescending(data => data.Date);
            return View(new ListViewModel(model.AsQueryable<StocksDatum>())
            {
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PAGE_SIZE,
                    CurrentPage = page,
                    TotalItems = model.Count()
                },
                CompanyId = companyId
            });
        }

        public async Task<IActionResult> Details(int stocksDatumId)
        {
            var datum = await client.GetStocksDatumById(stocksDatumId);
            if (datum != null)
            {
                return View(datum);
            }

            TempData["message"] = "Stock datum not found.";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Post(int companyId, double price, string date)
        {
            if (price == 0)
            {
                TempData["message"] = "Please enter a value greater than $0.";
                return RedirectToAction("Index", "Home");
            }
            var newData = await client.Add(new StocksDatum
            {
                CompanyId = companyId,
                StocksPrice = (decimal)Math.Round(price, 2),
                Date = date
            });

            TempData["message"] = "Stock datum sucessfully posted.";
            return RedirectToAction("Details", new { stocksDatumId = newData.StocksDataId });
        }

        public async Task<IActionResult> Put(int stocksDatumId, int companyId, double price, string date)
        {
            if (!(await client.GetAllStocksData()).Any(data => data.StocksDataId == stocksDatumId))
            {
                TempData["message"] = "Failed to update stock datum because it does not exist.";
                return RedirectToAction("Index", "Home");
            }

            if (price == 0)
            {
                TempData["message"] = "Please enter a value greater than $0.";
                return RedirectToAction("Index", "Home");
            }

            TempData["message"] = "Stock datum sucessfully updated.";
            return View("Details", await client.Update(new StocksDatum
            {
                StocksDataId = stocksDatumId,
                CompanyId = companyId,
                StocksPrice = (decimal)Math.Round(price, 2),
                Date = date
            }));
        }

        public async Task<IActionResult> Delete(int stocksDatumId)
        {
            var deleted = await client.Delete(stocksDatumId);
            if (deleted == null)
            {
                TempData["message"] = "Failed to delete stock datum because it does not exist.";
                return RedirectToAction("Index", "Home");
            }

            TempData["message"] = "Stock datum successfully deleted.";
            return View("Details", deleted);
        }
    }
}