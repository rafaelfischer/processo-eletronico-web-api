using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Restrito.Validacao;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ProcessoEletronicoService.Negocio.Restrito
{
    public class SinalizacaoNegocio : ISinalizacaoNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<Sinalizacao> repositorioSinalizacoes;

        public SinalizacaoNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioSinalizacoes = repositorios.Sinalizacoes;
        }

        public List<SinalizacaoModeloNegocio> Pesquisar(int idOrganizacaoPatriarca)
        {
            var sinalizacoes = repositorioSinalizacoes.Where(s => s.OrganizacaoProcesso.IdOrganizacao == idOrganizacaoPatriarca)
                                                      .Include(pc => pc.OrganizacaoProcesso)
                                                      .ToList();

            return Mapper.Map<List<Sinalizacao>, List<SinalizacaoModeloNegocio>>(sinalizacoes);
        }
    }
}
