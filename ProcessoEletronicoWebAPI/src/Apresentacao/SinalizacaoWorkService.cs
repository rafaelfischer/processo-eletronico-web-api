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
    public class SinalizacaoWorkService : ISinalizacaoWorkService
    {
        private ISinalizacaoNegocio sinalizacaoNegocio;

        public SinalizacaoWorkService(ISinalizacaoNegocio sinalizacaoNegocio)
        {
            this.sinalizacaoNegocio = sinalizacaoNegocio;
        }

        public List<SinalizacaoModelo> Pesquisar(string guidOrganizacaoPatriarca)
        {
            List<SinalizacaoModeloNegocio> sinalizacoes = sinalizacaoNegocio.Pesquisar(guidOrganizacaoPatriarca);

            return Mapper.Map<List<SinalizacaoModeloNegocio>, List<SinalizacaoModelo>>(sinalizacoes);
        }
    }
}
