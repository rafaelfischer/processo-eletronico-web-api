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

        #region Preenchimento de campos obrigatórios
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
                GuidPreenchido(municipio);
            }
        }
        
        internal void GuidPreenchido(MunicipioProcessoModeloNegocio municipio)
        {
            if (string.IsNullOrWhiteSpace(municipio.GuidMunicipio))
            {
                throw new RequisicaoInvalidaException("Identificador do município não preenchido.");
            }
        }
        #endregion

        #region Validação de campos 
        public void Valido(List<MunicipioProcessoModeloNegocio> municipios)
        {
            foreach (MunicipioProcessoModeloNegocio municipio in municipios)
            {
                Valido(municipio);
            }
        }

        public void Valido(MunicipioProcessoModeloNegocio municipio)
        {
            if (municipio != null)
            {
                GuidValido(municipio);
            }
        }

        internal void GuidValido(MunicipioProcessoModeloNegocio municipio)
        {
            try
            {
                Guid guid = new Guid(municipio.GuidMunicipio);
            }
            catch (Exception)
            {
                throw new RequisicaoInvalidaException($"Identificador do município {municipio.GuidMunicipio} inválido");
            }
        }
        #endregion


    }
}
