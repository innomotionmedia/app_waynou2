using System;
using System.Collections.Generic;
using Khaled.Helpers;
using Xamarin.Forms;

namespace Khaled.Views.ContentViews.Categories
{	
	public partial class CV_CategoriesDetail : ContentView
	{	
		public CV_CategoriesDetail ()
		{
			InitializeComponent ();

            if (CachedUser.localCode == localCodes.ar)
                this.FlowDirection = FlowDirection.RightToLeft;

        }
	}
}

