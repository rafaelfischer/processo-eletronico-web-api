using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IProcessoNegocio
    {
        void Listar();
        ProcessoModeloNegocio Pesquisar(int idOrganizacaoProcesso, int idProcesso);
        ProcessoModeloNegocio Pesquisar(string numero);
        DespachoModeloNegocio PesquisarDespacho(int idDespacho, int idProcesso, int idOrganizacaoProcesso);
        List<ProcessoModeloNegocio> PesquisarProcessoNaUnidade(int idOrganizacaoProcesso, int idUnidade);
        ProcessoModeloNegocio Autuar(ProcessoModeloNegocio processoNegocio, int idOrganizacao);
        DespachoModeloNegocio Despachar(int idOrganizacaoProcesso, int idProcesso, DespachoModeloNegocio despachoNegocio);
        void Excluir();
        List<ProcessoModeloNegocio> PesquisarProcessoNaOrganizacao(int idOrganizacaoProcesso, int idOrganizacao);
        AnexoModeloNegocio PesquisarAnexo(int idOrganizacao, int idProcesso, int idDespacho, int idAnexo);
    }
}
