// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace WeatherXamarinMultiplatform.iOS
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel description { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel humidity { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView image { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel last_upd { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel place { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel sun_time { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel temp { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (description != null) {
                description.Dispose ();
                description = null;
            }

            if (humidity != null) {
                humidity.Dispose ();
                humidity = null;
            }

            if (image != null) {
                image.Dispose ();
                image = null;
            }

            if (last_upd != null) {
                last_upd.Dispose ();
                last_upd = null;
            }

            if (place != null) {
                place.Dispose ();
                place = null;
            }

            if (sun_time != null) {
                sun_time.Dispose ();
                sun_time = null;
            }

            if (temp != null) {
                temp.Dispose ();
                temp = null;
            }
        }
    }
}