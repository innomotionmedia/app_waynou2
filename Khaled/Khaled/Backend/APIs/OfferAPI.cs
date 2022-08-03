using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Kumulos;
using Com.Kumulos.Abstractions;
using Xamarin.Forms;

namespace Khaled.Backend.APIs
{
    public class OfferAPI
    {
        public static List<OfferType> Deserialize(ApiResponse response)
        {
            List<OfferType> ad = new List<OfferType>();
            var array = (Newtonsoft.Json.Linq.JArray)response.payload;
            foreach (var item in array)
            {
                OfferType x = (OfferType)(item.ToObject(typeof(OfferType)));
                ad.Add(x);
            }
            return ad;
        }

        public static async Task<ApiResponse> GetOffers(int start, int count)
        {
            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("start", start.ToString()),
                new KeyValuePair<string, string>("count", count.ToString()),
            };
            return await Kumulos.Current.Build.CallAPI("tblOffers_getOffers", parameters).ConfigureAwait(false);
        }
    }

    public enum OfferTypeEnum
    {
        link = 0,
        ad = 1
    }

    public class OfferType
    {
        public string title { get; set; }
        public string thumbnail { get; set; }
        public ImageSource thumbnailImage { get; set; }
        public int order { get; set; }
        public string type { get; set; }
        public string extraInformation { get; set; }

    }
}

