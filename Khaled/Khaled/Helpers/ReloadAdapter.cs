using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Khaled.Backend.APIs;
using Khaled.Views.ContentViews.MainMenuTabs;
using Xamarin.Forms;

namespace Khaled.Helpers
{
    public class ReloadAdapter
    {

        public async static Task<ObservableCollection<AdsType>> LoadAdsByName(string title, int start, int count, AdsListType adsListType)
        {
            ObservableCollection<AdsType> res = new ObservableCollection<AdsType>();

            List<FullAdType> x = new List<FullAdType>();

            if (adsListType == AdsListType.mainView)
                x = AdsAPI.Deserialize(await AdsAPI.GetAdsByTitle(title, start, count));
            
            return await FormatAd(x);
        }

        public async static Task<ObservableCollection<AdsType>> LoadAllAds(int start, int count, int radius, double inputLat, double inputLong, int categoryId, string info, AdsListType adsListType, string info2 = "", int categoryId2 = 0, int prevCat = 0)
        {
            ObservableCollection<AdsType> res = new ObservableCollection<AdsType>();

            List<FullAdType> x = new List<FullAdType>();

            if (adsListType == AdsListType.mainView)
                x = AdsAPI.Deserialize(await AdsAPI.GetAllAds(start, count, radius, inputLat, inputLong, categoryId, info));
            else if (adsListType == AdsListType.inCat1)
                x = AdsAPI.Deserialize(await AdsAPI.GetAllAds_Cat1(start, count, radius, inputLat, inputLong, categoryId, info));
            else if (adsListType == AdsListType.title)
                x = AdsAPI.Deserialize(await AdsAPI.GetAdsByTitle(info, start, count));
            else if (adsListType == AdsListType.inCat2)
                x = AdsAPI.Deserialize(await AdsAPI.GetAllAds_Cat2(start, count, radius, inputLat, inputLong, prevCat, categoryId2, info));
            else if (adsListType == AdsListType.titleIncat1)
                x = AdsAPI.Deserialize(await AdsAPI.GetAdsByTitleCat1(info, start, count, info2));
            else if (adsListType == AdsListType.titleIncat1)
                x = AdsAPI.Deserialize(await AdsAPI.GetAdsByTitleCat2(info, start, count, info2));
            else if (adsListType == AdsListType.favorites)
            {
                var favs = await App.DatabaseFAV.GetAllMyFavs();
                foreach (var elem in favs)
                {
                    FullAdType singleElem = new FullAdType
                    {
                        title = elem.title,
                        titleAr = elem.titleAr,
                        titleDe = elem.titleDe,
                        thumbnail = elem.thumbnail,
                        distance = elem.distance,
                        description = elem.description,
                        descriptionAR = elem.descriptionAR,
                        descriptionENG = elem.description,
                        tblAdID = elem.tblAdId
                    };

                    x.Add(singleElem);
                }
            }
            else
            {
                x = AdsAPI.Deserialize(await AdsAPI.GetAllAds(start, count, radius, inputLat, inputLong, categoryId, info));
            }
            return await FormatAd(x);
        }

        public async static Task<ObservableCollection<SubCatType>> LoadSubCategories(LayerEnum layer, int belongsTo)
        {
            ObservableCollection<SubCatType> res = new ObservableCollection<SubCatType>();
            List<SubCatType> x = new List<SubCatType>();

            if(layer == LayerEnum.subCat1)
                 x = CategoryAPI.Deserialize(await CategoryAPI.GetSubCats1(belongsTo));
            else
                 x = CategoryAPI.Deserialize(await CategoryAPI.GetSubCats2(belongsTo));


            foreach (var elem in x)
            {
                switch (CachedUser.localCode)
                {
                    case localCodes.de:
                        elem.title = elem.titleDe;
                        break;
                    case localCodes.ar:
                        elem.flowDirection = FlowDirection.RightToLeft;
                        elem.title = elem.titleAr;
                        break;
                }

                
                res.Add(elem);
            }
            return res;
        }

        public async static Task<ObservableCollection<OfferType>> LoadAllOffers(int start, int count)
        {
            List<OfferType> list = new List<OfferType>();
            ObservableCollection<OfferType> res = new ObservableCollection<OfferType>(); 

            list = OfferAPI.Deserialize(await OfferAPI.GetOffers(start, count));

            foreach(var elem in list)
            {
                var offer = new OfferType
                {
                    thumbnailImage = Converters.ReturnImageSourceFromString(elem.thumbnail),
                    extraInformation = elem.extraInformation,
                    type = elem.type
                };

                res.Add(offer);
            }
            return res; 
        }

        private async static Task<ObservableCollection<AdsType>> FormatAd(List<FullAdType> x)
        {
            ObservableCollection<AdsType> res = new ObservableCollection<AdsType>();
            foreach (var elem in x)
            {
                switch (CachedUser.localCode)
                {
                    case localCodes.en:
                        elem.title = elem.title;
                        elem.description = elem.descriptionENG;
                        break;
                    case localCodes.de:
                        elem.title = elem.titleDe;
                        elem.description = elem.descriptionGER;
                        break;
                    case localCodes.ar:
                        elem.flowDirection = FlowDirection.RightToLeft;
                        elem.description = elem.descriptionENG;
                        elem.description = elem.descriptionAR;
                        elem.title = elem.titleAr;
                        break;
                }

                if (elem.distance != null)
                {
                    if (elem.distance.Length > 5)
                        elem.distance = elem.distance.Substring(0, 5);
                }
                else
                {
                    elem.distance = "";
                    elem.IsDistanceVisible = false;

                }

                elem.distance = elem.distance + " km";

                if (elem.thumbnail != null)
                    elem.imageSource_thumbnail = Converters.ReturnImageSourceFromString(elem.thumbnail);


                var dbElem = await App.DatabaseFAV.GetNoteAsync(elem);
                if (dbElem != null)
                {
                    // set fav
                    var color = Converters.ColorFromResourceKey("MainTheme");
                    elem.tintColor = Color.FromRgba(color.R, color.G, color.B, color.A).ToHex();
                }

                res.Add(elem);
            }
            return res;
        }



    }
}
