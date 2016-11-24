using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;

namespace ProcessoEletronicoService.Negocio.Validacao
{
    public class ContatoValidacao
    {

        IRepositorioGenerico<TipoContato> repositorioTiposContato;

        public ContatoValidacao (IProcessoEletronicoRepositorios repositorios)
        {
            this.repositorioTiposContato = repositorios.TiposContato;
        }

        public void Preenchido(List<ContatoModeloNegocio> contatos)
        {
            foreach (ContatoModeloNegocio contato in contatos)
            {
                Preenchido(contato);
            }
        }

        public void Preenchido(ContatoModeloNegocio contato)
        {
            if (contato != null)
            {
                TelefonePreenchido(contato);
                TipoContatoPreenchido(contato);
            }
        }

        #region Preenchimento de campos obrigatórios

        internal void TelefonePreenchido(ContatoModeloNegocio contato)
        {
            if (string.IsNullOrWhiteSpace(contato.Telefone))
            {
                throw new RequisicaoInvalidaException("Telefone do interessado não preenchido."); 
            }
        }

        internal void TipoContatoPreenchido(ContatoModeloNegocio contato)
        {
            if (contato.TipoContato.Id <= 0)
            {
                throw new RequisicaoInvalidaException("Tipo do Contato do interessado não preenchido.");
            }
        }

        #endregion

        #region Validação dos campos
        public void Valido(List<ContatoModeloNegocio> contatos)
        {
            foreach (ContatoModeloNegocio contato in contatos)
            {
                Valido(contato);
            }
        }

        public void Valido(ContatoModeloNegocio contato)
        {
            TipoContatoExistente(contato);
            TelefoneValido(contato);
        }

        internal void TipoContatoExistente(ContatoModeloNegocio contato)
        {
            if (repositorioTiposContato.Where(tc => tc.Id == contato.TipoContato.Id).SingleOrDefault() == null)
            {
                throw new RecursoNaoEncontradoException("Tipo de Contato inexistente");
            }
        }

        private void TelefoneValido(ContatoModeloNegocio contato)
        {
            ApenasNumeros(contato);
            NumeroDigitosValido(contato);
        }

        private void ApenasNumeros(ContatoModeloNegocio contato)
        {
            try
            {
                long.Parse(contato.Telefone);
            }
            catch (Exception)
            {
                throw new RequisicaoInvalidaException("O Telefone deve possuir apenas números.");
            }
        }

        private void NumeroDigitosValido(ContatoModeloNegocio contato)
        {
            TipoContato tipoContato = repositorioTiposContato.Where(t => t.Id == contato.TipoContato.Id).Single();
            if (contato.Telefone.Length != tipoContato.QuantidadeDigitos)
            {
                throw new RequisicaoInvalidaException("Telefones do tipo " + tipoContato.Descricao + " devem possuir " + tipoContato.QuantidadeDigitos + " dígitos.");
            }
        }

        #endregion


    }
}
