using StocksMasterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksMasterAPI.Services
{
    public interface IStocksMasterRepository
    {
        Task<IEnumerable<StocksDatum>> GetStocksData();
        Task<StocksDatum> GetStocksDatum(int stocksId);
        Task<StocksDatum> PutStocksDatum(int stocksId, StocksDatum stocksDatum);
        Task<StocksDatum> PostStocksDatum(StocksDatum stocksDatum);
        Task<StocksDatum> DeleteStocksDatum(int stocksId);
        Task<bool> StocksDatumExists(int stocksId);

        Task<IEnumerable<Company>> GetCompanies();
        Task<Company> GetCompany(int companyId);
    }
}
