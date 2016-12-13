using ProcessoEletronicoService.Apresentacao.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Base
{
    public interface IProcessoWorkService : IBaseWorkService
    {
        ProcessoCompletoModelo Pesquisar(int id);
        ProcessoCompletoModelo Pesquisar(string numero);
        ProcessoCompletoModelo Autuar(ProcessoModeloPost processoPost);
        List<ProcessoModelo> PesquisarProcessosNaUnidade(string guidUnidade);
        List<ProcessoModelo> PesquisarProcessosNaOrganizacao(string guidOrganizacao);
        List<ProcessoModelo> PesquisarProcessosDespachadosUsuario();
    }
}
