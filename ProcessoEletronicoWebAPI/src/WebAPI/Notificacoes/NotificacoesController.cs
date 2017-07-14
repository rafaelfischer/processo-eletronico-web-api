using Microsoft.AspNetCore.Mvc;
using Negocio.Notificacoes.Base;
using ProcessoEletronicoService.WebAPI.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Notificacoes
{
    [Route("api/notificacoes")]
    public class NotificacoesController : BaseController
    {
        private INotificacoesService _service;
        public NotificacoesController(INotificacoesService service)
        {
            {
                _service = service;
            }
        }

        [HttpPut]
        public IActionResult Get()
        {
            _service.Run();
            return Ok();
        }
    }
}
