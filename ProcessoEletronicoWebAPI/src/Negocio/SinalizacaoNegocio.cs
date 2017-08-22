using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Restrito.Validacao;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProcessoEletronicoService.Negocio.Validacao;
using System;

namespace ProcessoEletronicoService.Negocio
{
    public class SinalizacaoNegocio : ISinalizacaoNegocio
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IRepositorioGenerico<Sinalizacao> _repositorioSinalizacoes;
        private SinalizacaoValidacao _validacao;

        public SinalizacaoNegocio(IProcessoEletronicoRepositorios repositorios, IMapper mapper)
        {
            _unitOfWork = repositorios.UnitOfWork;
            _mapper = mapper;
            _repositorioSinalizacoes = repositorios.Sinalizacoes;
            _validacao = new SinalizacaoValidacao(repositorios);
        }

        public List<SinalizacaoModeloNegocio> Pesquisar(string guidOrganizacaoPatriarca)
        {
            _validacao.GuidValido(guidOrganizacaoPatriarca);
            Guid guid = new Guid(guidOrganizacaoPatriarca);

            var sinalizacoes = _repositorioSinalizacoes.Where(s => s.OrganizacaoProcesso.GuidOrganizacao.Equals(guid))
                                                      .Include(pc => pc.OrganizacaoProcesso)
                                                      .ToList();

            return _mapper.Map<List<SinalizacaoModeloNegocio>>(sinalizacoes);
        }
    }
}