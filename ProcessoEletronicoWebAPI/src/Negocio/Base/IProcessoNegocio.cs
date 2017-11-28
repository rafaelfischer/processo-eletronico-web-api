using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IProcessoNegocio
    {
        ProcessoModeloNegocio Pesquisar(int id);
        ProcessoModeloNegocio Pesquisar(string numero);
        ProcessoModeloNegocio PesquisarSemDespachos(string numero);
        ProcessoModeloNegocio PesquisarSimplificado(string numero);
        List<ProcessoModeloNegocio> PesquisarProcessosNaUnidade(string guidUnidade);
        ProcessoModeloNegocio Autuar(ProcessoModeloNegocio processoNegocio);
        ProcessoModeloNegocio Post(int idRascunhoProcesso);
        List<ProcessoModeloNegocio> PesquisarProcessosNaOrganizacao(string guidOrganizacao);
        List<ProcessoModeloNegocio> PesquisarProcessosDespachadosUsuario();
    }
}
