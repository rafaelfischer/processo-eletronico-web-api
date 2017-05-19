using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IInteressadoPessoaFisicaNegocio : IBaseNegocio
    {
        IList<InteressadoPessoaFisicaModeloNegocio> Get(int idRascunhoProcesso);
        InteressadoPessoaFisicaModeloNegocio Get(int idRascunhoProcesso, int id);
        InteressadoPessoaFisicaModeloNegocio Post(int idRascunhoProcesso, InteressadoPessoaFisicaModeloNegocio interessadoPessoaFisicaNegocio);
        void Patch(int idRascunhoProcesso, int id, InteressadoPessoaFisicaModeloNegocio interessadoPessoaFisicaNegocio);
        void Delete(int idRascunhoProcesso, int id);

        void Delete(ICollection<InteressadoPessoaFisica> interessadosPessoaFisica);
        void Delete(InteressadoPessoaFisica interessadoPessoaFisica);
    }
}
