using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Comum;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Restrito.Validacao
{
    public class SinalizacaoValidacao
    {
        IRepositorioGenerico<Sinalizacao> repositorio;

        public SinalizacaoValidacao(IRepositorioGenerico<Sinalizacao> repositorio)
        {
            this.repositorio = repositorio;
        }

        internal void DescricaoExistente(string descricao)
        {
            var tdocumental = repositorio.SingleOrDefault(td => td.Descricao.ToUpper().Equals(descricao.ToUpper()));

            if (tdocumental != null)
                throw new SinalizacaoException("Já existe uma Sinalização com esta descrição.");
        }

        internal void IdExistente(int id)
        {
            var tdocumental = repositorio.SingleOrDefault(td => td.Id == id);

            if (tdocumental == null)
                throw new SinalizacaoException("Sinalização não encontrada.");
        }

        internal void DescricaoValida(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new SinalizacaoException("O campo Descrição não pode ser vazio ou nulo.");
        }

        internal void IdValido(int id)
        {
            if (id == default(int))
                throw new SinalizacaoException("Identificador da Sinalização inválido.");
        }

        internal void SinalizacaoValida(SinalizacaoModeloNegocio sinalizacao)
        {
            if (sinalizacao == null)
                throw new SinalizacaoException("Sinalização não pode ser nula.");
        }

        internal void IdAlteracaoValido(int id, SinalizacaoModeloNegocio sinalizacao)
        {
            if (id != sinalizacao.Id)
                throw new Exception("Identificador da Sinalização não podem ser diferentes.");
        }
    }

    public class SinalizacaoException : ProcessoEletronicoServiceRestritoException
    {
        public SinalizacaoException(string mensagem) : base(mensagem) { }

        public SinalizacaoException(string mensagem, Exception ex) : base(mensagem, ex) { }
    }
}
