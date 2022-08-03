using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Khaled.Helpers
{

        public class MapsHelper
        {

            public static async Task OpenMaps(string adresse)
            {
                string uriString = "https://maps.google.com/maps?q=" + adresse;

                await Launcher.OpenAsync(uriString);
            }

    }
}

