using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Comum.Validacao;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao
{
    public class AnexoValidacao : IBaseValidation<AnexoModeloNegocio, AnexoRascunho>, IBaseCollectionValidation<AnexoModeloNegocio>
    {
        private const long tamanhoMaximo = 5242880;
        private IRepositorioGenerico<TipoDocumental> _repositorioTiposDocumentais;

        public AnexoValidacao(IProcessoEletronicoRepositorios repositorios)
        {
            _repositorioTiposDocumentais = repositorios.TiposDocumentais;
        }

        public void Exists(AnexoRascunho anexo)
        {
            if (anexo == null)
            {
                throw new RecursoNaoEncontradoException("Anexo não encontrado");
            }
        }

        public void IsFilled(AnexoModeloNegocio anexo)
        {
        }

        public void IsFilled(IEnumerable<AnexoModeloNegocio> anexos)
        {
            if (anexos != null)
            {
                foreach (AnexoModeloNegocio anexo in anexos)
                {
                    IsFilled(anexo);
                }
            }
        }

        public void IsValid(AnexoModeloNegocio anexo)
        {
            if (anexo != null)
            {
                TamanhoIsValid(anexo);
                ConteudoIsValid(anexo);
                TipoDocumentalIsValid(anexo);
            }
        }

        public void IsValid(IEnumerable<AnexoModeloNegocio> anexos)
        {
            if (anexos != null)
            {
                foreach (AnexoModeloNegocio anexo in anexos)
                {
                    IsValid(anexo);
                }
            }
        }

        private void TamanhoIsValid(AnexoModeloNegocio anexo)
        {
            if (anexo.Conteudo != null)
            {
                if (anexo.Conteudo.Length > tamanhoMaximo)
                {
                    throw new RequisicaoInvalidaException("Tamanho do anexo " + anexo.Nome + " é maior que o limite máximo de" + (tamanhoMaximo / 1048576) + "MB.");
                }
            }
        }

        private void ConteudoIsValid(AnexoModeloNegocio anexo)
        {
            if (!string.IsNullOrWhiteSpace(anexo.ConteudoString))
            {
                try
                {
                    anexo.Conteudo = Convert.FromBase64String(anexo.ConteudoString);
                }
                catch (FormatException)
                {
                    throw new RequisicaoInvalidaException("Conteúdo do anexo " + anexo.Nome + " inválido.");
                }
            }
        }

        private void TipoDocumentalIsValid(AnexoModeloNegocio anexo)
        {
            if (anexo.TipoDocumental != null)
            {
                if (_repositorioTiposDocumentais.Where(td => td.Id == anexo.TipoDocumental.Id).SingleOrDefault() == null)
                {
                    throw new RecursoNaoEncontradoException("Tipo Documental informado não existe");
                }
            }
        }
    }
}
