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
using Microsoft.EntityFrameworkCore;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo
{
    public class AnexoNegocio : IAnexoNegocio
    {
        private IRepositorioGenerico<AnexoRascunho> _repositorioAnexosRascunho;
        private IRepositorioGenerico<RascunhoProcesso> _repositorioRascunhos;
        private AnexoValidacao _validacao;
        private RascunhoProcessoValidacao _rascunhoProcessoValidacao;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public AnexoNegocio(IProcessoEletronicoRepositorios repositorios, AnexoValidacao validacao, RascunhoProcessoValidacao rascunhoProcessoValidacao, IMapper mapper)
        {
            _repositorioAnexosRascunho = repositorios.AnexosRascunho;
            _repositorioRascunhos = repositorios.RascunhosProcesso;
            _validacao = validacao;
            _rascunhoProcessoValidacao = rascunhoProcessoValidacao;
            _unitOfWork = repositorios.UnitOfWork;
            _mapper = mapper;
        }

        public IList<AnexoModeloNegocio> Get(int idRascunhoProcesso)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            
            // O conteúdo do anexo será trazido apenas na busca por identificador
            return _mapper.Map<IList<AnexoModeloNegocio>>(_repositorioAnexosRascunho.Select(a => new AnexoRascunho
            {
                Id = a.Id,
                Nome = a.Nome,
                Descricao = a.Descricao,
                MimeType = a.MimeType,
                TipoDocumental = a.TipoDocumental,
                IdRascunhoProcesso = a.IdRascunhoProcesso
                
            }).Where(a => a.IdRascunhoProcesso == idRascunhoProcesso)).ToList();
        }
        
        public AnexoModeloNegocio Get(int idRascunhoProcesso, int id)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);

            AnexoRascunho anexo = _repositorioAnexosRascunho
                            .Where(a => a.IdRascunhoProcesso == idRascunhoProcesso
                                     && a.Id == id).Include(a => a.TipoDocumental)
                                     .SingleOrDefault();

            _validacao.Exists(anexo);
            return _mapper.Map<AnexoModeloNegocio>(anexo);
        }

        public AnexoModeloNegocio Post(int idRascunhoProcesso, AnexoModeloNegocio anexoRascunhoModeloNegocio)
        {
            RascunhoProcesso rascunhoProcesso = _repositorioRascunhos.Where(r => r.Id == idRascunhoProcesso).SingleOrDefault();
            _rascunhoProcessoValidacao.Exists(rascunhoProcesso);

            _validacao.IsFilled(anexoRascunhoModeloNegocio);
            _validacao.IsValid(anexoRascunhoModeloNegocio, rascunhoProcesso.IdAtividade);

            AnexoRascunho anexoRascunho = new AnexoRascunho();
            _mapper.Map(anexoRascunhoModeloNegocio, anexoRascunho);
            anexoRascunho.IdRascunhoProcesso = idRascunhoProcesso;
            _repositorioAnexosRascunho.Add(anexoRascunho);

            _unitOfWork.Save();
            return Get(idRascunhoProcesso, anexoRascunho.Id);
        }

        public void Patch(int idRascunhoProcesso, int id, AnexoModeloNegocio anexoRascunhoModeloNegocio)
        {
            RascunhoProcesso rascunhoProcesso = _repositorioRascunhos.Where(r => r.Id == idRascunhoProcesso).SingleOrDefault();
            _rascunhoProcessoValidacao.Exists(rascunhoProcesso);

            AnexoRascunho anexo = _repositorioAnexosRascunho
                             .Where(m => m.IdRascunhoProcesso == idRascunhoProcesso
                                      && m.Id == id)
                                      .SingleOrDefault();

            _validacao.Exists(anexo);
            _validacao.IsFilled(anexoRascunhoModeloNegocio);
            _validacao.IsValid(anexoRascunhoModeloNegocio, rascunhoProcesso.IdAtividade);
            MapAnexo(anexoRascunhoModeloNegocio, anexo);
            _unitOfWork.Save();
        }

        public void Delete(int idRascunhoProcesso, int id)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);

            AnexoRascunho anexo = _repositorioAnexosRascunho
                             .Where(m => m.IdRascunhoProcesso == idRascunhoProcesso
                                      && m.Id == id)
                                      .SingleOrDefault();

            _validacao.Exists(anexo);
            _repositorioAnexosRascunho.Remove(anexo);
            _unitOfWork.Save();
        }

        public void Delete(AnexoRascunho anexo)
        {
            if (anexo != null)
            {
                _repositorioAnexosRascunho.Remove(anexo);
            }
        }

        public void Delete(ICollection<AnexoRascunho> anexos)
        {
            if (anexos != null)
            {
                foreach (AnexoRascunho anexo in anexos)
                {
                    Delete(anexo);
                }
            }
        }

        private void MapMunicipio(MunicipioProcessoModeloNegocio municipioProcessoModeloNegocio, MunicipioRascunhoProcesso municipio)
        {
            Guid guidMunicipio;
            if (Guid.TryParse(municipioProcessoModeloNegocio.GuidMunicipio, out guidMunicipio))
            {
                municipio.GuidMunicipio = guidMunicipio;
            }
            else
            {
                municipio.GuidMunicipio = null;
            }

        }
        
        private void MapAnexo (AnexoModeloNegocio anexoModeloNegocio, AnexoRascunho anexo)
        {
            anexo.Conteudo = anexoModeloNegocio.Conteudo;
            anexo.Descricao = anexoModeloNegocio.Descricao;
            anexo.MimeType = anexoModeloNegocio.MimeType;
            anexo.IdTipoDocumental = anexoModeloNegocio.TipoDocumental != null ? anexoModeloNegocio.TipoDocumental.Id : (int?) null;
        }        
    }
}
