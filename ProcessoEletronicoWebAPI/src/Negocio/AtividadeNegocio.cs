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

namespace ProcessoEletronicoService.Negocio
{
    public class AtividadeNegocio : BaseNegocio, IAtividadeNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<Atividade> repositorioAtividades;

        public AtividadeNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioAtividades = repositorios.Atividades;
        }

        public List<AtividadeModeloNegocio> Pesquisar(int idFuncao)
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
    }
}
