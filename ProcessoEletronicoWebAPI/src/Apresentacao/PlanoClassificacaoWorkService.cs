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

        public PlanoClassificacaoProcessoGetModelo Pesquisar(int id)
        {
            PlanoClassificacaoModeloNegocio planoClassificacao = planoClassificacaoNegocio.Pesquisar(id);
            return Mapper.Map<PlanoClassificacaoModeloNegocio, PlanoClassificacaoProcessoGetModelo>(planoClassificacao);
        }

        public IEnumerable<PlanoClassificacaoModelo> Pesquisar(string guidOrganizacao)
        {
            List<PlanoClassificacaoModeloNegocio> planosClassificacao = planoClassificacaoNegocio.Pesquisar(guidOrganizacao);

            return Mapper.Map<List<PlanoClassificacaoModeloNegocio>, List<PlanoClassificacaoModelo>>(planosClassificacao);
        }

        public IEnumerable<PlanoClassificacaoModelo> Pesquisar()
        {
            List<PlanoClassificacaoModeloNegocio> planosClassificacao = planoClassificacaoNegocio.Pesquisar();

            return Mapper.Map<List<PlanoClassificacaoModeloNegocio>, List<PlanoClassificacaoModelo>>(planosClassificacao);
        }

        public PlanoClassificacaoProcessoGetModelo Inserir(PlanoClassificacaoModeloPost planoClassificacao)
        {
            PlanoClassificacaoModeloNegocio planoClassificacaoModeloNegocio = new PlanoClassificacaoModeloNegocio();
            Mapper.Map(planoClassificacao, planoClassificacaoModeloNegocio);

            planoClassificacaoModeloNegocio = planoClassificacaoNegocio.Inserir(planoClassificacaoModeloNegocio);

            return Mapper.Map<PlanoClassificacaoModeloNegocio, PlanoClassificacaoProcessoGetModelo>(planoClassificacaoModeloNegocio);

        }

        public void Excluir(int id)
        {
            planoClassificacaoNegocio.Excluir(id);
        }

             
    }
}
