﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace appPrueba.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(string id,string p = null);
        Task<T> GetItemAsync(int id);
        Task<T> GetItemAsync(string id);
        Task<ICollection<T>> GetItemsAsync();


    }
}
