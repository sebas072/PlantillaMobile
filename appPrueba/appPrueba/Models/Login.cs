using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace appPrueba.Models
{
    public class Login
    {
        [PrimaryKey]
        public string usuario1 { get; set; }
        public string pass { get; set; }
        public bool remeber { get; set; }
      
    }
}
