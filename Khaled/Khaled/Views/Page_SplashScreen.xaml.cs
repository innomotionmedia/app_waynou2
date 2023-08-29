using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Khaled.Helpers;
using Xamarin.Forms;
namespace Khaled.Views
{
    public partial class Page_SplashScreen : ContentPage
    {
        public Page_SplashScreen()
        {
            InitializeComponent();

            StartFade();

        }

        private async void StartFade()
        {

            //var test = await CheckIfWeSpokeBefore();
            await System.Threading.Tasks.Task.Delay(200);
            image_logo.Opacity = 0;
            image_logo.IsVisible = true;
            await image_logo.FadeTo(1, 4000);
            Application.Current.MainPage = new MainPage(true, false);
        }

        public static async Task<List<Testtype>> CheckIfWeSpokeBefore()
        {
            var sr = Constants.GetConnectionString();
            SqlConnection connection = new SqlConnection(sr);
            try
            {
                connection.Open();

            }
            catch (Exception e)
            {

            }
            string query = "SELECT *  " +
                        "FROM Persons ";
            SqlCommand command = new SqlCommand(query, connection);
            List<Testtype> results = new List<Testtype>();
            try
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {

                        var message = new Testtype
                        {
                            PersonID = Convert.ToInt32(reader[reader.GetOrdinal("PersonID")]),
                            LastName = Convert.ToString(reader[reader.GetOrdinal("LastName")]),
                        };
                        results.Add(message);
                    }
                }
            }
            catch (Exception y)
            {
                Console.WriteLine(y);
                return null;
            }
            return results;

        }
    }

    public class Testtype
    {
        public int PersonID { get; set; }
        public string LastName { get; set; }
    }

}


