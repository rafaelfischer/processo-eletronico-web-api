using ProcessoEletronicoService.Apresentacao.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Modelos;

namespace ProcessoEletronicoService.Apresentacao
{
    public class TipoDocumentalWorkService : ITipoDocumentalWorkService
    {
        private ITipoDocumentalNegocio tipoDocumentalNegocio;

        public TipoDocumentalWorkService(ITipoDocumentalNegocio tipoDocumentalNegocio)
        {
            this.tipoDocumentalNegocio = tipoDocumentalNegocio;
        }

        public void Excluir(int id)
        {
            tipoDocumentalNegocio.Excluir(id);
        }

        public List<TipoDocumentalModelo> ObterTiposDocumentais()
        {
            var tiposDocumentais = tipoDocumentalNegocio.ObterTiposDocumentais().Select(td => new TipoDocumentalModelo { Id = td.Id, Descricao = td.Descricao })
                                                                                .ToList();

            return tiposDocumentais;
        }

        public TipoDocumentalModelo ObterTiposDocumentais(int id)
        {
            var td = tipoDocumentalNegocio.ObterTiposDocumentais(id);

            TipoDocumentalModelo tipoDocumental = null;
            if (td != null)
            {
                tipoDocumental = new TipoDocumentalModelo();
                tipoDocumental.Id = td.Id;
                tipoDocumental.Descricao = td.Descricao;
            }

            return tipoDocumental;
        }

        public TipoDocumentalModelo Incluir(TipoDocumentalModelo tipoDocumental)
        {
            //TODO: Colocar AutoMapper
            TipoDocumentalModeloNegocio tdm = new TipoDocumentalModeloNegocio();

            tdm = new TipoDocumentalModeloNegocio();
            tdm.Id = tipoDocumental.Id;
            tdm.Descricao = tipoDocumental.Descricao;

            var td = tipoDocumentalNegocio.Incluir(tdm);

            tipoDocumental.Id = td.Id;

            return tipoDocumental;
        }

        public void Alterar(int id, TipoDocumentalModelo tipoDocumental)
        {
            //TODO: Colocar AutoMapper
            TipoDocumentalModeloNegocio tdm = new TipoDocumentalModeloNegocio();

            tdm = new TipoDocumentalModeloNegocio();
            tdm.Id = tipoDocumental.Id;
            tdm.Descricao = tipoDocumental.Descricao;

            tipoDocumentalNegocio.Alterar(id, tdm);
        }
    }
}
