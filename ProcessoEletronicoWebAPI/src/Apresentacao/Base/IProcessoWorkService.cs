using ProcessoEletronicoService.Apresentacao.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Base
{
    public interface IProcessoWorkService
    {
        void Listar();
        ProcessoCompletoModelo Pesquisar(int idOrganizacaoProcesso, int idProcesso);
        ProcessoCompletoModelo Pesquisar(string numero);
        DespachoProcessoGetModelo PesquisarDespacho(int idDespacho, int idProcesso, int idOrganizacaoProcesso);
        ProcessoCompletoModelo Autuar(ProcessoModeloPost processoPost, int idOrganizacao);
        DespachoProcessoGetModelo Despachar(int idOrganizacao, int idProcesso, DespachoProcessoModeloPost despachoPost);
        void Excluir();
        List<ProcessoModelo> PesquisarProcessosNaUnidade(int idOrganizacaoProcesso, int idUnidade);
        List<ProcessoModelo> PesquisarProcessosNaOrganizacao(int idOrganizacaoProcesso, int idOrganizacao);
    }
}
