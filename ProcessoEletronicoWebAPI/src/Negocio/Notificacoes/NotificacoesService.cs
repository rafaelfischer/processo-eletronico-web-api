using Negocio.Notificacoes.Base;
using System;
using System.Collections.Generic;
using System.Text;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Dominio.Base;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Negocio.Notificacoes
{
    public class NotificacoesService : INotificacoesService
    {
        private string emailPadraoProcesso = "Notificacao de Processo";
        private string emailPadraoDespacho = "Notificacao de Despacho";

        private IRepositorioGenerico<Notificacao> _repositorioNotificacoes;
        private IUnitOfWork _unityOfWork;

        public NotificacoesService(IProcessoEletronicoRepositorios repositorios)
        {
            _repositorioNotificacoes = repositorios.Notificacoes;
            _unityOfWork = repositorios.UnitOfWork;
        }

        public IList<Notificacao> Get()
        {
            return _repositorioNotificacoes.Where(n => n.DataNotificacao == null).ToList();
        }

        public void Post(int idProcesso, int idDespacho, string email)
        {
            Notificacao notificacao = new Notificacao { IdProcesso = idProcesso, IdDespacho = idDespacho, Email = email };
            _repositorioNotificacoes.Add(notificacao);
        }

        public void Run()
        {
            IList<Notificacao> notificacoes = Get();
            foreach (Notificacao notificacao in notificacoes)
            {
                if (!notificacao.IdDespacho.HasValue)
                {
                    NotificarAutuacao(notificacao);
                } else
                {
                    NotificarDespacho(notificacao);
                }
}
        }

        private void NotificarAutuacao(Notificacao notificacao)
        {
            
        }

        private void NotificarDespacho(Notificacao notificacao)
        {
            throw new NotImplementedException();
        }
    }
}
