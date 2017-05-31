using AutoMapper;
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
    public class TipoDocumentalWorkService : BaseWorkService, ITipoDocumentalWorkService
    {
        private ITipoDocumentalNegocio tipoDocumentalNegocio;

        public TipoDocumentalWorkService(ITipoDocumentalNegocio tipoDocumentalNegocio)
        {
            this.tipoDocumentalNegocio = tipoDocumentalNegocio;
        }

        public TipoDocumentalModeloGet Pesquisar(int id)
        {
            TipoDocumentalModeloNegocio tipoDocumentalModeloNegocio = tipoDocumentalNegocio.Pesquisar(id);
            return Mapper.Map<TipoDocumentalModeloNegocio, TipoDocumentalModeloGet>(tipoDocumentalModeloNegocio);
        }

        public List<TipoDocumentalModeloGet> PesquisarPorAtividade(int idAtividade)
        {
            List<TipoDocumentalModeloNegocio> tiposDocumentais = tipoDocumentalNegocio.PesquisarPorAtividade(idAtividade);

            return Mapper.Map<List<TipoDocumentalModeloGet>>(tiposDocumentais);
            
        }

        public TipoDocumentalModeloGet Inserir(TipoDocumentalModeloPost tipoDocumental)
        {
            TipoDocumentalModeloNegocio tipoDocumentalModeloNegocio = new TipoDocumentalModeloNegocio();
            Mapper.Map(tipoDocumental, tipoDocumentalModeloNegocio);

            tipoDocumentalModeloNegocio = tipoDocumentalNegocio.Inserir(tipoDocumentalModeloNegocio);

            return Mapper.Map<TipoDocumentalModeloNegocio, TipoDocumentalModeloGet>(tipoDocumentalModeloNegocio);
        }

        public void Excluir(int id)
        {
            tipoDocumentalNegocio.Excluir(id);
        }
                
    }
}
