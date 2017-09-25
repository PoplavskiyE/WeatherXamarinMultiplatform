using System;
using FFImageLoading;
using FFImageLoading.Work;
using UIKit;

namespace WeatherXamarinMultiplatform.iOS
{
    public partial class ViewController : UIViewController,IRootView

    {
        private RootPresenter rp;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            rp = new RootPresenter(this);
            place.AccessibilityIdentifier = "place";
            last_upd.AccessibilityIdentifier = "last_upd";
            image.AccessibilityIdentifier = "image";
            description.AccessibilityIdentifier = "description";
            humidity.AccessibilityIdentifier = "humidity";
            sun_time.AccessibilityIdentifier = "sun_time";
            temp.AccessibilityIdentifier = "temp";

            rp.getWeather("53,9", "27,57");

			//place.Text = "Vyaliki Trastsyanets, BY";
			//last_upd.Text = "Last Updated: 01.07.1991 11:45";
			//description.Text = "clear sky";
			//humidity.Text = "Humidity: 62%";
			//sun_time.Text = "Sunrise: 7:01 / Sunset: 18:59";
			//temp.Text = "Temp: 14 °C";


            // Perform any additional setup after loading the view, typically from a nib.
            //Button.AccessibilityIdentifier = "myButton";
            //Button.TouchUpInside += delegate
            //{
            //    var title = string.Format("{0} clicks!", count++);
            //    Button.SetTitle(title, UIControlState.Normal);
            //};


        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.		
        }

        public void SetCity(string locationName)
        {
            place.Text = locationName;
        }

        public void SetDescription(string desc)
        {
            description.Text = desc;
        }

        public void SetUpd(string upd)
        {
            last_upd.Text = upd;
        }

        public void SetHumidity(string humidity)
        {
            this.humidity.Text = humidity;
        }

        public void SetSunTime(string sunTime)
        {
            sun_time.Text = sunTime;
        }

        public void SetTemp(string temp)
        {
            this.temp.Text = temp;
        }

        public void loadImage(string imageUrl)
        {
            ImageService.Instance.LoadUrl(imageUrl)
						.LoadingPlaceholder("placeholder", ImageSource.CompiledResource)
							.Into(image);
        }
    }
}
