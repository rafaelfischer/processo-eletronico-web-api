using ProcessoEletronicoService.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.Notificacoes.Base
{
    public interface INotificacoesService
    {
        IList<Notificacao> Get();
        void Post(int idProcesso, int idDespacho, string email);
        void Run();
    }
}
