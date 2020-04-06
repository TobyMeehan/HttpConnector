using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using TobyMeehan.Http;

namespace Tester
{
    class Program
    {
        public static HttpClient Client { get; set; } = new HttpClient();

        static async Task Main(string[] args)
        {
            // see https://sunrise-sunset.org/api

            Client.Init();

            Console.WriteLine("First example sun information:");
            await LoadSunInformation("https://api.sunrise-sunset.org/json?lat=36.7201600&lng=-4.4203400");
            Console.WriteLine();

            Console.WriteLine("Second example sun information:");
            await LoadSunInformation("https://api.sunrise-sunset.org/json?lat=36.7201600&lng=-4.4203400&date=today");
            Console.WriteLine();

            Console.WriteLine("Third example sun information:");
            await LoadSunInformation("https://api.sunrise-sunset.org/json?lat=36.7201600&lng=-4.4203400&date=2020-04-06");
            Console.WriteLine();

            Console.WriteLine("Final example sun information:");
            await LoadSunInformation("https://api.sunrise-sunset.org/json?lat=36.7201600&lng=-4.4203400&formatted=0");
            Console.WriteLine();

            Console.ReadLine();
        }

        static void DisplayInformation(SunResponse response)
        {
            Sun result = response.Results;

            Console.WriteLine($"Sunrise: {result.Sunrise}");
            Console.WriteLine($"Sunset: {result.Sunset}");
        }

        /// <summary>
        /// Get sun information using the library
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        static async Task LoadSunInformation(string url)
        {
            await Client.Get(url)
                .OnBadRequest<object>((response, statusCode, reasonPhrase) =>
                {
                    Console.WriteLine(reasonPhrase);
                })
                .OnOK<SunResponse>(DisplayInformation)
                .SendAsync();
        }
    }
}
