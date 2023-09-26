using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Gagger.Data.Api
{
    public class RestHelper
    {
        public static async Task<Root> ReverseGeoCoding(string adresse)
        {

            adresse = adresse.Replace(" ", "%20"); // empty spaces need be better
            //https://maps.googleapis.com/maps/api/geocode/json?address=Hoisberg%2014%2022359&key=AIzaSyDfh-0G3VggM3UPtC-KgftvQBRgTdzeKHE
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Helpers.Constants.AUTOCOMPLETEURL1 + adresse.Trim() + Helpers.Constants.AUTOCOMPLETEURL2);

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("").Result;
            string content = (response.Content.ReadAsStringAsync().Result);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    client.Dispose();
                    return JsonConvert.DeserializeObject<Root>(content);
                }
                catch (Exception ex)
                {
                    return new Root();
                }
            }
            else
            {
                return new Root();
            }

        }
    }

    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Viewport
    {
        public Location northeast { get; set; }
        public Location southwest { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }

    public class PlusCode
    {
        public string compound_code { get; set; }
        public string global_code { get; set; }
    }

    public class Result
    {
        public List<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public PlusCode plus_code { get; set; }
        public List<string> types { get; set; }
    }

    public class Root
    {
        public List<Result> results { get; set; }
        public string status { get; set; }
    }
}
