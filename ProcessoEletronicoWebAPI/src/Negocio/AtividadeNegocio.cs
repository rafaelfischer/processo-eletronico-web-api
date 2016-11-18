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
    public class AtividadeNegocio : IAtividadeNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<Atividade> repositorioAtividades;

        public AtividadeNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioAtividades = repositorios.Atividades;
        }

        public List<AtividadeModeloNegocio> Pesquisar(int idOrganizacaoPatriarca, int idFuncao)
        {
            var atividades = repositorioAtividades.Where(f => f.Funcao.PlanoClassificacao.OrganizacaoProcesso.IdOrganizacao == idOrganizacaoPatriarca
                                                      && f.Funcao.Id == idFuncao)
                                            .Include(pc => pc.Funcao)
                                            .ToList();

            return Mapper.Map<List<Atividade>, List<AtividadeModeloNegocio>>(atividades);
        }
    }
}
