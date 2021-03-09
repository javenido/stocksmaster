using Microsoft.EntityFrameworkCore;
using StocksMasterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksMasterAPI.Services
{
    public class StocksMasterRepository : IStocksMasterRepository
    {
        private StocksMasterDBContext _context;

        public StocksMasterRepository(StocksMasterDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StocksDatum>> GetStocksData()
        {
            var result = _context.StocksData.OrderBy(s => s.CompanyId);
            return await result.ToListAsync();
        }

        public async Task<StocksDatum> GetStocksDatum(int stocksId)
        {
            IQueryable<StocksDatum> result;
            result = _context.StocksData.Where(s => s.StocksDataId == stocksId);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<StocksDatum> PutStocksDatum(int stocksId, StocksDatum stocksDatum)
        {
            IQueryable<StocksDatum> result;

            result = _context.StocksData.Where(s => s.StocksDataId == stocksId);

            StocksDatum stocksDatumOld = await result.FirstOrDefaultAsync();

            if (stocksDatumOld != null)
            {
                stocksDatumOld.StocksPrice = stocksDatum.StocksPrice;
                stocksDatumOld.Date = stocksDatum.Date;
                stocksDatumOld.CompanyId = stocksDatum.CompanyId;

                await _context.SaveChangesAsync();
            }
            return stocksDatum;
        }

        public async Task<StocksDatum> PostStocksDatum(StocksDatum stocksDatum)
        {
            _context.StocksData.Add(stocksDatum);
            await _context.SaveChangesAsync();
            return stocksDatum;
        }

        public async Task<StocksDatum> DeleteStocksDatum(int stocksId)
        {
            StocksDatum stocksDatum = _context.StocksData.FirstOrDefault(s => s.StocksDataId == stocksId);
            if (stocksDatum != null)
            {
                _context.StocksData.Remove(stocksDatum);
                await _context.SaveChangesAsync();  
            }
            return stocksDatum;
        }

        public async Task<bool> StocksDatumExists(int stocksId)
        {
            return await _context.StocksData.AnyAsync<StocksDatum>(s => s.StocksDataId == stocksId);
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var result = _context.Companies.OrderBy(c => c.CompanyId);
            return await result.ToListAsync();
        }

        public async Task<Company> GetCompany(int companyId)
        {
            IQueryable<Company> result;
            result = _context.Companies.Where(c => c.CompanyId == companyId);
            return await result.FirstOrDefaultAsync();
        }
    }
}
