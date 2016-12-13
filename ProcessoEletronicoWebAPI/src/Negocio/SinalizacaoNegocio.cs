using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Restrito.Validacao;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProcessoEletronicoService.Negocio.Validacao;
using System;

namespace ProcessoEletronicoService.Negocio.Restrito
{
    public class SinalizacaoNegocio : ISinalizacaoNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<Sinalizacao> repositorioSinalizacoes;
        private SinalizacaoValidacao Validacao;

        public SinalizacaoNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioSinalizacoes = repositorios.Sinalizacoes;
            Validacao = new SinalizacaoValidacao(repositorios);
        }

        public List<SinalizacaoModeloNegocio> Pesquisar(string guidOrganizacaoPatriarca)
        {
            Validacao.GuidValido(guidOrganizacaoPatriarca);

            var sinalizacoes = repositorioSinalizacoes.Where(s => s.OrganizacaoProcesso.GuidOrganizacao.Equals(new Guid(guidOrganizacaoPatriarca)))
                                                      .Include(pc => pc.OrganizacaoProcesso)
                                                      .ToList();

            return Mapper.Map<List<Sinalizacao>, List<SinalizacaoModeloNegocio>>(sinalizacoes);
        }
    }
}
