using ProcessoEletronicoService.Apresentacao.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Base
{
    public interface IProcessoWorkService : IBaseWorkService
    {
        void Listar();
        ProcessoCompletoModelo Pesquisar(int idOrganizacaoProcesso, int idProcesso);
        ProcessoCompletoModelo Pesquisar(string numero);
        DespachoProcessoModeloGet PesquisarDespacho(int idDespacho, int idProcesso, int idOrganizacaoProcesso);
        ProcessoCompletoModelo Autuar(ProcessoModeloPost processoPost, int idOrganizacao);
        DespachoProcessoModeloGet Despachar(int idOrganizacao, int idProcesso, DespachoProcessoModeloPost despachoPost);
        void Excluir();
        List<ProcessoModelo> PesquisarProcessosNaUnidade(int idOrganizacaoProcesso, int idUnidade);
        List<ProcessoModelo> PesquisarProcessosNaOrganizacao(string guidOrganizacao);
        AnexoModeloGet PesquisarAnexo(int idOrganizacao, int idProcesso, int idDespacho, int idAnexo);
        List<ProcessoModelo> PesquisarProcessosDespachadosUsuario(int id, string cpfUsuario);
        List<DespachoProcessoModeloCompleto> PesquisarDespachosUsuario(int idOrganizacao, string cpfUsuario);
    }
}
