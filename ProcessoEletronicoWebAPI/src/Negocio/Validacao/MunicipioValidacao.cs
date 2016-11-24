using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Validacao
{
    public class MunicipioValidacao
    {

        public void Preenchido(List<MunicipioProcessoModeloNegocio> municipios)
        {
            foreach (MunicipioProcessoModeloNegocio municipio in municipios)
            {
                Preenchido(municipio);
            }
        }

        public void Preenchido(MunicipioProcessoModeloNegocio municipio)
        {
            if (municipio != null)
            {
                UfPreenchida(municipio);
                MunicipioPreenchido(municipio);
            }
        }

        #region Preenchimento de campos obrigatórios

        internal void UfPreenchida(MunicipioProcessoModeloNegocio municipio)
        {
            if (string.IsNullOrWhiteSpace(municipio.Uf))
            {
                throw new RequisicaoInvalidaException("Uf do município não preenchido.");
            }
        }

        internal void MunicipioPreenchido(MunicipioProcessoModeloNegocio municipio)
        {
            if (string.IsNullOrWhiteSpace(municipio.Nome))
            {
                throw new RequisicaoInvalidaException("Município não preenchido.");
            }
        }

        #endregion

    }
}
