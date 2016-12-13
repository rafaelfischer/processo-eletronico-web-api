using ProcessoEletronicoService.Apresentacao.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using AutoMapper;

namespace ProcessoEletronicoService.Apresentacao
{
    public class PlanoClassificacaoWorkService : BaseWorkService, IPlanoClassificacaoWorkService
    {
        IPlanoClassificacaoNegocio planoClassificacaoNegocio;

        public PlanoClassificacaoWorkService(IPlanoClassificacaoNegocio planoClassificacaoNegocio)
        {
            this.planoClassificacaoNegocio = planoClassificacaoNegocio;
        }

        public IEnumerable<PlanoClassificacaoModelo> Pesquisar(string guidOrganizacao)
        {
            List<PlanoClassificacaoModeloNegocio> planosClassificacao = planoClassificacaoNegocio.Pesquisar(guidOrganizacao);

            return Mapper.Map<List<PlanoClassificacaoModeloNegocio>, List<PlanoClassificacaoModelo>>(planosClassificacao);
        }

        public override void RaiseUsuarioAlterado()
        {
            planoClassificacaoNegocio.Usuario = Usuario;
        }
    }
}
