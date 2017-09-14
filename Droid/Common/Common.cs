using System;
using System.Text;

namespace XamarinWeather.Common
{
    public class Common
    {
        public static string API_KEY = "345e7286c79940d622ca2aa094bd7203";
        public static string API_LINK = "http://api.openweathermap.org/data/2.5/weather";

        public static string APIRequest(string lat,string lng){
            StringBuilder sb = new StringBuilder(API_LINK);
            sb.AppendFormat("?lat={0}&lon={1}&APPID={2}&units",lat,lng,API_KEY);
            return sb.ToString();
        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp){
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(unixTimeStamp).ToLocalTime();
            return dt;
        }
        public static string GetImage (string icon){
            return $"http://openweathermap.org/img/w/{icon}.png";
        }
        public Common()
        {}
    }
}
