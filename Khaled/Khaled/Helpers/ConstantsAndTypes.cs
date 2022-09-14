using System;
using System.Collections.Generic;
using Khaled.Backend.Helpers;

namespace Khaled.Helpers
{
    public class Constants
    {
        public static int AdsToLoadAtOnce = 5;
        public static int OffersToLoadAtOnce = 4;
        public static int sliderBaseValue = 50;

        public static string Delimiter = ";-";

        public static string PREFKEY_CITYPICKED = "CITYPICKED";
        public static string PREFKEY_CITYLONG= "PREFKEY_CITYLONG";
        public static string PREFKEY_CITYLAT = "PREFKEY_CITYLAT";
        public static string PREFKEY_CITYRAD = "PREFKEY_CITYRAD";
        public static string CHOSENLANG = "chosen_lang";


        public static string WebappBaseUrl = "https://waynou.de/webapp/v1/index.html?"; //bsp https://waynou.de/webapp/v1/index.html?adId=5&local=eng

        public static string urlDataPrivacy = "https://waynou.de/dataprivacy.html";
        public static string urlImprint = "https://waynou.de/imprint.html";
        public static string urlToS = "https://waynou.de/termsofuse.html";



        public static int[] appPath = new int[3];
    }

    public class CachedUser
    {
        public static localCodes localCode;
        public static double lati = 0;
        public static double longi = 0;
        public static int radius = 0;
        public static SupportedCities pickedCity; 
        public static bool cityPicked = false; 
        
    }

    public enum CategoriesEnum
    {
        // Ids must be identical to belongsTo in subCat1s on server
        food = 1,
        health = 2,
        shopping = 3,
        law = 4,
        car = 5,
        beauty = 6,
        dailyShopping = 7,
        hotels = 8,
        travel = 9,
        construction = 10,
        rentHouse = 11,
        services = 12,
        sights = 13,
        happenings = 14,
        culture = 15,
        hobby = 16,
        jobs = 17,
        edu = 18,
        orgas = 19,
        authorities = 20,
        religion = 21,
        emergenices = 22
    }


    public enum LayerEnum
    {
        subCat1 = 0,
        subCat2 = 1
    }


    public enum localCodes
    {
        en = 0,
        de = 1,
        ar = 2
    }

    public enum TextType
    {
        adDescription = 1,
        adTitle = 2,
    }
}
