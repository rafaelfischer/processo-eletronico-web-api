using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao
{
    public class RascunhoProcessoValidacao : IBaseValidation<RascunhoProcessoModeloNegocio, RascunhoProcesso>
    {
        private IRepositorioGenerico<RascunhoProcesso> _repositorioRascunhosProcesso;
        private IRepositorioGenerico<Atividade> _repositorioAtividades;
        private ICurrentUserProvider _user;

        public RascunhoProcessoValidacao(IProcessoEletronicoRepositorios repositorios, ICurrentUserProvider user)
        {
            _repositorioRascunhosProcesso = repositorios.RascunhosProcesso;
            _repositorioAtividades = repositorios.Atividades;
            _user = user;
        }

        public void Exists(RascunhoProcesso rascunhoProcesso)
        {
            if (rascunhoProcesso == null)
            {
                throw new RecursoNaoEncontradoException("Rascunho de processo não encontrado");
            }
        }

        public void Exists(int id)
        {
            if (_repositorioRascunhosProcesso.Where(r => r.Id == id).SingleOrDefault() == null)
            {
                throw new RecursoNaoEncontradoException("Rascunho de Processo não encontrado");
            }
        }

        #region Preenchimento de campos obrigatórios
        public void IsFilled(RascunhoProcessoModeloNegocio rascunhoProcesso)
        {
            GuidOrganizacaoFilled(rascunhoProcesso);
            GuidUnidadeFilled(rascunhoProcesso);
        }

        /*Órgao Autuador*/
        internal void GuidOrganizacaoFilled(RascunhoProcessoModeloNegocio rascunhoProcesso)
        {
            if (string.IsNullOrWhiteSpace(rascunhoProcesso.GuidOrganizacao))
            {
                throw new RequisicaoInvalidaException("Identificador da Organização não preenchido.");
            }
        }

        /*Unidade Autuadora*/
        internal void GuidUnidadeFilled(RascunhoProcessoModeloNegocio rascunhoProcesso)
        {
            if (string.IsNullOrWhiteSpace(rascunhoProcesso.GuidUnidade))
            {
                throw new RequisicaoInvalidaException("Identificador da Unidade não preenchido.");
            }
        }
        #endregion

        #region Validação dos campos

        public void IsValid(RascunhoProcessoModeloNegocio rascunhoProcesso)
        {
            AtividadeIsValid(rascunhoProcesso);
        }

        private void AtividadeIsValid(RascunhoProcessoModeloNegocio rascunhoProcesso)
        {
            if (rascunhoProcesso.Atividade != null && rascunhoProcesso.Atividade.Id > 0)
            {
                if (_repositorioAtividades.Where(a => a.Id == rascunhoProcesso.Atividade.Id).Where(pc => pc.Funcao.PlanoClassificacao.GuidOrganizacao.Equals(_user.UserGuidOrganizacao)).SingleOrDefault() == null)
                {
                    throw new RecursoNaoEncontradoException("Atividade não encontrada.");
                }
            }
        }
        #endregion

    }
}
