using appPrueba.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace appPrueba.Services.Intefaces
{
    public interface INotification<T> : IDataStore<T>
    {
        Task<Notification> getNotification();
        Task<ControlNotification> getControlNotification();
        void guardarSw(ControlNotification notificacionControl);
    }
}
