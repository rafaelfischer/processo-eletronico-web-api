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
    public class FuncaoNegocio : IFuncaoNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<Funcao> repositorioFuncoes;

        public FuncaoNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioFuncoes = repositorios.Funcoes;
        }

        public List<FuncaoModeloNegocio> Pesquisar(int idOrganizacaoPatriarca, int idPlanoClassificacao)
        {
            var funcoes = repositorioFuncoes.Where(f => f.PlanoClassificacao.OrganizacaoProcesso.IdOrganizacao == idOrganizacaoPatriarca
                                                    && f.PlanoClassificacao.Id == idPlanoClassificacao)
                                            .Include(pc => pc.PlanoClassificacao)
                                            .ToList();

            return Mapper.Map<List<Funcao>, List<FuncaoModeloNegocio>>(funcoes);
        }
    }
}
