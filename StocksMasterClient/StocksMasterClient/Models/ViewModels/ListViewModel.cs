using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksMasterClient.Models.ViewModels
{
    public class ListViewModel
    {
        private IQueryable<StocksDatum> stocksData;

        public PagingInfo PagingInfo { get; set; }

        public IEnumerable<StocksDatum> StocksDataList =>
            stocksData.Skip((PagingInfo.CurrentPage - 1) * PagingInfo.ItemsPerPage)
            .Take(PagingInfo.ItemsPerPage);

        public ListViewModel(IQueryable<StocksDatum> data)
        {
            stocksData = data;
        }

        public int CompanyId { get; set; }
    }
}