using Microsoft.Datasync.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using TodoApp.Data.Models;
using System.Linq;

namespace TodoApp.Data.Services
{
    // minimal version of service to connect to db (without token or ui updates which I dont use anyways)

    public class RemoteCategoriesService // change here 
    {
        private DatasyncClient _client = null;
        private IRemoteTable<tblCategories> _table = null; //change here
        private bool _initialized = false;
        private readonly SemaphoreSlim _asyncLock = new(1, 1);
        public Func<Task<AuthenticationToken>> TokenRequestor;

        public RemoteCategoriesService(Func<Task<AuthenticationToken>> tokenRequestor)
        {
            TokenRequestor = tokenRequestor;
        }
        private async Task InitializeAsync()
        {
            if (_initialized) return;

            try
            {
                await _asyncLock.WaitAsync();
                if (_initialized) return;

                var options = new DatasyncClientOptions
                {
                    HttpPipeline = new HttpMessageHandler[] { new LoggingHandler() }
                };

                _client = TokenRequestor == null
                    ? new DatasyncClient(Constants.ServiceUri, options)
                    : new DatasyncClient(Constants.ServiceUri, new GenericAuthenticationProvider(TokenRequestor), options);
                _table = _client.GetRemoteTable<tblCategories>(); //change here
                _initialized = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _asyncLock.Release();
            }
        }
        public async Task RemoveItemAsync(tblCategories item) //change here
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (item.Id == null)
            {
                return;
            }
            await InitializeAsync();
            await _table.DeleteItemAsync(item);
        }
        public async Task SaveItemAsync(tblCategories item) //change here
        {
            if (item == null) throw new ArgumentException(nameof(item));
            await InitializeAsync();
            if (item.Id == null)
            {
                try
                {
                    await _table.InsertItemAsync(item);
                }
                catch (Exception e)
                {
                    var message = e.Message;
                }
            }
            else await _table.ReplaceItemAsync(item);
        }

        public async Task<List<tblCategories>> GetAllItems()
        {
            await InitializeAsync();
            try
            {
                var x = _table.GetAsyncItems();//.Where(x => x.BelongsToUserId == companyId);
                return await x.ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
