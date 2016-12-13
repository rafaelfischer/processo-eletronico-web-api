using ProcessoEletronicoService.Apresentacao.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Base
{
    public interface IDespachoWorkService : IBaseWorkService
    {
        DespachoModeloGet Pesquisar(int idDespacho);
        DespachoModeloGet Despachar(DespachoModeloPost despachoPost);
        List<DespachoModeloGet> PesquisarDespachosUsuario();
    }
}
