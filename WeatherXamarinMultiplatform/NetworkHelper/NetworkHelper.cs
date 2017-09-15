﻿using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using XamarinWeather.Model;
//using Java.IO;
//using Java.Net;

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
        public async System.Threading.Tasks.Task<OpenWeatherMap> getWeatherDataAsync(String urlString)
        {
            //String stream = null;
            //try { 
            //URL url = new URL(urlString);
            //    using(var urlConnection = (HttpURLConnection)url.OpenConnection())
            //    {
            //        if (urlConnection.ResponseCode == HttpStatus.Ok) {
            //            BufferedReader r = new BufferedReader(new InputStreamReader(urlConnection.InputStream));
            //            StringBuilder sb = new StringBuilder();
            //            String line;
            //            while((line = r.ReadLine())!=null){
            //                sb.Append(line);
            //            }
            //            stream = sb.ToString();
            //            urlConnection.Disconnect();
            //        }
            //    }
            //}catch(Exception ex){
            //    System.Diagnostics.Debug.WriteLine(ex.Message);
            //}
            //return stream;
            OpenWeatherMap openWeatherMap = null;
            HttpWebRequest request = (System.Net.HttpWebRequest)WebRequest.Create(urlString);
            var response = await request.GetResponseAsync().ConfigureAwait(false);
            var stream = response.GetResponseStream();
            var streamReader = new StreamReader(stream);
            string responseText = streamReader.ReadToEnd();
            openWeatherMap = JsonConvert.DeserializeObject<OpenWeatherMap>(responseText);
            return openWeatherMap;
        }
    }
}
