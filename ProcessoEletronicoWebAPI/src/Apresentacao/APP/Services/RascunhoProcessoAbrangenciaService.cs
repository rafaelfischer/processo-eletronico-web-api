using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using AutoMapper;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services
{
    public class RascunhoProcessoAbrangenciaService : MensagemService, IRascunhoProcessoAbrangenciaService
    {
        private IMapper _mapper;
        private ICurrentUserProvider _user;        
        private IMunicipioNegocio _municipioNegocio;
        

        public RascunhoProcessoAbrangenciaService(
            IMapper mapper,
            ICurrentUserProvider user,
            IRascunhoProcessoNegocio rascunho,
            IMunicipioNegocio municipioNegocio
            )
        {
            _mapper = mapper;
            _user = user;            
            _municipioNegocio = municipioNegocio;
        }

        public IEnumerable<MunicipioViewModel> GetMunicipiosPorIdRascunho(int idRascunho)
        {
            try
            {
                IEnumerable<MunicipioProcessoModeloNegocio> municipios = _municipioNegocio.Get(idRascunho);
                IEnumerable<MunicipioViewModel> municipiosViewModel = _mapper.Map<List<MunicipioViewModel>>(municipios);

                return municipiosViewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MunicipioViewModel PostMunicipioPorIdRascunho(int idRascunho, MunicipioViewModel municipioViewModel)
        {
            try
            {   
                MunicipioProcessoModeloNegocio municipioNegocio = _mapper.Map<MunicipioProcessoModeloNegocio>(municipioViewModel);
                return _mapper.Map<MunicipioViewModel>(_municipioNegocio.Post(idRascunho, municipioNegocio));                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResultViewModel<List<MunicipioViewModel>> UpdateMunicipioPorIdRascunho(int idRascunho, List<string> municipios)
        {
            ResultViewModel<List<MunicipioViewModel>> result = new ResultViewModel<List<MunicipioViewModel>>();
            try
            {
                result.Entidade = _mapper.Map<List<MunicipioViewModel>>(_municipioNegocio.PostCollection(idRascunho, municipios));
                SetMensagemSucesso(result.Mensagens, "Abrangência atualizada com sucesso.");
            }
            catch (Exception e)
            {
                SetMensagemErro(result.Mensagens, e);
            }

            return result;
        }

        public ResultViewModel<List<MunicipioViewModel>> DeleteAllMunicipio(int idRascunho)
        {
            ResultViewModel<List<MunicipioViewModel>> result = new ResultViewModel<List<MunicipioViewModel>>();

            try
            {
                _municipioNegocio.DeleteAll(idRascunho);                
                SetMensagemSucesso(result.Mensagens, "Abrangência atualizada com sucesso.");
            }
            catch (Exception e)
            {
                SetMensagemErro(result.Mensagens, e);
            }

            return result;
        }
    }
}
