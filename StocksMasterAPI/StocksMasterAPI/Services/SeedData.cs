using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StocksMasterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksMasterAPI.Services
{
    public static class SeedData
    {
        private static Random r = new Random();

        public static void EnsurePopulated(StocksMasterDBContext context)
        {
            if (!context.StocksData.Any())
            {
                // generate 100 entries for each company
                foreach (Company c in context.Companies)
                {
                    // determine slope
                    var slope = GetRandom(1, 10);

                    // generate prices according to slope
                    List<double> prices = new List<double>();
                    if (slope >= 4) // there's a 70% chance that the slope is positive
                    {
                        prices = GetNumbersPositiveSlope(GetRandom(1, 500), GetRandom(501, 1000));
                    }
                    else if (slope >= 2) // there's a 20% chance that the slope is negative
                    {
                        prices = GetNumbersNegativeSlope(GetRandom(1, 500), GetRandom(501, 1000));
                    }
                    else // there's a 10% chance that the slope is neutral
                    {
                        prices = GetNumbersNeutralSlope(GetRandom(50, 950));
                    }

                    // create StocksDatum objects and save to DB
                    var date = DateTime.Now.AddDays(-100);
                    foreach (var price in prices)
                    {
                        context.StocksData.Add(new StocksDatum
                        {
                            CompanyId = c.CompanyId,
                            StocksPrice = (decimal)Math.Round(price, 2),
                            Date = date.ToString("yyyy/MM/dd")
                        });
                        date = date.AddDays(1);
                    }
                }

                context.SaveChanges();
            }
        }

        private static List<double> GetNumbersNeutralSlope(double reference)
        {
            List<double> list = new List<double>();
            for (int i = 0; i < 100; i++)
            {
                list.Add(GetRandom(reference - 10, reference + 10));
            }
            return list;
        }

        private static List<double> GetNumbersNegativeSlope(double lo, double hi)
        {
            List<double> list = new List<double>();
            while (list.Count < 100)
            {
                list.Add(GetRandom(lo, hi));
            }
            return list.OrderByDescending(d => d).ToList<double>();
        }

        private static List<double> GetNumbersPositiveSlope(double lo, double hi)
        {
            List<double> list = new List<double>();
            while (list.Count < 100)
            {
                list.Add(GetRandom(lo, hi));
            }
            return list.OrderBy(d => d).ToList<double>();
        }

        private static double GetRandom(double lo, double hi)
        {
            if (lo >= hi)
                throw new ArgumentException("lo cannot be greater than or equal to hi");

            return r.NextDouble() * (hi - lo) + lo;
        }
    }
}