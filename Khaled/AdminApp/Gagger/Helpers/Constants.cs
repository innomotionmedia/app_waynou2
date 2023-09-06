namespace Gagger.Helpers
{
    public static class Constants
    {
        public static string GetConnectionString()
        {
            var res = "Server=tcp:db-waynoudbserver.database.windows.net,1433;Database=db_waynou;User ID=serveradmin@db-waynoudbserver;Password={XXX};Trusted_Connection=False;Encrypt=True;";
            var mySecret = "30sajdiuan2sad2AAG";//UserSecretsManager.Settings["con"]; //TODO LOLL U KNOW WHAT
            var secret = mySecret.Replace("1", "!");
            return res.Replace("{XXX}", secret);
        }

        public static int MAXIMAGESIZE = 500; // in kb
        public static int MAXUPLOADIN24 = 30; // NEED CHANGING WHEN LIVE LOL
        public static int IDLENGTH = 30;

    }

    public enum MemePoints
    {
        UploadOfNewMeme = 10,
        WriteAComment = 2,
    }

    public enum MemeScrollerType
    {
        MainPage,
        UserUploads
    }
}

