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
using ProcessoEletronicoService.Negocio.Comum.Base;

namespace ProcessoEletronicoService.Negocio
{
    public class AtividadeNegocio : IAtividadeNegocio
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ICurrentUserProvider _user;
        private IRepositorioGenerico<Atividade> _repositorioAtividades;
        private AtividadeValidacao _validacao;

        public AtividadeNegocio(IProcessoEletronicoRepositorios repositorios, IMapper mapper, ICurrentUserProvider user)
        {
            _unitOfWork = repositorios.UnitOfWork;
            _mapper = mapper;
            _user = user;
            _repositorioAtividades = repositorios.Atividades;
            _validacao = new AtividadeValidacao(repositorios);
        }

        public AtividadeModeloNegocio Pesquisar(int id)
        {
            Atividade atividade = _repositorioAtividades.Where(a => a.Id == id).Include(a => a.Funcao).ThenInclude(f => f.PlanoClassificacao).SingleOrDefault();
            _validacao.NaoEncontrado(atividade);

            return _mapper.Map<AtividadeModeloNegocio>(atividade);
        }

        public List<AtividadeModeloNegocio> PesquisarPorFuncao(int idFuncao)
        {
            var atividades = _repositorioAtividades.Where(f => f.Funcao.Id == idFuncao)
                                            .Include(pc => pc.Funcao)
                                            .ToList();

            return _mapper.Map<List<AtividadeModeloNegocio>>(atividades);
        }

        public List<AtividadeModeloNegocio> Pesquisar()
        {
            var atividades = _repositorioAtividades.Where(a => a.Funcao.PlanoClassificacao.OrganizacaoProcesso.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca)
                                                                    && (a.Funcao.PlanoClassificacao.GuidOrganizacao.Equals(_user.UserGuidOrganizacao)
                                                                    || !a.Funcao.PlanoClassificacao.AreaFim))
                                                           .Include(a => a.Funcao).ThenInclude(f => f.PlanoClassificacao).ThenInclude(pc => pc.OrganizacaoProcesso)
                                                           .ToList();

            return _mapper.Map<List<AtividadeModeloNegocio>>(atividades);
        }

        public AtividadeModeloNegocio Inserir (AtividadeModeloNegocio atividadeModeloNegocio)
        {
            _validacao.Preenchido(atividadeModeloNegocio);
            _validacao.Valido(atividadeModeloNegocio, _user.UserGuidOrganizacao);

            Atividade atividade = new Atividade();
            _mapper.Map(atividadeModeloNegocio, atividade);
            _repositorioAtividades.Add(atividade);
            _unitOfWork.Save();

            return Pesquisar(atividade.Id);
        }

        public void Excluir(int id)
        {
            _validacao.PossivelExcluir(id, _user.UserGuidOrganizacao);
            Atividade atividade = _repositorioAtividades.Where(a => a.Id == id).Single();
            _repositorioAtividades.Remove(atividade);
            _unitOfWork.Save();
        }
    }
}
