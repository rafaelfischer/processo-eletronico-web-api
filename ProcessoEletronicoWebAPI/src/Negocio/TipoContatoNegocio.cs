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

namespace ProcessoEletronicoService.Negocio
{
    public class TipoContatoNegocio : ITipoContatoNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<TipoContato> repositorioTiposContato;

        public TipoContatoNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioTiposContato = repositorios.TiposContato;
        }

        public List<TipoContatoModeloNegocio> Listar()
        {
            List<TipoContato> tiposContato = repositorioTiposContato.ToList();

            return Mapper.Map<List<TipoContato>, List<TipoContatoModeloNegocio>>(tiposContato);
        }
    }
}
