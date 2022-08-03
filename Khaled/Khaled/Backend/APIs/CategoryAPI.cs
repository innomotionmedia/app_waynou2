using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Kumulos;
using Com.Kumulos.Abstractions;
using Xamarin.Forms;

namespace Khaled.Backend.APIs
{
    public class CategoryAPI
    {
        public static List<SubCatType> Deserialize(ApiResponse response)
        {
            List<SubCatType> ad = new List<SubCatType>();
            var array = (Newtonsoft.Json.Linq.JArray)response.payload;
            foreach (var item in array)
            {
                SubCatType x = (SubCatType)(item.ToObject(typeof(SubCatType)));
                ad.Add(x);
            }
            return ad;
        }

        public static List<CategoryIds> DeserializePath(ApiResponse response)
        {
            List<CategoryIds> ad = new List<CategoryIds>();
            var array = (Newtonsoft.Json.Linq.JArray)response.payload;
            foreach (var item in array)
            {
                CategoryIds x = (CategoryIds)(item.ToObject(typeof(CategoryIds)));
                ad.Add(x);
            }
            return ad;
        }

        public static async Task<ApiResponse> GetSubCats1(int belongsToMain)
        {
            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("belongsToMain", belongsToMain.ToString()),
            };
            return await Kumulos.Current.Build.CallAPI("getSubCats1", parameters).ConfigureAwait(false);
        }

        public static async Task<ApiResponse> GetSubCats2(int belongsToSubCat0)
        {
            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("belongsToSubCat0", belongsToSubCat0.ToString()),
            };
            return await Kumulos.Current.Build.CallAPI("getSubCats2", parameters).ConfigureAwait(false);
        }

        public static async Task<ApiResponse> GetPrimaryKeyOfPath(int mainCat, int subCat1, int subCat2)
        {
            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("mainCat", mainCat.ToString()),
                new KeyValuePair<string, string>("subCat1", subCat1.ToString()),
                new KeyValuePair<string, string>("subCat2", subCat2.ToString()),
            };
            return await Kumulos.Current.Build.CallAPI("getPrimaryKeyOfPath", parameters).ConfigureAwait(false);
        }
    }

    public class SubCatType
    {
        //abuse title to show localised title in reload adapter
        public string title { get; set; }
        public string titleAr { get; set; }
        public string titleDe { get; set; }
        public string picture { get; set; }
        public int tblSubCat1ID { get; set; } // the id to get to the next layer. 0 means, no more layers
        public int tblSubCat2ID { get; set; } // the id to get to the next layer. 0 means, no more layers        
        public FlowDirection flowDirection { get; set; } = FlowDirection.LeftToRight;
    }

    public class CategoryIds
    {
        public int tblCategoryIdID { get; set; }
        public int mainCat { get; set; }
        public int subCat1 { get; set; }
        public int subCat2 { get; set; }

    }
}

