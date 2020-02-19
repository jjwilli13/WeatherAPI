using System;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherApi
{
    class Program
    {
        static void Main(string[] args)
        {

            string url = "https://api.openweathermap.org/data/2.5/weather?";

   
            Console.WriteLine("What is your zip code?");
            int userZip = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            string zipCode = $"zip={userZip},us";

   

            //pulled api key
            string key = File.ReadAllText("appsettings.Debug.json");
            JObject jObject = JObject.Parse(key);
            JToken token = jObject["ApiKey"];
            string apiKey = token.ToString();

            string apikey = $"&appid={apiKey}";

            string apiCall = $"{url}{zipCode}{apikey}";

            var httpRequest = new HttpClient();

            Task<string> response = httpRequest.GetStringAsync(apiCall);

            //Console.WriteLine(response.Result);

            //pulled temperature
            string newResponse = response.Result;
            JObject jObject1 = JObject.Parse(newResponse);
            JToken temp = jObject1["main"]["temp"];
            var newtemp = temp.ToString();
            double num = Convert.ToDouble(newtemp);
            var sum = (num * 1.8) - 459.67;
            var currentTemp = Convert.ToInt32(sum);

            //pulled feels like temperature
            JObject jObject2 = JObject.Parse(newResponse);
            JToken feelsTemp = jObject2["main"]["feels_like"];
            var newfeels = feelsTemp.ToString();
            double num1 = Convert.ToDouble(newfeels);
            var sum1 = (num1 * 1.8) - 459.67;
            var currentFeels = Convert.ToInt32(sum1);

            //pulled high temp
            JObject jObject4 = JObject.Parse(newResponse);
            JToken hightemp = jObject4["main"]["temp_max"];
            var newhigh = hightemp.ToString();
            double num2 = Convert.ToDouble(newhigh);
            var sum2 = (num2 * 1.8) - 459.67;
            var tempmax = Convert.ToInt32(sum2);

            //pulled low temp
            JObject jObject5 = JObject.Parse(newResponse);
            JToken lowtemp = jObject5["main"]["temp_min"];
            var newlow = lowtemp.ToString();
            double num3 = Convert.ToDouble(newlow);
            var sum3 = (num3 * 1.8) - 459.67;
            var tempmin = Convert.ToInt32(sum3);

            //pulled city name
            JObject jObject3 = JObject.Parse(newResponse);
            JToken cityName = jObject3["name"];
            string currentCity = cityName.ToString();


            Console.WriteLine($"The current temparature for {currentCity} is {currentTemp} degrees.");
            Console.WriteLine($"Feels like {currentFeels} degrees.");
            Console.WriteLine($"The high for {currentCity} today is {tempmax} degrees, with a low of {tempmin} degrees.");



        }
    }
}
