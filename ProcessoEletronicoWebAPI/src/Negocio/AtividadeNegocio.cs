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
using ProcessoEletronicoService.Negocio.Comum;

namespace ProcessoEletronicoService.Negocio
{
    public class AtividadeNegocio : BaseNegocio, IAtividadeNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<Atividade> repositorioAtividades;
        private AtividadeValidacao atividadeValidacao;

        public AtividadeNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioAtividades = repositorios.Atividades;
            atividadeValidacao = new AtividadeValidacao(repositorios);
        }

        public AtividadeModeloNegocio Pesquisar(int id)
        {
            Atividade atividade = repositorioAtividades.Where(a => a.Id == id).Include(a => a.Funcao).ThenInclude(f => f.PlanoClassificacao).SingleOrDefault();
            atividadeValidacao.NaoEncontrado(atividade);

            return Mapper.Map<Atividade, AtividadeModeloNegocio>(atividade);
        }

        public List<AtividadeModeloNegocio> PesquisarPorFuncao(int idFuncao)
        {
            var atividades = repositorioAtividades.Where(f => f.Funcao.Id == idFuncao)
                                            .Include(pc => pc.Funcao)
                                            .ToList();

            return Mapper.Map<List<Atividade>, List<AtividadeModeloNegocio>>(atividades);
        }

        public List<AtividadeModeloNegocio> Pesquisar()
        {
            var atividades = repositorioAtividades.Where(a => a.Funcao.PlanoClassificacao.OrganizacaoProcesso.GuidOrganizacao.Equals(UsuarioGuidOrganizacaoPatriarca)
                                                                    && (a.Funcao.PlanoClassificacao.GuidOrganizacao.Equals(UsuarioGuidOrganizacao)
                                                                    || !a.Funcao.PlanoClassificacao.AreaFim))
                                                           .Include(a => a.Funcao).ThenInclude(f => f.PlanoClassificacao).ThenInclude(pc => pc.OrganizacaoProcesso)
                                                           .ToList();

            return Mapper.Map<List<Atividade>, List<AtividadeModeloNegocio>>(atividades);
        }

        public AtividadeModeloNegocio Inserir (AtividadeModeloNegocio atividadeModeloNegocio)
        {
            atividadeValidacao.Preenchido(atividadeModeloNegocio);
            atividadeValidacao.Valido(atividadeModeloNegocio, UsuarioGuidOrganizacao);

            Atividade atividade = new Atividade();
            Mapper.Map(atividadeModeloNegocio, atividade);
            repositorioAtividades.Add(atividade);
            unitOfWork.Save();

            return Pesquisar(atividade.Id);
            
        }

        public void Excluir(int id)
        {
            atividadeValidacao.PossivelExcluir(id, UsuarioGuidOrganizacao);
            Atividade atividade = repositorioAtividades.Where(a => a.Id == id).Single();

            repositorioAtividades.Remove(atividade);
            unitOfWork.Save();
        }
    }
}
