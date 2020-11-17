using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEACBE.Models;

namespace SEACBE.Services
{
    public interface ISolicitudesService
    {
        public IEnumerable<Solicitud> GetSolicitudes();
        public Solicitud GetSolicitudById(int id);
        public Solicitud CrearSolicitud(Solicitud solicitud);
        public Solicitud ActualizarSolicitud(Solicitud solicitud);
    }
}
