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

namespace Negocio.RascunhosDespacho
{
    public class RascunhoDespachoCore : IRascunhoDespachoCore
    {
        private ICurrentUserProvider _user;
        private IMapper _mapper;
        private IOrganizacaoService _organizacaoService;
        private IRascunhoDespachoValidation _validation;
        private IUnidadeService _unidadeService;
        private IRepositorioGenerico<OrganizacaoProcesso> _repositorioOrganizacoesProcesso;
        private IRepositorioGenerico<RascunhoDespacho> _repositorioRascunhosDespacho;
        private IUnitOfWork _unitOfWork;

        private AnexoValidacao _anexoValidacao;

        public RascunhoDespachoCore(ICurrentUserProvider user, IMapper mapper, IOrganizacaoService organizacaoService, IRascunhoDespachoValidation validation, IUnidadeService unidadeService, IProcessoEletronicoRepositorios repositorios, AnexoValidacao anexoValidacao)
        {
            _user = user;
            _mapper = mapper;
            _organizacaoService = organizacaoService;
            _validation = validation;
            _unidadeService = unidadeService;
            _repositorioOrganizacoesProcesso = repositorios.OrganizacoesProcesso;
            _repositorioRascunhosDespacho = repositorios.RascunhosDespacho;
            _unitOfWork = repositorios.UnitOfWork;

            _anexoValidacao = anexoValidacao;
        }

        public RascunhoDespachoModel Search(int id)
        {
            RascunhoDespacho rascunhoDespacho = _repositorioRascunhosDespacho.Where(rascunho => rascunho.Id == id).SingleOrDefault();
            return _mapper.Map<RascunhoDespachoModel>(rascunhoDespacho);
        }

        public IEnumerable<RascunhoDespachoModel> SearchByOrganizacao(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RascunhoDespachoModel> SearchByUsuario(int id)
        {
            throw new NotImplementedException();
        }

        public RascunhoDespachoModel Add(RascunhoDespachoModel rascunhoDespachoModel)
        {
            _validation.IsFilled(rascunhoDespachoModel);
            _validation.IsValid(rascunhoDespachoModel);

            FillOrganizacaoDestino(rascunhoDespachoModel);
            FillUnidadeDestino(rascunhoDespachoModel);
            FillUserAndDataHora(rascunhoDespachoModel);
            FillOrganizacao(rascunhoDespachoModel);
            
            RascunhoDespacho rascunhoDespacho = new RascunhoDespacho();
            _mapper.Map(rascunhoDespachoModel, rascunhoDespacho);
            _repositorioRascunhosDespacho.Add(rascunhoDespacho);
            _unitOfWork.Save();

            return Search(rascunhoDespacho.Id);
        }
        
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        
        public void Update(int id, RascunhoDespachoModel rascunhoDespacho)
        {
            throw new NotImplementedException();
        }

        private void FillOrganizacaoDestino(RascunhoDespachoModel rascunhoDespachoModel)
        {
            if (!string.IsNullOrWhiteSpace(rascunhoDespachoModel.GuidOrganizacaoDestino))
            {
                Organizacao organizacao = _organizacaoService.Search(new Guid(rascunhoDespachoModel.GuidOrganizacaoDestino)).ResponseObject;
                rascunhoDespachoModel.NomeOrganizacaoDestino = organizacao.RazaoSocial;
                rascunhoDespachoModel.SiglaOrganizacaoDestino = organizacao.Sigla;
            }
        }

        private void FillUnidadeDestino(RascunhoDespachoModel rascunhoDespachoModel)
        {
            if (!string.IsNullOrWhiteSpace(rascunhoDespachoModel.GuidUnidadeDestino))
            {
                Unidade unidade = _unidadeService.Search(new Guid(rascunhoDespachoModel.GuidUnidadeDestino)).ResponseObject;
                rascunhoDespachoModel.NomeUnidadeDestino = unidade.Nome;
                rascunhoDespachoModel.SiglaUnidadeDestino = unidade.Sigla;
            }
        }

        private void FillUserAndDataHora(RascunhoDespachoModel rascunhoDespachoModel)
        {
            rascunhoDespachoModel.IdUsuario = _user.UserCpf;
            rascunhoDespachoModel.NomeUsuario = _user.UserNome;
            rascunhoDespachoModel.DataHora = DateTime.Now;
        }

        private void FillOrganizacao(RascunhoDespachoModel rascunhoDespachoModel)
        {
            rascunhoDespachoModel.IdOrganizacaoProcesso = _repositorioOrganizacoesProcesso
                                                   .Where(o => o.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca))
                                                   .Single().Id;
        }
    }
}
