using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Khaled.Helpers;
using Khaled.Resources;
using TodoApp.Data;
using Xamarin.Forms;

namespace Khaled.Views.ContentViews.Categories
{	
	public partial class CV_TopCats : ContentView
	{
		public static CV_TopCats Instance; 

		public CV_TopCats ()
		{
			InitializeComponent ();

            if (CachedUser.localCode == localCodes.ar)
                this.FlowDirection = FlowDirection.RightToLeft;
            else
                this.FlowDirection = FlowDirection.LeftToRight;

            Instance = this;
			//LoadDummyData(); 
		}

        public async void LoadDummyData()
        {

            // Called from CV_AllAdsRes.LoadMainContent. If not done, cats wont render first 

            ObservableCollection<TopCats> data = new ObservableCollection<TopCats>();

			var item1 = new TopCats
			{
				thumbnailL = ImageSource.FromFile("cat_emergency.png"),
				nameL = AppResources.Emergencies,
				categoriesEnumL = CategoriesEnum.emergenices,
				thumbnailM = ImageSource.FromFile("cat_flag.png"),
				categoriesEnumM = CategoriesEnum.authorities,
				nameM = AppResources.AuthoritiesAndEmbassies,
				thumbnailR = ImageSource.FromFile("cat_religion.png"),
				nameR = AppResources.ReligiousInstitions,
				categoriesEnumR = CategoriesEnum.religion
			};
			var item2 = new TopCats
			{
				thumbnailL = ImageSource.FromFile("cat_party.png"),
				categoriesEnumL = CategoriesEnum.happenings,
				nameL = AppResources.HappeningsAndEvents,
				thumbnailM = ImageSource.FromFile("cat_landmarks.png"),
				categoriesEnumM = CategoriesEnum.sights,
				nameM = AppResources.SightAndExcursion,
				thumbnailR = ImageSource.FromFile("cat_bank.png"),
				categoriesEnumR = CategoriesEnum.culture,
				nameR = AppResources.CultureAndArt,
			};
			var item3 = new TopCats
			{
				thumbnailL = ImageSource.FromFile("cat_people.png"),
				categoriesEnumL = CategoriesEnum.orgas,
				nameL = AppResources.OrganizationsAndAssociations,
				backgroundColor2 = "Transparent",
				backgroundColor3 = "Transparent",
			};

			data.Add(item1);
			data.Add(item2);
			data.Add(item3);

			//await Task.Delay(10000);// ugly workaround for rendered only upon refresh 
			carouselview.ItemsSource = data; 
		}

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
			CV_CategoryList.Instance.ClickOnTopCats(sender, e);
		}
    }

	public class TopCats
	{
		public ImageSource thumbnailL { get; set; }
		public ImageSource thumbnailM { get; set; }
		public ImageSource thumbnailR { get; set; }

		public string nameL { get; set; }
		public string nameM { get; set; }
		public string nameR { get; set; }

		public CategoriesEnum categoriesEnumL { get; set; }
		public CategoriesEnum categoriesEnumR { get; set; }
		public CategoriesEnum categoriesEnumM { get; set; }

		public string backgroundColor { get; set; } = "white";
		public string backgroundColor2 { get; set; } = "white";
		public string backgroundColor3 { get; set; } = "white";

	}
}

