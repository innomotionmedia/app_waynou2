using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Khaled.Backend.APIs;
using Khaled.Helpers;
using Khaled.Resources;
using Khaled.Views.ContentViews.AdDisplay;
using Khaled.Views.ContentViews.MainMenuTabs;
using Khaled.Views.MainMenu;
using Xamarin.Forms;

namespace Khaled.Views.ContentViews.Categories
{	
	public partial class CV_CategoryList : ContentView
	{
		public static CV_CategoryList Instance;
		static bool canLoad = false; 

		public CV_CategoryList ()
		{
			InitializeComponent ();
			Instance = this;
			LoadContent();

            if (CachedUser.localCode == localCodes.ar)
                this.FlowDirection = FlowDirection.RightToLeft;

            if (CachedUser.localCode == localCodes.ar)
				this.FlowDirection = FlowDirection.RightToLeft;
			else
				this.FlowDirection = FlowDirection.LeftToRight;
			
		}

		public async void RemoveLayout(int pageIndex)
        {
			//move one page back 
			MethodInfo dynMethod = tabbedPageView.GetType().GetMethod("UpdateSelectedIndex", BindingFlags.NonPublic | BindingFlags.Instance);
			dynMethod?.Invoke(tabbedPageView, new object[] { pageIndex, false });

			if(pageIndex == 0)
            {
				await Task.Delay(500);
				stacklayout_layout3.Children.Clear();
				stacklayout_layout2.Children.Clear();
			}
			if (pageIndex == 1)
			{
				await Task.Delay(500);
				stacklayout_layout3.Children.Clear();
			}


		}

		public async void GoToPage3(SubCatType item, CategoriesEnum prevCat)
		{
			stacklayout_layout3.Children.Add(new Page_SubCategories(LayerEnum.subCat2, item.tblSubCat1ID, item.title, prevCat));
		
			MethodInfo dynMethod = tabbedPageView.GetType().GetMethod("UpdateSelectedIndex", BindingFlags.NonPublic | BindingFlags.Instance);
			dynMethod?.Invoke(tabbedPageView, new object[] { 2, false });
		
		}

		public async void ClickOnTopCats(System.Object sender, System.EventArgs e)
        {
			var tapp = e as TappedEventArgs;
			var tappedItem = sender as Frame;
			var item = tappedItem.BindingContext as TopCats;
			stacklayout_layout3.Children.Clear();
			stacklayout_layout2.Children.Clear();

			if (tapp.Parameter.ToString() == "left")
			{
				if(item.nameL == null)
					return;
				stacklayout_layout2.Children.Add(new Page_SubCategories(item.categoriesEnumL, item.nameL));
			}
			else if (tapp.Parameter.ToString() == "mid")
			{
				if (item.nameM == null)
					return;
				stacklayout_layout2.Children.Add(new Page_SubCategories(item.categoriesEnumM, item.nameM));
			}
			else
			{
				if (item.nameR == null)
					return;
				stacklayout_layout2.Children.Add(new Page_SubCategories(item.categoriesEnumR, item.nameR));
			}

			MethodInfo dynMethod = tabbedPageView.GetType().GetMethod("UpdateSelectedIndex", BindingFlags.NonPublic | BindingFlags.Instance);
			dynMethod?.Invoke(tabbedPageView, new object[] { 1, false });
		}

		private async Task LoadContent()
        {
			layout_offers.Children.Clear();
			layout_topCats.Children.Clear();

			layout_topCats.Children.Add(new CV_TopCats());
			layout_offers.Children.Add(new CV_Offers());

			if (canLoad)
				CV_TopCats.Instance.LoadDummyData(); // second load
			else
				canLoad = true; 

        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
			var btn = sender as Frame;

			// add whatever it is to page two
			stacklayout_layout3.Children.Clear();
			stacklayout_layout2.Children.Clear();

			#region cats
			if (btn.Equals(frame_food))
				stacklayout_layout2.Children.Add(new Page_SubCategories(Helpers.CategoriesEnum.food, AppResources.FoodAndDrink));
			else if (btn.Equals(frame_health))
				stacklayout_layout2.Children.Add(new Page_SubCategories(Helpers.CategoriesEnum.health, AppResources.Health));
			else if (btn.Equals(frame_shopping))
				stacklayout_layout2.Children.Add(new Page_SubCategories(Helpers.CategoriesEnum.shopping, AppResources.Shopping));
			else if (btn.Equals(frame_law))
				stacklayout_layout2.Children.Add(new Page_SubCategories(Helpers.CategoriesEnum.law, AppResources.LawAndFinance));
			else if (btn.Equals(frame_car))
				stacklayout_layout2.Children.Add(new Page_SubCategories(Helpers.CategoriesEnum.car, AppResources.CarWorksShop));
			else if (btn.Equals(frame_beauty))
				stacklayout_layout2.Children.Add(new Page_SubCategories(Helpers.CategoriesEnum.beauty, AppResources.BeautyAndWellness));
			else if (btn.Equals(frame_shopping2))
				stacklayout_layout2.Children.Add(new Page_SubCategories(Helpers.CategoriesEnum.dailyShopping, AppResources.DailyShopping));
			else if (btn.Equals(frame_hotels))
				stacklayout_layout2.Children.Add(new Page_SubCategories(Helpers.CategoriesEnum.hotels, AppResources.HotelsAndProperties));
			else if (btn.Equals(frame_travel))
				stacklayout_layout2.Children.Add(new Page_SubCategories(Helpers.CategoriesEnum.travel, AppResources.TravelAndToursim));
			else if (btn.Equals(frame_construction))
				stacklayout_layout2.Children.Add(new Page_SubCategories(Helpers.CategoriesEnum.construction, AppResources.ConstructionAndRepair));
			else if (btn.Equals(frame_house))
				stacklayout_layout2.Children.Add(new Page_SubCategories(Helpers.CategoriesEnum.rentHouse, AppResources.RentAndBuyRealEstate));
			else if (btn.Equals(frame_services))
				stacklayout_layout2.Children.Add(new Page_SubCategories(Helpers.CategoriesEnum.services, AppResources.Services));
			else if (btn.Equals(frame_sights))
				stacklayout_layout2.Children.Add(new Page_SubCategories(Helpers.CategoriesEnum.sights, AppResources.SightAndExcursion));
			else if (btn.Equals(frame_party))
				stacklayout_layout2.Children.Add(new Page_SubCategories(Helpers.CategoriesEnum.happenings, AppResources.HappeningsAndEvents));
			else if (btn.Equals(frame_culture))
				stacklayout_layout2.Children.Add(new Page_SubCategories(Helpers.CategoriesEnum.culture, AppResources.CultureAndArt));
			else if (btn.Equals(frame_hobby))
				stacklayout_layout2.Children.Add(new Page_SubCategories(Helpers.CategoriesEnum.hobby, AppResources.HobbyAndAmusement));
			else if (btn.Equals(frame_jobs))
				stacklayout_layout2.Children.Add(new Page_SubCategories(Helpers.CategoriesEnum.jobs, AppResources.Jobs));
			else if (btn.Equals(frame_edu))
				stacklayout_layout2.Children.Add(new Page_SubCategories(Helpers.CategoriesEnum.edu, AppResources.EducationalInstitutions));
			else if (btn.Equals(frame_organizations))
				stacklayout_layout2.Children.Add(new Page_SubCategories(Helpers.CategoriesEnum.orgas, AppResources.OrganizationsAndAssociations));
			else if (btn.Equals(frame_authorities))
				stacklayout_layout2.Children.Add(new Page_SubCategories(Helpers.CategoriesEnum.authorities, AppResources.AuthoritiesAndEmbassies));
			else if (btn.Equals(frame_religion))
				stacklayout_layout2.Children.Add(new Page_SubCategories(Helpers.CategoriesEnum.religion, AppResources.ReligiousInstitions));
			else if (btn.Equals(frame_emergency))
				stacklayout_layout2.Children.Add(new Page_SubCategories(Helpers.CategoriesEnum.emergenices, AppResources.Emergencies));
			#endregion

			//move to page 2
			MethodInfo dynMethod = tabbedPageView.GetType().GetMethod("UpdateSelectedIndex", BindingFlags.NonPublic | BindingFlags.Instance);
			dynMethod?.Invoke(tabbedPageView, new object[] { 1, false });

		}

        async void refreshview_Refreshing(System.Object sender, System.EventArgs e)
        {
			refreshview.IsRefreshing = true; 
			await LoadContent();
			refreshview.IsRefreshing = false; 
        }

        void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
			Page_MainMenu.instance.IsPresented = true; 
		}

        void btn_search_Clicked(System.Object sender, System.EventArgs e)
        {
			var btn = sender as Button;
			if (btn.Equals(btn_search))
				Navigation.PushAsync(new Page_DisplayContainer(AdsListType.title, entry_title?.Text));
		}

        void ImageButton_Clicked_1(System.Object sender, System.EventArgs e)
        {
        }

    }
}

