using Newtonsoft.Json;
using StocksMasterClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StocksMasterClient.Services
{
    public class StocksMasterAPIClient : IStocksMasterAPIClient
    {
        // API Configuration
        //private const string BASE_ADDRESS = "http://stocksmasterapi-dev.us-east-1.elasticbeanstalk.com/";
        private const string MAPPING_PREFIX = "api";

        // Proxy Configuration
        //private const string BASE_ADDRESS = "http://mdelara2-eval-test.apigee.net/";
        //private const string MAPPING_PREFIX = "stocksmasterapiproxy/api";
        //private const string API_KEY = "4Sux64oLntHA4nlGRUOWOKHQGKGm3eOG";

        // Local Configuration
        private const string BASE_ADDRESS = "http://localhost:51665/";

        private HttpClient client;

        public StocksMasterAPIClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_ADDRESS);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("apikey", API_KEY);
        }

        // COMPANIES
        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            HttpResponseMessage response = await client.GetAsync($"{MAPPING_PREFIX}/Companies");
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Company>>(await response.Content.ReadAsStringAsync());
            return null;
        }

        public async Task<Company> GetCompanyById(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{MAPPING_PREFIX}/Companies/{id}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<Company>();
            return null;
        }

        // STOCKSDATUMS
        public async Task<IEnumerable<StocksDatum>> GetAllStocksData()
        {
            HttpResponseMessage response = await client.GetAsync($"{MAPPING_PREFIX}/StocksDatums?includeId=true");
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<StocksDatum>>(await response.Content.ReadAsStringAsync());
            return null;
        }

        public async Task<StocksDatum> GetStocksDatumById(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{MAPPING_PREFIX}/StocksDatums/{id}?includeId=true");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<StocksDatum>();
            return null;
        }

        public async Task<StocksDatum> Add(StocksDatum stocksDatum)
        {
            String json = JsonConvert.SerializeObject(stocksDatum);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{MAPPING_PREFIX}/StocksDatums?includeId=true", content);
            if (response.IsSuccessStatusCode)            
                return await response.Content.ReadAsAsync<StocksDatum>();            
            return null;
        }

        public async Task<StocksDatum> Update(StocksDatum stocksDatum)
        {
            String json = JsonConvert.SerializeObject(stocksDatum);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"{MAPPING_PREFIX}/StocksDatums/{stocksDatum.StocksDataId}?includeId=true", content);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<StocksDatum>();
            return null;
        }

        public async Task<StocksDatum> Delete(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{MAPPING_PREFIX}/StocksDatums/{id}?includeId=true");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<StocksDatum>();
            return null;
        }
    }
}