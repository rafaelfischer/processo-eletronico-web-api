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
        void Pesquisar(int id);
        void Pesquisar(string numeroProcesso);
        void Autuar();
        void Despachar();
        void Excluir();
        List<ProcessoModelo> Pesquisar(int idOrganizacaoProcesso, int idUnidade);
    }
}
