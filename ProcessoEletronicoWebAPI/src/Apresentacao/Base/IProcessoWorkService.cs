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
        DespachoProcessoModeloGet PesquisarDespacho(int idDespacho, int idProcesso, int idOrganizacaoProcesso);
        ProcessoCompletoModelo Autuar(ProcessoModeloPost processoPost);
        DespachoProcessoModeloGet Despachar(int idOrganizacao, int idProcesso, DespachoProcessoModeloPost despachoPost);
        List<ProcessoModelo> PesquisarProcessosNaUnidade(string guidUnidade);
        List<ProcessoModelo> PesquisarProcessosNaOrganizacao(string guidOrganizacao);
        AnexoModeloGet PesquisarAnexo(int idOrganizacao, int idProcesso, int idDespacho, int idAnexo);
        List<ProcessoModelo> PesquisarProcessosDespachadosUsuario();
        List<DespachoProcessoModeloCompleto> PesquisarDespachosUsuario(int idOrganizacao, string cpfUsuario);
    }
}
