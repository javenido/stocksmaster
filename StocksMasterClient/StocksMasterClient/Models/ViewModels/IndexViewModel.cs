using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksMasterClient.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<CompanyWithLinearRegression> AllCompanies { get; set; }
        public IEnumerable<CompanyWithLinearRegression> Top5Companies => AllCompanies.OrderByDescending(c => c.Slope).Take(5);
    }
}