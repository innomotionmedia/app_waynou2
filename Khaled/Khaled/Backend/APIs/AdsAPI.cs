using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Kumulos;
using Com.Kumulos.Abstractions;
using Khaled.Helpers;
using Xamarin.Forms;

namespace Khaled.Backend.APIs
{
    public class AdsAPI
    {
        public static List<FullAdType> Deserialize(ApiResponse response)
        {
            List<FullAdType> ad = new List<FullAdType>();
            var array = (Newtonsoft.Json.Linq.JArray)response.payload;
            foreach (var item in array)
            {
                FullAdType x = (FullAdType)(item.ToObject(typeof(FullAdType)));        
                ad.Add(x);       
            }       
            return ad;
        }

        public static List<FullAdType> DeserializeFullAd(ApiResponse response)
        {
            List<FullAdType> ad = new List<FullAdType>();
            var array = (Newtonsoft.Json.Linq.JArray)response.payload;
            foreach (var item in array)
            {
                FullAdType x = (FullAdType)(item.ToObject(typeof(FullAdType)));
                ad.Add(x);
            }
            return ad;
        }

        public static async Task<ApiResponse> GetFullAdInfo(int tblAdID)
        {
            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("tblAdID", tblAdID.ToString()),
            };
            return await Kumulos.Current.Build.CallAPI("tblAds_getFullAdInfo", parameters).ConfigureAwait(false);
        }

        public static async Task<ApiResponse> GetAdsByTitle(string title, int start, int count)
        {
            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("title", title),
                new KeyValuePair<string, string>("start", start.ToString()),
                new KeyValuePair<string, string>("count", count.ToString()),
            };
            var x =  await Kumulos.Current.Build.CallAPI("tblAds_getAdsByTitle", parameters).ConfigureAwait(false);
            return x; 
        }

        public static async Task<ApiResponse> GetAdsByTitleCat1(string title, int start, int count, string belongsToMain)
        {
            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("title", title),
                new KeyValuePair<string, string>("start", start.ToString()),
                new KeyValuePair<string, string>("count", count.ToString()),
                new KeyValuePair<string, string>("belongsToMain", belongsToMain),

            };
            var x = await Kumulos.Current.Build.CallAPI("tblAds_getAdsByTitleInCat1", parameters).ConfigureAwait(false);
            return x;
        }

        public static async Task<ApiResponse> GetAdsByTitleCat2(string title, int start, int count, string belongsToMain)
        {
            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("title", title),
                new KeyValuePair<string, string>("start", start.ToString()),
                new KeyValuePair<string, string>("count", count.ToString()),
                new KeyValuePair<string, string>("belongsToSub1", belongsToMain),

            };
            var x = await Kumulos.Current.Build.CallAPI("tblAds_getAdsByTitleInCat2", parameters).ConfigureAwait(false);
            return x;
        }

        public static async Task<ApiResponse> GetAllAds(int start, int count, int maxKmRadius, double inputLat, double inputLong, int categoryId, string title)
        {
            var lat = Converters.TurnCommaIntoDot(inputLat.ToString());
            var longi = Converters.TurnCommaIntoDot(inputLong.ToString());

            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("start", start.ToString()),
                new KeyValuePair<string, string>("count", count.ToString()),
                new KeyValuePair<string, string>("inputLat", lat),
                new KeyValuePair<string, string>("inputLong", longi),
                new KeyValuePair<string, string>("categoryId", categoryId.ToString()),
                new KeyValuePair<string, string>("maxKmRadius", maxKmRadius.ToString()),
                new KeyValuePair<string, string>("title", title),

            };

            var x = await Kumulos.Current.Build.CallAPI("tblAds_getAllAds", parameters).ConfigureAwait(false);
            return x;
        }

        public static async Task<ApiResponse> GetAllAds_Cat1(int start, int count, int maxKmRadius, double inputLat, double inputLong, int categoryId, string title)
        {
            var lat = Converters.TurnCommaIntoDot(inputLat.ToString());
            var longi = Converters.TurnCommaIntoDot(inputLong.ToString());

            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("start", start.ToString()),
                new KeyValuePair<string, string>("count", count.ToString()),
                new KeyValuePair<string, string>("inputLat", lat),
                new KeyValuePair<string, string>("inputLong", longi),
                new KeyValuePair<string, string>("categoryId", categoryId.ToString()),
                new KeyValuePair<string, string>("maxKmRadius", maxKmRadius.ToString()),
                new KeyValuePair<string, string>("title", title),

            };

            var x = await Kumulos.Current.Build.CallAPI("tblAds_getAllAds_Cat1", parameters).ConfigureAwait(false);
            return x;
        }

        public static async Task<ApiResponse> GetAllAds_Cat2(int start, int count, int maxKmRadius, double inputLat, double inputLong, int categoryId, int categoryId2, string title)
        {
            var lat = Converters.TurnCommaIntoDot(inputLat.ToString());
            var longi = Converters.TurnCommaIntoDot(inputLong.ToString());

            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("start", start.ToString()),
                new KeyValuePair<string, string>("count", count.ToString()),
                new KeyValuePair<string, string>("inputLat", lat),
                new KeyValuePair<string, string>("inputLong", longi),
                new KeyValuePair<string, string>("categoryId", categoryId.ToString()),
                new KeyValuePair<string, string>("categoryId2", categoryId2.ToString()),
                new KeyValuePair<string, string>("maxKmRadius", maxKmRadius.ToString()),
                new KeyValuePair<string, string>("title", title),

            };

            var x = await Kumulos.Current.Build.CallAPI("tblAds_getAllAds_Cat2", parameters).ConfigureAwait(false);
            return x;
        }

        public static async Task<ApiResponse> GetFullPic(int tblAdID)
        {
            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("tblAdID", tblAdID.ToString()),
            };

            var x = await Kumulos.Current.Build.CallAPI("tblAds_getFullPic", parameters).ConfigureAwait(false);
            return x;
        }
    }

    public class AdsType
    {
        public string title { get; set; }
        public string titleAr { get; set; }
        public string titleDe { get; set; }

        public string description { get; set; }

        //public string tintColor { get; set; } = "Transparent";

        public ImageSource FaveImageSource { get; set; }

        public int tblAdID { get; set; }
        public long UnixCreated { get; set; }
        public ImageSource imageSource_thumbnail { get; set; }
        public string thumbnail { get; set; }
        public ImageSource imageSource_fullPic { get; set; }
        public string fullPic { get; set; }

        public string distance { get; set; }

        public FlowDirection flowDirection { get; set; } = FlowDirection.LeftToRight;

        public bool IsDistanceVisible { get; set; } = true; 

    }

    public class FullAdType : AdsType
    {
        public string descriptionENG { get; set; }
        public string descriptionGER { get; set; }
        public string descriptionAR { get; set; }

        public string adresse { get; set; }
        public string telephone { get; set; }
        public string hours { get; set; }
        public string email { get; set; }
        public string web { get; set; }


    }
}
