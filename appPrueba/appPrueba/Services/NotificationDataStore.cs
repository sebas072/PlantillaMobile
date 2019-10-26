using appPrueba.Data;
using appPrueba.Models;
using appPrueba.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration;

namespace appPrueba.Services
{
    public class NotificationDataStore : INotification<Notification>
    {
        private DataAccess dataAccess;
        NetService netService = new NetService();
        ApiService apiService = new ApiService();
        public NotificationDataStore()
        {
            dataAccess = new DataAccess();
        }
        public async Task<bool> AddItemAsync(Notification item)
        {
            dataAccess.Insert(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = dataAccess.GetList<Notification>().FirstOrDefault(s => s.id == id);
            if (oldItem != null)
            {
                dataAccess.Delete(oldItem);
            }
            return await Task.FromResult(true);
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Notification> GetItemAsync(int id)=> await Task.FromResult(dataAccess.GetList<Notification>().FirstOrDefault(s => s.id == id));
        public Task<Notification> GetItemAsync(string id, string p = null)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Notification>> GetItemsAsync() => await Task.FromResult(dataAccess.GetList<Notification>());

        public Task<bool> UpdateItemAsync(Notification item)
        {
            throw new NotImplementedException();
        }

        public async Task<Notification> getNotification() {
            Response R = new Response();
            
            if (netService.IsConected())
            {
                R = await apiService.APICosumeGet<Notification>("/api/Notifications");
                ActualizarMensajes((List<Notification>)R.Result);
            }
            else
            {
                var list = await GetItemsAsync();
                R = new Response
                {
                    IsSucces = list.Count > 0,
                    Message = "Ok",
                    Result = list
                };
            }
            Random random = new Random();
            var lists = (List<Notification>)R.Result;
            return lists[random.Next(lists.Count)];
        }
        private async void ActualizarMensajes(List<Notification> result)
        {
            var oldItems = await GetItemsAsync();
            foreach (var item in oldItems)
                await DeleteItemAsync(item.id);
            foreach (var item in result)
                await AddItemAsync(item);
        }

        public async Task<ControlNotification> getControlNotification()
        {
            var controlN = dataAccess.GetList<ControlNotification>();
            if (controlN.Count > 0)
            {
                return await Task.FromResult(controlN[0]);
            }
            else {
                if (netService.IsConected())
                {
                    Response r = new Response();
                    r = await apiService.APICosumeGetiD<ControlNotification>("api/ControlNotifications/" + GetId());
                    if (r.IsSucces)
                    {
                        if (r.Result == null)
                        {
                            var model = new ControlNotification
                            {
                                activo = false,
                                device = GetId(),
                                intervaloHoras = 3,
                            };
                            ActulizarControl(model);
                            await apiService.APICosumePost("api/ControlNotifications", model);
                            return model;
                        }
                        else {
                            ActulizarControl((ControlNotification)r.Result);
                            return (ControlNotification)r.Result;
                        }
                    }
                    else {
                        var model =  new ControlNotification
                        {
                            activo = false,
                            device = GetId(),
                            intervaloHoras = 3,
                        };
                        await apiService.APICosumePost("api/ControlNotifications", model);
                        return model;
                    }
                }
                return new ControlNotification
                {
                    activo = false,
                    device = GetId(),
                    intervaloHoras = 3,
                };
            }
        }
        private void ActulizarControl(ControlNotification result)
        {
            var oldItems = dataAccess.GetList<ControlNotification>();
            oldItems.ForEach(s => dataAccess.Delete(s));
            dataAccess.Insert(result);
        }

        private string GetId() {
            return netService.getId();
        }
        public async void guardarSw(ControlNotification notificacionControl)
        {
            ActulizarControl(notificacionControl);
            if (netService.IsConected())
                await apiService.APICosumePut($"api/ControlNotifications/{notificacionControl.device}", notificacionControl);
        }
    }
}
