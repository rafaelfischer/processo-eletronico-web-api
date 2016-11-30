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

        private string regexNomeArquivo = @"^[\w,\s-]+\.[A-Za-z0-9]{2-4}+$";
        private long tamanhoMaximo = 4000000;

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
        }

        internal void NomePreenchido(AnexoModeloNegocio anexo)
        {
            if (string.IsNullOrWhiteSpace(anexo.Nome))
            {
                throw new RequisicaoInvalidaException("Nome do anexo não preenchido.");
            }
        }
        internal void ConteudoPreenchido(AnexoModeloNegocio anexo)
        {
            if (anexo.Conteudo.Length == 0)
            {
                throw new RequisicaoInvalidaException("Conteúdo do anexo não preenchido.");
            }
        }

        #endregion

        #region Validação dos campos

        public void Valido(List<AnexoModeloNegocio> anexos)
        {
            if (anexos != null)
            {
                foreach (AnexoModeloNegocio anexo in anexos)
                {
                    Valido(anexo);
                }

                Duplicidade(anexos);
            }

        }


        #region Preenchimento de campos obrigatórios
        public void Valido(AnexoModeloNegocio anexo)
        {
            NomeValido(anexo);
            TamanhoValido(anexo);
        }

        private void NomeValido(AnexoModeloNegocio anexo)
        {
            Regex nomeRegex = new Regex(regexNomeArquivo);

            if (!nomeRegex.IsMatch(anexo.Nome))
            {
                throw new RequisicaoInvalidaException("Nome do arquivo inválido.");
            }
        }

        private void TamanhoValido(AnexoModeloNegocio anexo)
        {
            if (anexo.Conteudo.Length > tamanhoMaximo)
            {
                throw new RequisicaoInvalidaException("Anexo maior que " + (tamanhoMaximo / 1000000) + "MB.");
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
                        throw new RequisicaoInvalidaException("Nomes de arquivos duplicados");
                    }
                }

            }
        }

        #endregion


    }
}
