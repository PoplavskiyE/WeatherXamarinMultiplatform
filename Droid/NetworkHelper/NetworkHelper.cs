using System;
using System.Text;
using Java.IO;
using Java.Net;

namespace XamarinWeather.NetworkHelper
{
    public class NetworkHelper{
		public static string API_KEY = "345e7286c79940d622ca2aa094bd7203";
		public static string WEATHER_URL = "http://api.openweathermap.org/data/2.5/weather";

		public static string GetRequestUrl(string lat, string lng){
			StringBuilder sb = new StringBuilder(WEATHER_URL);
			sb.AppendFormat("?lat={0}&lon={1}&APPID={2}&units", lat, lng, API_KEY);
			return sb.ToString();
		}
		public static string GetImageUrl(string icon){
			return $"http://openweathermap.org/img/w/{icon}.png";
		}
        public String getWeatherData(String urlString){
            String stream = null;
            try { 
            URL url = new URL(urlString);
                using(var urlConnection = (HttpURLConnection)url.OpenConnection())
                {
                    if (urlConnection.ResponseCode == HttpStatus.Ok) {
                        BufferedReader r = new BufferedReader(new InputStreamReader(urlConnection.InputStream));
                        StringBuilder sb = new StringBuilder();
                        String line;
                        while((line = r.ReadLine())!=null){
                            sb.Append(line);
                        }
                        stream = sb.ToString();
                        urlConnection.Disconnect();
                    }

                }
            }catch(Exception ex){
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return stream;
        }
    }
}
