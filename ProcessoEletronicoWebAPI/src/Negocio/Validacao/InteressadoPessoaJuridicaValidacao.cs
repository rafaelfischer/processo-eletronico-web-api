using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Dominio.Base;

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
            foreach (InteressadoPessoaJuridicaModeloNegocio interessado in interessados)
            {
                Preenchido(interessado);
            }
        }

        public void Preenchido(InteressadoPessoaJuridicaModeloNegocio interessado)
        {
            if (interessado != null)
            {
                RazaoSocialPreenchida(interessado);
                CnpjPreenchido(interessado);
                UfPreenchida(interessado);
                MunicipioPreenchido(interessado);
            }
        }

        #region Preenchimento de campos obrigatórios

        internal void RazaoSocialPreenchida(InteressadoPessoaJuridicaModeloNegocio interessado)
        {
            if (string.IsNullOrWhiteSpace(interessado.RazaoSocial))
            {
                throw new RequisicaoInvalidaException("Razão Social do interessado não preenchida."); 
            }
        }

        internal void CnpjPreenchido(InteressadoPessoaJuridicaModeloNegocio interessado)
        {
            if (string.IsNullOrWhiteSpace(interessado.Cnpj))
            {
                throw new RequisicaoInvalidaException("Cnpj do interessado não preenchido.");
            }
        }

        internal void UfPreenchida(InteressadoPessoaJuridicaModeloNegocio interessado)
        {
            if (string.IsNullOrWhiteSpace(interessado.Uf))
            {
                throw new RequisicaoInvalidaException("Uf do interessado não preenchido.");
            }
        }

        internal void MunicipioPreenchido(InteressadoPessoaJuridicaModeloNegocio interessado)
        {
            if (string.IsNullOrWhiteSpace(interessado.Municipio))
            {
                throw new RequisicaoInvalidaException("Municipio do interessado não preenchido.");
            }
        }

        
        #endregion
        
        #region Validação dos campos
        internal void Valido(List<InteressadoPessoaJuridicaModeloNegocio> interessados)
        {
            foreach (InteressadoPessoaJuridicaModeloNegocio interessado in interessados)
            {
                Valido(interessado);
            }   
        }

        internal void Valido(InteressadoPessoaJuridicaModeloNegocio interessado)
        {
            cnpjValidacao.CnpjValido(interessado.Cnpj);
            emailValidacao.Valido(interessado.Emails);
            contatoValidacao.Valido(interessado.Contatos);
            
        }

        #endregion
    }
}
