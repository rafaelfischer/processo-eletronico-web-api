using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Restrito.Validacao
{
    public class TipoDocumentalValidacao
    {
        IRepositorioGenerico<TipoDocumental> repositorioTiposDocumentais;

        public TipoDocumentalValidacao(IRepositorioGenerico<TipoDocumental> repositorioTiposDocumentais)
        {
            this.repositorioTiposDocumentais = repositorioTiposDocumentais;
        }

        internal void DescricaoExistente(string descricao)
        {
            var tdocumental = repositorioTiposDocumentais.SingleOrDefault(td => td.Descricao.ToUpper().Equals(descricao.ToUpper()));

            if (tdocumental != null)
                throw new RequisicaoInvalidaException("Já existe um Tipo Documental com esta descrição.");
        }

        internal void IdExistente(int id)
        {
            var tdocumental = repositorioTiposDocumentais.SingleOrDefault(td => td.Id == id);

            if (tdocumental == null)
                throw new RecursoNaoEncontradoException("Tipo Documental não encontrado.");
        }

        internal void DescricaoValida(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new RequisicaoInvalidaException("O campo Descrição não pode ser vazio ou nulo.");
        }

        internal void IdValido(int id)
        {
            if (id == default(int))
                throw new RequisicaoInvalidaException("Identificador do Tipo Documental inválido.");
        }

        internal void TipoDocumentalValido(TipoDocumentalModeloNegocio tipoDocumental)
        {
            if (tipoDocumental == null)
                throw new RequisicaoInvalidaException("Tipo Documental não pode ser nulo.");
        }

        internal void IdAlteracaoValido(int id, TipoDocumentalModeloNegocio tipoDocumental)
        {
            if (id != tipoDocumental.Id)
                throw new Exception("Identificador do Tipo Documental não podem ser diferentes.");
        }
    }
}

