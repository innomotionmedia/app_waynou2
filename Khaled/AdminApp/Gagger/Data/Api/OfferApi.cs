using Gagger.Helpers;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using Constants = Gagger.Helpers.Constants;

namespace Gagger.Data.Api
{
    public class OfferType
    {
        public string title { get; set; }
        public string id { get; set; }
        public string thumbnail { get; set; }
        public int order { get; set; }
        public string type { get; set; }
        public string extraInformation { get; set; }

    }
    public static class OfferApi
    {

        public static async Task DeleteOfferById(string Id)
        {
            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();

                string query = @"
                    DELETE
                    FROM [offer]
                    WHERE id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                        }

                    }

                }
            }
        }


        public static async Task<List<OfferType>> GetAllOffers(int start, int count)
        {

            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT *
                    FROM [Offer]     
                    ORDER BY DateCreated DESC
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
                                id = Convert.ToString(reader[reader.GetOrdinal("id")]),
                                order = Convert.ToInt32(reader[reader.GetOrdinal("order")]),
                                type = Convert.ToString(reader[reader.GetOrdinal("type")]),
                                extraInformation = Convert.ToString(reader[reader.GetOrdinal("extraInformation")]),
                                title = Convert.ToString(reader[reader.GetOrdinal("title")]),
                                thumbnail = Convert.ToString(reader.GetValue(reader.GetOrdinal("thumbnail"))),
                            };
                            ads.Add(x);
                        }

                        return ads;
                    }
                }
            }
        }


        public static async Task Createoffer(OfferType of)
        {
            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();
                string insertStatement = @"
                INSERT INTO [Offer] (
                    [id],
                    [title],
                    [thumbnail], 
                    [order],
                    [type],
                    [extraInformation]
                )
                VALUES (
                    @id,
                    @title,
                    @thumbnail,
                    @order,
                    @type,
                    @extraInformation

                );
            ";
                try
                {
                    using (SqlCommand command = new SqlCommand(insertStatement, connection))
                    {
                        command.Parameters.AddWithValue("@id", of.id);
                        command.Parameters.AddWithValue("@title",of.title);
                        command.Parameters.AddWithValue("@thumbnail", of.thumbnail);
                        command.Parameters.AddWithValue("@order", of.order);
                        command.Parameters.AddWithValue("@type", of.type);
                        command.Parameters.AddWithValue("@extraInformation", of.extraInformation);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read()) { }
                        }
                    }
                }
                catch (Exception ex)
                {
                }

            }
        }

    }

}
