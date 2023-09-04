using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Khaled.Helpers;
using TodoApp.Data;

namespace Khaled.Backend.APIs
{
    public class CityAPI
    {


        public static async Task<List<CitiesType>> GetAllCities()
        {
            List<CitiesType> res = new List<CitiesType>();
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

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            // var thumb = reader.GetValue(reader.GetOrdinal("thumbnail"));
                            // byte[] thumbnail = { };
                            // if (!string.IsNullOrEmpty(thumb.ToString())) thumbnail = (byte[])thumb;

                            var y = new CitiesType
                            {
                                //title = Convert.ToString(reader[reader.GetOrdinal("title")]),
                                //UnixCreated = DateTime.Convert.ToString(reader.GetValue(reader.GetOrdinal("UnixCreated"))),

                            };
                            res.Add(y);
                        }
                        return res;
                    }
                }
            }
        }
    }

    public class CitiesType
    {
        public string cityName { get; set; }
        public string cityNameAR { get; set; }
        public string cityNameGER { get; set; }
        public string tblSupportedCityID { get; set; }
        public double longitudeCityCenter { get; set; }
        public double latitudeCityCenter { get; set; }

    }
}
