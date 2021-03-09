using System;
using System.Collections.Generic;

namespace StocksMasterClient.Models
{
    public partial class StocksDatum
    {
        public int StocksDataId { get; set; }
        public decimal StocksPrice { get; set; }
        public string Date { get; set; }
        public int CompanyId { get; set; }

        //public virtual Company Company { get; set; }
    }
}