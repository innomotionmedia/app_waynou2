using System;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms;

namespace Khaled.Views
{	
	public partial class TEST : ContentPage
	{	
		public TEST ()
		{
			InitializeComponent ();
		}

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
			//tabbedPageView.tab;

			MethodInfo dynMethod = tabbedPageView.GetType().GetMethod("UpdateSelectedIndex", BindingFlags.NonPublic | BindingFlags.Instance);
			dynMethod?.Invoke(tabbedPageView, new object[] { 1, false });

		}

        void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
			MethodInfo dynMethod = tabbedPageView.GetType().GetMethod("UpdateSelectedIndex", BindingFlags.NonPublic | BindingFlags.Instance);
			dynMethod?.Invoke(tabbedPageView, new object[] { 0, false });
		}
    }
}

