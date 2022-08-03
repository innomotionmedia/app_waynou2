using System;
using System.IO;
using Khaled.Backend.Databases;
using Khaled.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


[assembly: ExportFont("Raleway-Regular.ttf", Alias = "RegText")]
[assembly: ExportFont("Raleway-Bold.ttf", Alias = "BoldText")]
[assembly: ExportFont("Raleway-ExtraBold.ttf", Alias = "ExtraBoldText")]


namespace Khaled
{
    public partial class App : Application
    {
        static DB_Favorites database_favs;


        public static DB_Favorites DatabaseFAV
        {
            get
            {
                if (database_favs == null)
                {
                    database_favs = new DB_Favorites(Path.Combine
                        (Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "db_FAVS_waynou_app.db"));
                }
                return database_favs;
            }
        }

        public App ()
        {
            InitializeComponent();

            Syncfusion.Licensing.SyncfusionLicenseProvider.
                    RegisterLicense("Njc2Mjc0QDMyMzAyZTMyMmUzMFFZMFlDbUUrY1I1c3dIVHhCQWM1T0ZQVURMajhjbUJJeEg3ZkxHaGdGTUU9"); // personal key from syncfusion. use anywhere else in another project will be noted by our systems. so no stealing possible ;-) 

            MainPage = new Page_SplashScreen();
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

