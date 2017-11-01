using Negocio.Comum.Validacao;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Comum.Validacao;
using ProcessoEletronicoService.Negocio.Modelos;
using Prodest.ProcessoEletronico.Integration.Organograma.Base;
using Prodest.ProcessoEletronico.Integration.Organograma.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao
{
    public class MunicipioValidacao : IBaseValidation<MunicipioProcessoModeloNegocio, MunicipioRascunhoProcesso>, IBaseCollectionValidation<MunicipioProcessoModeloNegocio>
    {
        private IMunicipioService _municipioService;
        private GuidValidacao _guidValidacao;

        public MunicipioValidacao(IMunicipioService municipioService, GuidValidacao guidValidacao)
        {
            _municipioService = municipioService;
            _guidValidacao = guidValidacao;
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
                    if (_municipioService.Search(guidMunicipio).ResponseObject == null)
                    {
                        throw new RequisicaoInvalidaException($"Município informado não encontrado no Organograma (Guid : {guidMunicipio})");
                    }
                }
            }
        }

        public void IsGuidMunicipiosValidAndExistInOrganograma(IEnumerable<string> guidMunicipios)
        {
            IEnumerable<Municipio> municipiosES = _municipioService.SearchByEstado("ES").ResponseObject;
            IEnumerable<string> guidMunicipiosES = municipiosES.Select(m => m.Guid).ToList();

            foreach (string guidMunicipio in guidMunicipios)
            {
                _guidValidacao.IsValid(guidMunicipio);
                if (!guidMunicipiosES.Contains(guidMunicipio))
                {
                    throw new RecursoNaoEncontradoException($"Município de identificador {guidMunicipio} não encontrado no Organograma");
                }
            }
        }
    }
}
