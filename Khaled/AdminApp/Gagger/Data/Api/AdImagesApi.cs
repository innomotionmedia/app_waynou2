using Gagger.Helpers;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using Constants = Gagger.Helpers.Constants;

namespace Gagger.Data.Api
{
    public static class AdImagesApi
    {
        public static async Task<List<AdImageType>> GetAdImages(string belongsToAdId)
        {
            var res = new List<AdImageType>();
            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT *
                    FROM [AdImages]
                    WHERE BelongsToAdId = @belongsToAdId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@belongsToAdId", belongsToAdId);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            // var thumb = reader.GetValue(reader.GetOrdinal("thumbnail"));
                            // byte[] thumbnail = { };
                            // if (!string.IsNullOrEmpty(thumb.ToString())) thumbnail = (byte[])thumb;

                            var x = new AdImageType
                            {
                                Image = Convert.ToString(reader[reader.GetOrdinal("Image")]),
                                Position = Convert.ToInt32(reader[reader.GetOrdinal("Position")]),
                                Id = Convert.ToString(reader[reader.GetOrdinal("Id")]),
                            };
                            res.Add(x);
                        }
                        return res;
                    }
                }
            }
        }

        public static async Task CreateAdImage(AdImageType adImage)
        {
            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();
                string insertStatement = @"
                INSERT INTO [AdImages] (
                    [Id],
                    [Image],
                    [BelongsToAdId],
                    [Position]
                )
                VALUES (
                    @Id,
                    @Image,
                    @BelongsToAdId,
                    @Position
                );
            ";
                try
                {
                    using (SqlCommand command = new SqlCommand(insertStatement, connection))
                    {
                        command.Parameters.AddWithValue("@Id", adImage.Id);
                        command.Parameters.AddWithValue("@Image", adImage.Image);
                        command.Parameters.AddWithValue("@BelongsToAdId", adImage.BelongsToAdId);
                        command.Parameters.AddWithValue("@Position", adImage.Position);
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

   
        public class AdImageType
        {
            public string Id { get; set; }
            public string Image { get; set; }
            public string BelongsToAdId { get; set; }

            public int Position { get; set; }
            public DateTime DateCreated { get; set; }

        }


    }

}
