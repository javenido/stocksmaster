using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StocksMasterAPI.Models;
using StocksMasterAPI.Services;

namespace StocksMasterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly StocksMasterDBContext _context;
        private IStocksMasterRepository _stocksMasterRepository;

        public CompaniesController(StocksMasterDBContext context, IStocksMasterRepository stocksMasterRepository)
        {
            _context = context;
            _stocksMasterRepository = stocksMasterRepository;
        }

        // GET: api/Companies
        [HttpGet("/api/companies.{format}"), FormatFilter]
        [HttpGet]
        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await _stocksMasterRepository.GetCompanies();
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<Company> GetCompany(int id)
        {
            var stocksDatum = await _stocksMasterRepository.GetCompany(id);

            if (stocksDatum == null)
            {
                return null;
            }

            return stocksDatum;
        }
    }
}
