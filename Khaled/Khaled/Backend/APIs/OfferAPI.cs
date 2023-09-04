using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Com.Kumulos;
using Com.Kumulos.Abstractions;
using Khaled.Helpers;
using TodoApp.Data;
using Xamarin.Forms;

namespace Khaled.Backend.APIs
{
    public class OfferAPI
    {


        public static async Task<List<OfferType>> GetOffers(int start, int count)
        {

            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT *
                    FROM Offer
                    ORDER BY [order]
                    OFFSET @Start ROWS FETCH NEXT @Count ROWS ONLY;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@Start", start);
                    command.Parameters.AddWithValue("@Count", count);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        List<OfferType> ads = new List<OfferType>();
                        while (reader.Read())
                        {
                            // var thumb = reader.GetValue(reader.GetOrdinal("thumbnail"));
                            // byte[] thumbnail = { };
                            // if (!string.IsNullOrEmpty(thumb.ToString())) thumbnail = (byte[])thumb;

                            var x = new OfferType
                            {
                                title = Convert.ToString(reader[reader.GetOrdinal("title")]),
                                extraInformation = Convert.ToString(reader[reader.GetOrdinal("extraInformation")]),
                                type = Convert.ToString(reader[reader.GetOrdinal("type")]),
                                thumbnail = Convert.ToString(reader.GetValue(reader.GetOrdinal("thumbnail"))),
                            };
                            ads.Add(x);
                        }

                        return ads;
                    }
                }

            }
        }
    }

    public enum OfferTypeEnum
    {
        link = 0,
        ad = 1
    }

    public class OfferType
    {
        public string title { get; set; }
        public string id { get; set; }
        public string thumbnail { get; set; }
        public ImageSource thumbnailImage { get; set; }
        public int order { get; set; }
        public string type { get; set; }
        public string extraInformation { get; set; }

    }
}