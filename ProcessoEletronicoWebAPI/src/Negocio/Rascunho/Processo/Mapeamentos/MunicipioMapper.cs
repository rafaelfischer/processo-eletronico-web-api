﻿using AutoMapper;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Mapeamentos
{
    public class MunicipioMapper : Profile
    {
        public MunicipioMapper()
        {
            CreateMap<MunicipioProcessoModeloNegocio, MunicipioRascunhoProcesso>().ReverseMap();
        }
    }
}
