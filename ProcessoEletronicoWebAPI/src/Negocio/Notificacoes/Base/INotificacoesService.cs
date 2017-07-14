using ProcessoEletronicoService.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.Notificacoes.Base
{
    public interface INotificacoesService
    {
        IList<Notificacao> Get();
        void Insert(int idProcesso, string email);
        void Insert(int idProcesso, int idDespacho, string email);
        void Run();
    }
}
