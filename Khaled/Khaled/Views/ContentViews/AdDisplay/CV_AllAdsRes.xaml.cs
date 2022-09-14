using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Khaled.Backend.APIs;
using Khaled.Helpers;
using Khaled.Resources;
using Khaled.Views.ContentViews.Categories;
using Khaled.Views.MainMenu;
using Khaled.Views.PopUps;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Khaled.Views.ContentViews.MainMenuTabs
{	
	public partial class CV_AllAdsRes : ContentView
	{
        ObservableCollection<AdsType> mainList = new ObservableCollection<AdsType>();
        public static CV_AllAdsRes instance;
        string info, info2;
        public Label filterCity;
        List<CategoryIds> idList;
        AdsListType adsListType;

        //filter values
        public int radiusValue = Constants.sliderBaseValue;
        public double startPosLong = 0;
        public double startPosLat = 0;

        public CV_AllAdsRes (List<CategoryIds> idList)
		{
            adsListType = AdsListType.mainView;

            InitializeComponent();

            this.idList = idList;
            instance = this;
            filterCity = label_filter_city;

            StartUp();

        }

        public CV_AllAdsRes(AdsListType adsListType, string info)
        {
            InitializeComponent();

            this.adsListType = adsListType;
            this.info = info; 
            instance = this;
            filterCity = label_filter_city;

            StartUp();

        }

        public CV_AllAdsRes(AdsListType adsListType, string info, string info2)
        {
            InitializeComponent();

            this.adsListType = adsListType;
            this.info = info;
            this.info2 = info2;
            instance = this;
            filterCity = label_filter_city;

            StartUp();


        }

        async void StartUp()
        {
            if (CachedUser.localCode == localCodes.ar)
                this.FlowDirection = FlowDirection.RightToLeft;
            else
                this.FlowDirection = FlowDirection.LeftToRight;

            if (CachedUser.cityPicked)
            {
                radiusValue = CachedUser.pickedCity.radiusFromCenter;
                startPosLat = CachedUser.pickedCity.centerLatitude;
                startPosLong = CachedUser.pickedCity.centerLongitude;
                label_filter_city.Text = CachedUser.pickedCity.displayName;

                radiusValue = CachedUser.pickedCity.radiusFromCenter;
                slider_sliderRadius.Value = radiusValue;
                label_radius_filter.Text = "+" + CachedUser.pickedCity.radiusFromCenter.ToString() + "km";

            }
            else
            {
                if (CachedUser.longi != 0)
                {
                    if (Debugger.IsAttached)
                    {
                         CachedUser.lati = 53.64219;    //results from hoisberg
                         CachedUser.longi = 10.17745;   //results from hoisberg
                         radiusValue = 200;             //results from hoisberg
                    }

                    startPosLat = CachedUser.lati;
                    startPosLong = CachedUser.longi;
                    radiusValue = Constants.sliderBaseValue;
                    label_filter_city.Text = AppResources.YouCurrentLocation;
                    slider_sliderRadius.Value = radiusValue;
                    label_radius_filter.Text = "+" + radiusValue.ToString() + "km";
                }
            }

            frame_radiusFilter.IsVisible = true;

            await LoadMainContent(false);
        } 

        public async Task SetCityFilter(bool reset, double posLong, double posLat)
        {
            if (!reset)
            {
                //set pos filter
                startPosLong = posLong;
                startPosLat = posLat;
                frame_radiusFilter.IsVisible = true;
                RefreshMainView();
            }
            else
            {
                //reset pos filter
                frame_radiusFilter.IsVisible = false;
            }
        }

        private async Task LoadMainContent(bool loadMore)
        {
            int loadId = 0;
            if (idList != null && idList.Count != 0)
                loadId = idList[0].tblCategoryIdID; // primary key to load Ids with

            if (!loadMore)
            {
                mainList = await ReloadAdapter.LoadAllAds(start: 0,
                    count: Constants.AdsToLoadAtOnce,
                    radius: radiusValue,
                    inputLat: startPosLat,
                    inputLong: startPosLong,
                    categoryId: loadId,
                    info2: info2,
                    info: info,
                    adsListType: adsListType); 
                listview_mainView.ItemsSource = mainList;

                CV_TopCats.Instance.LoadDummyData(); // first load

            }
            else
            {
                var x = await ReloadAdapter.LoadAllAds(start: mainList.Count,
                    count: Constants.AdsToLoadAtOnce,
                    radius: radiusValue,
                    inputLat: startPosLat,
                    inputLong: startPosLong,
                    categoryId: loadId,
                    info: info,
                    info2: info2,
                    adsListType: adsListType); ;
                foreach (var item in x)
                    mainList.Add(item);
            }

            if (mainList.Count == 0)
                label_empty.IsVisible = true;
            else
                label_empty.IsVisible = false;

        }

        async void listview_mainView_Refreshing(System.Object sender, System.EventArgs e)
        {
            listview_mainView.IsRefreshing = true;
            await RefreshMainView();
            listview_mainView.IsRefreshing = false;
        }

        async Task RefreshMainView()
        {
            mainList.Clear();
            listview_mainView.ItemsSource = null;
            await LoadMainContent(false);
        }

        async void Clicked_AcceptRadius(System.Object sender, System.EventArgs e)
        {
            label_radius_filter.Text = "+" + radiusValue.ToString()+" " + AppResources.KM;
            layout_slider.IsVisible = false;
            await RefreshMainView();
        }
      
        void Clicked_AbortRadius(System.Object sender, System.EventArgs e)
        {
            layout_slider.IsVisible = false;
            label_radius_filter.Text = AppResources.Radius;
            ResetRadiusFilter();
        }

        void TAPPED_radius(System.Object sender, System.EventArgs e)
        {
            if (!layout_slider.IsVisible)
                layout_slider.IsVisible = true;
            else
                layout_slider.IsVisible = false;
        }

        void TAPPED_frame(System.Object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopUp_Template(PopUpTypes.changeLocation));
        }

        void Slider_ValueChanged(System.Object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            try
            {
                string value = e.NewValue.ToString();
                int index = 0;
                if (value.Contains(","))
                {
                    index = value.LastIndexOf(",");
                    value = value.Substring(0, index);
                }
                else if (value.Contains("."))
                {
                    index = value.LastIndexOf(".");
                    value = value.Substring(0, index);
                }
                label_radius.Text = value + " " + AppResources.KM;
                radiusValue = Int32.Parse(value);
            }
            catch (Exception  ex)
            {

            }
       
        }

        async void ResetRadiusFilter()
        {
            label_radius_filter.Text = "+50" + " " + AppResources.KM;
            label_radius.Text = "50" + " " + AppResources.KM;
            slider_sliderRadius.Value = Constants.sliderBaseValue;
            radiusValue = Constants.sliderBaseValue;
            await RefreshMainView();
        }

        async void listview_mainView_ItemAppearing(System.Object sender, Xamarin.Forms.ItemVisibilityEventArgs e)
        {
            var item = e.Item as AdsType;

            if (adsListType == AdsListType.favorites)
                return; // do no reload for now 

            if (item == null)
                return;

            if (item == mainList[mainList.Count - 1])
                await LoadMainContent(true);

        }

        async void listview_mainView_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            listview_mainView.SelectedItem = null;
            var clickedItem = e.Item as AdsType;
            if (clickedItem == null)
                return;
            await Navigation.PushAsync(new Page_FullAd(clickedItem));
        }
    }

    public enum AdsListType
    {
        mainView = 0,
        favorites = 1,
        title  = 2,
        inCat1 = 3,
        inCat2 = 4,
        titleIncat1 = 5,
        titleIncat2 = 6
    }
}

