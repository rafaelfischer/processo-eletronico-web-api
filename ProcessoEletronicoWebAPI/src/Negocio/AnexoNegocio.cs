using ProcessoEletronicoService.Negocio.Base;
using System.Linq;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProcessoEletronicoService.Negocio.Validacao;

namespace ProcessoEletronicoService.Negocio
{
    public class AnexoNegocio : IAnexoNegocio
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
