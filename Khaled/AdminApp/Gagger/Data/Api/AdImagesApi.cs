using Gagger.Helpers;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using Constants = Gagger.Helpers.Constants;

namespace Gagger.Data.Api
{
    public static class AdImagesApi
    {

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
