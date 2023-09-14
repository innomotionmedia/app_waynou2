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
