using System;
using System.Collections.Generic;
using Khaled.Helpers;
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
			var munich = new SupportedCities
			{
				displayName = AppResources.Munich,
				name = CityNames.munich,
				thumbnail = ImageSource.FromFile("munich_dummy.jpg"),
				centerLatitude = 48.137154,
				centerLongitude = 11.576124,
				radiusFromCenter = 50
			};
			var stuttgart = new SupportedCities
			{
				displayName = AppResources.Stuttgart,
				name = CityNames.stuttgart,
				thumbnail = ImageSource.FromFile("stuttgart.png"),
				centerLatitude = 48.783333,
				centerLongitude = 9.183333,
				radiusFromCenter = 50
			};
			var missing = new SupportedCities
			{
				displayName = AppResources.CommingSoon,
				name = CityNames.missing,
			};

			var returnList = new List<SupportedCities>();
			returnList.Add(hamburg);
			returnList.Add(berlin);
			returnList.Add(stuttgart);
			returnList.Add(munich);


			return returnList;
		}
	}



	public enum CityNames
    {
		berlin = 0,
		hamburg = 1,
		munich = 2,
		missing = 3,
		stuttgart = 4
    }


}

