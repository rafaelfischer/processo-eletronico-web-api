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
    public class FuncaoWorkService : IFuncaoWorkService
    {
        IFuncaoNegocio funcaoNegocio;

        public FuncaoWorkService(IFuncaoNegocio funcaoNegocio)
        {
            this.funcaoNegocio = funcaoNegocio;
        }

        public IEnumerable<FuncaoModelo> Pesquisar(int idOrganizacaoPatriarca, int idPlanoClassificacao)
        {
            List<FuncaoModeloNegocio> funcoes = funcaoNegocio.Pesquisar(idOrganizacaoPatriarca, idPlanoClassificacao);

            return Mapper.Map<List<FuncaoModeloNegocio>, List<FuncaoModelo>>(funcoes);
        }
    }
}
