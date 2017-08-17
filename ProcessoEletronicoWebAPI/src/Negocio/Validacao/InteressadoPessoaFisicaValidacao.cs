using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Comum.Validacao;

namespace ProcessoEletronicoService.Negocio.Validacao
{
    public class InteressadoPessoaFisicaValidacao
    {

        ContatoValidacao contatoValidacao;
        EmailValidacao emailValidacao;
        CpfValidacao cpfValidacao;

        public InteressadoPessoaFisicaValidacao(IProcessoEletronicoRepositorios repositorios)
        {
            this.contatoValidacao = new ContatoValidacao(repositorios);
            this.emailValidacao = new EmailValidacao();
            this.cpfValidacao = new CpfValidacao();
        }


        public void NaoEncontrado(InteressadoPessoaFisica interessadoPessoaFisica)
        {
            if (interessadoPessoaFisica == null)
            {
                throw new RecursoNaoEncontradoException("Interessado Pessoa Física não encontrado.");
            }
        }
        public void Preenchido(List<InteressadoPessoaFisicaModeloNegocio> interessados)
        {
            if (interessados != null)
            {
                foreach (InteressadoPessoaFisicaModeloNegocio interessado in interessados)
                {
                    Preenchido(interessado);
                }
            }
        }

        public void Preenchido(InteressadoPessoaFisicaModeloNegocio interessado)
        {
            if (interessado != null)
            {
                /*Preenchimento dos campos do interessado*/
                NomePreenchido(interessado);
                CpfPreenchido(interessado);
                GuidMunicipioPreenchido(interessado);

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
        
        internal void GuidMunicipioPreenchido(InteressadoPessoaFisicaModeloNegocio interessado)
        {
            if (string.IsNullOrWhiteSpace(interessado.GuidMunicipio))
            {
                throw new RequisicaoInvalidaException("Identificador do município do interessado não preenchido.");
            }
        }

        #endregion

        #region Validação dos campos
        public void Valido(List<InteressadoPessoaFisicaModeloNegocio> interessados)
        {
            if (interessados != null)
            {

                foreach (InteressadoPessoaFisicaModeloNegocio interessado in interessados)
                {
                    Valido(interessado);
                }

                DuplicidadeCPF(interessados);
            }

        }

        public void Valido(InteressadoPessoaFisicaModeloNegocio interessado)
        {
            cpfValidacao.CpfValido(interessado.Cpf);
            emailValidacao.Valido(interessado.Emails);
            contatoValidacao.Valido(interessado.Contatos);
            GuidMunicipioValido(interessado);
        }

        private void GuidMunicipioValido(InteressadoPessoaFisicaModeloNegocio interessado)
        {
            try
            {
                Guid guid = new Guid(interessado.GuidMunicipio);
            }
            catch (Exception)
            {
                throw new RequisicaoInvalidaException("Identificador do município do interessado inválido.");
            }
        }

        internal void DuplicidadeCPF(List<InteressadoPessoaFisicaModeloNegocio> interessados)
        {
            foreach (InteressadoPessoaFisicaModeloNegocio interessado in interessados)
            {
                if (interessados.Where(i => i.Cpf == interessado.Cpf).ToList().Count() > 1)
                {
                    throw new RequisicaoInvalidaException("Cpf " + interessado.Cpf + " duplicado.");
                }
            }
        }

        #endregion

    }
}
