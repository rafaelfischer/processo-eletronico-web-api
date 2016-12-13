using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Validacao
{
    public class PlanoClassificacaoValidacao
    {
        internal void GuidValido(string guid)
        {
            try
            {
                Guid g = new Guid(guid);

                if (g.Equals(Guid.Empty))
                    throw new RequisicaoInvalidaException("Identificador inválido.");
            }
            catch (FormatException)
            {
                throw new RequisicaoInvalidaException("Formato do identificador inválido.");
            }
        }

        internal void OrganizacaoPatriarcaExistente(BaseNegocio.OrganizacaoOrganogramaModelo organizacaoPatriarca)
        {
            if (organizacaoPatriarca == null)
                throw new RecursoNaoEncontradoException("Organização patriarca não encontrada.");

        }
    }
}
