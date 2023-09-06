using static System.Net.Mime.MediaTypeNames;
using System.Text;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Gagger.Helpers
{
    public static class Converters
    {
        public static string ReturnBase64ImageSourceFromBase64String(string input)
        {
            return "data:image/png;base64," + input;
        }

        public static int GetBase64ImageSize(string base64Image)
        {
            // Convert base64 to bytes
            byte[] imageBytes = Convert.FromBase64String(base64Image);

            // Get the size in bytes
            return imageBytes.Length;
        }

        public static string ReturnRandomString(int lenggth)
        { 
           
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, lenggth)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static long UnixTimestampFromDateTime(DateTime date)
        {
            long unixTimestamp = date.Ticks - new DateTime(1970, 1, 1).Ticks;
            unixTimestamp /= TimeSpan.TicksPerSecond;
            return unixTimestamp;
        }
    }
}
