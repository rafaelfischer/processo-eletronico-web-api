using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
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
                throw new RequisicaoInvalidaException("Já existe uma Sinalização com esta descrição.");
        }

        internal void IdExistente(int id)
        {
            var tdocumental = repositorio.SingleOrDefault(td => td.Id == id);

            if (tdocumental == null)
                throw new RecursoNaoEncontradoException("Sinalização não encontrada.");
        }

        internal void DescricaoValida(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new RequisicaoInvalidaException("O campo Descrição não pode ser vazio ou nulo.");
        }

        internal void IdValido(int id)
        {
            if (id == default(int))
                throw new RequisicaoInvalidaException("Identificador da Sinalização inválido.");
        }

        internal void SinalizacaoValida(SinalizacaoModeloNegocio sinalizacao)
        {
            if (sinalizacao == null)
                throw new RequisicaoInvalidaException("Sinalização não pode ser nula.");
        }

        internal void IdAlteracaoValido(int id, SinalizacaoModeloNegocio sinalizacao)
        {
            if (id != sinalizacao.Id)
                throw new RequisicaoInvalidaException("Identificador da Sinalização não podem ser diferentes.");
        }
    }
       
}
