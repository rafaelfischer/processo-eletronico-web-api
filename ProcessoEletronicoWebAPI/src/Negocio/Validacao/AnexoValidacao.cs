using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProcessoEletronicoService.Negocio.Validacao
{
    public class AnexoValidacao
    {

        //private string regexNomeArquivo = @"^[\w,\s-]+\.[A-Za-z0-9]{2,4}$";
        // 5 MB =  5242880 bytes
        private long tamanhoMaximo = 52428800;

        IRepositorioGenerico<TipoDocumental> repositorioTiposDocumentais;

        public AnexoValidacao(IProcessoEletronicoRepositorios repositorios)
        {
            this.repositorioTiposDocumentais = repositorios.TiposDocumentais;
        }

        public void NaoEncontrado(Anexo anexo)
        {
            if (anexo == null)
            {
                throw new RecursoNaoEncontradoException("Anexo não encontrado");
            }
        }

        public void Preenchido(List<AnexoModeloNegocio> anexos)
        {
            if (anexos != null)
            {
                foreach (AnexoModeloNegocio anexo in anexos)
                {
                    Preenchido(anexo);
                }
            }

        }

        #region Preenchimento de campos obrigatórios
        public void Preenchido(AnexoModeloNegocio anexo)
        {
            NomePreenchido(anexo);
            ConteudoPreenchido(anexo);
            MimeTypePreenchido(anexo);
        }

        private void NomePreenchido(AnexoModeloNegocio anexo)
        {
            if (string.IsNullOrWhiteSpace(anexo.Nome))
            {
                throw new RequisicaoInvalidaException("Nome do anexo não preenchido.");
            }
        }
        private void ConteudoPreenchido(AnexoModeloNegocio anexo)
        {
            if (string.IsNullOrWhiteSpace(anexo.ConteudoString))
            {
                throw new RequisicaoInvalidaException("Conteúdo do anexo não preenchido.");
            }
        }

        private void MimeTypePreenchido(AnexoModeloNegocio anexo)
        {
            if (string.IsNullOrWhiteSpace(anexo.MimeType))
            {
                throw new RequisicaoInvalidaException("Tipo do contéudo (mime type) do arquivo não preenchido.");
            }
        }

        
        #endregion

        #region Validação dos campos

        public void Valido(List<AnexoModeloNegocio> anexos, int idAtividadeProcesso)
        {
            if (anexos != null)
            {
                foreach (AnexoModeloNegocio anexo in anexos)
                {
                    Valido(anexo, idAtividadeProcesso);
                }

                Duplicidade(anexos);
            }

        }


        #region Preenchimento de campos obrigatórios
        public void Valido(AnexoModeloNegocio anexo, int idAtividadeProcesso)
        {
            //NomeValido(anexo);
            ConverteConteudo(anexo);
            TamanhoValido(anexo);
            TipoDocumentalValido(anexo);
            TipoDocumentalExistente(anexo, idAtividadeProcesso);
        }

        /*
        private void NomeValido(AnexoModeloNegocio anexo)
        {
            Regex nomeRegex = new Regex(regexNomeArquivo);

            if (!nomeRegex.IsMatch(anexo.Nome))
            {
                throw new RequisicaoInvalidaException("Nome do arquivo inválido.");
            }
        }
        */

        private void TamanhoValido(AnexoModeloNegocio anexo)
        {
            if (anexo.Conteudo.Length > tamanhoMaximo)
            {
                throw new RequisicaoInvalidaException("Anexos: Tamanho do anexo "+ anexo.Nome  +" é maior que o limite máximo de " + (tamanhoMaximo / 1048576) + " MB.");
            }
        }

        private void ConverteConteudo(AnexoModeloNegocio anexo)
        {
            try
            {
                anexo.Conteudo = Convert.FromBase64String(anexo.ConteudoString);
            }
            catch (FormatException)
            {
                throw new RequisicaoInvalidaException("Anexos: Conteúdo do anexo " + anexo.Nome + " inválido.");
            }
        }

        private void TipoDocumentalValido(AnexoModeloNegocio anexo)
        {
            if (anexo.TipoDocumental != null)
            {
                if (anexo.TipoDocumental.Id <= 0)
                {
                    throw new RequisicaoInvalidaException($"Anexos: Tipo Documental do anexo {anexo.Nome} inválido.");
                }
            }
        }

        public void TipoDocumentalExistente(List<AnexoModeloNegocio> anexos, int idAtividadeProcesso)
        {
            if (anexos != null)
            {
                foreach (AnexoModeloNegocio anexo in anexos)
                {
                    TipoDocumentalExistente(anexo, idAtividadeProcesso);
                }
            }

           
        }

        public void TipoDocumentalExistente(AnexoModeloNegocio anexo, int idAtividadeProcesso)
        {
            if (anexo.TipoDocumental != null)
            {
                if (repositorioTiposDocumentais.Where(td => td.Id == anexo.TipoDocumental.Id && td.IdAtividade == idAtividadeProcesso).ToList().Count == 0) 
                {
                    throw new RequisicaoInvalidaException($"Anexos: Tipo Documental do anexo {anexo.Nome} inexistente.");
                }
            }
        }

        #endregion


        internal void Duplicidade(List<AnexoModeloNegocio> anexos)
        {
            if (anexos != null)
            {

                foreach (AnexoModeloNegocio anexo in anexos)
                {

                    if (anexos.Where(a => a.Nome == anexo.Nome).ToList().Count > 1)
                    {
                        throw new RequisicaoInvalidaException($"Anexos: Nomes de arquivos {anexo.Nome} duplicados.");
                    }
                }

            }
        }

       #endregion


    }
}
