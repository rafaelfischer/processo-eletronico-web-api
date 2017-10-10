﻿using Apresentacao.APP.ViewModels;
using Apresentacao.APP.WorkServices.Base;
using AutoMapper;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
using System;
using System.Collections.Generic;

namespace Apresentacao.APP.WorkServices
{
    public class ProcessoService : IProcessoService
    {
        private IMapper _mapper;
        private IProcessoNegocio _negocio;
        private ICurrentUserProvider _user;
        private IRascunhoProcessoNegocio _rascunho;

        public ProcessoService(IMapper mapper, IProcessoNegocio negocio, ICurrentUserProvider user, IRascunhoProcessoNegocio rascunho)
        {
            _mapper = mapper;
            _negocio = negocio;
            _user = user;
            _rascunho = rascunho;
        }

        public GetProcessoViewModel GetProcessoPorNumero(string numero)
        {
            try
            {
                ProcessoModeloNegocio processoModeloNegocio = _negocio.Pesquisar(numero);
                GetProcessoViewModel getProcessoViewModel = _mapper.Map<GetProcessoViewModel>(processoModeloNegocio);
                return getProcessoViewModel;
            }
            catch (Exception e)
            {
                return null;
            }
        
        }

        public IEnumerable<GetProcessoViewModel> GetProcessosOrganizacao()
        {
            try
            {
                IEnumerable<ProcessoModeloNegocio> processos = _negocio.PesquisarProcessosNaOrganizacao(_user.UserGuidOrganizacao.ToString());
                IEnumerable<GetProcessoViewModel> getProcessosViewModel = _mapper.Map<List<GetProcessoViewModel>>(processos);
                return getProcessosViewModel;
            }
            catch (Exception e)
            {
                return null;
            }            
        }
    }
}
