using System;
using System.Collections.Generic;

#nullable disable

namespace StocksMasterAPI.Models
{
    public partial class Company
    {
        //public Company()
        //{
        //    StocksData = new HashSet<StocksDatum>();
        //}

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanySymbol { get; set; }

        //public virtual ICollection<StocksDatum> StocksData { get; set; }
    }
}
