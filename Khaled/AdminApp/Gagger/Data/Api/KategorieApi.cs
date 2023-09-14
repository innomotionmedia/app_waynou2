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
                                belongsToSubCat = Convert.ToString(reader[reader.GetOrdinal("belongsToSubCat")]),
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

        public static async Task<List<CategoryType>> GetSubSubCatsInSubCat(string belongsToSubCat)
        {
            List<CategoryType> res = new List<CategoryType>();
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
                    command.Parameters.AddWithValue("@belongsToSubCat", belongsToSubCat);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            var f = new CategoryType
                            {
                                Id = Convert.ToString(reader[reader.GetOrdinal("id")]),
                                belongsToMainCat = Convert.ToInt32(reader[reader.GetOrdinal("belongsToMainCat")]),
                                belongsToSubCat = Convert.ToString(reader[reader.GetOrdinal("belongsToSubCat")]),
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

        public static async Task DeleteSubCatbyId(string Id)
        {
            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();

                string query = @"
                    DELETE
                    FROM [SubCategories]
                    WHERE belongsToSubCat = @Id";

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

        public static async Task CreateCategory(CategoryType cat)
        {
            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();
                string insertStatement = @"
                INSERT INTO [SubCategories] (
                    [titleEng],
                    [titleAr],
                    [titleGer],
                    [id],
                    [belongsToMainCat],
                    [belongsToSubCat]
                   
                )
                VALUES (
                    @titleEng,
                    @titleAr,
                    @titleGer,
                    @id,
                    @belongsToMainCat,
                    @belongsToSubCat

                );
            ";
                try
                {
                    using (SqlCommand command = new SqlCommand(insertStatement, connection))
                    {
                        command.Parameters.AddWithValue("@titleEng", cat.titleEng);
                        command.Parameters.AddWithValue("@titleAr", cat.titleAr);
                        command.Parameters.AddWithValue("@titleGer", cat.titleGer);
                        command.Parameters.AddWithValue("@id", cat.Id);
                        command.Parameters.AddWithValue("@belongsToMainCat", cat.belongsToMainCat);
                        command.Parameters.AddWithValue("@belongsToSubCat", cat.belongsToSubCat);

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

        public class CategoryType
        {
            public string Id { get; set; }
            public int belongsToMainCat { get; set; }
            public string belongsToSubCat { get; set; }
            public string titleGer { get; set; }
            public string titleAr { get; set; }
            public string titleEng { get; set; }

        }

    }

}
