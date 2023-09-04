﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Khaled.Helpers;
using TodoApp.Data;


namespace Khaled.Backend.APIs
{
    public class AdsAPI
    {


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
                                //UnixCreated = DateTime.Convert.ToString(reader.GetValue(reader.GetOrdinal("UnixCreated"))),

                            };
                            res = x; 
                        }
                        return res;
                    }
                }
            }
        }

        public static async Task<List<FullAdType>> GetAdsByTitle(string title, int start, int count)
        {
            List<FullAdType> ads = new List<FullAdType>();
            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT title, titleDe, description, descriptionAR, descriptionGER, adresse, tblAdID, email, hours, descriptionENG, titleAr, telephone, web, distance, thumbnail, UnixCreated,
                    FROM [Ads]
                    WHERE title LIKE '%@title%'
                    OFFSET @Start ROWS FETCH NEXT @Count ROWS ONLY;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@Start", start);
                    command.Parameters.AddWithValue("@Count", count);

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
                                thumbnail = Convert.ToString(reader.GetValue(reader.GetOrdinal("thumbnail"))),
                                //UnixCreated = DateTime.Convert.ToString(reader.GetValue(reader.GetOrdinal("UnixCreated"))),

                            };
                            ads.Add(x);
                        }

                        return ads;
                    }
                }
            }
        }

        public static async Task<List<FullAdType>> GetAdsByTitleCat1(string title, int start, int count, string belongsToMain)
        {
            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();
                List<FullAdType> ads = new List<FullAdType>();

                var local = CachedUser.localCode;
                string table = "title";
                if (local == localCodes.de) table = "titleDe";
                if (local == localCodes.ar) table = "titleAr";


                string query = @"
                SELECT title, titleDe, description, descriptionAR, descriptionGER, adresse, tblAdID, email, hours, descriptionENG, titleAr, telephone, web, distance, thumbnail, UnixCreated
                FROM [Ads]
                WHERE "+ table + " LIKE '%' + @title + '%' AND Category = @category ORDER BY UnixCreated OFFSET @Start ROWS FETCH NEXT @Count ROWS ONLY;";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@Start", start);
                    command.Parameters.AddWithValue("@Count", count);
                    command.Parameters.AddWithValue("@category", belongsToMain);


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
                                thumbnail = Convert.ToString(reader.GetValue(reader.GetOrdinal("thumbnail"))),
                                //UnixCreated = DateTime.Convert.ToString(reader.GetValue(reader.GetOrdinal("UnixCreated"))),

                            };
                            ads.Add(x);
                        }

                        return ads;
                    }
                }
            }
        }

        public static async Task<List<FullAdType>> GetAdsByTitleCat2(string title, int start, int count, string subcategory)
        {
            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT title, titleDe, description, descriptionAR, descriptionGER, adresse, tblAdID, email, hours, descriptionENG, titleAr, telephone, web, distance, thumbnail, UnixCreated,
                    FROM [Ads]
                    WHERE title LIKE '%@title%'
                    AND Category = @subcategory
                    OFFSET @Start ROWS FETCH NEXT @Count ROWS ONLY;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@Start", start);
                    command.Parameters.AddWithValue("@Count", count);
                    command.Parameters.AddWithValue("@subcategory", subcategory);

                    List<FullAdType> ads = new List<FullAdType>();
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
                                thumbnail = Convert.ToString(reader.GetValue(reader.GetOrdinal("thumbnail"))),
                                //UnixCreated = DateTime.Convert.ToString(reader.GetValue(reader.GetOrdinal("UnixCreated"))),

                            };
                            ads.Add(x);
                        }

                        return ads;
                    }
                }
            }
        }

        public static async Task<List<FullAdType>> GetAllAds(int start, int count, int maxKmRadius, double inputLat, double inputLong, string categoryId, string title)
        {
            var lat = Converters.TurnCommaIntoDot(inputLat.ToString());
            var longi = Converters.TurnCommaIntoDot(inputLong.ToString());

            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT title, titleDe, description, descriptionAR, descriptionGER, adresse, tblAdID, email, hours, descriptionENG, titleAr, telephone, web, distance, thumbnail, UnixCreated,
                        (6371 * ACOS(
                            COS(RADIANS(@InputLat)) * COS(RADIANS(latitude)) * COS(RADIANS(longitude) - RADIANS(@InputLong)) +
                            SIN(RADIANS(@InputLat)) * SIN(RADIANS(latitude))
                        )) AS Distance
                    FROM [Ads]
                    WHERE
                        (6371 * ACOS(
                            COS(RADIANS(@InputLat)) * COS(RADIANS(latitude)) * COS(RADIANS(longitude) - RADIANS(@InputLong)) +
                            SIN(RADIANS(@InputLat)) * SIN(RADIANS(latitude))
                        )) <= @MaxKmRadius
                    ORDER BY tblAdID
                    OFFSET @Start ROWS FETCH NEXT @Count ROWS ONLY;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@InputLat", lat);
                    command.Parameters.AddWithValue("@InputLong", longi);
                    command.Parameters.AddWithValue("@MaxKmRadius", maxKmRadius);
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
                                //UnixCreated = DateTime.Convert.ToString(reader.GetValue(reader.GetOrdinal("UnixCreated"))),

                            };
                            ads.Add(x);
                        }

                        return ads;
                    }
                }
            }
        }

        public static async Task<List<FullAdType>> GetAllAds_Cat1(int start, int count, int maxKmRadius, double inputLat, double inputLong, string categoryId, string title)
        {
            var lat = Converters.TurnCommaIntoDot(inputLat.ToString());
            var longi = Converters.TurnCommaIntoDot(inputLong.ToString());

            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT title, titleDe, description, descriptionAR, descriptionGER, adresse, tblAdID, email, hours, descriptionENG, titleAr, telephone, web, distance, thumbnail, UnixCreated,
                        (6371 * ACOS(
                            COS(RADIANS(@InputLat)) * COS(RADIANS(latitude)) * COS(RADIANS(longitude) - RADIANS(@InputLong)) +
                            SIN(RADIANS(@InputLat)) * SIN(RADIANS(latitude))
                        )) AS Distance
                    FROM [Ads]
                    WHERE
                    category = @category
                    AND
                        (6371 * ACOS(
                            COS(RADIANS(@InputLat)) * COS(RADIANS(latitude)) * COS(RADIANS(longitude) - RADIANS(@InputLong)) +
                            SIN(RADIANS(@InputLat)) * SIN(RADIANS(latitude))
                        )) <= @MaxKmRadius
                    ORDER BY tblAdID
                    OFFSET @Start ROWS FETCH NEXT @Count ROWS ONLY;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@InputLat", lat);
                    command.Parameters.AddWithValue("@InputLong", longi);
                    command.Parameters.AddWithValue("@MaxKmRadius", maxKmRadius);
                    command.Parameters.AddWithValue("@Start", start);
                    command.Parameters.AddWithValue("@Count", count);
                    command.Parameters.AddWithValue("@category", categoryId);


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
                                //UnixCreated = DateTime.Convert.ToString(reader.GetValue(reader.GetOrdinal("UnixCreated"))),

                            };
                            ads.Add(x);
                        }

                        return ads;
                    }
                }

            }
        }

        public static async Task<List<FullAdType>> GetAllAds_Cat2(int start, int count, int maxKmRadius, double inputLat, double inputLong, int subcategory, int subsubcategory, string title)
        {
            var lat = Converters.TurnCommaIntoDot(inputLat.ToString());
            var longi = Converters.TurnCommaIntoDot(inputLong.ToString());

            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT title, titleDe, description, descriptionAR, descriptionGER, adresse, tblAdID, email, hours, descriptionENG, titleAr, telephone, web, distance, thumbnail, UnixCreated,
                        (6371 * ACOS(
                            COS(RADIANS(@InputLat)) * COS(RADIANS(latitude)) * COS(RADIANS(longitude) - RADIANS(@InputLong)) +
                            SIN(RADIANS(@InputLat)) * SIN(RADIANS(latitude))
                        )) AS Distance
                    FROM [Ads]
                    WHERE
                    subcategory = @subcategory
                    AND
                    subsubcategory = @subsubcategory
                    AND
                        (6371 * ACOS(
                            COS(RADIANS(@InputLat)) * COS(RADIANS(latitude)) * COS(RADIANS(longitude) - RADIANS(@InputLong)) +
                            SIN(RADIANS(@InputLat)) * SIN(RADIANS(latitude))
                        )) <= @MaxKmRadius
                    ORDER BY tblAdID
                    OFFSET @Start ROWS FETCH NEXT @Count ROWS ONLY;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@InputLat", lat);
                    command.Parameters.AddWithValue("@InputLong", longi);
                    command.Parameters.AddWithValue("@MaxKmRadius", maxKmRadius);
                    command.Parameters.AddWithValue("@Start", start);
                    command.Parameters.AddWithValue("@Count", count);
                    command.Parameters.AddWithValue("@subcategory", subcategory);
                    command.Parameters.AddWithValue("@subsubcategory", subsubcategory);


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
                                //UnixCreated = DateTime.Convert.ToString(reader.GetValue(reader.GetOrdinal("UnixCreated"))),

                            };
                            ads.Add(x);
                        }

                        return ads;
                    }
                }

            }
        }

        public static async Task<FullAdType> GetFullPic(int tblAdID)
        {
            List<FullAdType> ads = new List<FullAdType>();
            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT fullPic
                    FROM [Ads]
                    WHERE tblAdID LIKE '%@tblAdID%'
                    OFFSET @Start ROWS FETCH NEXT @Count ROWS ONLY;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@tblAdID", tblAdID);


                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            // var thumb = reader.GetValue(reader.GetOrdinal("thumbnail"));
                            // byte[] thumbnail = { };
                            // if (!string.IsNullOrEmpty(thumb.ToString())) thumbnail = (byte[])thumb;

                            var x = new FullAdType
                            {

                                fullPic = Convert.ToString(reader.GetValue(reader.GetOrdinal("fullPic"))),

                            };
                            return x;
                        }

                        return new FullAdType();
                    }
                }
            }
    }

    }

    public class AdsType
    {
        public string title { get; set; }
        public string titleAr { get; set; }
        public string titleDe { get; set; }

        public string description { get; set; }

        //public string tintColor { get; set; } = "Transparent";

       // public ImageSource FaveImageSource { get; set; }

        public string tblAdID { get; set; }
        public long UnixCreated { get; set; }
        public string thumbnail { get; set; }
       // public ImageSource imageSource_fullPic { get; set; }
        public string fullPic { get; set; }

        public string distance { get; set; }

       // public FlowDirection flowDirection { get; set; } = FlowDirection.LeftToRight;

        public bool IsDistanceVisible { get; set; } = true; 

    }

    public class FullAdType : AdsType
    {
        public string descriptionENG { get; set; }
        public string descriptionGER { get; set; }
        public string descriptionAR { get; set; }

        public string adresse { get; set; }
        public string telephone { get; set; }
        public string hours { get; set; }
        public string email { get; set; }
        public string web { get; set; }


    }
}