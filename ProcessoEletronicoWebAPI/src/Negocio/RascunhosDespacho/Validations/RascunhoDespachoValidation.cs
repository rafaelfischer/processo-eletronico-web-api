using Negocio.RascunhosDespacho.Validations.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Negocio.RascunhosDespacho.Models;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using Negocio.Comum.Validacao;
using Prodest.ProcessoEletronico.Integration.Organograma.Base;

namespace Negocio.RascunhosDespacho.Validations
{
    public class RascunhoDespachoValidation : IRascunhoDespachoValidation
    {
        private GuidValidacao _guidValidacao;
        private IOrganizacaoService _organizacaoService;
        private IUnidadeService _unidadeService;

        public RascunhoDespachoValidation(GuidValidacao guidValidacao, IOrganizacaoService organizacaoService, IUnidadeService unidadeService)
        {
            _guidValidacao = guidValidacao;
            _organizacaoService = organizacaoService;
            _unidadeService = unidadeService;
        }

        public void Exists(RascunhoDespacho rascunhoDespacho)
        {
            if (rascunhoDespacho == null)
            {
                throw new RecursoNaoEncontradoException("Rascunho de despacho não encontrado");
            }
        }

        public void IsFilled(RascunhoDespachoModel rascunhoDespachoModel)
        {
            //Não há campos obrigatórios a serem recebidos
        }

        public void IsValid(RascunhoDespachoModel rascunhoDespachoModel)
        {
            GuidOrganizacaoDestinoIsValid(rascunhoDespachoModel);
            GuidUnidadeDestinoIsValid(rascunhoDespachoModel);
            OrganizacaoDestinoExists(rascunhoDespachoModel);
            UnidadeDestinoExists(rascunhoDespachoModel);
        }

        private void GuidUnidadeDestinoIsValid(RascunhoDespachoModel rascunhoDespachoModel)
        {
            if (!string.IsNullOrWhiteSpace(rascunhoDespachoModel.GuidUnidadeDestino))
            {
                try
                {
                    _guidValidacao.IsValid(rascunhoDespachoModel.GuidUnidadeDestino);
                }
                catch (FormatException)
                {
                    throw new RequisicaoInvalidaException("Identificador da unidade de destino inválido");
                }
            }
        }

        private void GuidOrganizacaoDestinoIsValid(RascunhoDespachoModel rascunhoDespachoModel)
        {
            if (!string.IsNullOrWhiteSpace(rascunhoDespachoModel.GuidOrganizacaoDestino))
            {
                try
                {
                    _guidValidacao.IsValid(rascunhoDespachoModel.GuidOrganizacaoDestino);
                }
                catch (FormatException)
                {
                    throw new RequisicaoInvalidaException("Identificador da organização de destino inválido");
                }
            }
        }

        private void OrganizacaoDestinoExists(RascunhoDespachoModel rascunhoDespachoModel)
        {
            if (!string.IsNullOrWhiteSpace(rascunhoDespachoModel.GuidOrganizacaoDestino))
            {
                if (_organizacaoService.Search(new Guid(rascunhoDespachoModel.GuidOrganizacaoDestino)).ResponseObject == null)
                {
                    throw new RequisicaoInvalidaException("Organização de destino não encontrada no Organograma");
                }
            }
        }

        private void UnidadeDestinoExists(RascunhoDespachoModel rascunhoDespachoModel)
        {
            if (!string.IsNullOrWhiteSpace(rascunhoDespachoModel.GuidUnidadeDestino))
            {
                if (_unidadeService.Search(new Guid(rascunhoDespachoModel.GuidUnidadeDestino)).ResponseObject == null)
                {
                    throw new RequisicaoInvalidaException("Unidade de destino não encontrada no Organograma");
                }
            }
        }
    }
}
