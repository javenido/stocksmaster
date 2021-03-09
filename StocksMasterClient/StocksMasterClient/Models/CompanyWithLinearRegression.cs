using StocksMasterClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksMasterClient.Models
{
    public class CompanyWithLinearRegression
    {
        private double slope;
        private double yIntercept;
        private double rSquared;
        private IEnumerable<StocksDatum> stocksData;

        public Company Company { get; set; }
        public IEnumerable<StocksDatum> StocksData
        {
            get => stocksData;
            set
            {
                stocksData = value.OrderBy(data => data.Date);
                double[] xVals = new double[stocksData.Count()];
                double[] yVals = new double[stocksData.Count()];

                for (int i = 0; i < stocksData.Count(); i++)
                {
                    xVals[i] = i + 1;
                    yVals[i] = (double)stocksData.ElementAt(i).StocksPrice;
                }

                LinearRegression(xVals, yVals, 0, stocksData.Count(), out rSquared, out yIntercept, out slope);
            }
        }
        public double Slope { get => slope; }
        public double YIntercept { get => yIntercept; }
        public double RSquared { get => rSquared; }

        public decimal Predicted
        {
            get
            {
                double predicted = (slope * (stocksData.Count() + 1)) + yIntercept;
                return (decimal)Math.Round(predicted, 2);
            }
        }

        public int PercentIncrease
        {
            get
            {
                var oldVal = stocksData.OrderByDescending(data => data.Date).First().StocksPrice;
                var newVal = Predicted;
                var increase = newVal - oldVal;
                return (int)((increase / oldVal) * 100);
            }
        }

        public string StocksDataToString()
        {
            var stockDataSorted = stocksData.OrderBy(data => data.Date);
            string s = "";
            foreach (var data in stockDataSorted)
            {
                s += $"{data.StocksPrice},";
            }
            return s.Substring(0, s.Length - 1);
        }

        public override string ToString()
        {
            return $"{Company.CompanyName} ({Company.CompanySymbol})";
        }

        /// <summary>
        /// Fits a line to a collection of (x,y) points. Source: https://gist.github.com/tansey/1375526
        /// </summary>
        /// <param name="xVals">The x-axis values.</param>
        /// <param name="yVals">The y-axis values.</param>
        /// <param name="inclusiveStart">The inclusive inclusiveStart index.</param>
        /// <param name="exclusiveEnd">The exclusive exclusiveEnd index.</param>
        /// <param name="rsquared">The r^2 value of the line.</param>
        /// <param name="yintercept">The y-intercept value of the line (i.e. y = ax + b, yintercept is b).</param>
        /// <param name="slope">The slop of the line (i.e. y = ax + b, slope is a).</param>
        private static void LinearRegression(double[] xVals, double[] yVals,
                                            int inclusiveStart, int exclusiveEnd,
                                            out double rsquared, out double yintercept,
                                            out double slope)
        {
            double sumOfX = 0;
            double sumOfY = 0;
            double sumOfXSq = 0;
            double sumOfYSq = 0;
            double ssX = 0;
            double ssY = 0;
            double sumCodeviates = 0;
            double sCo = 0;
            double count = exclusiveEnd - inclusiveStart;

            for (int ctr = inclusiveStart; ctr < exclusiveEnd; ctr++)
            {
                double x = xVals[ctr];
                double y = yVals[ctr];
                sumCodeviates += x * y;
                sumOfX += x;
                sumOfY += y;
                sumOfXSq += x * x;
                sumOfYSq += y * y;
            }
            ssX = sumOfXSq - ((sumOfX * sumOfX) / count);
            ssY = sumOfYSq - ((sumOfY * sumOfY) / count);
            double RNumerator = (count * sumCodeviates) - (sumOfX * sumOfY);
            double RDenom = (count * sumOfXSq - (sumOfX * sumOfX))
             * (count * sumOfYSq - (sumOfY * sumOfY));
            sCo = sumCodeviates - ((sumOfX * sumOfY) / count);

            double meanX = sumOfX / count;
            double meanY = sumOfY / count;
            double dblR = RNumerator / Math.Sqrt(RDenom);
            rsquared = dblR * dblR;
            yintercept = meanY - ((sCo / ssX) * meanX);
            slope = sCo / ssX;
        }
    }
}