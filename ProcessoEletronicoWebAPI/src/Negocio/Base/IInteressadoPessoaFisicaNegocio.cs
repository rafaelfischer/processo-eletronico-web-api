using ProcessoEletronicoService.Dominio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IInteressadoPessoaFisicaNegocio : IBaseNegocio
    {
        void Excluir(ICollection<InteressadoPessoaFisica> interessadosPessoaFisica);
        void Excluir(InteressadoPessoaFisica interessadoPessoaFisica);
    }
}
