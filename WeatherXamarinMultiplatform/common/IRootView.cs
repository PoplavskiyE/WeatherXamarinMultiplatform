using System;
namespace WeatherXamarinMultiplatform
{
    public interface IRootView
    {
        void SetCity(String location);
        void SetDescription(String desc);
        void SetUpd(String lastUpdate);
        void SetHumidity(String humidity);
        void SetSunTime(String sunTime);
        void SetTemp(String temp);
        void loadImage(String image);
    }
}
