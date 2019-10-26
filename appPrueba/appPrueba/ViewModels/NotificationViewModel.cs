using appPrueba.Models;
using appPrueba.Services;
using appPrueba.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace appPrueba.ViewModels
{
    public class NotificationViewModel : BaseViewModel
    {
        public Notification notificacion { get; set; }
        public ControlNotification notificacionControl { get; set; }
        public INotification<Notification> DataStoreNotification => DependencyService.Get<INotification<Notification>>() ?? new NotificationDataStore();
        public NotificationViewModel()
        {
            Title = "Ahorro de energia";
            notificacion = new Notification();
            notificacionControl = new ControlNotification();
            ShowLoading(true);
            CargarNotificacion();
            ShowLoading(false);
            
        }
        public void  guardarSw()
        {
            DataStoreNotification.guardarSw(notificacionControl);
        }

        private async void CargarNotificacion()
        {
            notificacionControl = await DataStoreNotification.getControlNotification();
            notificacion = await DataStoreNotification.getNotification();
        }
    }
}
