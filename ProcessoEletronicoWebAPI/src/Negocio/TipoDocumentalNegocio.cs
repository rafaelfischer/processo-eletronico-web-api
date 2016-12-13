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
    public class TipoDocumentalNegocio : ITipoDocumentalNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<TipoDocumental> repositorioTiposDocumentais;


        public TipoDocumentalNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioTiposDocumentais = repositorios.TiposDocumentais;
        }

        public List<TipoDocumentalModeloNegocio> Pesquisar(int idAtividade)
        {
            var tiposDocumentais = repositorioTiposDocumentais.Where(td => td.IdAtividade == idAtividade)
                                                              .Include(td => td.Atividade)
                                                              .ToList();

            return Mapper.Map<List<TipoDocumental>, List<TipoDocumentalModeloNegocio>>(tiposDocumentais);
        }
    }
}
