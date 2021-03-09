using System;
using System.Collections.Generic;

#nullable disable

namespace StocksMasterAPI.Models
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
