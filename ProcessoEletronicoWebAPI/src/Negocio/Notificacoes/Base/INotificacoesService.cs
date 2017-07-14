using ProcessoEletronicoService.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.Notificacoes.Base
{
    public interface INotificacoesService
    {
        IList<Notificacao> Get();
        void Insert(Processo processo);
        void Insert(Despacho despacho);
        void Run();
    }
}
