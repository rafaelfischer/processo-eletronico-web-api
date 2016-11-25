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
        void Pesquisar(string numeroProcesso);
        void Autuar(ProcessoModeloPost processoPost, int idOrganizacao);
        void Despachar();
        void Excluir();
        List<ProcessoModelo> PesquisarProcessosNaUnidade(int idOrganizacaoProcesso, int idUnidade);
    }
}
