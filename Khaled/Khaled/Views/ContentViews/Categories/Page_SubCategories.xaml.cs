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

		CategoriesEnum cat;
		LayerEnum layer = LayerEnum.subCat1; // default is layer 1
		public int belongsToCat1Id = 0;
		int prevCat = 0;

		ObservableCollection<SubCatType> list = new ObservableCollection<SubCatType>();

		public Page_SubCategories(CategoriesEnum cat, string title)
		{
			InitializeComponent();

			// first contructor
			Constants.appPath[0] = (int)cat;
			this.cat = cat;
			text_pickCat.Text = title;
			StartUp();
		}

		public Page_SubCategories(LayerEnum layer, int belongsToCat1Id, string title, CategoriesEnum cat)
		{
			InitializeComponent();
			this.belongsToCat1Id = belongsToCat1Id;
			prevCat = (int)cat;
			// second layer 
			this.layer = layer;
			Constants.appPath[1] = belongsToCat1Id;
			text_pickCat.Text = title;

			StartUp();
		}

		private async void StartUp()
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
			}
			else
			{
				radiusValue = CachedUser.radius;
				startPosLat = CachedUser.lati;
				startPosLong = CachedUser.longi;
			}


			await GetCats();
			await GetAds();

		}

		private async Task GetAds()
		{
			await LoadMainContent(false);
		}

		private async Task LoadMainContent(bool loadMore)
		{
			AdsListType currentType = 0;

			if (belongsToCat1Id == 0)
				currentType = AdsListType.inCat1;
			else
				currentType = AdsListType.inCat2;

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
					categoryId: (int)cat,
					info: info,
					adsListType: currentType,
					categoryId2: belongsToCat1Id,
					prevCat: prevCat);
				listview_mainViews.ItemsSource = mainList;

			}
			else
			{
				var x = await ReloadAdapter.LoadAllAds(start: mainList.Count,
					count: Constants.AdsToLoadAtOnce,
					radius: radiusValue,
					inputLat: startPosLat,
					inputLong: startPosLong,
					categoryId: (int)cat,
					info: info,
					adsListType: currentType,
					categoryId2: belongsToCat1Id,
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
			if (layer == LayerEnum.subCat1)
				list = await ReloadAdapter.LoadSubCategories(layer, (int)cat);
			else
				list = await ReloadAdapter.LoadSubCategories(layer, belongsToCat1Id);

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
			await GetAds();
			listview_mainViews.IsRefreshing = false;
		}

		private async void GetResultsFromPath()
		{
			var res = CategoryAPI.DeserializePath(await CategoryAPI.GetPrimaryKeyOfPath
				(Constants.appPath[0], Constants.appPath[1], Constants.appPath[2]));

			await Navigation.PushAsync(new Page_DisplayContainer(res));
		}

		void back_Clicked(System.Object sender, System.EventArgs e)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				if (belongsToCat1Id == 0)
					CV_CategoryList.Instance.RemoveLayout(0);
				else
					CV_CategoryList.Instance.RemoveLayout(1);
			
			});
		}

		void comboBox_SelectionChanged(System.Object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
		{
            try
            {
				var item = list[comboBox.SelectedIndex];

				if (item.tblSubCat1ID != 0)
				{
					Device.BeginInvokeOnMainThread(() =>
					{
						CV_CategoryList.Instance.GoToPage3(item, cat);
					}
					);
				}
				else
				{
					Constants.appPath[2] = item.tblSubCat2ID;
					GetResultsFromPath();
				}
            }
            catch { }


		}

        async void btn_search_Clicked(System.Object sender, System.EventArgs e)
        {

			if (belongsToCat1Id == 0)
			{
				if (!string.IsNullOrEmpty(entry_title.Text))
					await Navigation.PushAsync(new Page_DisplayContainer(AdsListType.titleIncat1, entry_title.Text, Constants.appPath[0].ToString()));
			}
			else
			{
				if (!string.IsNullOrEmpty(entry_title.Text))
					await Navigation.PushAsync(new Page_DisplayContainer(AdsListType.titleIncat2, entry_title.Text, belongsToCat1Id.ToString()));
			}
			

		}
	}
}

