using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appPrueba.Data;
using appPrueba.Models;

namespace appPrueba.Services
{
    public class ClientsDataStore : IDataStore<Cliente>
    {
        private DataAccess dataAccess;
        public ClientsDataStore()
        {
            dataAccess = new DataAccess();
        }

        public async Task<bool> AddItemAsync(Cliente item)
        {
            dataAccess.Insert(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = dataAccess.GetList<Cliente>().FirstOrDefault(s => s.Cedula == id);
            if (oldItem != null)
            {
                dataAccess.Delete(oldItem);
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Cliente> GetItemAsync(string id, string p = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Cliente> GetItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Cliente> GetItemAsync(string id)
        {
            return await Task.FromResult(dataAccess.GetList<Cliente>().FirstOrDefault(s => s.Cedula == id));
        }

        public async Task<ICollection<Cliente>> GetItemsAsync()
        {
            return await Task.FromResult(dataAccess.GetList<Cliente>());
        }

        public async Task<bool> UpdateItemAsync(Cliente item)
        {
            dataAccess.Update(item);
            return await Task.FromResult(true);
        }
    }
}