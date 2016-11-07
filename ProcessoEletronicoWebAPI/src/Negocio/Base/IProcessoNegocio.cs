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
        void Autuar();
        void Despachar();
        void Excluir();
    }
}
