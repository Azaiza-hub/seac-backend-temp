using System.Collections.Generic;
using System.IO;
using SEACBE.Models;
using SEACBE.Repositories;

namespace SEACBE.Services
{
    
    public class SolicitudesService : ISolicitudesService
    {
        private ISolicitudesRepository _repo;
        public SolicitudesService(ISolicitudesRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<Solicitud> GetSolicitudes()
        {
            return _repo.GetSolicitudes();
        }

        public Solicitud GetSolicitudById(int id)
        {
            return _repo.GetSolicitudById(id);
        }

        public Solicitud CrearSolicitud(Solicitud solicitud)
        {
            using (var stream = new MemoryStream())
            {
                solicitud.ImagenForm.CopyTo(stream);
                solicitud.Imagen = stream.ToArray();
            }
            return _repo.CrearSolicitud(solicitud);
        }

        public Solicitud ActualizarSolicitud(Solicitud solicitud)
        {
            return _repo.ActualizarSolicitud(solicitud);
        }

    }
}
