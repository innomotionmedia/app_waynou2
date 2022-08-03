using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Khaled.Views
{	
	public partial class Page_SplashScreen : ContentPage
	{	
		public Page_SplashScreen ()
		{
			InitializeComponent ();

			StartFade();

		}

        private async void StartFade()
        {
			await System.Threading.Tasks.Task.Delay(200);
			image_logo.Opacity = 0;
			image_logo.IsVisible = true; 
			await image_logo.FadeTo(1, 4000);
			Application.Current.MainPage = new MainPage(false, false);
		}
    }
}

