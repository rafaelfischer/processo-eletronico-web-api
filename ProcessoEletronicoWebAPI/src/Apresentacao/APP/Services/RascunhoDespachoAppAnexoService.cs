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
    public class RascunhoDespachoAppAnexoService: MensagemService, IRascunhoDespachoAppAnexoService
    {
        private IMapper _mapper;
        private ICurrentUserProvider _user;
        private IAnexoRascunhoDespachoCore _anexoRascunhoDespacho;

        public RascunhoDespachoAppAnexoService(IMapper mapper, ICurrentUserProvider user, IAnexoRascunhoDespachoCore anexoRascunhoDespacho)
        {
            _mapper = mapper;
            _user = user;
            _anexoRascunhoDespacho = anexoRascunhoDespacho;
        }

        public ResultViewModel<AnexoRascunhoDespachoViewModel> Search(int idRascunhoDespacho, int id)
        {
            ResultViewModel<AnexoRascunhoDespachoViewModel> result = new ResultViewModel<AnexoRascunhoDespachoViewModel>();

            try
            {
                result.Entidade = _mapper.Map<AnexoRascunhoDespachoViewModel>(_anexoRascunhoDespacho.Search(idRascunhoDespacho, id));
            }
            catch (RecursoNaoEncontradoException e)
            {
                SetMensagemErro(result.Mensagens, e);
            }

            return result;
        }

        public ResultViewModel<ICollection<AnexoRascunhoDespachoViewModel>> Search(int idRascunhoDespacho)
        {
            ResultViewModel<ICollection<AnexoRascunhoDespachoViewModel>> result = new ResultViewModel<ICollection<AnexoRascunhoDespachoViewModel>>();

            try
            {
                result.Entidade = _mapper.Map<List<AnexoRascunhoDespachoViewModel>>(_anexoRascunhoDespacho.Search(idRascunhoDespacho));
            }
            catch (RecursoNaoEncontradoException e)
            {
                SetMensagemErro(result.Mensagens, e);
            }

            return result;
        }

        public ResultViewModel<AnexoRascunhoDespachoViewModel> Add(int idRascunhoDespacho, AnexoRascunhoDespachoViewModel anexoRascunhoDespacho)
        {
            ResultViewModel<AnexoRascunhoDespachoViewModel> result = new ResultViewModel<AnexoRascunhoDespachoViewModel>();

            try
            {
                result.Entidade = _mapper.Map<AnexoRascunhoDespachoViewModel>(_anexoRascunhoDespacho.Add(idRascunhoDespacho, _mapper.Map<AnexoRascunhoDespachoModel>(anexoRascunhoDespacho)));
            }
            catch (Exception e)
            {
                SetMensagemErro(result.Mensagens, e);
            }

            return result;
        }

        public ResultViewModel<AnexoRascunhoDespachoViewModel> Update(int idRascunhoDespacho, int id, AnexoRascunhoDespachoViewModel anexoRascunhoDespacho)
        {
            ResultViewModel<AnexoRascunhoDespachoViewModel> result = new ResultViewModel<AnexoRascunhoDespachoViewModel>();

            try
            {
                _anexoRascunhoDespacho.Update(idRascunhoDespacho, id, _mapper.Map<AnexoRascunhoDespachoModel>(anexoRascunhoDespacho));
                result.Entidade = _mapper.Map<AnexoRascunhoDespachoViewModel>(_anexoRascunhoDespacho.Search(id));

                SetMensagemSucesso(result.Mensagens, "Anexo de rascunho de despacho atualizado com sucesso.");
            }
            catch (Exception e)
            {
                SetMensagemErro(result.Mensagens, e);
            }

            return result;
        }

        public ResultViewModel<AnexoRascunhoDespachoViewModel> Delete(int idRascunhoDespacho, int id)
        {
            ResultViewModel<AnexoRascunhoDespachoViewModel> result = new ResultViewModel<AnexoRascunhoDespachoViewModel>();

            try
            {
                _anexoRascunhoDespacho.Delete(idRascunhoDespacho, id);
                SetMensagemSucesso(result.Mensagens, "Anexo de rascunho de despacho excluído com sucesso.");
            }
            catch (Exception e)
            {
                SetMensagemErro(result.Mensagens, e);
            }

            return result;
        }
    }
}
