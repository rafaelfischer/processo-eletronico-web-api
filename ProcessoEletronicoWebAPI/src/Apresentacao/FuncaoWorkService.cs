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
    public class FuncaoWorkService : BaseWorkService, IFuncaoWorkService
    {
        IFuncaoNegocio funcaoNegocio;

        public FuncaoWorkService(IFuncaoNegocio funcaoNegocio)
        {
            this.funcaoNegocio = funcaoNegocio;
        }
        
        public IEnumerable<FuncaoModelo> PesquisarPorPlanoClassificacao(int idPlanoClassificacao)
        {
            List<FuncaoModeloNegocio> funcoes = funcaoNegocio.PesquisarPorPlanoClassificacao(idPlanoClassificacao);

            return Mapper.Map<List<FuncaoModeloNegocio>, List<FuncaoModelo>>(funcoes);
        }

        public FuncaoProcessoGetModelo Pesquisar(int id)
        {
            FuncaoModeloNegocio funcao = funcaoNegocio.Pesquisar(id);

            return Mapper.Map<FuncaoModeloNegocio, FuncaoProcessoGetModelo>(funcao);
        }

        public FuncaoProcessoGetModelo Inserir(FuncaoModeloPost funcao)
        {
            FuncaoModeloNegocio funcaoModeloNegocio = new FuncaoModeloNegocio();
            Mapper.Map(funcao, funcaoModeloNegocio);

            funcaoModeloNegocio = funcaoNegocio.Inserir(funcaoModeloNegocio);

            return Mapper.Map<FuncaoModeloNegocio, FuncaoProcessoGetModelo>(funcaoModeloNegocio);
        }

        public void Excluir(int id)
        {
            funcaoNegocio.Excluir(id);
        }
        
    }
}
