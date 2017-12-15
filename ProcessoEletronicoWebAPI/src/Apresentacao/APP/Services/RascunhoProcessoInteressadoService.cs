using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using AutoMapper;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
using Prodest.ProcessoEletronico.Integration.Organograma.Base;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Apresentacao.APP.Services
{
    public class RascunhoProcessoInteressadoService: MensagemService, IRascunhoProcessoInteressadoService
    {
        private IMapper _mapper;
        private ICurrentUserProvider _user;
        private IInteressadoPessoaJuridicaNegocio _interessadoPessoaJuridica;
        private IInteressadoPessoaFisicaNegocio _interessadoPessoaFisica;
        private IMunicipioService _municipioService;

        public RascunhoProcessoInteressadoService(
            IMapper mapper, 
            ICurrentUserProvider user,
            IInteressadoPessoaJuridicaNegocio interessadoPessoaJuridica,
            IInteressadoPessoaFisicaNegocio interessadoPessoaFisica,
            IMunicipioService municipioService
            )
        {
            _mapper = mapper;
            _user = user;
            _interessadoPessoaJuridica = interessadoPessoaJuridica;
            _interessadoPessoaFisica = interessadoPessoaFisica;
            _municipioService = municipioService;
        }

        public ResultViewModel<InteressadoPessoaFisicaViewModel> PostInteressadoPF(int idRascunho, InteressadoPessoaFisicaViewModel interessado)
        {
            ResultViewModel<InteressadoPessoaFisicaViewModel> result = new ResultViewModel<InteressadoPessoaFisicaViewModel>();

            try
            {
                InteressadoPessoaFisicaModeloNegocio interessadoNegocio = _interessadoPessoaFisica.Post(idRascunho, _mapper.Map<InteressadoPessoaFisicaModeloNegocio>(interessado));
                result.Entidade = _mapper.Map<InteressadoPessoaFisicaViewModel>(interessadoNegocio);
                SetMensagemSucesso(result.Mensagens, "Interessado pessoa física salvo com sucesso.");
            }
            catch (Exception e)
            {
                SetMensagemErro(result.Mensagens, e);
            }

            return result;
        }        

        public ResultViewModel<InteressadoPessoaJuridicaViewModel> PostInteressadoPJ(int idRascunho, InteressadoPessoaJuridicaViewModel interessado)
        {
            ResultViewModel<InteressadoPessoaJuridicaViewModel> result = new ResultViewModel<InteressadoPessoaJuridicaViewModel>();

            try
            {
                InteressadoPessoaJuridicaModeloNegocio interessadoNegocio = _interessadoPessoaJuridica.Post(idRascunho, _mapper.Map<InteressadoPessoaJuridicaModeloNegocio>(interessado));
                result.Entidade = _mapper.Map<InteressadoPessoaJuridicaViewModel>(interessadoNegocio);
                SetMensagemSucesso(result.Mensagens, "Interessado pessoa jurídica salvo com sucesso.");
            }
            catch (Exception e)
            {
                SetMensagemErro(result.Mensagens, e);
            }

            return result;
        }

        public InteressadoPessoaJuridicaViewModel PostInteressadoPJOrganograma(int idRascunho, OrganizacaoViewModel organizacaoInteressada)
        {
            InteressadoPessoaJuridicaModeloNegocio interessadoNegocio = _interessadoPessoaJuridica.Post(idRascunho, _mapper.Map<InteressadoPessoaJuridicaModeloNegocio>(organizacaoInteressada));
            return _mapper.Map<InteressadoPessoaJuridicaViewModel>(interessadoNegocio);
        }

        public InteressadoPessoaJuridicaViewModel PostInteressadoPJOrganograma(int idRascunho, OrganizacaoViewModel organizacaoInteressada, UnidadeViewModel unidadeInteressada)
        {
            InteressadoPessoaJuridicaModeloNegocio interessadoNegocio = _mapper.Map<InteressadoPessoaJuridicaModeloNegocio>(organizacaoInteressada);
            interessadoNegocio.NomeUnidade = unidadeInteressada.Nome;
            interessadoNegocio.SiglaUnidade = unidadeInteressada.Sigla;
            return _mapper.Map<InteressadoPessoaJuridicaViewModel>(_interessadoPessoaJuridica.Post(idRascunho,interessadoNegocio));
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

        public InteressadoPessoaJuridicaViewModel GetInteressadoPJ(int idRascunho, int idInteressadoPJ)
        {
            try
            {
                InteressadoPessoaJuridicaViewModel interessadoPJ = _mapper.Map<InteressadoPessoaJuridicaViewModel>(_interessadoPessoaJuridica.Get(idRascunho, idInteressadoPJ));

                if (!string.IsNullOrEmpty(interessadoPJ.GuidMunicipio))
                {
                    interessadoPJ.Municipios = _mapper.Map<List<MunicipioViewModel>>(_municipioService.SearchByEstado(interessadoPJ.UfMunicipio).ResponseObject);                    
                }

                return interessadoPJ;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public InteressadoPessoaFisicaViewModel GetInteressadoPF(int idRascunho, int idInteressadoPJ)
        {
            try
            {
                InteressadoPessoaFisicaViewModel interessadoPF = _mapper.Map<InteressadoPessoaFisicaViewModel>(_interessadoPessoaFisica.Get(idRascunho, idInteressadoPJ));

                if (!string.IsNullOrEmpty(interessadoPF.GuidMunicipio))
                {
                    interessadoPF.Municipios = _mapper.Map<List<MunicipioViewModel>>(_municipioService.SearchByEstado(interessadoPF.UfMunicipio).ResponseObject);
                }

                return interessadoPF;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ResultViewModel<InteressadoPessoaJuridicaViewModel> ExcluirInteressadoPJ(int idRascunho, int idInteressadoPJ)
        {
            ResultViewModel<InteressadoPessoaJuridicaViewModel> result = new ResultViewModel<InteressadoPessoaJuridicaViewModel>();

            try
            {
                _interessadoPessoaJuridica.Delete(idRascunho, idInteressadoPJ);
                SetMensagemSucesso(result.Mensagens, "Interessado pessoa jurídica excluído com sucesso.");
            }
            catch (Exception e)
            {
                SetMensagemErro(result.Mensagens, e);
            }

            return result;
        }

        public ResultViewModel<InteressadoPessoaFisicaViewModel> ExcluirInteressadoPF(int idRascunho, int idInteressadoPF)
        {
            ResultViewModel<InteressadoPessoaFisicaViewModel> result = new ResultViewModel<InteressadoPessoaFisicaViewModel>();

            try
            {
                _interessadoPessoaFisica.Delete(idRascunho, idInteressadoPF);
                SetMensagemSucesso(result.Mensagens, "Interessado pessoa física excluído com sucesso.");
            }
            catch (Exception e)
            {
                SetMensagemErro(result.Mensagens, e);
            }

            return result;            
        }
    }
}
