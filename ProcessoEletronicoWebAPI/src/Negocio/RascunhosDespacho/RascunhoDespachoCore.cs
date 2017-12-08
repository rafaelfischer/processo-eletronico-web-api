using Negocio.RascunhosDespacho.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Negocio.RascunhosDespacho.Models;
using Negocio.RascunhosDespacho.Validations.Base;
using AutoMapper;
using ProcessoEletronicoService.Dominio.Modelos;
using Prodest.ProcessoEletronico.Integration.Organograma.Models;
using Prodest.ProcessoEletronico.Integration.Organograma.Base;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Dominio.Base;
using System.Linq;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao;
using ProcessoEletronicoService.Negocio.Comum.Validacao;
using Microsoft.EntityFrameworkCore;

namespace Negocio.RascunhosDespacho
{
    public class RascunhoDespachoCore : IRascunhoDespachoCore
    {
        private ICurrentUserProvider _user;
        private IMapper _mapper;
        private IOrganizacaoService _organizacaoService;
        private IRascunhoDespachoValidation _validation;
        private IAnexoRascunhoDespachoValidation _anexoRascunhoDespachoValidation;
        private IUnidadeService _unidadeService;
        private IRepositorioGenerico<OrganizacaoProcesso> _repositorioOrganizacoesProcesso;
        private IRepositorioGenerico<RascunhoDespacho> _repositorioRascunhosDespacho;
        private IRepositorioGenerico<AnexoRascunho> _repositorioRascunhosAnexo;
        private IUnitOfWork _unitOfWork;

        private AnexoValidacao _anexoValidacao;
        private UsuarioValidacao _usuarioValidacao;

        public RascunhoDespachoCore(ICurrentUserProvider user, IMapper mapper, IOrganizacaoService organizacaoService, IRascunhoDespachoValidation validation, IAnexoRascunhoDespachoValidation anexoRascunhoDespachoValidation, IUnidadeService unidadeService, IProcessoEletronicoRepositorios repositorios, AnexoValidacao anexoValidacao, UsuarioValidacao usuarioValidacao)
        {
            _user = user;
            _mapper = mapper;
            _organizacaoService = organizacaoService;
            _validation = validation;
            _unidadeService = unidadeService;
            _repositorioOrganizacoesProcesso = repositorios.OrganizacoesProcesso;
            _repositorioRascunhosDespacho = repositorios.RascunhosDespacho;
            _repositorioRascunhosAnexo = repositorios.AnexosRascunho;
            _unitOfWork = repositorios.UnitOfWork;

            _anexoRascunhoDespachoValidation = anexoRascunhoDespachoValidation;
            _usuarioValidacao = usuarioValidacao;
        }

        public RascunhoDespachoModel Search(int id)
        {
            RascunhoDespacho rascunhoDespacho = _repositorioRascunhosDespacho.Where(rascunho => rascunho.Id == id).Include(rascunho => rascunho.AnexosRascunho).ThenInclude(a => a.TipoDocumental).SingleOrDefault();
            return _mapper.Map<RascunhoDespachoModel>(rascunhoDespacho);
        }

        public IEnumerable<RascunhoDespachoModel> SearchByOrganizacao()
        {
            IEnumerable<RascunhoDespacho> rascunhosDespachoByOrganizacao = _repositorioRascunhosDespacho.Where(rascunho => rascunho.GuidOrganizacao.Equals(_user.UserGuidOrganizacao)).ToList();
            return _mapper.Map<IEnumerable<RascunhoDespachoModel>>(rascunhosDespachoByOrganizacao);
        }

        public IEnumerable<RascunhoDespachoModel> SearchByUsuario()
        {
            IEnumerable<RascunhoDespacho> rascunhosDespachoByUsuario = _repositorioRascunhosDespacho.Where(rascunho => rascunho.IdUsuario.Equals(_user.UserCpf)).ToList();
            return _mapper.Map<IEnumerable<RascunhoDespachoModel>>(rascunhosDespachoByUsuario);
        }

        public RascunhoDespachoModel Add(RascunhoDespachoModel rascunhoDespachoModel)
        {
            _validation.IsFilled(rascunhoDespachoModel);
            _validation.IsValid(rascunhoDespachoModel);
            _anexoRascunhoDespachoValidation.IsValid(rascunhoDespachoModel.Anexos);

            RascunhoDespacho rascunhoDespacho = new RascunhoDespacho();
            _mapper.Map(rascunhoDespachoModel, rascunhoDespacho);

            FillOrganizacaoDestino(rascunhoDespacho);
            FillUnidadeDestino(rascunhoDespacho);
            FillUserAndDataHora(rascunhoDespacho);
            FillOrganizacao(rascunhoDespacho);
            FillOrganizacaoProcesso(rascunhoDespacho);

            _repositorioRascunhosDespacho.Add(rascunhoDespacho);
            _unitOfWork.Save();

            return Search(rascunhoDespacho.Id);
        }

        public RascunhoDespachoModel Clone(int id)
        {
            RascunhoDespacho rascunhoDespacho = _repositorioRascunhosDespacho.Where(r => r.Id.Equals(id)).Include(r => r.AnexosRascunho).ThenInclude(a => a.TipoDocumental).SingleOrDefault();
            _validation.Exists(rascunhoDespacho);

            RascunhoDespachoModel rascunhoDespachoModel = _mapper.Map<RascunhoDespachoModel>(rascunhoDespacho);
            return Add(rascunhoDespachoModel);
        }

        public void Delete(int id)
        {
            RascunhoDespacho rascunhoDespacho = _repositorioRascunhosDespacho.Where(r => r.Id.Equals(id)).Include(r => r.AnexosRascunho).SingleOrDefault();
            _validation.Exists(rascunhoDespacho);
            _validation.IsRascunhoDespachoOfUser(rascunhoDespacho);

            _repositorioRascunhosDespacho.Remove(rascunhoDespacho);
            _unitOfWork.Save();
        }

        public void Update(int id, RascunhoDespachoModel rascunhoDespachoModel)
        {
            RascunhoDespacho rascunhoDespacho = _repositorioRascunhosDespacho.Where(r => r.Id.Equals(id)).SingleOrDefault();
            _validation.Exists(rascunhoDespacho);

            //Autenticação do usuário
            _usuarioValidacao.Autenticado(_user.UserCpf, _user.UserNome);
            _usuarioValidacao.PossuiOrganizaoPatriarca(_user.UserGuidOrganizacaoPatriarca);

            _validation.IsRascunhoDespachoOfUser(rascunhoDespacho);
            _validation.IsFilled(rascunhoDespachoModel);
            _validation.IsValid(rascunhoDespachoModel);
            MapAlteracaoDespacho(rascunhoDespachoModel, rascunhoDespacho);

            FillOrganizacaoDestino(rascunhoDespacho);
            FillUnidadeDestino(rascunhoDespacho);
            FillUserAndDataHora(rascunhoDespacho);

            _unitOfWork.Save();

        }

        private void FillOrganizacaoDestino(RascunhoDespacho rascunhoDespacho)
        {
            if (rascunhoDespacho.GuidOrganizacaoDestino.HasValue)
            {
                Organizacao organizacao = _organizacaoService.Search(rascunhoDespacho.GuidOrganizacaoDestino.Value).ResponseObject;
                rascunhoDespacho.NomeOrganizacaoDestino = organizacao.RazaoSocial;
                rascunhoDespacho.SiglaOrganizacaoDestino = organizacao.Sigla;
            }
            else
            {
                rascunhoDespacho.NomeOrganizacaoDestino = null;
                rascunhoDespacho.SiglaOrganizacaoDestino = null;
            }
        }

        private void FillUnidadeDestino(RascunhoDespacho rascunhoDespacho)
        {
            if (rascunhoDespacho.GuidUnidadeDestino.HasValue)
            {
                Unidade unidade = _unidadeService.Search(rascunhoDespacho.GuidUnidadeDestino.Value).ResponseObject;
                rascunhoDespacho.NomeUnidadeDestino = unidade.Nome;
                rascunhoDespacho.SiglaUnidadeDestino = unidade.Sigla;
            }
            else
            {
                rascunhoDespacho.NomeUnidadeDestino = null;
                rascunhoDespacho.SiglaUnidadeDestino = null;
            }
        }

        private void FillUserAndDataHora(RascunhoDespacho rascunhoDespacho)
        {
            rascunhoDespacho.IdUsuario = _user.UserCpf;
            rascunhoDespacho.NomeUsuario = _user.UserNome;
            rascunhoDespacho.DataHora = DateTime.Now;
        }

        private void FillOrganizacaoProcesso(RascunhoDespacho rascunhoDespacho)
        {
            rascunhoDespacho.IdOrganizacaoProcesso = _repositorioOrganizacoesProcesso
                                                   .Where(o => o.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca))
                                                   .Single().Id;
        }

        private void FillOrganizacao(RascunhoDespacho rascunhoDespacho)
        {
            rascunhoDespacho.GuidOrganizacao = _user.UserGuidOrganizacao;
        }

        private void MapAlteracaoDespacho(RascunhoDespachoModel rascunhoDespachoModel, RascunhoDespacho rascunhoDespacho)
        {
            rascunhoDespacho.Texto = rascunhoDespachoModel.Texto;
            rascunhoDespacho.GuidOrganizacaoDestino = !string.IsNullOrWhiteSpace(rascunhoDespachoModel.GuidOrganizacaoDestino) ? (Guid?)new Guid(rascunhoDespachoModel.GuidOrganizacaoDestino) : null;
            rascunhoDespacho.GuidUnidadeDestino = !string.IsNullOrWhiteSpace(rascunhoDespachoModel.GuidUnidadeDestino) ? (Guid?)new Guid(rascunhoDespachoModel.GuidUnidadeDestino) : null;
        }
    }
}
