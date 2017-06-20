using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Rascunho.Proceso.Base
{
    public interface IInteressadoPessoaJuridicaNegocio
    {
        IList<InteressadoPessoaJuridicaModeloNegocio> Get(int idRascunhoProcesso);
        InteressadoPessoaJuridicaModeloNegocio Get(int idRascunhoProcesso, int id);
        InteressadoPessoaJuridicaModeloNegocio Post(int idRascunhoProcesso, InteressadoPessoaJuridicaModeloNegocio interessadoPessoaJuridicaNegocio);
        void Patch(int idRascunhoProcesso, int id, InteressadoPessoaJuridicaModeloNegocio interessadoPessoaJuridicaNegocio);
        void Delete(int idRascunhoProcesso, int id);
        void Delete(ICollection<InteressadoPessoaJuridicaRascunho> interessadosPessoaJuridica);
        void Delete(InteressadoPessoaJuridicaRascunho interessadoPessoaJuridica);
    }
}
