using Microsoft.AspNetCore.Mvc;
using Negocio.Notificacoes.Base;
using ProcessoEletronicoService.WebAPI.Base;

namespace WebAPI.Notificacoes
{
    [Route("api/notificacoes")]
    public class NotificacoesController : BaseController
    {
        private INotificacoesService _service;
        public NotificacoesController(INotificacoesService service)
        {
            _service = service;
        }

        /// <summary>
        /// Executa o serviço de notificações
        /// </summary>
        /// <remarks>
        /// Esse serviço é executado em segundo plano de forma recorrente para notificar todos os interessados de um processo quando houver uma autuação ou despacho.
        /// </remarks>
        /// <response code="204">Serviço executado com sucesso</response>
        /// <response code="500">Erro inesperado</response>
        [HttpPut]
        public IActionResult Run()
        {
            _service.Run();
            return NoContent();
        }
    }
}
