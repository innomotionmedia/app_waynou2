using System;
using System.Collections.Generic;
using Khaled.Backend.APIs;
using Khaled.Helpers;
using Plugin.CrossPlatformTintedImage.Abstractions;
using TodoApp.Data;
using Xamarin.Forms;

namespace Khaled.Views.ContentViews
{
    public partial class CV_MainListLayout : ContentView
    {
        public CV_MainListLayout()
        {
            InitializeComponent();
            if (CachedUser.localCode == localCodes.ar)
                this.FlowDirection = FlowDirection.RightToLeft;
            else
                this.FlowDirection = FlowDirection.LeftToRight;
        }

        async void Tapped_Fav(Object sender, EventArgs e)
        {
            try
            {
                var img = sender as TintedImage;
                var context = img.BindingContext as FullAdType;
                var color = Converters.ColorFromResourceKey("MainTheme");

                var Source = ImageSource.FromFile("img_fav.png");

                if (img.Source.ToString() != Source.ToString())
                {
                    //unfav
                    img.Source = "img_fav.png";
                    await App.DatabaseFAV.DeleteMyFav(context);
            
                }
                else
                {
                    //fav
                    img.Source = "img_fav_selected.png";
                    await App.DatabaseFAV.SaveClickedOnFav(context);
                }
            }
            catch { }

        }
    }
}
