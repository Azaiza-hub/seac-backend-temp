using Microsoft.AspNetCore.Http;
using System;

namespace SEACBE.Models
{
    public class Solicitud
    {
        public int IdSolicitud { get; set; }
        public string Location { get; set; }
        public string Descripcion { get; set; }
        public Byte[] Imagen { get; set; }
        public IFormFile ImagenForm { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }

    }
}
