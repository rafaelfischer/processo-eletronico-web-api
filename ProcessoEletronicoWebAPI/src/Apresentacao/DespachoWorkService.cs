using AutoMapper;
using ProcessoEletronicoService.Apresentacao.Base;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao
{
    public class DespachoWorkService : BaseWorkService, IDespachoWorkService
    {
        private IDespachoNegocio despachoNegocio;

        public DespachoWorkService(IDespachoNegocio despachoNegocio)
        {
            this.despachoNegocio = despachoNegocio;
        }

        public override void RaiseUsuarioAlterado()
        {
            despachoNegocio.Usuario = Usuario;
        }
        
        public DespachoModeloGet Despachar(DespachoModeloPost despachoPost)
        {
            DespachoModeloNegocio despachoModeloNegocio = new DespachoModeloNegocio();
            Mapper.Map(despachoPost, despachoModeloNegocio);

            despachoModeloNegocio = despachoNegocio.Despachar(despachoModeloNegocio);

            return Mapper.Map<DespachoModeloNegocio, DespachoModeloGet>(despachoModeloNegocio);
            
        }

        public DespachoModeloGet Pesquisar(int idDespacho)
        {
            DespachoModeloNegocio despachoModeloNegocio = despachoNegocio.Pesquisar(idDespacho);

            return Mapper.Map<DespachoModeloNegocio, DespachoModeloGet>(despachoModeloNegocio);
        }

        public List<DespachoModeloGet> PesquisarDespachosUsuario()
        {
            List<DespachoModeloNegocio> despachosModeloNegocio = despachoNegocio.PesquisarDespachosUsuario();

            return Mapper.Map<List<DespachoModeloNegocio>, List<DespachoModeloGet>>(despachosModeloNegocio);
        }

    }
}
