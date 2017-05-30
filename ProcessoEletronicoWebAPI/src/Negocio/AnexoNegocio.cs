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
using ProcessoEletronicoService.Negocio.Comum;

namespace ProcessoEletronicoService.Negocio
{
    public class AnexoNegocio : BaseNegocio, IAnexoNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<Anexo> repositorioAnexos;
        private AnexoValidacao anexoValidacao;
        private IMapper _mapper;

        public AnexoNegocio(IProcessoEletronicoRepositorios repositorios, IMapper mapper)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioAnexos = repositorios.Anexos;
            _mapper = mapper;
            anexoValidacao = new AnexoValidacao(repositorios);
        }

        public AnexoModeloNegocio Pesquisar(int id)
        {
            Anexo anexo = repositorioAnexos.Where(a => a.Id == id)
                                           .Include(td => td.TipoDocumental)
                                           .Include(p => p.Processo)
                                           .SingleOrDefault();

            anexoValidacao.NaoEncontrado(anexo);

            return _mapper.Map<AnexoModeloNegocio>(anexo);
        }
    }
}
