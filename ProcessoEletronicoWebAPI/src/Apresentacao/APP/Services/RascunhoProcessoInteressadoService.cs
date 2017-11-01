using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using AutoMapper;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Apresentacao.APP.Services
{
    public class RascunhoProcessoInteressadoService: IRascunhoProcessoInteressadoService
    {
        private IMapper _mapper;
        private ICurrentUserProvider _user;
        private IInteressadoPessoaJuridicaNegocio _interessadoPessoaJuridica;
        private IInteressadoPessoaFisicaNegocio _interessadoPessoaFisica;
        //private IEmailNegocio _emailNegocio;
        //private IContatoNegocio _contatoNegocio;

        public RascunhoProcessoInteressadoService(
            IMapper mapper, 
            ICurrentUserProvider user,
            IInteressadoPessoaJuridicaNegocio interessadoPessoaJuridica,
            IInteressadoPessoaFisicaNegocio interessadoPessoaFisica
            //IEmailNegocio emailNegocio,
            //IContatoNegocio contatoNegocio
            )
        {
            _mapper = mapper;
            _user = user;
            _interessadoPessoaJuridica = interessadoPessoaJuridica;
            _interessadoPessoaFisica = interessadoPessoaFisica;
            //_emailNegocio = emailNegocio;
            //_contatoNegocio = contatoNegocio;
        }

        public InteressadoPessoaFisicaViewModel PostInteressadoPF(int idRascunho, InteressadoPessoaFisicaViewModel interessado)
        {
            InteressadoPessoaFisicaModeloNegocio interessadoNegocio = _interessadoPessoaFisica.Post(idRascunho, _mapper.Map<InteressadoPessoaFisicaModeloNegocio>(interessado));
            return _mapper.Map<InteressadoPessoaFisicaViewModel>(interessadoNegocio);
        }

        public InteressadoPessoaJuridicaViewModel PostInteressadoPJ(int idRascunho, OrganizacaoViewModel organizacaoInteressada)
        {
            InteressadoPessoaJuridicaModeloNegocio interessadoNegocio = _interessadoPessoaJuridica.Post(idRascunho, _mapper.Map<InteressadoPessoaJuridicaModeloNegocio>(organizacaoInteressada));            
            return _mapper.Map<InteressadoPessoaJuridicaViewModel>(interessadoNegocio);
        }

        public List<InteressadoPessoaFisicaViewModel> GetInteressadosPF(int idRascunho)
        {
            try
            {
                List<InteressadoPessoaFisicaViewModel> interessadosPF = _mapper.Map<List<InteressadoPessoaFisicaViewModel>>(_interessadoPessoaFisica.Get(idRascunho));
                return interessadosPF;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        public List<InteressadoPessoaJuridicaViewModel> GetInteressadosPJ(int idRascunho)
        {
            try
            {
                List<InteressadoPessoaJuridicaViewModel> interessadosPJ = _mapper.Map<List<InteressadoPessoaJuridicaViewModel>>(_interessadoPessoaJuridica.Get(idRascunho));
                return interessadosPJ;
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}
