using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao;
using Prodest.ProcessoEletronico.Integration.Organograma.Base;
using Prodest.ProcessoEletronico.Integration.Organograma.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo
{
    public class MunicipioNegocio : IMunicipioNegocio
    {
        private IRepositorioGenerico<MunicipioRascunhoProcesso> _repositorioMunicipiosRascunhoProcesso;
        private IRepositorioGenerico<RascunhoProcesso> _repositorioRascunhos;
        private MunicipioValidacao _validacao;
        private RascunhoProcessoValidacao _rascunhoProcessoValidacao;
        private IMunicipioService _municipioService;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public MunicipioNegocio(IProcessoEletronicoRepositorios repositorios, MunicipioValidacao validacao, RascunhoProcessoValidacao rascunhoProcessoValidacao, IMunicipioService municipioService, IMapper mapper)
        {
            _repositorioMunicipiosRascunhoProcesso = repositorios.MunicipiosRascunhoProcesso;
            _repositorioRascunhos = repositorios.RascunhosProcesso;
            _validacao = validacao;
            _rascunhoProcessoValidacao = rascunhoProcessoValidacao;
            _municipioService = municipioService;
            _unitOfWork = repositorios.UnitOfWork;
            _mapper = mapper;
        }

        public IList<MunicipioProcessoModeloNegocio> Get(int idRascunhoProcesso)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            return _mapper.Map<IList<MunicipioProcessoModeloNegocio>>(_repositorioMunicipiosRascunhoProcesso.Where(m => m.IdRascunhoProcesso == idRascunhoProcesso)).ToList();
        }

        public MunicipioProcessoModeloNegocio Get(int idRascunhoProcesso, int id)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);

            MunicipioRascunhoProcesso municipio = _repositorioMunicipiosRascunhoProcesso
                            .Where(m => m.IdRascunhoProcesso == idRascunhoProcesso
                                     && m.Id == id)
                                     .SingleOrDefault();

            _validacao.Exists(municipio);
            return _mapper.Map<MunicipioProcessoModeloNegocio>(municipio);
        }

        public MunicipioProcessoModeloNegocio Post(int idRascunhoProcesso, MunicipioProcessoModeloNegocio MunicipioProcessoModeloNegocio)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _validacao.IsFilled(MunicipioProcessoModeloNegocio);
            _validacao.IsValid(MunicipioProcessoModeloNegocio);

            MunicipioRascunhoProcesso municipioRascunho = new MunicipioRascunhoProcesso();
            _mapper.Map(MunicipioProcessoModeloNegocio, municipioRascunho);
            municipioRascunho.IdRascunhoProcesso = idRascunhoProcesso;
            InformacoesMunicipio(municipioRascunho);
            _repositorioMunicipiosRascunhoProcesso.Add(municipioRascunho);

            _unitOfWork.Save();
            return Get(idRascunhoProcesso, municipioRascunho.Id);
        }

        public IEnumerable<MunicipioProcessoModeloNegocio> PostCollection(int idRascunhoProcesso, IEnumerable<string> guidMunicipios)
        {
            RascunhoProcesso rascunhoProcesso = _repositorioRascunhos.Where(r => r.Id == idRascunhoProcesso).Include(m => m.MunicipiosRascunhoProcesso).SingleOrDefault();
            _rascunhoProcessoValidacao.Exists(rascunhoProcesso);
            
            _validacao.IsGuidMunicipiosValidAndExistInOrganograma(guidMunicipios);
            IEnumerable<MunicipioRascunhoProcesso> municipiosRascunho = FillMunicipioInformation(guidMunicipios);

            DeleteAll(idRascunhoProcesso);
            rascunhoProcesso.MunicipiosRascunhoProcesso.AddRange(municipiosRascunho);

            _unitOfWork.Save();
            return Get(idRascunhoProcesso);
        }
        
        public void Patch(int idRascunhoProcesso, int id, MunicipioProcessoModeloNegocio MunicipioProcessoModeloNegocio)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);

            MunicipioRascunhoProcesso municipio = _repositorioMunicipiosRascunhoProcesso
                             .Where(m => m.IdRascunhoProcesso == idRascunhoProcesso
                                      && m.Id == id)
                                      .SingleOrDefault();

            _validacao.Exists(municipio);
            _validacao.IsFilled(MunicipioProcessoModeloNegocio);
            _validacao.IsValid(MunicipioProcessoModeloNegocio);
            MapMunicipio(MunicipioProcessoModeloNegocio, municipio);
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

        public void DeleteAll(int idRascunhoProcesso)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);

            IEnumerable<MunicipioRascunhoProcesso> municipios = _repositorioMunicipiosRascunhoProcesso
                            .Where(m => m.IdRascunhoProcesso == idRascunhoProcesso)
                                     .ToList();

            _repositorioMunicipiosRascunhoProcesso.RemoveRange(municipios);
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

        private void MapMunicipio(MunicipioProcessoModeloNegocio MunicipioProcessoModeloNegocio, MunicipioRascunhoProcesso municipio)
        {
            Guid guidMunicipio;
            if (Guid.TryParse(MunicipioProcessoModeloNegocio.GuidMunicipio, out guidMunicipio))
            {
                municipio.GuidMunicipio = guidMunicipio;
            }
            else
            {
                municipio.GuidMunicipio = null;
            }

        }

        private void InformacoesMunicipio(MunicipioRascunhoProcesso municipioRascunhoProcesso)
        {
            if (municipioRascunhoProcesso.GuidMunicipio.HasValue)
            {
                Municipio municipio = _municipioService.Search(municipioRascunhoProcesso.GuidMunicipio.Value).ResponseObject;

                if (municipio == null)
                {
                    throw new RequisicaoInvalidaException($"Município informado não encontrado no Organograma (Guid : {municipioRascunhoProcesso.GuidMunicipio.Value})");
                }

                municipioRascunhoProcesso.Nome = municipio.Nome;
                municipioRascunhoProcesso.Uf = municipio.Uf;
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

        private IEnumerable<MunicipioRascunhoProcesso> FillMunicipioInformation(IEnumerable<string> guidMunicipios)
        {
            ICollection<MunicipioRascunhoProcesso> municipiosRascunhoProcesso = new List<MunicipioRascunhoProcesso>();
            IEnumerable<Municipio> municipiosES = _municipioService.SearchByEstado("ES").ResponseObject;

            foreach(string guidMunicipio in guidMunicipios)
            {
                Municipio municipio = municipiosES.Where(m => m.Guid.Equals(guidMunicipio)).Single();
                municipiosRascunhoProcesso.Add(_mapper.Map<MunicipioRascunhoProcesso>(municipio));
            }

            return municipiosRascunhoProcesso;
        }
    }
}
