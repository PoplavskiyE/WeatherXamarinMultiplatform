using System;
using System.Text;
using Java.Net;

namespace XamarinWeather.Utils
{
    public class Utils{
        public static String UnixTimeStampToDateTime(double unixTimeStamp){
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(unixTimeStamp).ToLocalTime();
            return dt.ToString("HH:mm");
        }
   }
}
