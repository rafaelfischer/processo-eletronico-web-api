using AutoMapper;
using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ProcessoEletronicoService.Negocio
{
    public class DestinacaoFinalNegocio : BaseNegocio, IDestinacaoFinalNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<DestinacaoFinal> repositorioDestinacoesFinais;


        public DestinacaoFinalNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioDestinacoesFinais = repositorios.DestinacoesFinais;
        }

        public List<DestinacaoFinalModeloNegocio> Listar()
        {
            List<DestinacaoFinal> destinacoesFinais = repositorioDestinacoesFinais.OrderBy(df => df.Descricao).ToList();

            return Mapper.Map<List<DestinacaoFinal>, List<DestinacaoFinalModeloNegocio>>(destinacoesFinais);
        }
    }
}
