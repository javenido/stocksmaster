using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksMasterAPI.DTOs
{
    public class StocksDatumDto
    {
        public decimal StocksPrice { get; set; }
        public string Date { get; set; }
        public int CompanyId { get; set; }
    }
}
