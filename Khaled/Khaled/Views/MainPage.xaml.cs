using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Khaled.CustomComps;
using Khaled.Helpers;
using Khaled.Resources;
using Khaled.Views.MainMenu;
using Khaled.Views.PopUps;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Khaled
{

    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Language> Languages { get; set; }
        public Language SelectedLanguage { get; set; }
        private List<DisplayedLanguage> supportedLanguages = new List<DisplayedLanguage>();
        public static MainPage Instance;
        bool gpsGranted = false; 
        bool comingFromBurger;

        public MainPage(bool preSelectLingua, bool comingFromBurger)
        {
            Instance = this;
            this.comingFromBurger = comingFromBurger;

             InitializeComponent();


            GetSupportedLanguages();

            if(Preferences.Get(Constants.CHOSENLANG, "") == "en")
            {
                LocalizationResourceManager.Instance.SetCulture(CultureInfo.GetCultureInfo("en"));
                CachedUser.localCode = localCodes.en;
            }
            if (Preferences.Get(Constants.CHOSENLANG, "") == "de")
            {
                LocalizationResourceManager.Instance.SetCulture(CultureInfo.GetCultureInfo("de"));
                CachedUser.localCode = localCodes.de;
            }
            if (Preferences.Get(Constants.CHOSENLANG, "") == "ar")
            {
                LocalizationResourceManager.Instance.SetCulture(CultureInfo.GetCultureInfo("ar"));
                CachedUser.localCode = localCodes.ar;

            }

            if (CachedUser.localCode == localCodes.ar)
                this.FlowDirection = FlowDirection.RightToLeft;
            else
                this.FlowDirection = FlowDirection.LeftToRight;


            if (preSelectLingua)
                SetPreLingua();

            carouselview.CurrentItemChanged += Carouselview_CurrentItemChanged;


        }

        private void Carouselview_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            var item = e.CurrentItem as Helpers.DisplayedLanguage;

            ChangeLocal(item.languageCode);
        }

        private void GetSupportedLanguages()
        {
            supportedLanguages = Helpers.SupportedLanguages.ReturnLanguages();
            carouselview.ItemsSource = supportedLanguages; 
        }

        private void SetPreLingua()
        {
            switch (CachedUser.localCode)
            {
                case localCodes.en:
                    carouselview.Position = 1;
                    break;
                case localCodes.ar:
                    carouselview.Position = 2;
                    break;
                case localCodes.de:
                    carouselview.Position = 0;
                    break;
            }
        }

        private async void LoadUp()
        {
            await GetCurrentDeviceLocation();

            if (!comingFromBurger)
            {
                await CreateKumulosContext();
                if (Preferences.Get("GPSFOUND", false))
                    GoToMainPage();
            }

        }

        private async Task<bool> GetCurrentDeviceLocation()
        {
            CancellationTokenSource cts;         
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();

                var currentDeviceLocation = await Geolocation.GetLocationAsync(request, cts.Token);

                if (currentDeviceLocation != null)
                {
                    CachedUser.lati = currentDeviceLocation.Latitude;
                    CachedUser.longi = currentDeviceLocation.Longitude;
                    CachedUser.radius = Constants.sliderBaseValue;
                    btn_useMyLocation.Text = AppResources.Go;
                    ReverseGeoCodeAdresse(currentDeviceLocation);
                    gpsGranted = true;
                    return true;
                }
                else
                    return false; 
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                return false; 
            }
            catch (FeatureNotEnabledException fneEx)
            {
                return false; 
            }
            catch (PermissionException pEx)
            {
                return false; 
            }
            catch (Exception ex)
            {
                return false; 
            }
           
        }

        private async void ReverseGeoCodeAdresse(Location currentDeviceLocation)
        {
          
            var placemarks = await Geocoding.GetPlacemarksAsync(currentDeviceLocation.Latitude, currentDeviceLocation.Longitude);

            var placemark = placemarks?.FirstOrDefault();
            if (placemark != null)
            {
                var geocodeAddress =
                    $"AdminArea:       {placemark.AdminArea}\n" +
                    $"CountryCode:     {placemark.CountryCode}\n" +
                    $"CountryName:     {placemark.CountryName}\n" +
                    $"FeatureName:     {placemark.FeatureName}\n" + // house number.. again
                    $"Locality:        {placemark.Locality}\n" + // city
                    $"PostalCode:      {placemark.PostalCode}\n" + // zip
                    $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                    $"SubLocality:     {placemark.SubLocality}\n" +
                    $"SubThoroughfare: {placemark.SubThoroughfare}\n" + // house number
                    $"Thoroughfare:    {placemark.Thoroughfare}\n"; // street
            }

            label_locality.Text = placemark.Locality;
        }

        private async Task CreateKumulosContext()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
                await KumulosHelper.InitKumulos();            
        }

        private void ChangeLocal(string v)
        {
            // do not change flow dir here, it will mess with the carouselview
            switch (v)
            {
                case "german":
                    LocalizationResourceManager.Instance.SetCulture(CultureInfo.GetCultureInfo("de"));
                    CachedUser.localCode = localCodes.de;
                    Preferences.Set(Constants.CHOSENLANG, "de");
                    break;
                case "arabic":
                    LocalizationResourceManager.Instance.SetCulture(CultureInfo.GetCultureInfo("ar"));
                    CachedUser.localCode = localCodes.ar;
                    Preferences.Set(Constants.CHOSENLANG, "ar");
                    break;
                case "english":
                    LocalizationResourceManager.Instance.SetCulture(CultureInfo.GetCultureInfo("en"));
                    CachedUser.localCode = localCodes.en;
                    Preferences.Set(Constants.CHOSENLANG, "en");
                    break;                  
            }

            LoadUp();

        }

        public async void GoToMainPage()
        {
            if(gpsGranted)
                Preferences.Set("GPSFOUND", true);

            string lat ="", longi ="";


            if (Preferences.Get(Constants.PREFKEY_CITYPICKED, false))
            {
                lat = Preferences.Get(Constants.PREFKEY_CITYLAT, "");
                longi = Preferences.Get(Constants.PREFKEY_CITYLONG, "");           
            }
            else
            {
                await GetCurrentDeviceLocation();
                lat = CachedUser.lati.ToString();
                longi = CachedUser.longi.ToString();
            }

            CachedUser.lati = Double.Parse(lat);
            CachedUser.longi = Double.Parse(longi);
            CachedUser.radius = Preferences.Get(Constants.PREFKEY_CITYRAD, 0);

            Application.Current.MainPage = new Page_MainMenu();
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var btn = sender as Button;

            if (btn.Equals(btn_useMyLocation))
            {
                if (gpsGranted)
                {
                    CachedUser.cityPicked = false;
                    CachedUser.pickedCity = null;
                    Preferences.Set(Constants.PREFKEY_CITYPICKED, false);
                    GoToMainPage();
                }
                else
                {
                    //bring up request for gps again
                    var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                    if(await GetCurrentDeviceLocation())
                    {
                        if (status == PermissionStatus.Denied)
                        {
                            await DisplayAlert(AppResources.Error, AppResources.GpsDenied, AppResources.Okay);
                        }
                    }
                }

            }

            if (btn.Equals(btn_linguaLeft))
            {
                 if (carouselview.Position == 0)
                     carouselview.Position = supportedLanguages.Count - 1;
                 else
                     carouselview.Position = carouselview.Position - 1;
            }
            else if (btn.Equals(btn_linguaRight))
            {
                if (carouselview.Position == supportedLanguages.Count - 1)
                    carouselview.Position = 0;
                else
                    carouselview.Position = carouselview.Position + 1;
            }
            else if (btn.Equals(open_cityPicker))
                 PopupNavigation.Instance.PushAsync(new PopUp_Template(PopUpTypes.pickFirstLocation));

        }

    }


    public class Language
    {
        public string Name { get; set; }
        public string CI { get; set; }

        public Language(string name, string ci)
        {
            Name = name;
            CI = ci;
        }

    }

}

