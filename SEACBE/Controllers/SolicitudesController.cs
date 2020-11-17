using Microsoft.AspNetCore.Mvc;
using SEACBE.Models;
using SEACBE.Models.ViewModels;
using SEACBE.Services;

namespace SEACBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SolicitudesController : ControllerBase
    {
        private ISolicitudesService _service;
        public SolicitudesController(ISolicitudesService service)
        {
            _service = service;
        }
        [HttpGet]
        public GetSolicitudesViewModel Get()
        {
            return GetSolicitudesViewModel.FromSolicitud(_service.GetSolicitudes());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSolicitud(int id)
        {
            var soli = _service.GetSolicitudById(id);
            if (soli == null)
            {
                return new NotFoundResult();
            }
            return new JsonResult(GetSolicitudViewModel.FromSolicitud(soli));
        }
        [HttpPost]
        public Solicitud CrearSolicitud([FromBody]Solicitud solicitud) 
        {
            return _service.CrearSolicitud(solicitud);
        }
        [HttpPut]
        public Solicitud ActualizarSolicitud([FromBody]Solicitud solicitud)
        {
            return _service.ActualizarSolicitud(solicitud);
        }

    }
}
