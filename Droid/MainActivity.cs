using Android.App;
using Android.Widget;
using Android.OS;
using Android.Locations;
using XamarinWeather.Model;
using Android.Runtime;
using System;
using WeatherXamarinMultiplatform.Droid;
using Square.Picasso;
using Android.Content;
using WeatherXamarinMultiplatform;

namespace XamarinWeather
{
    [Activity(Label = "XamarinWeather", MainLauncher = true,Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : Activity,ILocationListener,IRootView
    {
        private RootPresenter rp;
        private TextView city, lastUpdate, humidity, time, description, celsius,errorText;
        ImageView image;

        LocationManager locationManager;
        string provider;
        static double lat, lng;
        OpenWeatherMap openWeatherMap=new OpenWeatherMap();

        protected override void OnCreate(Bundle savedInstanceState){
            base.OnCreate(savedInstanceState);
            rp = new RootPresenter(this);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

			city = FindViewById<TextView>(Resource.Id.tv_city);
			humidity = FindViewById<TextView>(Resource.Id.tv_humidity);
			time = FindViewById<TextView>(Resource.Id.tv_time);
			description = FindViewById<TextView>(Resource.Id.tv_description);
			lastUpdate = FindViewById<TextView>(Resource.Id.tv_last_update);
			celsius = FindViewById<TextView>(Resource.Id.tv_celsius);
            errorText = FindViewById<TextView>(Resource.Id.tv_error_text);
			image = FindViewById<ImageView>(Resource.Id.imageView);

            initLocationManager();
        }

        private void initLocationManager()
        {
			locationManager = (LocationManager)GetSystemService(Context.LocationService);

            //provider = locationManager.GetBestProvider(new Criteria(), false);
            provider = LocationManager.NetworkProvider;
			//Criteria criteria = new Criteria
			//{
				//Accuracy = Accuracy.Fine
			//};
            //IList<String> acceptableProviders = locationManager.GetProviders(criteria, true);
            //if(acceptableProviders.Any()){
            //    provider = acceptableProviders.First();
            //}else{
            //    provider = string.Empty;
            //}
			Location location = locationManager.GetLastKnownLocation(provider);
			OnLocationChanged(location);
        }

        protected override void OnResume(){
            base.OnResume();
            locationManager.RequestLocationUpdates(provider,120000,10,this);

        }

        protected override void OnPause()
        {
            base.OnPause();
            locationManager.RemoveUpdates(this);
        }

        public void OnLocationChanged(Location location)
        {
            if (location == null){
                System.Diagnostics.Debug.WriteLine(GetString(Resource.String.error_no_location_lable));
                errorText.Visibility = Android.Views.ViewStates.Visible;
                errorText.Text = GetString(Resource.String.error_no_location_message);
                startRequestWeather("53,9","27,57");;
            }else {
                errorText.Visibility = Android.Views.ViewStates.Gone;
                lat = Math.Round(location.Latitude, 4);
                lng = Math.Round(location.Longitude, 4);
                startRequestWeather(lat.ToString(), lng.ToString());
            }
        }

        public void OnProviderDisabled(string provider){}

        public void OnProviderEnabled(string provider){}

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras){}

        public void startRequestWeather(string lat,string lng){
            rp.getWeather(lat,lng);
        }
        public void SetCity(string locationName)
        {
            city.Text = locationName;
        }

        public void SetDescription(string desc)
        {
            description.Text = desc;
        }

        public void SetUpd(string lastUpdate)
        {
            this.lastUpdate.Text = lastUpdate;
        }

        public void SetHumidity(string humidity)
        {
            this.humidity.Text = humidity;
        }

        public void SetSunTime(string sunTime)
        {
            time.Text = sunTime;
        }

        public void SetTemp(string temp)
        {
            celsius.Text = temp;
        }

        public void loadImage(string imageUrl)
        {
               Picasso.With(this)
                      .Load(imageUrl)
                               .Into(image);
        }
    }
}

