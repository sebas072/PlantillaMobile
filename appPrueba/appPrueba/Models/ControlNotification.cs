using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace appPrueba.Models
{
    public class ControlNotification
    {
        [PrimaryKey]
        public string device { get; set; }
        public bool activo { get; set; }
        public decimal intervaloHoras { get; set; }
    }
}
