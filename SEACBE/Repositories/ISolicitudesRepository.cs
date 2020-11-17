using SEACBE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEACBE.Repositories
{
    public interface ISolicitudesRepository
    {
        public IEnumerable<Solicitud> GetSolicitudes();
        public Solicitud GetSolicitudById(int id);
        public Solicitud CrearSolicitud(Solicitud solicitud);
        public Solicitud ActualizarSolicitud(Solicitud solicitud);
    }
}
