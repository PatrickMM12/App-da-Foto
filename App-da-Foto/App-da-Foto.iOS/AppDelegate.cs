﻿
using Foundation;
using UIKit;

namespace App_da_Foto.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
     
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            global::Xamarin.Forms.Forms.Init();
            global::Xamarin.Forms.FormsMaterial.Init();
            Xamarin.FormsGoogleMaps.Init("AIzaSyAcv-1gWQrPM8JhT7hKfNGhPvPn3o_jNPA");
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
