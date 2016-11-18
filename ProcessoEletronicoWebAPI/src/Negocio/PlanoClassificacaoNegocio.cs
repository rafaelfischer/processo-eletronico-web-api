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
    public class PlanoClassificacaoNegocio : IPlanoClassificacaoNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<PlanoClassificacao> repositorioPlanosClassificacao;

        public PlanoClassificacaoNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioPlanosClassificacao = repositorios.PlanosClassificacao;
        }

        public List<PlanoClassificacaoModeloNegocio> Pesquisar(int idOrganizacaoPatriarca, int idOrganizacao)
        {
            var planosClassificacao = repositorioPlanosClassificacao.Where(pc => pc.OrganizacaoProcesso.IdOrganizacao == idOrganizacaoPatriarca
                                                                              && (pc.IdOrganizacao == idOrganizacao
                                                                              ||  !pc.AreaFim))
                                                                    .Include(pc => pc.OrganizacaoProcesso)
                                                                    .ToList();

            return Mapper.Map<List<PlanoClassificacao>, List<PlanoClassificacaoModeloNegocio>>(planosClassificacao);
        }
    }
}
