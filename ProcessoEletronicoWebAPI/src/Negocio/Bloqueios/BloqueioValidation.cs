using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Comum.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Negocio.Bloqueios
{
    public class BloqueioValidation : IBaseValidation<BloqueioModel, Bloqueio>
    {
        private IRepositorioGenerico<Bloqueio> _repositorioBloqueios;
        private ICurrentUserProvider _user;

        public BloqueioValidation(IProcessoEletronicoRepositorios repositorios, ICurrentUserProvider user)
        {
            _repositorioBloqueios = repositorios.Bloqueios;
            _user = user;
        }

        public void Exists(Bloqueio bloqueio)
        {
            if (bloqueio == null)
            {
                throw new RecursoNaoEncontradoException("Bloqueio não encontrado");
            }
        }

        public void IsFilled(BloqueioModel bloqueioModel)
        {
            MotivoIsFilled(bloqueioModel);
        }

        private void MotivoIsFilled(BloqueioModel bloqueioModel)
        {
            if (string.IsNullOrWhiteSpace(bloqueioModel.Motivo))
            {
                throw new RequisicaoInvalidaException("O motivo do bloqueio deve ser informado");
            }
        }

        public void IsValid(BloqueioModel bloqueioModel)
        {
        }

        public void IsProcessoWithBloqueio(int idProcesso)
        {
            Bloqueio bloqueio = _repositorioBloqueios.Where(b => b.IdProcesso == idProcesso && b.DataFim == null).SingleOrDefault();

            if (bloqueio != null)
            {
                throw new RequisicaoInvalidaException($"O processo encontra-se bloqueado pelo usuário {_user.UserNome} através do sistema {_user.UserSistema}");
            }
        }

        public void IsProcessoBlockedByAnotherUsuarioOrSistema(int idProcesso)
        {
            Bloqueio bloqueio = _repositorioBloqueios.Where(b => b.IdProcesso == idProcesso && b.DataFim == null).SingleOrDefault();

            if (bloqueio != null && (bloqueio.CpfUsuario != _user.UserCpf || bloqueio.NomeSistema != _user.UserSistema))

            {
                throw new RequisicaoInvalidaException($"O processo encontra-se bloqueado pelo usuário {_user.UserNome} através do sistema {_user.UserSistema}");
            }
        }

        public void IsRemoveBloqueioPossible(Bloqueio bloqueio)
        {
            IsBloqueioNotActive(bloqueio);
            IsProcessoBlockedByAnotherUsuarioOrSistema(bloqueio.IdProcesso);
        }

        private void IsBloqueioNotActive(Bloqueio bloqueio)
        {
            if (bloqueio.DataFim != null)
            {
                throw new RequisicaoInvalidaException("O bloqueio não está mais em vigor");
            }
        }

    }
}
