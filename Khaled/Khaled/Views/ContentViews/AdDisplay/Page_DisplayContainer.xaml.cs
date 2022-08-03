using System;
using System.Collections.Generic;
using Khaled.Backend.APIs;
using Khaled.Views.ContentViews.MainMenuTabs;
using Xamarin.Forms;

namespace Khaled.Views.ContentViews.AdDisplay
{	
	public partial class Page_DisplayContainer : ContentPage
	{	
		public Page_DisplayContainer (AdsListType adsListType, string info)
		{
			InitializeComponent ();
			layout_container.Children.Add(new CV_AllAdsRes(adsListType, info));
		}

		public Page_DisplayContainer(AdsListType adsListType, string info, string info2)
		{
			InitializeComponent();
			layout_container.Children.Add(new CV_AllAdsRes(adsListType, info, info2));
		}

		public Page_DisplayContainer(List<CategoryIds> idList)
		{
			InitializeComponent();
			layout_container.Children.Add(new CV_AllAdsRes(idList));
		}

	}


}

