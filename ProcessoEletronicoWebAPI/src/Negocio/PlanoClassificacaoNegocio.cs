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
    public class PlanoClassificacaoNegocio : BaseNegocio, IPlanoClassificacaoNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<PlanoClassificacao> repositorioPlanosClassificacao;
        PlanoClassificacaoValidacao Validacao;

        public PlanoClassificacaoNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioPlanosClassificacao = repositorios.PlanosClassificacao;
            Validacao = new PlanoClassificacaoValidacao();
        }

        public List<PlanoClassificacaoModeloNegocio> Pesquisar(string guidOrganizacao)
        {
            Validacao.GuidValido(guidOrganizacao);

            Guid gOrganizacao = new Guid(guidOrganizacao);

            OrganizacaoOrganogramaModelo organizacaoPatriarca = PesquisarOrganizacaoPatriarca(gOrganizacao);

            Validacao.OrganizacaoPatriarcaExistente(organizacaoPatriarca);
            Validacao.GuidValido(organizacaoPatriarca.guid);

            var planosClassificacao = repositorioPlanosClassificacao.Where(pc => pc.OrganizacaoProcesso.GuidOrganizacao.Equals(new Guid(organizacaoPatriarca.guid))
                                                                              && (pc.GuidOrganizacao.Equals(gOrganizacao)
                                                                              ||  !pc.AreaFim))
                                                                    .Include(pc => pc.OrganizacaoProcesso)
                                                                    .ToList();

            return Mapper.Map<List<PlanoClassificacao>, List<PlanoClassificacaoModeloNegocio>>(planosClassificacao);
        }
    }
}
