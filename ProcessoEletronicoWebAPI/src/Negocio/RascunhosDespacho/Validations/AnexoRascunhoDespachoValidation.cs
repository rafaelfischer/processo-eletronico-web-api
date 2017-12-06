using Negocio.RascunhosDespacho.Validations.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Negocio.RascunhosDespacho.Models;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Dominio.Base;
using System.Linq;
using ProcessoEletronicoService.Negocio.Comum.Base;

namespace Negocio.RascunhosDespacho.Validations
{
    public class AnexoRascunhoDespachoValidation : IAnexoRascunhoDespachoValidation
    {
        private IRepositorioGenerico<TipoDocumental> _repositorioTiposDocumentais;
        private ICurrentUserProvider _user;

        public AnexoRascunhoDespachoValidation(IProcessoEletronicoRepositorios repositorio, ICurrentUserProvider user)
        {
            _repositorioTiposDocumentais = repositorio.TiposDocumentais;
            _user = user;
        }

        public void Exists(AnexoRascunho anexoRascunho)
        {
            if (anexoRascunho == null)
            {
                throw new RecursoNaoEncontradoException("Anexo não encontrado");
            }
        }

        public void IsFilled(AnexoRascunhoDespachoModel anexoRascunhoDespachoModel)
        {
            if (anexoRascunhoDespachoModel == null)
            {
                throw new RecursoNaoEncontradoException("Anexo não encontrado");
            }
        }

        public void IsFilled(IEnumerable<AnexoRascunhoDespachoModel> anexosRascunhoDespacho)
        {
        }

        public void IsValid(AnexoRascunhoDespachoModel anexoRascunhoDespachoModel)
        {
            if (anexoRascunhoDespachoModel != null)
            {
                ConteudoIsValid(anexoRascunhoDespachoModel);
                TipoDocumentalIsValid(anexoRascunhoDespachoModel);
            }
        }
        
        public void IsValid(IEnumerable<AnexoRascunhoDespachoModel> anexosRascunhoDespachoModel)
        {
            if (anexosRascunhoDespachoModel != null)
            {
                foreach (AnexoRascunhoDespachoModel anexoRascunhoDespachoModel in anexosRascunhoDespachoModel)
                {
                    IsValid(anexoRascunhoDespachoModel);
                }
            }
        }

        private void ConteudoIsValid(AnexoRascunhoDespachoModel anexoRascunhoDespachoModel)
        {
            if (!string.IsNullOrWhiteSpace(anexoRascunhoDespachoModel.ConteudoString))
            {
                try
                {
                    anexoRascunhoDespachoModel.Conteudo = Convert.FromBase64String(anexoRascunhoDespachoModel.ConteudoString);
                }
                catch (FormatException)
                {
                    throw new RequisicaoInvalidaException("Conteúdo do anexo " + anexoRascunhoDespachoModel.Nome + " inválido.");
                }
            }
        }

        private void TipoDocumentalIsValid(AnexoRascunhoDespachoModel anexoRascunhoDespachoModel)
        {
            if (anexoRascunhoDespachoModel.TipoDocumental?.Id > 0)
            {
                if (_repositorioTiposDocumentais.Where(td => td.Id == anexoRascunhoDespachoModel.TipoDocumental.Id 
                                                                   && td.Atividade.Funcao.PlanoClassificacao.OrganizacaoProcesso.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca))
                                                                   .ToList().Count == 0)
                {
                    throw new RequisicaoInvalidaException($"Tipo Documental de identificador {anexoRascunhoDespachoModel.TipoDocumental.Id} não encontrado");
                }
            }
        }
    }
}
