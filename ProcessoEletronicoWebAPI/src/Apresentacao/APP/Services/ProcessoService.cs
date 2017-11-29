using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using Apresentacao.APP.WorkServices.Base;
using AutoMapper;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
using System;
using System.Collections.Generic;

namespace Apresentacao.APP.WorkServices
{
    public class ProcessoService : MensagemService, IProcessoService
    {
        private IMapper _mapper;
        private IProcessoNegocio _negocio;
        private ICurrentUserProvider _user;
        private IRascunhoProcessoNegocio _rascunho;
        private ITipoDocumentalNegocio _tipoDocumental;

        public ProcessoService(
            IMapper mapper,
            IProcessoNegocio negocio,
            ICurrentUserProvider user,
            IRascunhoProcessoNegocio rascunho,
            ITipoDocumentalNegocio tipoDocumental)
        {
            _mapper = mapper;
            _negocio = negocio;
            _user = user;
            _rascunho = rascunho;
            _tipoDocumental = tipoDocumental;
        }

        public GetProcessoBasicoViewModel GetProcessoPorNumero(string numero)
        {
            try
            {
                ProcessoModeloNegocio processoModeloNegocio = _negocio.Pesquisar(numero);
                GetProcessoBasicoViewModel GetProcessoBasicoViewModel = _mapper.Map<GetProcessoBasicoViewModel>(processoModeloNegocio);
                return GetProcessoBasicoViewModel;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public ResultViewModel<GetProcessoViewModel> Search(int id)
        {
            ResultViewModel<GetProcessoViewModel> getProcessoResult = new ResultViewModel<GetProcessoViewModel>();

            try
            {
                ProcessoModeloNegocio processoModeloNegocio = _negocio.Pesquisar(id);
                getProcessoResult.Entidade = _mapper.Map<GetProcessoViewModel>(processoModeloNegocio);
                
            }
            catch (RecursoNaoEncontradoException e)
            {
                SetMensagemErro(getProcessoResult.Mensagens, e);
            }

            return getProcessoResult;

        }

        public ICollection<TipoDocumentalViewModel> GetTiposDocumentais(int idAtividade)
        {
            try
            {
                ICollection<TipoDocumentalModeloNegocio> tiposDocumentaisNegocio = _tipoDocumental.PesquisarPorAtividade(idAtividade);
                ICollection<TipoDocumentalViewModel> tiposDocumentaisViewModel = _mapper.Map<List<TipoDocumentalViewModel>>(tiposDocumentaisNegocio);
                return tiposDocumentaisViewModel;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public ICollection<GetProcessoBasicoViewModel> GetProcessosOrganizacao()
        {
            try
            {
                ICollection<ProcessoModeloNegocio> processos = _negocio.PesquisarProcessosNaOrganizacao(_user.UserGuidOrganizacao.ToString());
                ICollection<GetProcessoBasicoViewModel> getProcessosViewModel = _mapper.Map<List<GetProcessoBasicoViewModel>>(processos);
                return getProcessosViewModel;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ResultViewModel<GetProcessoViewModel> AutuarPorIdRascunho(int idRascunho)
        {
            ResultViewModel<GetProcessoViewModel> result = new ResultViewModel<GetProcessoViewModel>();

            try
            {
                ProcessoModeloNegocio processoModeloNegocio = _negocio.Post(idRascunho);
                result.Entidade = _mapper.Map<GetProcessoViewModel>(processoModeloNegocio);
                SetMensagemSucesso(result.Mensagens, "Processo autuado com sucesso.");
            }
            catch(Exception e)
            {
                SetMensagemErro(result.Mensagens, e);
            }

            return result;
        }
    }
}
