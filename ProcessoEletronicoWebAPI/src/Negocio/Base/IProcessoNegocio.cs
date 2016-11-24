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
        void Pesquisar(string numeroProcesso);
        List<ProcessoModeloNegocio> PesquisarProcessoNaUnidade(int idOrganizacaoProcesso, int idUnidade);
        void Autuar(ProcessoModeloNegocio processoNegocio);
        void Despachar();
        void Excluir();
    }
}
