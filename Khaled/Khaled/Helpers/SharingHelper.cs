using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Khaled.Helpers
{
	public class SharingHelper
	{
        public async static Task ShareUri(string uri)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = uri,
            });
        }
    }
}

