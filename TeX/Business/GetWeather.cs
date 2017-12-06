using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace TeX.Business
{
    public class Weather
    {
        public static string GetWeather(string location, string dia)
        {
            string results = "";
            string temperature = string.Empty;
            string max = string.Empty;
            string min = string.Empty;


            using (WebClient wc = new WebClient())
            {
                results = wc.DownloadString("https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22%" + location + "%22)%20and%20u%3D" + "\'" + "c" + "\'" + "&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");
            }

            dynamic json = JsonConvert.DeserializeObject(results);
            List<Dictionary<string, string>> listForecast = new List<Dictionary<string, string>>();
            foreach (var forecast in json.query.results.channel.item.forecast)
            {
                Dictionary<string, string> forecasts = JsonConvert.DeserializeObject<Dictionary<string, string>>(forecast.ToString());
                listForecast.Add(forecasts);
            }

            listForecast.Add(JsonConvert.DeserializeObject<Dictionary<string, string>>(json.query.results.channel.item.condition.ToString()));

            if (dia == "hoje")
            {
                listForecast[10].TryGetValue("temp", out temperature);
                listForecast[0].TryGetValue("low", out min);
                listForecast[0].TryGetValue("high", out max);
                return "A temperatura para hoje para " + location + " é de " + temperature + " com mínima de " + min + " e máxima de " + max + ".";
            }
            else if (dia == "amanhã")
            {
                listForecast[0].TryGetValue("low", out min);
                listForecast[0].TryGetValue("high", out max);
                return "A temperatura para amanhã em " + location + " está com mínima prevista para " + min + " e máxima de " + max + ".";
            }
            else
            {
                listForecast[10].TryGetValue("temp", out temperature);
                return "A temperatura para hoje para " + location + " é de " + temperature + " com mínima de " + min + " e máxima de " + max + ".";
            }
        }
    }
}