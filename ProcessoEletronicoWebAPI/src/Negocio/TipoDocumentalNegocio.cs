using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Restrito.Validacao;

namespace ProcessoEletronicoService.Negocio
{
    public class TipoDocumentalNegocio : ITipoDocumentalNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<TipoDocumental> repositorioTiposDocumentais;
        private TipoDocumentalValidacao validacao;

        public TipoDocumentalNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioTiposDocumentais = repositorios.TiposDocumentais;
            validacao = new TipoDocumentalValidacao(repositorioTiposDocumentais);
        }

        public void Excluir(int id)
        {
            validacao.IdExistente(id);

            var tipoDocumental = repositorioTiposDocumentais.Single(td => td.Id == id);

            repositorioTiposDocumentais.Remove(tipoDocumental);

            unitOfWork.Save();
        }

        public List<TipoDocumentalModeloNegocio> ObterTiposDocumentais()
        {
            var tiposDocumentais = repositorioTiposDocumentais.Select(td => new TipoDocumentalModeloNegocio { Id = td.Id, Descricao = td.Descricao })
                                                              .ToList();

            return tiposDocumentais;
        }

        public TipoDocumentalModeloNegocio ObterTiposDocumentais(int id)
        {
            var tipoDocumental = repositorioTiposDocumentais.Select(td => new TipoDocumentalModeloNegocio { Id = td.Id, Descricao = td.Descricao })
                                                            .SingleOrDefault(td => td.Id == id);

            return tipoDocumental;
        }

        public TipoDocumentalModeloNegocio Incluir(TipoDocumentalModeloNegocio tipoDocumental)
        {
            validacao.TipoDocumentalValido(tipoDocumental);

            validacao.DescricaoValida(tipoDocumental.Descricao);

            validacao.DescricaoExistente(tipoDocumental.Descricao);

            TipoDocumental td = new TipoDocumental();

            td.Descricao = tipoDocumental.Descricao;

            repositorioTiposDocumentais.Add(td);

            unitOfWork.Save();

            tipoDocumental.Id = td.Id;

            return tipoDocumental;
        }

        public void Alterar(int id, TipoDocumentalModeloNegocio tipoDocumental)
        {
            validacao.TipoDocumentalValido(tipoDocumental);

            validacao.IdValido(id);
            validacao.IdValido(tipoDocumental.Id);

            validacao.IdAlteracaoValido(id, tipoDocumental);

            validacao.IdExistente(id);

            validacao.DescricaoValida(tipoDocumental.Descricao);

            validacao.DescricaoExistente(tipoDocumental.Descricao);

            TipoDocumental td = repositorioTiposDocumentais.Where(t => t.Id == tipoDocumental.Id).Single();

            td.Descricao = tipoDocumental.Descricao;

            unitOfWork.Save();
        }
    }
}
