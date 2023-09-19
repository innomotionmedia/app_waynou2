using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Com.Kumulos;
using Com.Kumulos.Abstractions;
using Khaled.Helpers;
using Khaled.Views;
using TodoApp.Data;
using Xamarin.Essentials;
using Xamarin.Forms;
using static SQLite.SQLite3;

namespace Khaled.Backend.APIs
{
    public class AdImagesApi
    {


        public static async Task<List<AdImageType>> GetAllImagesFromAdId(string tblAdID)
        {
            var sr = Constants.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(sr))
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT *
                    FROM [AdImages]
                    WHERE BelongsToAdId = @tblAdID
                    ORDER BY Position";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@tblAdID", tblAdID);

                    var res = new List <AdImageType>();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {

                            var x = new AdImageType
                            {
                                Image = Convert.ToString(reader[reader.GetOrdinal("Image")]),
                                Position = Convert.ToInt32(reader[reader.GetOrdinal("Position")]),
                            };
                            res.Add(x); 
                        }
                        return res;
                    }
                }
            }
        }
    }


    public class AdImageType
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public int Position { get; set; } 
        public ImageSource ImageSource { get; set; }
        public string BelongsToAdId { get; set; }
        public DateTime DateCreated { get; set; }

    }


}
