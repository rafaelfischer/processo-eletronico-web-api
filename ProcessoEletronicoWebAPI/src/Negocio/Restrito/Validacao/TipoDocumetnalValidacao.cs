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
                throw new TipoDocumentalException("Já existe um Tipo Documental com esta descrição.");
        }

        internal void IdExistente(int id)
        {
            var tdocumental = repositorioTiposDocumentais.SingleOrDefault(td => td.Id == id);

            if (tdocumental == null)
                throw new TipoDocumentalException("Tipo Documental não encontrado.");
        }

        internal void DescricaoValida(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new TipoDocumentalException("O campo Descrição não pode ser vazio ou nulo.");
        }

        internal void IdValido(int id)
        {
            if (id == default(int))
                throw new TipoDocumentalException("Identificador do Tipo Documental inválido.");
        }

        internal void TipoDocumentalValido(TipoDocumentalModeloNegocio tipoDocumental)
        {
            if (tipoDocumental == null)
                throw new TipoDocumentalException("Tipo Documental não pode ser nulo.");
        }

        internal void IdAlteracaoValido(int id, TipoDocumentalModeloNegocio tipoDocumental)
        {
            if (id != tipoDocumental.Id)
                throw new Exception("Identificador do Tipo Documental não podem ser diferentes.");
        }
    }

    public class TipoDocumentalException : ProcessoEletronicoServiceRestritoException
    {
        public TipoDocumentalException(string mensagem) : base(mensagem) { }

        public TipoDocumentalException(string mensagem, Exception ex) : base(mensagem, ex) { }
    }
}
