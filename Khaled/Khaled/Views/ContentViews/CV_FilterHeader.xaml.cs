using System;
using System.Collections.Generic;
using Khaled.Helpers;
using TodoApp.Data;
using Xamarin.Forms;

namespace Khaled.Views.ContentViews
{	
	public partial class CV_FilterHeader : ContentView
	{	
		public CV_FilterHeader ()
		{
			InitializeComponent ();
            if (CachedUser.localCode == localCodes.ar)
                this.FlowDirection = FlowDirection.RightToLeft;
            else
                this.FlowDirection = FlowDirection.LeftToRight;
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
        }

        void TapGestureRecognizer_Tapped_1(System.Object sender, System.EventArgs e)
        {
        }
    }
}

