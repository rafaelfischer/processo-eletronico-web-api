using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Validacao;
using System.Collections.Generic;
using System.Linq;

namespace ProcessoEletronicoService.Negocio
{
    public class FuncaoNegocio : IFuncaoNegocio
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ICurrentUserProvider _user;
        private IRepositorioGenerico<Funcao> _repositorioFuncoes;
        private FuncaoValidacao _validacao;

        public FuncaoNegocio(IProcessoEletronicoRepositorios repositorios, IMapper mapper, ICurrentUserProvider user)
        {

            _validacao = new FuncaoValidacao(repositorios);
            _unitOfWork = repositorios.UnitOfWork;
            _mapper = mapper;
            _user = user;
            _repositorioFuncoes = repositorios.Funcoes;
        }

        public FuncaoModeloNegocio Pesquisar (int id)
        {
            Funcao funcao = _repositorioFuncoes.Where(f => f.Id == id).Include(p => p.PlanoClassificacao).SingleOrDefault();

            _validacao.NaoEncontrado(funcao);

            return _mapper.Map<FuncaoModeloNegocio>(funcao);
        }

        public List<FuncaoModeloNegocio> PesquisarPorPlanoClassificacao(int idPlanoClassificacao)
        {
            var funcoes = _repositorioFuncoes.Where(f => f.PlanoClassificacao.Id == idPlanoClassificacao)
                                            .Include(pc => pc.PlanoClassificacao)
                                            .ToList();

            return _mapper.Map<List<FuncaoModeloNegocio>>(funcoes);
        }

        public FuncaoModeloNegocio Inserir(FuncaoModeloNegocio funcaoModeloNegocio)
        {
            _validacao.Preenchido(funcaoModeloNegocio);
            _validacao.Valido(funcaoModeloNegocio, _user.UserGuidOrganizacao);

            Funcao funcao = new Funcao();
            _mapper.Map(funcaoModeloNegocio, funcao);

            _repositorioFuncoes.Add(funcao);
            _unitOfWork.Save();

            return Pesquisar(funcao.Id);

        }

        public void Excluir (int id)
        {
            _validacao.PossivelExcluir(id, _user.UserGuidOrganizacao);
            Funcao funcao = _repositorioFuncoes.Where(f => f.Id == id).Single();
            _repositorioFuncoes.Remove(funcao);
            _unitOfWork.Save();
        }
        
    }
}
