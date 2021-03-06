﻿using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao
{
    public class AnexoValidacao
    {
        private const long tamanhoMaximo = 5242880;
        private IRepositorioGenerico<TipoDocumental> _repositorioTiposDocumentais;
        private IRepositorioGenerico<RascunhoProcesso> _repositorioRascunhosProcesso;

        public AnexoValidacao(IProcessoEletronicoRepositorios repositorios)
        {
            _repositorioTiposDocumentais = repositorios.TiposDocumentais;
            _repositorioRascunhosProcesso = repositorios.RascunhosProcesso;
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

        public void IsValid(AnexoModeloNegocio anexo, int? idAtividade)
        {
            if (anexo != null)
            {
                TamanhoIsValid(anexo);
                ConteudoIsValid(anexo);
                TipoDocumentalIsValid(anexo, idAtividade);
            }
        }

        public void IsValid(IEnumerable<AnexoModeloNegocio> anexos, int? idAtividade)
        {
            if (anexos != null)
            {
                foreach (AnexoModeloNegocio anexo in anexos)
                {
                    IsValid(anexo, idAtividade);
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

        private void TipoDocumentalIsValid(AnexoModeloNegocio anexo, int? idAtividade)
        {
            if (anexo.TipoDocumental != null && anexo.TipoDocumental.Id > 0 && idAtividade.HasValue)
            {
                if (_repositorioTiposDocumentais.Where(td => td.Id == anexo.TipoDocumental.Id && td.IdAtividade == idAtividade.Value).SingleOrDefault() == null)
                {
                    throw new RecursoNaoEncontradoException("Tipo Documental informado não encontrado");
                }
            }
        }
    }
}
