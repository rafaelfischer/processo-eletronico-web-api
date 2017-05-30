using AutoMapper;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.WebAPI.Mapeamentos
{
    public class EmailMapper : Profile
    {
        public EmailMapper()
        {
            CreateMap<EmailModelo, EmailModeloNegocio>();
        }

    }
}
