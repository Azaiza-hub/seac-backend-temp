using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEACBE.Models.ViewModels
{
    public class GetSolicitudViewModel
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Descripcion { get; set; }
        public Byte[] Imagen { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public string Clasificacion { get; set; }
        public string Sentimentalismo { get; set; }

        public static GetSolicitudViewModel FromSolicitud(Solicitud solicitud)
        {
            return new GetSolicitudViewModel
            {
                Id = solicitud.IdSolicitud,
                Location = solicitud.Location,
                Descripcion = solicitud.Descripcion,
                Imagen = solicitud.Imagen,
                Fecha = solicitud.Fecha,
                Estado = solicitud.Estado,
                Clasificacion = solicitud.Clasificacion,
                Sentimentalismo = solicitud.Sentimentalismo
            };
        }
    }
}
