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
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IRepositorioGenerico<TipoContato> _repositorioTiposContato;

        public TipoContatoNegocio(IProcessoEletronicoRepositorios repositorios, IMapper mapper)
        {
            _unitOfWork = repositorios.UnitOfWork;
            _mapper = mapper;
            _repositorioTiposContato = repositorios.TiposContato;
        }

        public List<TipoContatoModeloNegocio> Listar()
        {
            List<TipoContato> tiposContato = _repositorioTiposContato.ToList();

            return _mapper.Map<List<TipoContatoModeloNegocio>>(tiposContato);
        }
    }
}
