using Negocio.Comum.Validacao;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using Prodest.ProcessoEletronico.Integration.Organograma.Base;
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
        private IOrganizacaoService _organizacaoService;
        private IUnidadeService _unidadeService;
        
        private GuidValidacao _guidValidacao;

        public RascunhoProcessoValidacao(IProcessoEletronicoRepositorios repositorios, ICurrentUserProvider user, IOrganizacaoService organizacaoService, IUnidadeService unidadeService, GuidValidacao guidValidacao)
        {
            _repositorioRascunhosProcesso = repositorios.RascunhosProcesso;
            _repositorioAtividades = repositorios.Atividades;
            _user = user;
            _organizacaoService = organizacaoService;
            _unidadeService = unidadeService;

            _guidValidacao = guidValidacao;
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
            GuidUnidadeIsFilled(rascunhoProcesso);
        }

        private void GuidUnidadeIsFilled(RascunhoProcessoModeloNegocio rascunhoProcesso)
        {
            if (string.IsNullOrWhiteSpace(rascunhoProcesso.GuidOrganizacao))
            {
                throw new RequisicaoInvalidaException("Organização deve ser informada");
            }
        }
        #endregion

        #region Validação dos campos

        public void IsValid(RascunhoProcessoModeloNegocio rascunhoProcesso)
        {
            AtividadeIsValid(rascunhoProcesso);
            GuidOrganizacaoIsValid(rascunhoProcesso);
            OrganizacaoExists(rascunhoProcesso);
            GuidUnidadeIsValid(rascunhoProcesso);
            UnidadeExists(rascunhoProcesso);
        }

        private void GuidOrganizacaoIsValid(RascunhoProcessoModeloNegocio rascunhoProcesso)
        {
            if (!string.IsNullOrWhiteSpace(rascunhoProcesso.GuidOrganizacao))
            {
                try
                {
                    _guidValidacao.IsValid(rascunhoProcesso.GuidOrganizacao);
                }
                catch (FormatException)
                {
                    throw new RequisicaoInvalidaException("Identificador da organização inválido");
                }
            }
        }

        private void OrganizacaoExists(RascunhoProcessoModeloNegocio rascunhoProcesso)
        {
            if (!string.IsNullOrWhiteSpace(rascunhoProcesso.GuidOrganizacao))
            {
                if (_organizacaoService.Search(new Guid(rascunhoProcesso.GuidOrganizacao)).ResponseObject == null)
                {
                    throw new RequisicaoInvalidaException("Organização não encontrada no Organograma");
                }
            }
        }

        private void GuidUnidadeIsValid(RascunhoProcessoModeloNegocio rascunhoProcesso)
        {
            if (!string.IsNullOrWhiteSpace(rascunhoProcesso.GuidUnidade))
            {
                try
                {
                    _guidValidacao.IsValid(rascunhoProcesso.GuidUnidade);
                }
                catch (FormatException)
                {
                    throw new RequisicaoInvalidaException("Identificador da unidade inválido");
                }
            }
        }

        private void UnidadeExists(RascunhoProcessoModeloNegocio rascunhoProcesso)
        {
            if (!string.IsNullOrWhiteSpace(rascunhoProcesso.GuidUnidade))
            {
                if (_unidadeService.Search(new Guid(rascunhoProcesso.GuidUnidade)).ResponseObject == null)
                {
                    throw new RequisicaoInvalidaException("Unidade não encontrada no Organograma");
                }
            }
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
