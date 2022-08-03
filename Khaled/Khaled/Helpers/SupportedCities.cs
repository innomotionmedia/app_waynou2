using System;
using System.Collections.Generic;
using Khaled.Resources;
using Xamarin.Forms;

namespace Khaled.Backend.Helpers
{
	public class GetSupportedCites
	{
		public static List<SupportedCities> ReturnCityList()
		{
			var berlin = new SupportedCities
			{
				displayName = AppResources.Berlin,
				name = CityNames.berlin,
				thumbnail = ImageSource.FromFile("img_brandenburg.jpeg"),
				centerLatitude = 52.520008,
				centerLongitude = 13.404954,
				radiusFromCenter = 50
			};
			var hamburg = new SupportedCities
			{
				displayName = AppResources.Hamburg,
				name = CityNames.hamburg,
				thumbnail = ImageSource.FromFile("hamburg_dummy.jpg"),
				centerLatitude = 53.551086,
				centerLongitude = 9.993682,
				radiusFromCenter = 50
			};
			var missing = new SupportedCities
			{
				displayName = AppResources.CommingSoon,
				name = CityNames.missing,
			};

			var returnList = new List<SupportedCities>();
			returnList.Add(berlin);
			returnList.Add(hamburg);
			returnList.Add(missing);
			returnList.Add(missing);
			returnList.Add(missing);
			return returnList;
		}
	}

	public class SupportedCities
	{
		public CityNames name { get; set; }
		public string displayName { get; set; }
		public double centerLatitude { get; set; }
		public double centerLongitude { get; set; }
		public ImageSource thumbnail { get; set; }
		public int radiusFromCenter { get; set; }

	}

	public enum CityNames
    {
		berlin = 0,
		hamburg = 1,
		missing = 2
    }


}

