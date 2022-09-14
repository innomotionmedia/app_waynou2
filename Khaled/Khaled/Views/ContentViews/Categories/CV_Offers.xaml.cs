using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Khaled.Backend.APIs;
using Khaled.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Khaled.Views.ContentViews.Categories
{	
	public partial class CV_Offers : ContentView
	{
		ObservableCollection<SplitOffer> contentOfCarouselList = new ObservableCollection<SplitOffer>();
		public static CV_Offers Instance;
		bool delayBind = true; 
		public CV_Offers ()
		{
			InitializeComponent ();
			Instance = this;
			LoadData(true);

            if (CachedUser.localCode == localCodes.ar)
                this.FlowDirection = FlowDirection.RightToLeft;
            else
                this.FlowDirection = FlowDirection.LeftToRight;
        }

		public async Task LoadData(bool firstLoad)
		{
			// called from wrapper 
            if (firstLoad)
            {
                if (delayBind)
                {
					await Task.Delay(50);
					delayBind = false; 
				}

				var res = await ReloadAdapter.LoadAllOffers(0, Constants.OffersToLoadAtOnce);
				if(res.Count == 0)
                {
					// no offers
					label_nooffers.IsVisible = true; 
					return; 
                }
				else
					label_nooffers.IsVisible = false;

				contentOfCarouselList = PutOffersTogether(res);
				carouselview.ItemsSource = contentOfCarouselList;
			}
            else
            {
				var res = await ReloadAdapter.LoadAllOffers(contentOfCarouselList.Count*2, Constants.OffersToLoadAtOnce);
				var list = PutOffersTogether(res);
				foreach(var elem in list)
                {
					contentOfCarouselList.Add(elem);
                }
			}


		}

		public ObservableCollection<SplitOffer> PutOffersTogether(ObservableCollection<OfferType> input)
        {
			ObservableCollection<SplitOffer> res = new ObservableCollection<SplitOffer>();

			for(int i = 0; i < input.Count; i++)
			{
				var singleOffer = new SplitOffer();

				singleOffer.thumbnailL = input[i].thumbnailImage;
				singleOffer.extraInformationL = input[i].extraInformation;
				singleOffer.typeL = input[i].type;

				i++;

				if (i > input.Count - 1)
				{ 
					res.Add(singleOffer);
					break;

				}

				singleOffer.thumbnailR = input[i].thumbnailImage;
				singleOffer.extraInformationR = input[i].extraInformation;
				singleOffer.typeR = input[i].type;

				res.Add(singleOffer);
            }

			return res; 
		 }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
			var tapp = e as TappedEventArgs; 
			var tappedItem = sender as Frame;
			var item = tappedItem.BindingContext as SplitOffer;

			if(tapp.Parameter.ToString() == "left")
            {
				// left clicked
				if (item.typeL == OfferTypeEnum.link.ToString())
					Browser.OpenAsync(item.extraInformationL);
            }
			else
            {
				// right clicked
				if (item.typeR == OfferTypeEnum.link.ToString())
					Browser.OpenAsync(item.extraInformationR);
			}
        }

        async void carouselview_CurrentItemChanged(System.Object sender, Xamarin.Forms.CurrentItemChangedEventArgs e)
        {
			var item = e.CurrentItem as SplitOffer;

			if (item == contentOfCarouselList[contentOfCarouselList.Count - 1])
			{
				await LoadData(false);
			}
		}
    }

	public class SplitOffer
	{
		public ImageSource thumbnailL { get; set; }
		public ImageSource thumbnailR { get; set; }

		public string extraInformationL { get; set; }
		public string extraInformationR { get; set; }

		public string typeL { get; set; }
		public string typeR { get; set; }

	}
}

