using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PolygonOptionsAPI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("secrets.json")
                .Build();

            var apiKey = configuration.GetValue<string>("PolygonAPIKey");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.polygon.io/v2/");
                client.DefaultRequestHeaders.Add("Api-Key", apiKey);
                string key = "?apiKey=" + apiKey;
                var response = await client.GetAsync("aggs/ticker/AAPL/range/1/day/2023-01-09/2023-01-09" + key);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(responseContent);
                }
                else
                {
                    Console.WriteLine("Request failed with status code: " + response.StatusCode);
                }
            }
        }
    }
}
