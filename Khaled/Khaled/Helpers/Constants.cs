using System;
using TodoApp.Data;
using Xamarin.Forms;

namespace Khaled.Helpers
{
    public class CachedUser
    {
        public static localCodes localCode;
        public static double lati = 0;
        public static double longi = 0;
        public static int radius = 0;
        public static SupportedCities pickedCity;
        public static bool cityPicked = false;

    }

    public class SupportedCities
    {
        public Backend.Helpers.CityNames name { get; set; }
        public string displayName { get; set; }
        public double centerLatitude { get; set; }
        public double centerLongitude { get; set; }
        public ImageSource thumbnail { get; set; }
        public int radiusFromCenter { get; set; }

    }
}

