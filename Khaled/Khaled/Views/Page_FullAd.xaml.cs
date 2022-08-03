using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Khaled.Backend.APIs;
using Khaled.Helpers;
using Khaled.Resources;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Khaled.Views
{
    public partial class Page_FullAd : ContentPage
    {
        AdsType ad;
        bool isBusy;
        List<FullAdType> data;
        public Page_FullAd(AdsType ad)
        {
            InitializeComponent();
            this.ad = ad; 
            DownloadRemainingAdData(); 
        }

        private async Task DownloadRemainingAdData()
        {
            data = AdsAPI.DeserializeFullAd(await AdsAPI.GetFullAdInfo(ad.tblAdID));

            if (data == null)
                return;

            await SetLike(true);

            SetData(data); 

        }

        private void SetData(List<FullAdType> data)
        {
            //label_title.Text = Converters.ReturnCorrectLingua(data[0], Helpers.TextType.adTitle);

            label_description.Text = Converters.ReturnCorrectLingua(data[0], Helpers.TextType.adDescription);
            ad.imageSource_fullPic = Converters.ReturnImageSourceFromString(data[0]?.fullPic);
            label_adresse.Text = data[0]?.adresse;
            label_email.Text = data[0]?.email;
            label_hours.Text = data[0]?.hours;
            label_telephone.Text = data[0]?.telephone;
            label_web.Text = data[0]?.web;
            img_mainPic.Source = ad.imageSource_fullPic;
        }

        async void RefreshView_Refreshing(System.Object sender, System.EventArgs e)
        {
            if (!isBusy)
            {
                isBusy = true;
                refreshview_view.IsRefreshing = true;
                await DownloadRemainingAdData();
                refreshview_view.IsRefreshing = false;
                isBusy = false;
            }
        }

        void Tapped_tabs(System.Object sender, System.EventArgs e)
        {
            var greyColor = Converters.ColorFromResourceKey("TabGray");

            if (sender.Equals(tab1))
            {
                tab1.BackgroundColor = Color.White;
                tab2.BackgroundColor = greyColor;
                tab3.BackgroundColor = greyColor;

                layout1.IsVisible = true; 
            }
            else if (sender.Equals(tab2))
            {
                tab1.BackgroundColor = greyColor;
                tab2.BackgroundColor = Color.White;
                tab3.BackgroundColor = greyColor;

                layout1.IsVisible = false;
            }
            else
            {
                tab1.BackgroundColor = greyColor;
                tab2.BackgroundColor = greyColor;
                tab3.BackgroundColor = Color.White;

                layout1.IsVisible = false;
            }
        }

        async void Tapped_Quicks(System.Object sender, System.EventArgs e)
        {
            if (sender.Equals(frame_telephone) || sender.Equals(label_telephone))
            {
                try
                {
                    PhoneDialer.Open(data[0]?.telephone);
                }
                catch (ArgumentNullException anEx)
                {
                    // Number was null or white space
                }
            }
            if (sender.Equals(frame_gps) || sender.Equals(label_adresse))
            {
                await MapsHelper.OpenMaps(data[0]?.adresse);
            }
            if (sender.Equals(frame_mail) || sender.Equals(label_email))
            {
                Device.OpenUri(new Uri($"mailto:{data[0]?.email}"));
            }
            if (sender.Equals(label_web))
            {
                var uri = data[0]?.web;
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            if (sender.Equals(frame_heart))
            {
                await SetLike(false); 
            }
            if (sender.Equals(frame_share))
            {
                SharePage();
            }
        }

        private async void SharePage()
        {
            var localCode = CachedUser.localCode;
            string local = "";
            string title = "";

            switch (localCode)
            {
                case localCodes.en:
                    local = "eng";
                    title = ad.title;
                    break;
                case localCodes.de:
                    local = "de";
                    title = ad.titleDe;
                    break;
                case localCodes.ar:
                    local = "ar";
                    title = ad.titleAr;
                    break;
            }

            await SharingHelper.ShareUri(
               AppResources.SharingText
               + "'"+title+"'"
               + "\r\n\r\n"
               + Constants.WebappBaseUrl
               + "adId=" + ad.tblAdID.ToString()
               + "&local=" + local
            );
        }

        private async Task SetLike(bool justChecking)
        {
            var res = await App.DatabaseFAV.GetNoteAsync(ad);

            if (!justChecking)
            {
                if (res != null)
                {
                    //unfav
                    img_heart.TintColor = Color.Transparent;
                    await App.DatabaseFAV.DeleteMyFav(ad);

                }
                else
                {
                    //fav
                    img_heart.TintColor = Color.Black;
                    await App.DatabaseFAV.SaveClickedOnFav(data[0]);
                }
            }
            else
            {
                if (res != null)
                    img_heart.TintColor = Color.Black;
                else
                    img_heart.TintColor = Color.Transparent;
            }

        }
    }
}
