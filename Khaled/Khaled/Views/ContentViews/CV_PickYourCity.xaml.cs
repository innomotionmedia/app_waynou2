using System;
using System.Collections.Generic;
using Khaled.Backend.Helpers;
using Khaled.Helpers;
using Khaled.Resources;
using Khaled.Views.PopUps;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Khaled.Views.ContentViews
{	
	public partial class CV_PickYourCity : ContentView
	{
        bool changeLocation = false;

		public CV_PickYourCity (bool changeLocation)
		{
            this.changeLocation = changeLocation; 
			InitializeComponent ();
            SetCities();
            SetPicker();

            SetMap();
		}

        private void SetCities()
        {
            var cityList = GetSupportedCites.ReturnCityList();
            layout_cities.ItemsSource = cityList;
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

        private void SetPicker()
        {
            List<string> content = new List<string>();
            content.Add(AppResources.Germany);
            content.Add(AppResources.YourGpsLocation);
            picker_head.ItemsSource = content;
        }

        void Slider_ValueChanged(System.Object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            try
            {
                string value = e.NewValue.ToString();
                int index = 0;
                if (value.Contains(","))
                    index = value.LastIndexOf(",");
                else
                    index = value.LastIndexOf(".");
                value = value.Substring(0, index);
            }
            catch (Exception ex)
            {

            }

        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            PopUp_Template.instance.ClickedOnClosed(null, null); 
        }

        void picker_head_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            var picker = sender as Picker;
            if (picker.SelectedItem == null)
                return; 

            if(picker.SelectedItem.ToString() == AppResources.Germany)
            {
                layout_cities.IsVisible = true;
                layout_gps.IsVisible = false; 
            }
            else if (picker.SelectedItem.ToString() == AppResources.YourGpsLocation)
            {
                layout_gps.IsVisible = true;
                layout_cities.IsVisible = false;
            }
        }

        void layout_cities_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var view = sender as ListView;
            var item = view.SelectedItem as SupportedCities;
            if (item == null)
                return;

            CachedUser.pickedCity = item;
            CachedUser.cityPicked = true;

            Preferences.Set(Constants.PREFKEY_CITYPICKED, true);
            // need to be string because of , or . type error
            Preferences.Set(Constants.PREFKEY_CITYLAT, item.centerLatitude.ToString());
            Preferences.Set(Constants.PREFKEY_CITYLONG, item.centerLongitude.ToString());
            Preferences.Set(Constants.PREFKEY_CITYRAD, item.radiusFromCenter);

            PopUp_Template.instance.ClickedOnClosed(null, null);

            if(!changeLocation)
                MainPage.Instance.GoToMainPage();
            else
            {
                //todo not just restart the app 
                MainPage.Instance.GoToMainPage();
            }

        }
    }
}

