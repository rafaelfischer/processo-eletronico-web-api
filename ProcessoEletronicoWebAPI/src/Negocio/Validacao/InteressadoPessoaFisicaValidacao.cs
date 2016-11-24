using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Dominio.Base;

namespace ProcessoEletronicoService.Negocio.Validacao
{
    public class InteressadoPessoaFisicaValidacao
    {

        ContatoValidacao contatoValidacao;
        EmailValidacao emailValidacao;
        CpfValidacao cpfValidacao;

        public InteressadoPessoaFisicaValidacao (IProcessoEletronicoRepositorios repositorios)
        {
            this.contatoValidacao = new ContatoValidacao(repositorios);
            this.emailValidacao = new EmailValidacao();
            this.cpfValidacao = new CpfValidacao();
        }

        public void Preenchido(List<InteressadoPessoaFisicaModeloNegocio> interessados)
        {
            foreach (InteressadoPessoaFisicaModeloNegocio interessado in interessados)
            {
                Preenchido(interessado);
            }
        }

        public void Preenchido(InteressadoPessoaFisicaModeloNegocio interessado)
        {
            if (interessado != null)
            {
                /*Preenchimento dos campos do interessado*/
                NomePreenchido(interessado);
                CpfPreenchido(interessado);

                /*Preenchimento de objetos associados ao interessado*/
                contatoValidacao.Preenchido(interessado.Contatos);
                emailValidacao.Preenchido(interessado.Emails);

            }
        }

        #region Preenchimento de campos obrigatórios

        internal void NomePreenchido(InteressadoPessoaFisicaModeloNegocio interessado)
        {
            if (string.IsNullOrWhiteSpace(interessado.Nome))
            {
                throw new RequisicaoInvalidaException("Nome do interessado não preenchido."); 
            }
        }

        internal void CpfPreenchido(InteressadoPessoaFisicaModeloNegocio interessado)
        {
            if (string.IsNullOrWhiteSpace(interessado.Cpf))
            {
                throw new RequisicaoInvalidaException("Cpf do interessado não preenchido.");
            }
        }

        internal void UfPreenchida(InteressadoPessoaFisicaModeloNegocio interessado)
        {
            if (string.IsNullOrWhiteSpace(interessado.UfMunicipio))
            {
                throw new RequisicaoInvalidaException("Uf do interessado não preenchido.");
            }
        }

        internal void MunicipioPreenchido(InteressadoPessoaFisicaModeloNegocio interessado)
        {
            if (string.IsNullOrWhiteSpace(interessado.NomeMunicipio))
            {
                throw new RequisicaoInvalidaException("Municipio do interessado não preenchido.");
            }
        }

        #endregion

        #region Validação dos campos
        public void Valido(List<InteressadoPessoaFisicaModeloNegocio> interessados)
        {
            foreach(InteressadoPessoaFisicaModeloNegocio interessado in interessados)
            {
                Valido(interessado);
            }
            
        }

        public void Valido (InteressadoPessoaFisicaModeloNegocio interessado)
        {
            cpfValidacao.CpfValido(interessado.Cpf);
            emailValidacao.Valido(interessado.Emails);
            contatoValidacao.Valido(interessado.Contatos);
        }

        #endregion

    }
}
