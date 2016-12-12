using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IProcessoNegocio : IBaseNegocio
    {
        ProcessoModeloNegocio Pesquisar(int id);
        ProcessoModeloNegocio Pesquisar(string numero);
        DespachoModeloNegocio PesquisarDespacho(int idDespacho, int idProcesso, int idOrganizacaoProcesso);
        List<ProcessoModeloNegocio> PesquisarProcessoNaUnidade(string guidUnidade);
        ProcessoModeloNegocio Autuar(ProcessoModeloNegocio processoNegocio);
        DespachoModeloNegocio Despachar(int idOrganizacaoProcesso, int idProcesso, DespachoModeloNegocio despachoNegocio);
        List<ProcessoModeloNegocio> PesquisarProcessoNaOrganizacao(string guidOrganizacao);
        AnexoModeloNegocio PesquisarAnexo(int idOrganizacao, int idProcesso, int idDespacho, int idAnexo);
        List<ProcessoModeloNegocio> PesquisarProcessosDespachadosUsuario();
        List<DespachoModeloNegocio> PesquisarDespachosUsuario(int idOrganizacao, string cpfUsuario);
    }
}
