﻿using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Infraestrutura.Repositorios;
using ProcessoEletronicoService.Infraestrutura.Mapeamento;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;
using ProcessoEletronicoService.Negocio.Modelos;
using AutoMapper;

namespace ProcessoEletronicoService.Negocio.Restrito
{
    public class ProcessoNegocio : IProcessoNegocio
    {
        IUnitOfWork unitOfWork;
        IRepositorioGenerico<Processo> repositorioProcessos;
        IRepositorioGenerico<Despacho> repositorioDespachos;


        public ProcessoNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioProcessos = repositorios.Processos;
            repositorioDespachos = repositorios.Despachos;
        }

        public void Listar()
        {
            throw new NotImplementedException();
        }

        public void Pesquisar(int id)
        {
            throw new NotImplementedException();
        }

        public void Pesquisar(string numeroProcesso)
        {
            throw new NotImplementedException();
        }

        public List<ProcessoModeloNegocio> Pesquisar(int idOrganizacaoProcesso, int idUnidade)
        {
            var processosSemDespachoNaUnidade = repositorioProcessos.Where(p => p.IdOrganizacaoProcesso == idOrganizacaoProcesso
                                                                             && p.IdUnidadeAutuadora == idUnidade
                                                                             && !p.Despacho.Any())
                                                                    .Include(p => p.OrganizacaoProcesso)
                                                                    .Include(p => p.Atividade);

            var ultimosDespachosDosProcessos = repositorioDespachos.Where(d => d.Processo.IdOrganizacaoProcesso == idOrganizacaoProcesso)
                                                                   .GroupBy(d => d.IdProcesso)
                                                                   .Select(d => new { IdProcesso = d.Key, DataHoraDespacho = d.Max(gbd => gbd.DataHoraDespacho) });

            var idsUltimosDespachosParaUnidade = repositorioDespachos.Where(d => d.Processo.IdOrganizacaoProcesso == idOrganizacaoProcesso
                                                                         && d.IdUnidadeDestino == idUnidade)
                                                                     .Join(ultimosDespachosDosProcessos,
                                                                            d => d.IdProcesso,
                                                                            ud => ud.IdProcesso,
                                                                            (d, ud) => new { Despacho = d, DespachoProcesso = ud })
                                                                     .Where(d => d.Despacho.DataHoraDespacho == d.DespachoProcesso.DataHoraDespacho)
                                                                     .Select(d => d.Despacho.Id);

            var processosDespachadosParaUnidade = repositorioProcessos.Where(p => p.IdOrganizacaoProcesso == idOrganizacaoProcesso
                                                                               && p.Despacho.Any(d => idsUltimosDespachosParaUnidade.Contains(d.Id)))
                                                                      .Include(p => p.OrganizacaoProcesso)
                                                                      .Include(p => p.Atividade);

            var processosNaUnidade = processosDespachadosParaUnidade.Union(processosSemDespachoNaUnidade.Include(p => p.OrganizacaoProcesso)
                                                                                                        .Include(p => p.Atividade))
                                                                  .OrderBy(p => p.Sequencial)
                                                                  .ThenBy(p => p.Ano)
                                                                  .ThenBy(p => p.DigitoPoder)
                                                                  .ThenBy(p => p.DigitoEsfera)
                                                                  .ThenBy(p => p.DigitoOrganizacao)
                                                                  .Include(p => p.OrganizacaoProcesso)
                                                                  .Include(p => p.Atividade)
                                                                  .ToList();

            var r = Mapper.Map<List<Processo>, List<ProcessoModeloNegocio>>(processosNaUnidade);
            return r;
        }

        public void Autuar()
        {
            throw new NotImplementedException();
        }

        public void Despachar()
        {
            throw new NotImplementedException();
        }

        public void Excluir()
        {
            throw new NotImplementedException();
        }
    }
}
