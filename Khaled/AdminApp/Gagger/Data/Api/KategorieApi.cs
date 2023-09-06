using Gagger.Helpers;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using Constants = Gagger.Helpers.Constants;

namespace Gagger.Data.Api
{
    public static class Category
    { 

        public static async Task<List<CategoryType>> GetSubCatsInMainCat(string belongsToMainCat)
        {
             List < CategoryType >res = new List<CategoryType>();
            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT *
                    FROM [SubCategories]
                    WHERE belongsToMainCat = @belongsToMainCat";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@belongsToMainCat", belongsToMainCat);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            var f = new CategoryType
                            {
                                Id = Convert.ToString(reader[reader.GetOrdinal("id")]),
                                belongsToMainCat = Convert.ToInt32(reader[reader.GetOrdinal("belongsToMainCat")]),
                                belongsToSubCat = Convert.ToInt32(reader[reader.GetOrdinal("belongsToSubCat")]),
                                belongsToSubSubCat = Convert.ToInt32(reader[reader.GetOrdinal("belongsToSubSubCat")]),
                                titleGer = Convert.ToString(reader[reader.GetOrdinal("titleGer")]),
                                titleAr = Convert.ToString(reader[reader.GetOrdinal("titleAr")]),
                                titleEng = Convert.ToString(reader[reader.GetOrdinal("titleEng")]),
                            };
                            res.Add(f);
                        }

                    }
                    return res;

                }
            }
        }
        public static async Task DeleteCatbyId(string Id)
        {
            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();

                string query = @"
                    DELETE
                    FROM [SubCategories]
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

        public class CategoryType
        {
            public string Id { get; set; }
            public int belongsToMainCat { get; set; }
            public int belongsToSubCat { get; set; }
            public int belongsToSubSubCat { get; set; }
            public string titleGer { get; set; }
            public string titleAr { get; set; }
            public string titleEng { get; set; }

        }

    }

}
