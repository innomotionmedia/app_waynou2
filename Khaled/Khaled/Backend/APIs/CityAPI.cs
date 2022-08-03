using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Kumulos;
using Com.Kumulos.Abstractions;

namespace Khaled.Backend.APIs
{
    public class CityAPI
    {
        public static List<CitiesType> Deserialize(ApiResponse response)
        {
            List<CitiesType> ad = new List<CitiesType>();
            var array = (Newtonsoft.Json.Linq.JArray)response.payload;
            foreach (var item in array)
            {
                CitiesType x = (CitiesType)(item.ToObject(typeof(CitiesType)));
                ad.Add(x);
            }
            return ad;
        }

        public static async Task<ApiResponse> GetAllCities()
        {
            var parameters = new List<KeyValuePair<string, string>>
            {
                //new KeyValuePair<string, string>("tblAdID", tblAdID.ToString()),
            };
            return await Kumulos.Current.Build.CallAPI("getAllCities", parameters).ConfigureAwait(false);
        }
    }

    public class CitiesType
    {
        public string cityName { get; set; }
        public string cityNameAR { get; set; }
        public string cityNameGER { get; set; }
        public string tblSupportedCityID { get; set; }
        public double longitudeCityCenter { get; set; }
        public double latitudeCityCenter { get; set; }

    }
}
