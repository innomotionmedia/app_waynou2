using System;
using System.Threading.Tasks;
using Com.Kumulos;

namespace Khaled.Helpers
{
	public class KumulosHelper
	{   
        public static async Task<bool> InitKumulos()
        {
            var config = Kumulos.CurrentConfig.AddKeys(
                "91b17c3a-2be0-451f-83a4-fe6d8a19347e",
                "8HRDrrjrOSTEgEfZnQgPxpYf2Q3ZyM799v/9");

            try
            {
                Kumulos.Current.Initialize(config);
            }
            catch (Exception e)
            {

            }


            return true;
        }
    }
}

