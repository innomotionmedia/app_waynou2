using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Khaled.Backend.APIs;
using Khaled.Helpers;
using Khaled.Resources;
using Khaled.Views.ContentViews.AdDisplay;
using Khaled.Views.ContentViews.MainMenuTabs;
using Khaled.Views.MainMenu;
using Syncfusion.XForms.ComboBox;
using TodoApp.Data;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Khaled.Views.ContentViews.Categories
{
	public partial class Page_SubCategories : ContentView
	{
		// can be inflated as first or second.
		ObservableCollection<AdsType> mainList = new ObservableCollection<AdsType>();
		public static CV_AllAdsRes instance;
		string info;
		public Label filterCity;
		List<CategoryIds> idList;
		AdsListType adsListType;

		//filter values
		public int radiusValue = Constants.sliderBaseValue;
		public double startPosLong = 0;
		public double startPosLat = 0;
		int prevCat = 0;

		ObservableCollection<SubCatType> list = new ObservableCollection<SubCatType>();

		public Page_SubCategories(CategoriesEnum cat, string title)
		{
			InitializeComponent();
			Constants.CurrentCatWeWantToLoad.name = cat.ToString();
            Constants.CurrentCatWeWantToLoad.id = ((int)cat).ToString();
			Constants.WhatPositionAmIOnRightNow = WhatPositionAmIOnRightNow.IamInJustIntoMainCategories;
			text_pickCat.Text = title;
			StartUp();
		}

		public Page_SubCategories(SubCatType item)
		{
			InitializeComponent();
			Constants.CurrentCatWeWantToLoad.name = item.title;
			Constants.CurrentCatWeWantToLoad.id = item.Id;
			Constants.WhatPositionAmIOnRightNow = WhatPositionAmIOnRightNow.IamInTheSubCategorie;
			text_pickCat.Text = item.titleDe;
			StartUp();
		}

		private async void StartUp()
		{
            if (CachedUser.localCode == localCodes.ar)
			{
                this.FlowDirection = FlowDirection.RightToLeft;
				img_backbutton.Rotation = 0; 

            }
            else
                this.FlowDirection = FlowDirection.LeftToRight;

            if (CachedUser.cityPicked)
			{
				radiusValue = CachedUser.pickedCity.radiusFromCenter;
                radiusValue = CachedUser.radius;
                if (radiusValue == 0) radiusValue = 1;
                startPosLat = CachedUser.pickedCity.centerLatitude;
				startPosLong = CachedUser.pickedCity.centerLongitude;
			}
			else
			{

				radiusValue = CachedUser.radius;
				if (radiusValue == 0) radiusValue = 1;
				startPosLat = CachedUser.lati;
				startPosLong = CachedUser.longi;
			}


			await GetCats();
            await LoadMainContent(false);

        }


		private async Task LoadMainContent(bool loadMore)
		{
			AdsListType currentType = 0;

			if (Constants.WhatPositionAmIOnRightNow == WhatPositionAmIOnRightNow.IamInJustIntoMainCategories)
				currentType = AdsListType.inCat1;
			else if (Constants.WhatPositionAmIOnRightNow == WhatPositionAmIOnRightNow.IamInTheSubCategorie)
                currentType = AdsListType.inCat2;

            if (!loadMore)
			{
				mainList = await ReloadAdapter.LoadAllAds(start: 0,
					count: Constants.AdsToLoadAtOnce,
					radius: radiusValue,
					inputLat: startPosLat,
					inputLong: startPosLong,
					categoryId: "",
					info: Constants.CurrentCatWeWantToLoad.id,
					adsListType: currentType);
				listview_mainViews.ItemsSource = mainList;

			}
			else
			{
				var x = await ReloadAdapter.LoadAllAds(start: mainList.Count,
					count: Constants.AdsToLoadAtOnce,
					radius: radiusValue,
					inputLat: startPosLat,
					inputLong: startPosLong,
					categoryId: "",
					info: Constants.CurrentCatWeWantToLoad.id,
					adsListType: currentType,
					prevCat: prevCat);
				foreach (var item in x)
					mainList.Add(item);
			}

			//if (mainList.Count == 0)
			//	label_empty.IsVisible = true;
			//else
			//	label_empty.IsVisible = false;

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

		async void listview_mainViews_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			listview_mainViews.SelectedItem = null;
			var clickedItem = e.Item as AdsType;
			if (clickedItem == null)
				return;
			await Navigation.PushAsync(new Page_FullAd(clickedItem));
		}

		private async Task GetCats()
		{
			list.Clear();

			list = await ReloadAdapter.LoadSubCategories(Constants.CurrentCatWeWantToLoad.id);


			comboBox.BindingContext = list;
			comboBox.DataSource = list;

			if (CachedUser.localCode == localCodes.en)
				comboBox.DisplayMemberPath = "title";
			else if (CachedUser.localCode == localCodes.de)
				comboBox.DisplayMemberPath = "titleDe";
			else
                comboBox.DisplayMemberPath = "titleAr";
        }

		async void listview_mainViews_Refreshing(System.Object sender, System.EventArgs e)
		{
			listview_mainViews.IsRefreshing = true;
			list.Clear();
			await GetCats();
            await LoadMainContent(false);
            listview_mainViews.IsRefreshing = false;
		}

		void back_Clicked(System.Object sender, System.EventArgs e)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
                if (Constants.WhatPositionAmIOnRightNow == WhatPositionAmIOnRightNow.IamInJustIntoMainCategories)
                {
                    CV_CategoryList.Instance.RemoveLayout(0);
                }
                else if (Constants.WhatPositionAmIOnRightNow == WhatPositionAmIOnRightNow.IamInTheSubCategorie)
                {
                    CV_CategoryList.Instance.RemoveLayout(1);
                }
				else if(Constants.WhatPositionAmIOnRightNow == WhatPositionAmIOnRightNow.IamInSubSubThatIsTheEnd)
				{
                    // since the backbutton is visible on the last layout. but back here is clicked, we assume that we acually werent here but instead one further. so we remove in the other class
                    CV_CategoryList.Instance.RemoveLayout(1);
                }
            });
		}

		async void comboBox_SelectionChanged(System.Object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
		{
            try
            {
				var item = list[comboBox.SelectedIndex];

				if (Constants.WhatPositionAmIOnRightNow == WhatPositionAmIOnRightNow.IamInJustIntoMainCategories)
				{
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        CV_CategoryList.Instance.GoToNextPage(item);
                    });
				}
				else 
				{
					Constants.WhatPositionAmIOnRightNow = WhatPositionAmIOnRightNow.IamInSubSubThatIsTheEnd;
					Constants.CurrentCatWeWantToLoad.id = item.Id;
					Constants.CurrentCatWeWantToLoad.name = item.title;
                    await Navigation.PushAsync(new Page_DisplayContainer(item.Id));
                }

            }
            catch { }

			comboBox.SelectedItem = null; 
		}

        async void btn_search_Clicked(System.Object sender, System.EventArgs e)
        {


			await Navigation.PushAsync(new Page_DisplayContainer(Constants.WhatPositionAmIOnRightNow, entry_title.Text, Constants.CurrentCatWeWantToLoad.id));
			
			

		}
	}
}

