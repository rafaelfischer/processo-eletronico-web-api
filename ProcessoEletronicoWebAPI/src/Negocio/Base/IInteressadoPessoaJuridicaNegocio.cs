using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Comum.Base;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IInteressadoPessoaJuridicaNegocio : IBaseNegocio
    {
        void Excluir(ICollection<InteressadoPessoaJuridica> interessadosPessoaJuridica);
        void Excluir(InteressadoPessoaJuridica interessadoPessoaJuridica);
    }
}
