
using Khaled.Backend.APIs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TodoApp.Data;
using TextType = TodoApp.Data.TextType;

namespace Khaled.Helpers
{
    public class Converters
    {
        public static string ReturnCorrectLingua(FullAdType input, TextType type)
        {
            switch (CachedUser.localCode)
            {
                case localCodes.en:
                    switch (type)
                    {
                        case TextType.adDescription:
                            return input.descriptionENG;
                        case TextType.adTitle:
                            return input.title;
                        default: return "";
                    }
                case localCodes.de:
                    switch (type)
                    {
                        case TextType.adDescription:
                            return input.descriptionGER;
                        case TextType.adTitle:
                            return input.titleDe;
                        default: return "";

                    }
                case localCodes.ar:
                    switch (type)
                    {
                        case TextType.adDescription:
                            return input.descriptionAR;
                        case TextType.adTitle:
                            return input.titleAr;
                        default: return "";

                    }
                default:
                    return "";
            }
        }

        public static string FormatPhoneNumber(string input, string areaCode)
        {
            if (input.StartsWith("0"))
            {
                // remove all spaces and evetthing that isnt a number 
                string value = Regex.Replace(input, "[^0-9]", "");
                string withoutFirst = value.Remove(0, 1);
                return areaCode + withoutFirst;
            }
            else
            {
                return null;
            }
        }



        public static string GetRandomString(int length)
        {

            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string FormatPhoneNumberToJustNumber(string input)
        {
            if (input.StartsWith("+"))
            {
                string withoutFirst = input.Remove(0, 1);
                // add 0 to the start (0 151)
                withoutFirst = "00" + withoutFirst;
                input = withoutFirst;
            }

            // remove all spaces and evetthing that isnt a number 
            return Regex.Replace(input, "[^0-9]", "");
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        public static byte[] StreamToByteArray(Stream input)
        {
            if (input.CanSeek)
            {
                input.Position = 0;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static long UnixTimestampFromDateTime(DateTime date)
        {
            long unixTimestamp = date.Ticks - new DateTime(1970, 1, 1).Ticks;
            unixTimestamp /= TimeSpan.TicksPerSecond;
            return unixTimestamp;
        }

        public static Stream ByteArrayToStream(byte[] arr)
        {
            byte[] myByteArray = arr;
            MemoryStream stream = new MemoryStream(myByteArray);
            return stream;
        }

        public static int ReturnPriceInCents(string value)
        {
            if (value.Contains(","))
                value = value.Replace(",", string.Empty);
            else
                value = value.Replace(".", string.Empty);

            return Int32.Parse(value);
        }

       // public static ImageSource ReturnImageSourceFromString(string source)
       // {
       //     byte[] arr = StringToByteArray(source);
       //     return ImageSource.FromStream(() => new MemoryStream(arr));
       // }

        public static string GetDecimalPriceFromCents(int priceInCents)
        {
            string res = "";

            if (priceInCents < 100)
            {
                // is less than one eur
                res = "0." + priceInCents;
            }
            else
            {
                double decimalPrice = float.Parse(priceInCents.ToString()) / 100f;
                res = decimalPrice.ToString("0.00");

                if (res.Contains(','))
                    res = res.Replace(',', '.');
            }


            return res;
        }

        public static string TurnCommaIntoDot(string input)
        {
            if (input.Contains(","))
            {
                input = input.Replace(',', '.');
            }
            return input;
        }

        public static string ReturnFormattedPrice(string priceWithOutCurrencySymbol)
        {
            if (priceWithOutCurrencySymbol == null)
                return "0,00";

            //remove currency or white space
            if (priceWithOutCurrencySymbol.Contains('€'))
            {
                priceWithOutCurrencySymbol = priceWithOutCurrencySymbol.Replace("€", String.Empty);
                priceWithOutCurrencySymbol = priceWithOutCurrencySymbol.Trim();

            }


            // check if multiple commas, if so return 0 
            int count = priceWithOutCurrencySymbol.Split(',').Length - 1;
            if (count > 1)
                return "0,00";

            // check if multiple dots, if so return 0 
            int count2 = priceWithOutCurrencySymbol.Split('.').Length - 1;
            if (count2 > 1)
                return "0,00";

            // replace dot with comma if contains 
            if (priceWithOutCurrencySymbol.Contains('.'))
                priceWithOutCurrencySymbol = priceWithOutCurrencySymbol.Replace('.', ',');


            //if starts with 0 and the next one isnt a comma or dot remove all zeros
            if (priceWithOutCurrencySymbol.StartsWith("0"))
            {
                string valueWithoutFirstChar = priceWithOutCurrencySymbol.Remove(0, 1);
                if (!valueWithoutFirstChar.StartsWith(",") && priceWithOutCurrencySymbol.Length != 1) // because could be zero
                {
                    //bullshit detected
                    priceWithOutCurrencySymbol = priceWithOutCurrencySymbol.Replace("0", String.Empty);
                    if (priceWithOutCurrencySymbol.StartsWith(","))
                        priceWithOutCurrencySymbol = "0" + priceWithOutCurrencySymbol;

                    if (priceWithOutCurrencySymbol == String.Empty)
                        return "0,00";
                }
            }

            string[] values = new string[2];
            List<string> valueList = new List<string>();

            valueList.Add("");
            valueList.Add("");

            // cause it will be either dot or comma 
            if (priceWithOutCurrencySymbol.Contains(","))
                values = priceWithOutCurrencySymbol.Split(',');
            else
                values = priceWithOutCurrencySymbol.Split('.');

            valueList[0] = values[0];


            if (values.Length == 1)
                valueList[1] = ",00";
            else if (values[1].Length == 1)
                valueList[1] = "," + values[1] + "0";
            else
                valueList[1] = "," + values[1];


            // disable more than 3 digits after comma
            if (valueList[1].Length > 3)
                valueList[1] = valueList[1].Substring(0, 3);


            if (valueList[1].Length > 3)
                valueList[1] = ",00";

            string result = valueList[0] + valueList[1];

            if (result == "0,")
                result = "0,00";

            if (result.StartsWith(","))
            {
                result = result.Remove(0, 1);
                if (result.StartsWith("0"))
                    result = result.Remove(0, 1);

                return ReturnFormattedPrice(result);
            }
            else
                return result;

        }

        public static string ByteArrayToString(byte[] arr)
        {
            if (arr == null)
                return "";
            string strb64 = Convert.ToBase64String(arr);
            return strb64;
        }

        public static byte[] StringToByteArray(string str)
        {
            byte[] array = Convert.FromBase64String(str);
            return array;
        }
 
        public static Stream StringToStream(string toStream)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(toStream);
            writer.Flush();
            return writer.BaseStream;
        }

        public static byte[] CombineByteArrays(byte[] first, byte[] second)
        {
            byte[] bytes = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, bytes, 0, first.Length);
            Buffer.BlockCopy(second, 0, bytes, first.Length, second.Length);
            return bytes;
        }
    }

    public class BooleanJsonConverter : JsonConverter
    {
        #region Overrides of JsonConverter

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            // Handle only boolean types.
            return objectType == typeof(bool);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader"/> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.Value.ToString().ToLower().Trim())
            {
                case "true":
                case "yes":
                case "y":
                case "1":
                    return true;
                case "false":
                case "no":
                case "n":
                case "0":
                    return false;
            }

            // If we reach here, we're pretty much going to throw an error so let's let Json.NET throw it's pretty-fied error message.
            return new JsonSerializer().Deserialize(reader, objectType);
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter"/> to write to.</param><param name="value">The value.</param><param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue((bool)value);
        }

        #endregion Overrides of JsonConverter
    }
}
