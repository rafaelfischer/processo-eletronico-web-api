using AutoMapper;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Mapeamentos
{
    public class EmailMapper : Profile
    {
        public EmailMapper()
        {
            CreateMap<EmailModeloNegocio, GetEmailDto>();
            CreateMap<PostEmailDto, EmailModeloNegocio>();
            CreateMap<EmailModeloNegocio, PatchContatoDto>().ReverseMap();
        }
    }
}
