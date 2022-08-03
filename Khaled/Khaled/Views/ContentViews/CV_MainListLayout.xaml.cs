using System;
using System.Collections.Generic;
using Khaled.Backend.APIs;
using Khaled.Helpers;
using Plugin.CrossPlatformTintedImage.Abstractions;
using Xamarin.Forms;

namespace Khaled.Views.ContentViews
{
    public partial class CV_MainListLayout : ContentView
    {
        public CV_MainListLayout()
        {
            InitializeComponent();
        }

        async void Tapped_Fav(Object sender, EventArgs e)
        {
            try
            {
                var img = sender as TintedImage;
                var context = img.BindingContext as FullAdType;
                var color = Converters.ColorFromResourceKey("MainTheme");

                if (img.TintColor == Color.FromRgba(color.R, color.G, color.B, color.A))
                {
                    //unfav
                    img.TintColor = Color.Transparent;
                    await App.DatabaseFAV.DeleteMyFav(context);

                }
                else
                {
                    //fav
                    img.TintColor = Color.FromRgba(color.R, color.G, color.B, color.A);
                    await App.DatabaseFAV.SaveClickedOnFav(context);
                }
            }
            catch { }

        }
    }
}
