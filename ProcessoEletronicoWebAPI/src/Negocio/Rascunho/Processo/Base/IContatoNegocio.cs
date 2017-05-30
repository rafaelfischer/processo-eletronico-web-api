using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Base
{
    public interface IContatoNegocio : IBaseNegocio
    {
        IList<ContatoModeloNegocio> Get(int idRascunhoProcesso, int idInteressado);
        ContatoModeloNegocio Get(int idRascunhoProcesso, int idInteressado, int id);
        ContatoModeloNegocio Post(int idRascunhoProcesso, int idInteressado, ContatoModeloNegocio contatoModeloNegocio);
        void Patch(int idRascunhoProcesso, int idInteressado, int id, ContatoModeloNegocio contatoModeloNegocio);
        void Delete(int idRascunhoProcesso, int idInteressado, int id);
        void Delete(ICollection<ContatoRascunho> contatos);
        void Delete(ContatoRascunho contato);
    }

}
