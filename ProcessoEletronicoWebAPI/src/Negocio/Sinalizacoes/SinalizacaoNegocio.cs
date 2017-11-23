using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Negocio.Comum.Validacao;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Comum.Validacao;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Sinalizacoes.Base;
using ProcessoEletronicoService.Negocio.Sinalizacoes.Validacao;
using ProcessoEletronicoService.Negocio.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessoEletronicoService.Negocio.Sinalizacoes
{
    public class SinalizacaoNegocio : ISinalizacaoNegocio
    {
        private SinalizacoesValidacao _validacao;
        private UsuarioValidacao _usuarioValidacao;
        private GuidValidacao _guidValidacao;
        private ICurrentUserProvider _user;

        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IRepositorioGenerico<Sinalizacao> _repositorioSinalizacoes;
        private IRepositorioGenerico<OrganizacaoProcesso> _repositorioOrganizacoes;

        public SinalizacaoNegocio(IProcessoEletronicoRepositorios repositorios, IMapper mapper, SinalizacoesValidacao validacao, GuidValidacao guidValidacao, UsuarioValidacao usuarioValidacao, ICurrentUserProvider user)
        {
            _unitOfWork = repositorios.UnitOfWork;
            _mapper = mapper;
            _user = user;
            _repositorioSinalizacoes = repositorios.Sinalizacoes;
            _repositorioOrganizacoes = repositorios.OrganizacoesProcesso;
            _validacao = validacao;
            _guidValidacao = guidValidacao;
            _usuarioValidacao = usuarioValidacao;
        }

        public SinalizacaoModeloNegocio Get(int id)
        {
            Sinalizacao sinalizacao = _repositorioSinalizacoes.Where(s => s.Id == id).Include(s => s.OrganizacaoProcesso).SingleOrDefault();
            _validacao.Exists(sinalizacao);
            return _mapper.Map<SinalizacaoModeloNegocio>(sinalizacao);
        }

        public IList<SinalizacaoModeloNegocio> Get()
        {
            _usuarioValidacao.PossuiOrganizaoPatriarca(_user.UserGuidOrganizacaoPatriarca);
            IList<Sinalizacao> sinalizacoes = _repositorioSinalizacoes.Where(s => s.OrganizacaoProcesso.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca)).ToList();
            return _mapper.Map<IList<SinalizacaoModeloNegocio>>(sinalizacoes);
        }

        public IList<SinalizacaoModeloNegocio> Get(string guidOrganizacaoPatriarca)
        {
            Guid guid = _guidValidacao.ValidateAndReturnGuid(guidOrganizacaoPatriarca);

            IList<Sinalizacao> sinalizacoes = _repositorioSinalizacoes.Where(s => s.OrganizacaoProcesso.GuidOrganizacao.Equals(guid))
                                                      .Include(pc => pc.OrganizacaoProcesso)
                                                      .ToList();

            return _mapper.Map<List<SinalizacaoModeloNegocio>>(sinalizacoes);
        }

        public SinalizacaoModeloNegocio Add(SinalizacaoModeloNegocio sinalizacaoModeloNegocio)
        {
            _usuarioValidacao.PossuiOrganizaoPatriarca(_user.UserGuidOrganizacaoPatriarca);
            _validacao.IsFilled(sinalizacaoModeloNegocio);
            _validacao.IsValid(sinalizacaoModeloNegocio);

            //criar mapeamento
            Sinalizacao sinalizacao = _mapper.Map<Sinalizacao>(sinalizacaoModeloNegocio);

            //Preenche a organizacao da Sinalizacao conforme o guid da organizacao patriarca
            FillOrganizacao(sinalizacao);

            _repositorioSinalizacoes.Add(sinalizacao);
            _unitOfWork.Save();

            return Get(sinalizacao.Id);
        }

        public void Update(int id, SinalizacaoModeloNegocio sinalizacaoModeloNegocio)
        {
            _usuarioValidacao.PossuiOrganizaoPatriarca(_user.UserGuidOrganizacaoPatriarca);

            Sinalizacao sinalizacao = _repositorioSinalizacoes.Where(s => s.Id == id && s.OrganizacaoProcesso.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca)).SingleOrDefault();
            _validacao.Exists(sinalizacao);

            _validacao.IsFilled(sinalizacaoModeloNegocio);
            _validacao.IsValid(sinalizacaoModeloNegocio);

            _mapper.Map(sinalizacaoModeloNegocio, sinalizacao);
            _unitOfWork.Save();
        }


        public void Delete(int id)
        {
            _usuarioValidacao.PossuiOrganizaoPatriarca(_user.UserGuidOrganizacaoPatriarca);

            Sinalizacao sinalizacao = _repositorioSinalizacoes.Where(s => s.Id == id && s.OrganizacaoProcesso.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca)).SingleOrDefault();
            _validacao.Exists(sinalizacao);

            _validacao.ExistsInProcesso(sinalizacao);
            _validacao.ExistsInRascunhoProcesso(sinalizacao);

            _repositorioSinalizacoes.Remove(sinalizacao);
            _unitOfWork.Save();
        }

        private void FillOrganizacao(Sinalizacao sinalizacao)
        {
            OrganizacaoProcesso organizacaoProcesso = _repositorioOrganizacoes.Where(o => o.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca)).SingleOrDefault();
            sinalizacao.IdOrganizacaoProcesso = organizacaoProcesso.Id;
        }

    }
}
