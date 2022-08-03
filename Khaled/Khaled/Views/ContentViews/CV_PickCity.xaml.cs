using System;
using System.Collections.Generic;
using Khaled.Backend.APIs;
using Khaled.Helpers;
using Khaled.Resources;
using Khaled.Views.ContentViews.MainMenuTabs;
using Khaled.Views.MainMenu;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Khaled.Views.ContentViews
{
    public partial class CV_PickCity : ContentView
    {
        bool cityPicked = false;
        List<CitiesType> cityList = new List<CitiesType>();
        public CV_PickCity()
        {
            InitializeComponent();

            if (CV_AllAdsRes.instance.filterCity.Text != AppResources.YouCurrentLocation)
                switch_main.IsToggled = true; 

            FillPicker();

            SetMap();
        }

        private void SetMap()
        {
            // demo values of hamburg city center
            if (CachedUser.lati != 0)
            {
                maps_map.MoveToRegion(MapSpan.FromCenterAndRadius
                      (new Position(CachedUser.lati, CachedUser.longi),
                      Distance.FromMeters(500)));
            }
            else
            {
                maps_map.IsEnabled = false;
                maps_map.IsVisible = false;
            }

        }

        private async void FillPicker()
        {
            cityList = CityAPI.Deserialize(await CityAPI.GetAllCities());

            switch (CachedUser.localCode)
            {
                case localCodes.en:
                    picker_pickCity.ItemDisplayBinding = new Binding("cityName");
                    break;
                case localCodes.de:
                    picker_pickCity.ItemDisplayBinding = new Binding("cityNameGER");
                    break;
                case localCodes.ar:
                    picker_pickCity.ItemDisplayBinding = new Binding("cityNameAR");
                    break; 
            }

            picker_pickCity.ItemsSource = cityList;

            picker_pickCity.SelectedIndex = 0; 
        }

        void Slider_ValueChanged(System.Object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            try
            {
                string value = e.NewValue.ToString();
                int index = value.LastIndexOf(",");
                value = value.Substring(0, index);
            }
            catch { }

        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            string nameOfCity = "";
            if (cityPicked)
            {
                switch (CachedUser.localCode)
                {
                    case localCodes.en:
                        nameOfCity = cityList[picker_pickCity.SelectedIndex].cityName;
                        break;
                    case localCodes.de:
                        nameOfCity = cityList[picker_pickCity.SelectedIndex].cityNameGER;
                        break;
                    case localCodes.ar:
                        nameOfCity = cityList[picker_pickCity.SelectedIndex].cityNameAR;
                        break;
                }

                CV_AllAdsRes.instance.filterCity.Text = nameOfCity;
                await CV_AllAdsRes.instance.SetCityFilter(false, cityList[picker_pickCity.SelectedIndex].longitudeCityCenter, cityList[picker_pickCity.SelectedIndex].latitudeCityCenter);
            }
            else
            {
                CV_AllAdsRes.instance.filterCity.Text = AppResources.YouCurrentLocation;
                await CV_AllAdsRes.instance.SetCityFilter(false, CachedUser.longi, CachedUser.lati);

            }
            Page_MainMenuDetail.instance.ClosePopUpTemplate(); 
        }

        void Switch_Toggled(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            if (e.Value)
            {
                cityPicked = true;
                layout_cityCenter.IsVisible = true;
                layout_location.IsVisible = false; 
            }
            else
            {
                cityPicked = false; 
                layout_cityCenter.IsVisible = false;
                layout_location.IsVisible = true;
            }
        }
    }
}
