using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using Com.Kumulos;
using Com.Kumulos.Abstractions;
using Khaled.Helpers;
using TodoApp.Data;
using Xamarin.Forms;

namespace Khaled.Backend.APIs
{
    public class CategoryAPI
    {
        public static async Task<List<SubCatType>> GetSubCats1(string belongsToMain)
        {
            List<SubCatType> res = new List<SubCatType>();
            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT *
                    FROM [SubCategories]
                    WHERE belongsToMainCat = @belongsToMain";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@belongsToMain", belongsToMain);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            // var thumb = reader.GetValue(reader.GetOrdinal("thumbnail"));
                            // byte[] thumbnail = { };
                            // if (!string.IsNullOrEmpty(thumb.ToString())) thumbnail = (byte[])thumb;

                            var y = new SubCatType
                            {
                                title = Convert.ToString(reader[reader.GetOrdinal("titleEng")]),
                                titleAr = Convert.ToString(reader[reader.GetOrdinal("titleAr")]),
                                titleDe = Convert.ToString(reader[reader.GetOrdinal("titleGer")]),
                                belongsToMain = Convert.ToInt32(reader[reader.GetOrdinal("belongsToMainCat")]),
                                belongsToSubCat = Convert.ToString(reader[reader.GetOrdinal("belongsToSubCat")]),
                                Id = Convert.ToString(reader[reader.GetOrdinal("id")]),


                                //UnixCreated = DateTime.Convert.ToString(reader.GetValue(reader.GetOrdinal("UnixCreated"))),

                            };
                            res.Add(y);
                        }
                        return res;
                    }
                }
            }
        }

        public static async Task<List<SubCatType>> GetSubCats2(string belongsToSubCat0)
        {
            List<SubCatType> res = new List<SubCatType>();
            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT *
                    FROM [SubCategories]
                    WHERE belongsToSubCat = @belongsToSubCat";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@belongsToSubCat", belongsToSubCat0);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            // var thumb = reader.GetValue(reader.GetOrdinal("thumbnail"));
                            // byte[] thumbnail = { };
                            // if (!string.IsNullOrEmpty(thumb.ToString())) thumbnail = (byte[])thumb;

                            var y = new SubCatType
                            {
                                title = Convert.ToString(reader[reader.GetOrdinal("titleEng")]),
                                titleAr = Convert.ToString(reader[reader.GetOrdinal("titleAr")]),
                                titleDe = Convert.ToString(reader[reader.GetOrdinal("titleGer")]),
                                belongsToMain = Convert.ToInt32(reader[reader.GetOrdinal("belongsToMainCat")]),
                                belongsToSubCat = Convert.ToString(reader[reader.GetOrdinal("belongsToSubCat")]),
                                Id = Convert.ToString(reader[reader.GetOrdinal("id")]),

                                //UnixCreated = DateTime.Convert.ToString(reader.GetValue(reader.GetOrdinal("UnixCreated"))),

                            };
                            res.Add(y);
                        }
                        return res;
                    }
                }
            }
        }

        public static async Task<List<CategoryIds>> GetPrimaryKeyOfPath(int mainCat, int subCat1)
        {
            List<CategoryIds> res = new List<CategoryIds>();
            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT *
                    FROM [SubCategories]
                    WHERE belongsToSubCat = @belongsToSubCat
                    AND belongsToMainCat = @belongsToMainCat";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@belongsToMainCat", mainCat);
                    command.Parameters.AddWithValue("@belongsToSubCat", subCat1);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {

                            var y = new CategoryIds
                            {
                                tblCategoryIdID = Convert.ToString(reader[reader.GetOrdinal("id")]),
                                mainCat = Convert.ToInt32(reader[reader.GetOrdinal("belongsToMainCat")]),
                                subCat1 = Convert.ToInt32(reader[reader.GetOrdinal("belongsToSubCat")]),
                            };
                            res.Add(y);
                        }
                        return res;
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
        public string Id { get; set; }
        public int belongsToMain { get; set; }
        public string belongsToSubCat { get; set; }


        public FlowDirection flowDirection { get; set; } = FlowDirection.LeftToRight;
    }

    public class CategoryIds
    {
        public string tblCategoryIdID { get; set; }
        public int mainCat { get; set; }
        public int subCat1 { get; set; }
        public int subCat2 { get; set; }

    }
}

