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
        void Pesquisar(int id);
        void Pesquisar(string numeroProcesso);
        List<ProcessoModeloNegocio> Pesquisar(int idOrganizacaoProcesso, int idUnidade);
        void Autuar(ProcessoModeloNegocio processoNegocio, int idOrganizacao);
        void Despachar();
        void Excluir();
    }
}
