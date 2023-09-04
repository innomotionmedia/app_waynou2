using System;
using System.Collections.Generic;
using Khaled.Backend.APIs;
using Khaled.Helpers;
using Khaled.Views.ContentViews.MainMenuTabs;
using TodoApp.Data;
using Xamarin.Forms;

namespace Khaled.Views.ContentViews.AdDisplay
{	
	public partial class Page_DisplayContainer : ContentPage
	{	
		public Page_DisplayContainer (AdsListType adsListType, string info)
		{
			InitializeComponent ();
            if (CachedUser.localCode == localCodes.ar)
                this.FlowDirection = FlowDirection.RightToLeft;
            else
                this.FlowDirection = FlowDirection.LeftToRight;
            layout_container.Children.Add(new CV_AllAdsRes(adsListType, info));
		}

		public Page_DisplayContainer(AdsListType adsListType, string info, string info2)
		{
			InitializeComponent();
            if (CachedUser.localCode == localCodes.ar)
                this.FlowDirection = FlowDirection.RightToLeft;
            else
                this.FlowDirection = FlowDirection.LeftToRight;
            layout_container.Children.Add(new CV_AllAdsRes(adsListType, info, info2));
		}

		public Page_DisplayContainer(List<CategoryIds> idList)
		{
			InitializeComponent();
            if (CachedUser.localCode == localCodes.ar)
                this.FlowDirection = FlowDirection.RightToLeft;
            else
                this.FlowDirection = FlowDirection.LeftToRight;
            layout_container.Children.Add(new CV_AllAdsRes(idList));
		}

	}


}

