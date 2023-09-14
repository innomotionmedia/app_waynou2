using Gagger.Helpers;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using Constants = Gagger.Helpers.Constants;

namespace Gagger.Data.Api
{
    public static class AdsApi
    {
        public static async Task<List<FullAdType>> GetAllAds(int start, int count)
        {

            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT title, titleDe, description, descriptionAR, descriptionGER, adresse, tblAdID, email, hours, descriptionENG, titleAr, telephone, web, distance, thumbnail
                    FROM [Ads]     
                    ORDER BY datecreated DESC
                    OFFSET @Start ROWS FETCH NEXT @Count ROWS ONLY;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@Start", start);
                    command.Parameters.AddWithValue("@Count", count);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        List<FullAdType> ads = new List<FullAdType>();
                        while (reader.Read())
                        {
                            // var thumb = reader.GetValue(reader.GetOrdinal("thumbnail"));
                            // byte[] thumbnail = { };
                            // if (!string.IsNullOrEmpty(thumb.ToString())) thumbnail = (byte[])thumb;

                            var x = new FullAdType
                            {
                                title = Convert.ToString(reader[reader.GetOrdinal("title")]),
                                titleDe = Convert.ToString(reader[reader.GetOrdinal("titleDe")]),
                                description = Convert.ToString(reader[reader.GetOrdinal("description")]),
                                descriptionAR = Convert.ToString(reader[reader.GetOrdinal("descriptionAR")]),
                                descriptionGER = Convert.ToString(reader[reader.GetOrdinal("descriptionGER")]),
                                adresse = Convert.ToString(reader[reader.GetOrdinal("adresse")]),
                                tblAdID = Convert.ToString(reader[reader.GetOrdinal("tblAdID")]),
                                email = Convert.ToString(reader[reader.GetOrdinal("email")]),
                                hours = Convert.ToString(reader[reader.GetOrdinal("hours")]),
                                descriptionENG = Convert.ToString(reader[reader.GetOrdinal("descriptionENG")]),
                                titleAr = Convert.ToString(reader[reader.GetOrdinal("titleAr")]),
                                telephone = Convert.ToString(reader[reader.GetOrdinal("telephone")]),
                                web = Convert.ToString(reader[reader.GetOrdinal("web")]),
                                distance = Convert.ToString(reader[reader.GetOrdinal("Distance")]),
                                thumbnail = Convert.ToString(reader.GetValue(reader.GetOrdinal("thumbnail"))),
                            };
                            ads.Add(x);
                        }

                        return ads;
                    }
                }
            }
        }

        public static async Task<FullAdType> GetFullAdInfo(string tblAdID)
        {

            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT *
                    FROM [Ads]
                    WHERE tblAdID = @tblAdID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@tblAdID", tblAdID);

                    FullAdType res = new FullAdType();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            // var thumb = reader.GetValue(reader.GetOrdinal("thumbnail"));
                            // byte[] thumbnail = { };
                            // if (!string.IsNullOrEmpty(thumb.ToString())) thumbnail = (byte[])thumb;

                            var x = new FullAdType
                            {
                                title = Convert.ToString(reader[reader.GetOrdinal("title")]),
                                titleDe = Convert.ToString(reader[reader.GetOrdinal("titleDe")]),
                                description = Convert.ToString(reader[reader.GetOrdinal("description")]),
                                descriptionAR = Convert.ToString(reader[reader.GetOrdinal("descriptionAR")]),
                                descriptionGER = Convert.ToString(reader[reader.GetOrdinal("descriptionGER")]),
                                adresse = Convert.ToString(reader[reader.GetOrdinal("adresse")]),
                                tblAdID = Convert.ToString(reader[reader.GetOrdinal("tblAdID")]),
                                email = Convert.ToString(reader[reader.GetOrdinal("email")]),
                                hours = Convert.ToString(reader[reader.GetOrdinal("hours")]),
                                descriptionENG = Convert.ToString(reader[reader.GetOrdinal("descriptionENG")]),
                                titleAr = Convert.ToString(reader[reader.GetOrdinal("titleAr")]),
                                telephone = Convert.ToString(reader[reader.GetOrdinal("telephone")]),
                                web = Convert.ToString(reader[reader.GetOrdinal("web")]),
                                distance = Convert.ToString(reader[reader.GetOrdinal("Distance")]),
                                fullPic = Convert.ToString(reader.GetValue(reader.GetOrdinal("fullPic"))),
                                thumbnail = Convert.ToString(reader.GetValue(reader.GetOrdinal("thumbnail")))


                            };
                            res = x;
                        }
                        return res;
                    }
                }
            }
        }

        public static async Task<List<AdsType>> CreateAd(FullAdType ad)
        {
            List<AdsType> res = new List<AdsType>();
            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();
                string insertStatement = @"
                INSERT INTO [Ads] (
                    [title],
                    [titleAr],
                    [titleDe],
                    [description],
                    [tblAdID],
                    [thumbnail],
                    [fullPic],
                    [descriptionENG],
                    [descriptionGER],
                    [descriptionAR],
                    [adresse],
                    [telephone],
                    [hours],
                    [email],
                    [web],
                    [latitude],
                    [longitude],
                    [category],
                    [subcategory],
                    [subsubcategory]
                )
                VALUES (
                    @title,
                    @titleAr,
                    @titleDe,
                    @description,
                    @tblAdID,
                    @thumbnail,
                    @fullPic,
                    @descriptionENG,
                    @descriptionGER,
                    @descriptionAR,
                    @adresse,
                    @telephone,
                    @hours,
                    @email,
                    @web,
                    @latitude,
                    @longitude,
                    @category,
                    @subcategory,
                    @subsubcategory
                );
            ";
                try
                {
                    using (SqlCommand command = new SqlCommand(insertStatement, connection))
                    {
                        command.Parameters.AddWithValue("@tblAdID", ad.tblAdID);
                        command.Parameters.AddWithValue("@title", ad.title);
                        command.Parameters.AddWithValue("@titleAr", ad.titleAr);
                        command.Parameters.AddWithValue("@titleDe", ad.titleDe);
                        command.Parameters.AddWithValue("@description", ad.description);
                        command.Parameters.AddWithValue("@thumbnail", ad.thumbnail);
                        command.Parameters.AddWithValue("@fullPic", ad.fullPic);
                        command.Parameters.AddWithValue("@descriptionENG", ad.descriptionENG);
                        command.Parameters.AddWithValue("@descriptionGER", ad.descriptionGER);
                        command.Parameters.AddWithValue("@descriptionAR", ad.descriptionAR);
                        command.Parameters.AddWithValue("@adresse", ad.adresse);
                        command.Parameters.AddWithValue("@telephone", ad.telephone);
                        command.Parameters.AddWithValue("@hours", ad.hours);
                        command.Parameters.AddWithValue("@email", ad.email);
                        command.Parameters.AddWithValue("@web", ad.web);
                        command.Parameters.AddWithValue("@latitude", ad.latitude);
                        command.Parameters.AddWithValue("@longitude", ad.longitude);
                        command.Parameters.AddWithValue("@category", ad.category);
                        command.Parameters.AddWithValue("@subcategory", ad.subcategory);
                        command.Parameters.AddWithValue("@subsubcategory", ad.subsubcategory);


                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read()) { }
                        }
                    }
                }
                catch (Exception ex)
                {
                }


                // now get user and return my dude 
                //var x = await GetUserFromEmail(user.Email);
                return null; // todo
            }
        }

        public static async Task DeleteAdById(string Id)
        {
            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();

                string query = @"
                    DELETE
                    FROM [Ads]
                    WHERE tblAdID = @Id";

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
        public class SubCatType
        {
            //abuse title to show localised title in reload adapter
            public string title { get; set; }
            public string titleAr { get; set; }
            public string titleDe { get; set; }
            public string picture { get; set; }
            public int tblSubCat1ID { get; set; } // the id to get to the next layer. 0 means, no more layers
            public int tblSubCat2ID { get; set; } // the id to get to the next layer. 0 means, no more layers

            public int belongsToMain { get; set; }
            public int belongsToSubCat { get; set; }

            public int belongsToSubSubCat { get; set; }

        }

        public class AdsType
        {

            public string title { get; set; }
            public string titleAr { get; set; }
            public string titleDe { get; set; }

            public string description { get; set; }
            public string tblAdID { get; set; }
            public DateTime UnixCreated { get; set; }
            public string thumbnail { get; set; }
            public string fullPic { get; set; }

            public string distance { get; set; }

            public bool IsDistanceVisible { get; set; } = true;

        }

        public class FullAdType : AdsType
        {
            public string descriptionENG { get; set; }
            public string descriptionGER { get; set; }
            public string descriptionAR { get; set; }
            public float latitude { get; set; }
            public float longitude { get; set; }

            public int category { get; set; }
            public string subcategory { get; set; }
            public string subsubcategory { get; set; }

            public string adresse { get; set; }
            public string telephone { get; set; }
            public string hours { get; set; }
            public string email { get; set; }
            public string web { get; set; }


        }
    }

}
