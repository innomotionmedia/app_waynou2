using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Content.Res;
using Android.Util;
using Plugin.CrossPlatformTintedImage.Android;

namespace Khaled.Droid
{
    [Activity(Label = "Khaled", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        // KUMULOS WORKING PACKAGES IS 6.1.1

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            TintedImageRenderer.Init();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            initFontScale();
            LoadApplication(new App());

            // change status bar colo
            Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 204, 204, 0));


        }

        public override void OnBackPressed()
        {
            // needed for android popup backbutton
            Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void initFontScale()
        {
            Configuration configuration = Resources.Configuration;
            configuration.FontScale = (float)1;
            //0.85 small, 1 standard, 1.15 big，1.3 more bigger ，1.45 supper big 
            DisplayMetrics metrics = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(metrics);
            metrics.ScaledDensity = configuration.FontScale * metrics.Density;
            BaseContext.ApplicationContext.CreateConfigurationContext(configuration);
            BaseContext.Resources.DisplayMetrics.SetTo(metrics);
        }
    }
}
