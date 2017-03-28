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

namespace ProcessoEletronicoService.Negocio
{
    public class SinalizacaoRascunhoProcessoNegocio : ISinalizacaoRascunhoProcessoNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<SinalizacaoRascunhoProcesso> repositorioSinalizacoesRascunhoProcesso;

        public SinalizacaoRascunhoProcessoNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioSinalizacoesRascunhoProcesso = repositorios.SinalizacoesRascunhoProcesso;
        }

        public void Excluir(ICollection<SinalizacaoRascunhoProcesso> sinalizacoesRascunhoProcesso)
        {
            if (sinalizacoesRascunhoProcesso != null)
            {
                foreach (var sinalizacaoRascunhoProcesso in sinalizacoesRascunhoProcesso)
                {
                    Excluir(sinalizacaoRascunhoProcesso);
                }
            }
        }
        public void Excluir(SinalizacaoRascunhoProcesso sinalizacaoRascunhoProcesso)
        {
            if (sinalizacaoRascunhoProcesso != null)
            {
                repositorioSinalizacoesRascunhoProcesso.Remove(sinalizacaoRascunhoProcesso);
            }
        }

        
    }
}
