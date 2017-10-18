using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProcessoEletronicoService.Negocio.Validacao;
using ProcessoEletronicoService.Negocio.Comum.Base;
using Prodest.ProcessoEletronico.Integration.Organograma.Base;
using Prodest.ProcessoEletronico.Integration.Organograma.Models;

namespace ProcessoEletronicoService.Negocio
{
    public class PlanoClassificacaoNegocio : IPlanoClassificacaoNegocio
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ICurrentUserProvider _user;
        private IRepositorioGenerico<PlanoClassificacao> _repositorioPlanosClassificacao;
        private IRepositorioGenerico<OrganizacaoProcesso> _repositorioOrganizacoesProcesso;
        private PlanoClassificacaoValidacao _validacao;
        private IOrganizacaoService _organizacaoService;

        public PlanoClassificacaoNegocio(IProcessoEletronicoRepositorios repositorios, IMapper mapper, IOrganizacaoService organizacaoService, ICurrentUserProvider user)
        {
            _unitOfWork = repositorios.UnitOfWork;
            _mapper = mapper;
            _user = user;
            _repositorioPlanosClassificacao = repositorios.PlanosClassificacao;
            _repositorioOrganizacoesProcesso = repositorios.OrganizacoesProcesso;
            _validacao = new PlanoClassificacaoValidacao(repositorios);
            _organizacaoService = organizacaoService;
        }
        
        public List<PlanoClassificacaoModeloNegocio> Pesquisar(string guidOrganizacao)
        {
            _validacao.GuidValido(guidOrganizacao);

            Guid gOrganizacao = new Guid(guidOrganizacao);
            Organizacao organizacaoPatriarca = _organizacaoService.SearchPatriarca(gOrganizacao).ResponseObject;

            _validacao.OrganizacaoPatriarcaExistente(organizacaoPatriarca);
            _validacao.GuidValido(organizacaoPatriarca.Guid);

            var planosClassificacao = _repositorioPlanosClassificacao.Where(pc => pc.OrganizacaoProcesso.GuidOrganizacao.Equals(new Guid(organizacaoPatriarca.Guid))
                                                                              && (pc.GuidOrganizacao.Equals(gOrganizacao)
                                                                              ||  !pc.AreaFim))
                                                                    .Include(pc => pc.OrganizacaoProcesso)
                                                                    .ToList();

            return _mapper.Map<List<PlanoClassificacaoModeloNegocio>>(planosClassificacao);
        }

        public PlanoClassificacaoModeloNegocio Pesquisar(int id)
        {
            PlanoClassificacao planoClassificacao = _repositorioPlanosClassificacao.Where(pc => pc.Id == id).Include(op => op.OrganizacaoProcesso).SingleOrDefault();

            _validacao.NaoEncontrado(planoClassificacao);

            return _mapper.Map<PlanoClassificacaoModeloNegocio>(planoClassificacao);
        }

        public List<PlanoClassificacaoModeloNegocio> Pesquisar()
        {
            var planosClassificacao = _repositorioPlanosClassificacao.Where(pc => pc.GuidOrganizacao.Equals(_user.UserGuidOrganizacao))
                                                                    .Include(pc => pc.OrganizacaoProcesso)
                                                                    .ToList();

            return _mapper.Map<List<PlanoClassificacaoModeloNegocio>>(planosClassificacao);
        }

        public PlanoClassificacaoModeloNegocio Inserir(PlanoClassificacaoModeloNegocio planoClassificacaoModeloNegocio)
        {
            _validacao.Preenchido(planoClassificacaoModeloNegocio);
            _validacao.Valido(planoClassificacaoModeloNegocio);
            _validacao.Permissao(planoClassificacaoModeloNegocio, _user.UserGuidOrganizacao);
            InformacoesPadrao(planoClassificacaoModeloNegocio);

            PlanoClassificacao planoClassificacao = new PlanoClassificacao();
            _mapper.Map(planoClassificacaoModeloNegocio, planoClassificacao);

            _repositorioPlanosClassificacao.Add(planoClassificacao);
            _unitOfWork.Save(); 

            return Pesquisar(planoClassificacao.Id);
        }

        private void InformacoesPadrao(PlanoClassificacaoModeloNegocio planoClassificacao)
        {
            planoClassificacao.OrganizacaoProcesso = new OrganizacaoProcessoModeloNegocio { Id = _repositorioOrganizacoesProcesso.Where(op => op.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca)).Single().Id };
        }

        public void Excluir(int id)
        {
            _validacao.PossivelExcluir(id);
            PlanoClassificacao planoClassificacao = _repositorioPlanosClassificacao.Where(pc => pc.Id == id).Single();
            _repositorioPlanosClassificacao.Remove(planoClassificacao);
            _unitOfWork.Save();       
        }
    }
}
