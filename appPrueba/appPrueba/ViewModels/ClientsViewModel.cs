using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using appPrueba.Models;
using appPrueba.Views;
using appPrueba.Services;
using System.Collections.Generic;

namespace appPrueba.ViewModels
{
    public class ClientsViewModel : BaseViewModel
    {
        public IDataStore<Cliente> DataStore => DependencyService.Get<IDataStore<Cliente>>() ?? new ClientsDataStore();
        public ObservableCollection<Cliente> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public ClientsViewModel()
        {
            Title = "Clientes";
            Items = new ObservableCollection<Cliente>();
            LoadClients();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        private async Task ExecuteLoadItemsCommand()
        {
            IDataStore<Cliente> da = DependencyService.Get<IDataStore<Cliente>>() ?? new ClientsDataStore();
            var user = await da.GetItemsAsync();
            
        }

        private async void LoadClients()
        {
            Items.Clear();
            if (netService.IsConected())
            {
                var itemsGet = await apiService.APICosumeGet<Cliente>("api/Clientes");
                if (itemsGet.IsSucces)
                {
                    foreach (var it in (List<Cliente>)itemsGet.Result)
                    {
                      await DataStore.AddItemAsync(it);                     
                    }
                }
             
            }
            var itemsGe = await DataStore.GetItemsAsync();
            foreach (var it in itemsGe)
                Items.Add(it);
        }
    }
}