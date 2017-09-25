using System;
using XamarinWeather.Model;
using XamarinWeather.NetworkHelper;
using XamarinWeather.Utils;

namespace WeatherXamarinMultiplatform
{
    public class RootPresenter
    {
        public IRootView rv;
        public RootPresenter(IRootView rv){
            this.rv = rv;
        }
        public void getWeather(String lat,String lng){
            OpenWeatherMap o = new NetworkHelper().getWeatherDataAsync(NetworkHelper.GetRequestUrl(lat,lng)).Result;
            rv.SetCity($"{o.name},{o.sys.country}");
            rv.SetUpd($"Last Updated: {DateTime.Now.ToString("dd MM yyyy HH:mm")}");
            rv.SetDescription($"{o.weather[0].description}");
            rv.SetHumidity($"Humidity: {o.main.humidity} %");
            rv.SetSunTime($"Sunrise: {Utils.UnixTimeStampToDateTime(o.sys.sunrise)}/Sunset: {Utils.UnixTimeStampToDateTime(o.sys.sunset)}");
			
			var t = o.main.temp - 273.16;
            rv.SetTemp($"Temp: {Math.Round(t)} °C");

			if (!String.IsNullOrEmpty(o.weather[0].icon))
			{
                rv.loadImage(NetworkHelper.GetImageUrl(o.weather[0].icon));
			}
        }
    }
}
