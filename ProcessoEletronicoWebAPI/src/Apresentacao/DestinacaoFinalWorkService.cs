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
    public class DestinacaoFinalWorkService : BaseWorkService, IDestinacaoFinalWorkService
    {
        private IDestinacaoFinalNegocio destinacaoFinalNegocio;

        public DestinacaoFinalWorkService(IDestinacaoFinalNegocio destinacaoFinalNegocio)
        {
            this.destinacaoFinalNegocio = destinacaoFinalNegocio;
        }

       
        public DestinacaoFinalModeloGet Inserir(DestinacaoFinalModeloPost destinacaoFinalPost)
        {
            DestinacaoFinalModeloNegocio destinacaoFinalModeloNegocio = new DestinacaoFinalModeloNegocio();
            Mapper.Map(destinacaoFinalPost, destinacaoFinalModeloNegocio);

            destinacaoFinalModeloNegocio = destinacaoFinalNegocio.Inserir(destinacaoFinalModeloNegocio);

            return Mapper.Map<DestinacaoFinalModeloNegocio, DestinacaoFinalModeloGet>(destinacaoFinalModeloNegocio);

        }

        public List<DestinacaoFinalModeloGet> Listar()
        {
            List<DestinacaoFinalModeloNegocio> destinacoesFinais = destinacaoFinalNegocio.Listar();

            return Mapper.Map<List<DestinacaoFinalModeloNegocio>, List<DestinacaoFinalModeloGet>>(destinacoesFinais);
        }

        public DestinacaoFinalModeloGet Pesquisar(int id)
        {
            DestinacaoFinalModeloNegocio destinacaoFinalModeloNegocio = destinacaoFinalNegocio.Pesquisar(id);

            return Mapper.Map<DestinacaoFinalModeloNegocio, DestinacaoFinalModeloGet>(destinacaoFinalModeloNegocio);

        }

        public void Excluir(int id)
        {
            destinacaoFinalNegocio.Excluir(id);
        }

        public override void RaiseUsuarioAlterado()
        {
            destinacaoFinalNegocio.Usuario = Usuario;
        }
    }
}
