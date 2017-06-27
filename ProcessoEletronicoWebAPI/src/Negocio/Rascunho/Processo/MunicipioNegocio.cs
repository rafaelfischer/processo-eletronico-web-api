using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao;
using AutoMapper;
using ProcessoEletronicoService.Negocio.Comum.Validacao;
using static ProcessoEletronicoService.Negocio.Comum.Validacao.OrganogramaValidacao;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo
{
    public class MunicipioNegocio : IMunicipioNegocio
    {
        private IRepositorioGenerico<MunicipioRascunhoProcesso> _repositorioMunicipiosRascunhoProcesso;
        private IRepositorioGenerico<RascunhoProcesso> _repositorioRascunhos;
        private MunicipioValidacao _validacao;
        private RascunhoProcessoValidacao _rascunhoProcessoValidacao;
        private OrganogramaValidacao _organogramaValidacao;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public MunicipioNegocio(IProcessoEletronicoRepositorios repositorios, MunicipioValidacao validacao, RascunhoProcessoValidacao rascunhoProcessoValidacao, OrganogramaValidacao organogramaValidacao, IMapper mapper)
        {
            _repositorioMunicipiosRascunhoProcesso = repositorios.MunicipiosRascunhoProcesso;
            _repositorioRascunhos = repositorios.RascunhosProcesso;
            _validacao = validacao;
            _rascunhoProcessoValidacao = rascunhoProcessoValidacao;
            _organogramaValidacao = organogramaValidacao;
            _unitOfWork = repositorios.UnitOfWork;
            _mapper = mapper;
        }

        public IList<MunicipioRascunhoProcessoModeloNegocio> Get(int idRascunhoProcesso)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            return _mapper.Map<IList<MunicipioRascunhoProcessoModeloNegocio>>(_repositorioMunicipiosRascunhoProcesso.Where(m => m.IdRascunhoProcesso == idRascunhoProcesso)).ToList();
        }

        public MunicipioRascunhoProcessoModeloNegocio Get(int idRascunhoProcesso, int id)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);

            MunicipioRascunhoProcesso municipio = _repositorioMunicipiosRascunhoProcesso
                            .Where(m => m.IdRascunhoProcesso == idRascunhoProcesso
                                     && m.Id == id)
                                     .SingleOrDefault();

            _validacao.Exists(municipio);
            return _mapper.Map<MunicipioRascunhoProcessoModeloNegocio>(municipio);
        }

        public MunicipioRascunhoProcessoModeloNegocio Post(int idRascunhoProcesso, MunicipioRascunhoProcessoModeloNegocio municipioRascunhoProcessoModeloNegocio)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _validacao.IsFilled(municipioRascunhoProcessoModeloNegocio);
            _validacao.IsValid(municipioRascunhoProcessoModeloNegocio);

            MunicipioRascunhoProcesso municipioRascunho = new MunicipioRascunhoProcesso();
            _mapper.Map(municipioRascunhoProcessoModeloNegocio, municipioRascunho);
            municipioRascunho.IdRascunhoProcesso = idRascunhoProcesso;
            InformacoesMunicipio(municipioRascunho);
            _repositorioMunicipiosRascunhoProcesso.Add(municipioRascunho);

            _unitOfWork.Save();
            return Get(idRascunhoProcesso, municipioRascunho.Id);
        }

        public void Patch(int idRascunhoProcesso, int id, MunicipioRascunhoProcessoModeloNegocio municipioRascunhoProcessoModeloNegocio)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);

            MunicipioRascunhoProcesso municipio = _repositorioMunicipiosRascunhoProcesso
                             .Where(m => m.IdRascunhoProcesso == idRascunhoProcesso
                                      && m.Id == id)
                                      .SingleOrDefault();

            _validacao.Exists(municipio);
            _validacao.IsFilled(municipioRascunhoProcessoModeloNegocio);
            _validacao.IsValid(municipioRascunhoProcessoModeloNegocio);
            MapMunicipio(municipioRascunhoProcessoModeloNegocio, municipio);
            InformacoesMunicipio(municipio);
            ClearNomeUf(municipio);
            _unitOfWork.Save();
        }

        public void Delete(int idRascunhoProcesso, int id)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);

            MunicipioRascunhoProcesso municipio = _repositorioMunicipiosRascunhoProcesso
                            .Where(m => m.IdRascunhoProcesso == idRascunhoProcesso
                                     && m.Id == id)
                                     .SingleOrDefault();

            _validacao.Exists(municipio);
            _repositorioMunicipiosRascunhoProcesso.Remove(municipio);
            _unitOfWork.Save();
        }

        public void Delete(MunicipioRascunhoProcesso municipio)
        {
            if (municipio != null)
            {
                _repositorioMunicipiosRascunhoProcesso.Remove(municipio);
            }
        }

        public void Delete(ICollection<MunicipioRascunhoProcesso> municipios)
        {
            if (municipios != null)
            {
                foreach (MunicipioRascunhoProcesso municipio in municipios)
                {
                    Delete(municipio);
                }
            }
        }

        private void MapMunicipio(MunicipioRascunhoProcessoModeloNegocio municipioRascunhoProcessoModeloNegocio, MunicipioRascunhoProcesso municipio)
        {
            Guid guidMunicipio;
            if (Guid.TryParse(municipioRascunhoProcessoModeloNegocio.GuidMunicipio, out guidMunicipio))
            {
                municipio.GuidMunicipio = guidMunicipio;
            }
            else
            {
                municipio.GuidMunicipio = null;
            }

        }

        private void InformacoesMunicipio(MunicipioRascunhoProcesso municipio)
        {
            if (municipio.GuidMunicipio.HasValue)
            {
                MunicipioOrganogramaModelo municipioOrganograma = _organogramaValidacao.PesquisarMunicipio(municipio.GuidMunicipio.Value);

                if (municipioOrganograma == null)
                {
                    throw new RequisicaoInvalidaException($"Município informado não encontrado no Organograma (Guid : {municipio.GuidMunicipio.Value})");
                }

                municipio.Nome = municipioOrganograma.nome;
                municipio.Uf = municipioOrganograma.uf;
            }
        }

        private void ClearNomeUf(MunicipioRascunhoProcesso municipio)
        {
            if (!municipio.GuidMunicipio.HasValue)
            {
                municipio.Nome = null;
                municipio.Uf = null;
            }
        }
    }
}
