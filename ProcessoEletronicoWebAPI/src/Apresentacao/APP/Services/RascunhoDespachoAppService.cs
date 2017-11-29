using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using AutoMapper;
using Negocio.RascunhosDespacho.Base;
using Negocio.RascunhosDespacho.Models;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Comum.Base;
using System;
using System.Collections.Generic;

namespace Apresentacao.APP.Services
{
    public class RascunhoDespachoAppService: MensagemService, IRascunhoDespachoAppService
    {
        private IMapper _mapper;
        private ICurrentUserProvider _user;
        private IRascunhoDespachoCore _rascunhoDespacho;

        public RascunhoDespachoAppService(
            IMapper mapper,
            ICurrentUserProvider user,
            IRascunhoDespachoCore rascunhoDespacho
            )
        {
            _mapper = mapper;
            _user = user;
            _rascunhoDespacho = rascunhoDespacho;
        }

        public ResultViewModel<RascunhoDespachoViewModel> Search(int id)
        {
            ResultViewModel<RascunhoDespachoViewModel> result = new ResultViewModel<RascunhoDespachoViewModel>();

            try
            {
                result.Entidade = _mapper.Map<RascunhoDespachoViewModel>(_rascunhoDespacho.Search(id));
            }
            catch (RecursoNaoEncontradoException e)
            {
                SetMensagemErro(result.Mensagens, e);
            }

            return result;
        }

        public ResultViewModel<ICollection<RascunhoDespachoViewModel>> Search()
        {
            ResultViewModel<ICollection<RascunhoDespachoViewModel>> result = new ResultViewModel<ICollection<RascunhoDespachoViewModel>>();

            try
            {
                result.Entidade = _mapper.Map<List<RascunhoDespachoViewModel>>(_rascunhoDespacho.SearchByUsuario());
            }
            catch (RecursoNaoEncontradoException e)
            {
                SetMensagemErro(result.Mensagens, e);
            }

            return result;
        }

        public ResultViewModel<RascunhoDespachoViewModel> Add(RascunhoDespachoViewModel rascunhoDespacho)
        {
            ResultViewModel<RascunhoDespachoViewModel> result = new ResultViewModel<RascunhoDespachoViewModel>();

            try
            {
                result.Entidade = _mapper.Map<RascunhoDespachoViewModel>(_rascunhoDespacho.Add(_mapper.Map<RascunhoDespachoModel>(rascunhoDespacho)));
            }
            catch (Exception e)
            {
                SetMensagemErro(result.Mensagens, e);
            }

            return result;
        }

        public ResultViewModel<RascunhoDespachoViewModel> Update(RascunhoDespachoViewModel rascunhoDespacho)
        {
            ResultViewModel<RascunhoDespachoViewModel> result = new ResultViewModel<RascunhoDespachoViewModel>();

            try
            {
                _rascunhoDespacho.Update(rascunhoDespacho.Id, _mapper.Map<RascunhoDespachoModel>(rascunhoDespacho));
                result.Entidade = _mapper.Map<RascunhoDespachoViewModel>(_rascunhoDespacho.Search(rascunhoDespacho.Id));

                SetMensagemSucesso(result.Mensagens, "Rascunho de despacho atualizado com sucesso.");
            }
            catch (Exception e)
            {
                SetMensagemErro(result.Mensagens, e);
            }

            return result;
        }

        public ResultViewModel<RascunhoDespachoViewModel> Delete(int id)
        {
            ResultViewModel<RascunhoDespachoViewModel> result = new ResultViewModel<RascunhoDespachoViewModel>();

            try
            {
                _rascunhoDespacho.Delete(id);
                SetMensagemSucesso(result.Mensagens, "Rascunho de despacho excluído com sucesso.");
            }
            catch (Exception e)
            {
                SetMensagemErro(result.Mensagens, e);
            }

            return result;
        }

    }
}
