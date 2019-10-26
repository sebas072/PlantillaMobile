using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace appPrueba.Models
{
    public class Notification
    {
        [PrimaryKey]
        public int id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
