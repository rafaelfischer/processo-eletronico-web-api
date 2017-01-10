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
        
        public IEnumerable<FuncaoModelo> Pesquisar(int idPlanoClassificacao)
        {
            List<FuncaoModeloNegocio> funcoes = funcaoNegocio.PesquisarPorPlanoClassificacao(idPlanoClassificacao);

            return Mapper.Map<List<FuncaoModeloNegocio>, List<FuncaoModelo>>(funcoes);
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

        public override void RaiseUsuarioAlterado()
        {
            funcaoNegocio.Usuario = Usuario;
        }
    }
}
