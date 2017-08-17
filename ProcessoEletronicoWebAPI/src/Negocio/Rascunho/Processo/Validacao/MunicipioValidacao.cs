using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Comum.Validacao;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao
{
    public class MunicipioValidacao : IBaseValidation<MunicipioProcessoModeloNegocio, MunicipioRascunhoProcesso>, IBaseCollectionValidation<MunicipioProcessoModeloNegocio>
    {
        private OrganogramaValidacao _organogramaValidacao;

        public MunicipioValidacao(OrganogramaValidacao organogramaValidacao)
        {
            _organogramaValidacao = organogramaValidacao;
        }
        public void Exists(MunicipioRascunhoProcesso municipioRascunhoProcesso)
        {
            if (municipioRascunhoProcesso == null)
            {
                throw new RecursoNaoEncontradoException("Municipio não encontrado");
            }
        }

        public void IsFilled(IEnumerable<MunicipioProcessoModeloNegocio> municipiosRascunhoProcesso)
        {
            if (municipiosRascunhoProcesso != null)
            {
                foreach (MunicipioProcessoModeloNegocio municipioRascunhoProcesso in municipiosRascunhoProcesso)
                {
                    IsFilled(municipioRascunhoProcesso);
                }
            }
        }

        public void IsFilled(MunicipioProcessoModeloNegocio municipioRascunhoProcesso)
        {
        }

        public void IsValid(IEnumerable<MunicipioProcessoModeloNegocio> municipiosRascunhoProcesso)
        {
            if (municipiosRascunhoProcesso != null)
            {
                foreach (MunicipioProcessoModeloNegocio municipioRascunhoProcesso in municipiosRascunhoProcesso)
                {
                    IsValid(municipioRascunhoProcesso);
                }
            }
        }

        public void IsValid(MunicipioProcessoModeloNegocio municipioRascunhoProcesso)
        {
            if (municipioRascunhoProcesso != null)
            {
                if (!string.IsNullOrWhiteSpace(municipioRascunhoProcesso.GuidMunicipio))
                {
                    //Verificar se o Guid é válido
                    Guid guidMunicipio;
                    if (!Guid.TryParse(municipioRascunhoProcesso.GuidMunicipio, out guidMunicipio))
                    {
                        throw new RequisicaoInvalidaException($"Guid do município inválido (Guid: {municipioRascunhoProcesso.GuidMunicipio})");
                    }

                    //Verificar se o município existe no Organograma
                    if (_organogramaValidacao.PesquisarMunicipio(guidMunicipio) == null)
                    {
                        throw new RequisicaoInvalidaException($"Município informado não encontrado no Organograma (Guid : {guidMunicipio})");
                    }
                }
            }
        }
    }
}
