using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEACBE.Models.ViewModels
{
    public class GetSolicitudesItem
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Descripcion { get; set; }        
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public string Clasificacion { get; set; }
        public string Sentimentalismo { get; set; }
    }
    public class GetSolicitudesViewModel : List<GetSolicitudesItem>
    {
        public static GetSolicitudesViewModel FromSolicitud(IEnumerable<Solicitud> solicitudes)
        {
            var lista = new GetSolicitudesViewModel();
            foreach (var solicitud in solicitudes)
            {
                lista.Add(new GetSolicitudesItem
                {
                    Id = solicitud.IdSolicitud,
                    Location = solicitud.Location,
                    Descripcion = solicitud.Descripcion,
                    Fecha = solicitud.Fecha,
                    Estado = solicitud.Estado,
                    Clasificacion = solicitud.Clasificacion,
                    Sentimentalismo = solicitud.Sentimentalismo
                });

            }
            return lista;
        }
    }
}
