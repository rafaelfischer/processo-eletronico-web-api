using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Restrito.Validacao;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProcessoEletronicoService.Negocio.Validacao;

namespace ProcessoEletronicoService.Negocio
{
    public class FuncaoNegocio : BaseNegocio, IFuncaoNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<Funcao> repositorioFuncoes;
        private FuncaoValidacao funcaoValidacao;

        public FuncaoNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            funcaoValidacao = new FuncaoValidacao(repositorios);
            unitOfWork = repositorios.UnitOfWork;
            repositorioFuncoes = repositorios.Funcoes;
        }

        public FuncaoModeloNegocio Pesquisar (int id)
        {
            Funcao funcao = repositorioFuncoes.Where(f => f.Id == id).Include(p => p.PlanoClassificacao).SingleOrDefault();

            funcaoValidacao.NaoEncontrado(funcao);

            return Mapper.Map<Funcao, FuncaoModeloNegocio>(funcao);
        }

        public List<FuncaoModeloNegocio> PesquisarPorPlanoClassificacao(int idPlanoClassificacao)
        {
            var funcoes = repositorioFuncoes.Where(f => f.PlanoClassificacao.Id == idPlanoClassificacao)
                                            .Include(pc => pc.PlanoClassificacao)
                                            .ToList();

            return Mapper.Map<List<Funcao>, List<FuncaoModeloNegocio>>(funcoes);
        }

        public FuncaoModeloNegocio Inserir(FuncaoModeloNegocio funcaoModeloNegocio)
        {
            funcaoValidacao.Preenchido(funcaoModeloNegocio);
            funcaoValidacao.Valido(funcaoModeloNegocio, UsuarioGuidOrganizacao);

            Funcao funcao = new Funcao();
            Mapper.Map(funcaoModeloNegocio, funcao);

            repositorioFuncoes.Add(funcao);
            unitOfWork.Save();

            return Pesquisar(funcao.Id);

        }

        public void Excluir (int id)
        {
            funcaoValidacao.PossivelExcluir(id, UsuarioGuidOrganizacao);
            Funcao funcao = repositorioFuncoes.Where(f => f.Id == id).Single();
            repositorioFuncoes.Remove(funcao);
            unitOfWork.Save();
        }
        
    }
}
