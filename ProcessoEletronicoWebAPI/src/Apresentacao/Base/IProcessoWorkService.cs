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
        object Pesquisar(int id, int idUnidade);
    }
}
