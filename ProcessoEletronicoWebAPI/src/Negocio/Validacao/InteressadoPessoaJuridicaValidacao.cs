﻿using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Negocio.Comum.Validacao;

namespace ProcessoEletronicoService.Negocio.Validacao
{
    public class InteressadoPessoaJuridicaValidacao
    {

        private CnpjValidacao cnpjValidacao;
        ContatoValidacao contatoValidacao;
        EmailValidacao emailValidacao;

        public InteressadoPessoaJuridicaValidacao(IProcessoEletronicoRepositorios repositorios)
        {
            this.cnpjValidacao = new CnpjValidacao();
            this.contatoValidacao = new ContatoValidacao(repositorios);
            this.emailValidacao = new EmailValidacao();
        }

        public void Preenchido(List<InteressadoPessoaJuridicaModeloNegocio> interessados)
        {
            if (interessados != null)
            {
                foreach (InteressadoPessoaJuridicaModeloNegocio interessado in interessados)
                {
                    Preenchido(interessado);
                }

            }
        }

        public void Preenchido(InteressadoPessoaJuridicaModeloNegocio interessado)
        {
            if (interessado != null)
            {
                RazaoSocialPreenchida(interessado);
                CnpjPreenchido(interessado);
                GuidMunicipioPreenchido(interessado);
            }
        }

        #region Preenchimento de campos obrigatórios

        private void RazaoSocialPreenchida(InteressadoPessoaJuridicaModeloNegocio interessado)
        {
            if (string.IsNullOrWhiteSpace(interessado.RazaoSocial))
            {
                throw new RequisicaoInvalidaException("Razão Social do interessado não preenchida.");
            }
        }

        private void CnpjPreenchido(InteressadoPessoaJuridicaModeloNegocio interessado)
        {
            if (string.IsNullOrWhiteSpace(interessado.Cnpj))
            {
                throw new RequisicaoInvalidaException("Cnpj do interessado não preenchido.");
            }
        }

        private void GuidMunicipioPreenchido(InteressadoPessoaJuridicaModeloNegocio interessado)
        {
            if (string.IsNullOrWhiteSpace(interessado.GuidMunicipio))
            {
                throw new RequisicaoInvalidaException($"Municipio do interessado {interessado.RazaoSocial} não preenchido.");
            }
        }


        #endregion

        #region Validação dos campos
        internal void Valido(List<InteressadoPessoaJuridicaModeloNegocio> interessados)
        {
            if (interessados != null)
            {
                foreach (InteressadoPessoaJuridicaModeloNegocio interessado in interessados)
                {
                    Valido(interessado);
                }

                DuplicidadeCNPJ(interessados);

            }
        }

        internal void Valido(InteressadoPessoaJuridicaModeloNegocio interessado)
        {
            cnpjValidacao.CnpjValido(interessado.Cnpj);
            emailValidacao.Valido(interessado.Emails);
            contatoValidacao.Valido(interessado.Contatos);
            GuidMunicipioValido(interessado);
        }

        private void GuidMunicipioValido(InteressadoPessoaJuridicaModeloNegocio interessado)
        {
            try
            {
                Guid guid = new Guid(interessado.GuidMunicipio);
            }
            catch (Exception)
            {
                throw new RequisicaoInvalidaException($"Identificador do município {interessado.GuidMunicipio} do interessado {interessado.RazaoSocial} inválido.");
            }
        }

        private void DuplicidadeCNPJ(List<InteressadoPessoaJuridicaModeloNegocio> interessados)
        {
            foreach (InteressadoPessoaJuridicaModeloNegocio interessado in interessados)
            {

                IEnumerable<InteressadoPessoaJuridicaModeloNegocio> query;
                query = interessados.Where(i => i.Cnpj == interessado.Cnpj);

                if (!string.IsNullOrWhiteSpace(interessado.SiglaUnidade))
                {
                    query = query.Where(i => !string.IsNullOrWhiteSpace(i.SiglaUnidade) && i.SiglaUnidade.Equals(interessado.SiglaUnidade));
                }
                else
                {
                    query = query.Where(i => string.IsNullOrWhiteSpace(i.SiglaUnidade));
                }

                if (query.ToList().Count() > 1)
                {

                    string concatenacaoUnidade = "";

                    if (!string.IsNullOrWhiteSpace(interessado.SiglaUnidade))
                    {
                        concatenacaoUnidade = " - " + interessado.SiglaUnidade;
                    }

                    throw new RequisicaoInvalidaException("Interessado " + interessado.Sigla + concatenacaoUnidade +  " duplicado.");
                }
            }
        }

        #endregion
    }
}
