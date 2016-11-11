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
    public class PlanoClassificacaoWorkService : IPlanoClassificacaoWorkService
    {
        IPlanoClassificacaoNegocio planoClassificacaoNegocio;

        public PlanoClassificacaoWorkService(IPlanoClassificacaoNegocio planoClassificacaoNegocio)
        {
            this.planoClassificacaoNegocio = planoClassificacaoNegocio;
        }

        public IEnumerable<PlanoClassificacaoModelo> Pesquisar(int idOrganizacaoPatriarca, int idOrganizacao)
        {
            List<PlanoClassificacaoModeloNegocio> planosClassificacao = planoClassificacaoNegocio.Pesquisar(idOrganizacaoPatriarca, idOrganizacao);

            return Mapper.Map<List<PlanoClassificacaoModeloNegocio>, List<PlanoClassificacaoModelo>>(planosClassificacao);
        }
    }
}
