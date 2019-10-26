using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace appPrueba.Models
{
    public class Cliente
    {
        [PrimaryKey]
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime Fecha { get; set; }
        public bool Genero { get; set; }
        public string CorreoElectronico { get; set; }
        public string NumeroCelular { get; set; }
        public string Direccion { get; set; }
        public byte[] ArchivoPDF { get; set; }
        public byte[] FirmaCliente { get; set; }
        public int Estado { get; set; }
    }
}