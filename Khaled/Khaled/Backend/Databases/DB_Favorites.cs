using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Khaled.Backend.APIs;
using Khaled.Helpers;
using SQLite;

namespace Khaled.Backend.Databases
{
	public class DB_Favorites
	{
        readonly SQLiteAsyncConnection _databaseFAVs;

        public DB_Favorites(string dbPath)
        {
            _databaseFAVs = new SQLiteAsyncConnection(dbPath);
            _databaseFAVs.CreateTableAsync<SavedAdType>().Wait();
        }

        public async Task<List<SavedAdType>> GetAllMyFavs()
        {

            List<SavedAdType> yx = null;

            try
            {
                yx = await _databaseFAVs.Table<SavedAdType>().ToListAsync();
            }
            catch (Exception e)
            {

            }

            return yx;

        }

        public async Task<SavedAdType> GetNoteAsync(AdsType ad)
        {
            SavedAdType yx = null;

            try
            {
                yx = await _databaseFAVs.Table<SavedAdType>()
                            .Where(i => i.tblAdId == ad.tblAdID)
                            .FirstOrDefaultAsync();

            }
            catch (Exception e)
            {

            }

            return yx;
        }

        public async Task SaveClickedOnFav(FullAdType ad)
        {
            SavedAdType res = null;

            try
            {
                res = await GetNoteAsync(ad);
            }
            catch { }

            var note = new SavedAdType
            {
                tblAdId = ad.tblAdID,
                title = ad.title,
                titleDe = ad.titleDe,
                titleAr = ad.titleAr,
                description = ad.description,
                descriptionAR = ad.descriptionAR,
                descriptionGER = ad.descriptionGER,
                distance = ad.distance,
                thumbnail = ad.thumbnail
            };

            if (res != null)
            {
                await _databaseFAVs.UpdateAsync(note);
            }
            else
            {
                await _databaseFAVs.InsertAsync(note);
            }
        }

        public async Task DeleteMyFav(AdsType ad)
        {
            SavedAdType note = await GetNoteAsync(ad);

            await _databaseFAVs.DeleteAsync(note);
        }

        public Task<int> WipeAllData()
        {
            return _databaseFAVs.DeleteAllAsync<AdsType>();
        }
    }

    public class SavedAdType
    {
        [PrimaryKey, AutoIncrement]
        public int noteID { get; set; }

        public int tblAdId { get; set; }

        public string title { get; set; }
        public string titleDe { get; set; }
        public string titleAr { get; set; }

        public string description { get; set; }
        public string descriptionGER { get; set; }
        public string descriptionAR { get; set; }

        public string distance { get; set; }

        public string thumbnail { get; set; }


    }


}

