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

        public List<DestinacaoFinalModeloGet> Listar()
        {
            List<DestinacaoFinalModeloNegocio> destinacoesFinais = destinacaoFinalNegocio.Listar();

            return Mapper.Map<List<DestinacaoFinalModeloNegocio>, List<DestinacaoFinalModeloGet>>(destinacoesFinais);
        }

        public override void RaiseUsuarioAlterado()
        {
            destinacaoFinalNegocio.Usuario = Usuario;
        }
    }
}
