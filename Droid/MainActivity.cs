using Android.App;
using Android.Widget;
using Android.OS;
using Android.Locations;
using XamarinWeather.Model;
using Android.Runtime;
using System;
using Newtonsoft.Json;
using WeatherXamarinMultiplatform.Droid;
using Square.Picasso;
using Android.Content;

namespace XamarinWeather
{
    [Activity(Label = "XamarinWeather", MainLauncher = true,Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : Activity,ILocationListener
    {
        TextView city, lastUpdate, humidity, time, description, celsius;
        ImageView image;

        LocationManager locationManager;
        string provider;
        static double lat, lng;
        OpenWeatherMap openWeatherMap=new OpenWeatherMap();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            locationManager = (LocationManager)GetSystemService(Context.LocationService);
            provider = locationManager.GetBestProvider(new Criteria(), false);

            Location location = locationManager.GetLastKnownLocation(provider);
            if(location == null){
                System.Diagnostics.Debug.WriteLine("No location");
            }
        }

        protected override void OnResume(){
            base.OnResume();
            locationManager.RequestLocationUpdates(provider,400,1,this);
        }

        protected override void OnPause()
        {
            base.OnPause();
            locationManager.RemoveUpdates(this);
        }

        public void OnLocationChanged(Location location)
        {
            lat = Math.Round(location.Latitude,4);
            lng = Math.Round(location.Longitude,4);

            new GetWeather(this, openWeatherMap).Execute(Common.Common.APIRequest(lat.ToString(), lng.ToString()));
        }

        public void OnProviderDisabled(string provider)
        {
            
        }

        public void OnProviderEnabled(string provider)
        {
            
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras){
            
        }

        private class GetWeather : AsyncTask<String, Java.Lang.Void, String>
        {
            private ProgressDialog pd = new ProgressDialog(Application.Context);
            private MainActivity activity;
            OpenWeatherMap openWeatherMap;
            public GetWeather(MainActivity activity,OpenWeatherMap openWeatherMap){
                this.openWeatherMap = openWeatherMap;
                this.activity = activity;
            }
            protected override void OnPreExecute()
            {
                base.OnPreExecute();
                pd.Window.SetType(Android.Views.WindowManagerTypes.SystemAlert);
                pd.SetTitle("Please wait...");
                pd.Show();
            }

            protected override string RunInBackground(params string[] @params)
            {
                string stream = null;
                string urlString = @params[0];
                //urlString=Common.Common.APIRequest(lat.ToString(), lng.ToString());
				Helper.Helper http = new Helper.Helper();
                stream = http.getHTTPData(urlString);
                return stream;
            }
            protected override void OnPostExecute(string result){
                base.OnPostExecute(result);
                if (result.Contains("Error: Not found city")){
                    pd.Dismiss();
                    return;
                }
                openWeatherMap = JsonConvert.DeserializeObject<OpenWeatherMap>(result);
                pd.Dismiss();

                //Control
                activity.city = activity.FindViewById<TextView>(Resource.Id.tv_city);
                activity.humidity = activity.FindViewById<TextView>(Resource.Id.tv_humidity);
                activity.time = activity.FindViewById<TextView>(Resource.Id.tv_time);
                activity.description = activity.FindViewById<TextView>(Resource.Id.tv_description);
                activity.lastUpdate = activity.FindViewById<TextView>(Resource.Id.tv_last_update);
                activity.celsius = activity.FindViewById<TextView>(Resource.Id.tv_celsius);

                activity.image = activity.FindViewById<ImageView>(Resource.Id.imageView);

                //Add Data
                if (openWeatherMap != null)
                {

                    //Add Data
                    activity.city.Text = $"{openWeatherMap.name},{openWeatherMap.sys.country}";
                    activity.lastUpdate.Text = $"Last Updated: {DateTime.Now.ToString("dd MM yyyy HH:mm")}";
                    activity.description.Text = $"{openWeatherMap.weather[0].description}";
                    activity.humidity.Text = $"Humidity: {openWeatherMap.main.humidity} %";
                    activity.time.Text = $"{Common.Common.UnixTimeStampToDateTime(openWeatherMap.sys.sunrise).ToString("HH:mm")}/{Common.Common.UnixTimeStampToDateTime(openWeatherMap.sys.sunset).ToString("HH:mm")}";
                    activity.celsius.Text = $"Temp: {openWeatherMap.main.temp} °C";

                    if(!String.IsNullOrEmpty(openWeatherMap.weather[0].icon)){
                        Picasso.With(activity.ApplicationContext)
                               .Load(openWeatherMap.weather[0].icon)
                               .Into(activity.image);
                    }
                }
            }
        }
    }
}

