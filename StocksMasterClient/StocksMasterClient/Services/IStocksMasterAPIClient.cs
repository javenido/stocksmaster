using StocksMasterClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksMasterClient.Services
{
    public interface IStocksMasterAPIClient
    {
        Task<IEnumerable<Company>> GetAllCompanies();
        Task<Company> GetCompanyById(int id);
        Task<IEnumerable<StocksDatum>> GetAllStocksData();
        Task<StocksDatum> GetStocksDatumById(int id);
        Task<StocksDatum> Add(StocksDatum stocksDatum);
        Task<StocksDatum> Update(StocksDatum stocksDatum);
        Task<StocksDatum> Delete(int id);
    }
}