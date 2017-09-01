using AutoMapper;
using ProcessoEletronicoService.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.Bloqueios
{
    public class BloqueioMapping : Profile
    {
        public BloqueioMapping()
        {
            CreateMap<BloqueioModel, Bloqueio>();
            CreateMap<Bloqueio, BloqueioModel>();
        }
    }
}
