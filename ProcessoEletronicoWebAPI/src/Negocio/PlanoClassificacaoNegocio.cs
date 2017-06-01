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
using ProcessoEletronicoService.Negocio.Comum.Validacao;
using static ProcessoEletronicoService.Negocio.Comum.Validacao.OrganogramaValidacao;
using ProcessoEletronicoService.Negocio.Comum.Base;

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
        private OrganogramaValidacao _organogramaValidacao;

        public PlanoClassificacaoNegocio(IProcessoEletronicoRepositorios repositorios, IMapper mapper, OrganogramaValidacao organogramaValidacao, ICurrentUserProvider user)
        {
            _unitOfWork = repositorios.UnitOfWork;
            _mapper = mapper;
            _user = user;
            _repositorioPlanosClassificacao = repositorios.PlanosClassificacao;
            _repositorioOrganizacoesProcesso = repositorios.OrganizacoesProcesso;
            _validacao = new PlanoClassificacaoValidacao(repositorios);
            _organogramaValidacao = organogramaValidacao;
        }
        
        public List<PlanoClassificacaoModeloNegocio> Pesquisar(string guidOrganizacao)
        {
            _validacao.GuidValido(guidOrganizacao);

            Guid gOrganizacao = new Guid(guidOrganizacao);

            OrganizacaoOrganogramaModelo organizacaoPatriarca = _organogramaValidacao.PesquisarOrganizacaoPatriarca(gOrganizacao);

            _validacao.OrganizacaoPatriarcaExistente(organizacaoPatriarca);
            _validacao.GuidValido(organizacaoPatriarca.guid);

            var planosClassificacao = _repositorioPlanosClassificacao.Where(pc => pc.OrganizacaoProcesso.GuidOrganizacao.Equals(new Guid(organizacaoPatriarca.guid))
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
