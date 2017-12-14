using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPP.Controllers
{
    public class SinalizacaoController : BaseController
    {
        private ISinalizacaoService _service;
        private IMapper _mapper;
        public SinalizacaoController(ISinalizacaoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetSinalizacoes()
        {
            ICollection<SinalizacaoViewModel> sinalizacoesViewModel = _mapper.Map<ICollection<SinalizacaoViewModel>>(_service.Search().Entidade);
            return View("Sinalizacao", sinalizacoesViewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("AddSinalizacao");
        }

        [HttpGet]
        public IActionResult Update(int Id)
        {
            ResultViewModel<SinalizacaoViewModel> resultViewModel = _service.Search(Id);
            return PartialView("UpdateSinalizacao", resultViewModel.Entidade);
        }


        [HttpPost]
        [Authorize(Policy = "Sinalizacao.Edit")]
        public IActionResult Add(SinalizacaoViewModel sinalizacaoForm)
        {
            ResultViewModel<SinalizacaoViewModel> resultViewModel = new ResultViewModel<SinalizacaoViewModel>();
            resultViewModel = _service.Add(sinalizacaoForm);
            SetMensagens(resultViewModel.Mensagens);

            ICollection<SinalizacaoViewModel> sinalizacoesViewModel = _mapper.Map<ICollection<SinalizacaoViewModel>>(_service.Search().Entidade);
            return PartialView("ListSinalizacoes", sinalizacoesViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "Sinalizacao.Edit")]
        public IActionResult Update(SinalizacaoViewModel sinalizacaoForm)
        {
            ResultViewModel<SinalizacaoViewModel> resultViewModel = new ResultViewModel<SinalizacaoViewModel>();
            resultViewModel = _service.Update(sinalizacaoForm);
            SetMensagens(resultViewModel.Mensagens);
            
            if(resultViewModel.Entidade == null)
            {
                return PartialView("ItemSinalizacao" ,_service.Search(sinalizacaoForm.Id).Entidade);
            }

            return PartialView("ItemSinalizacao", resultViewModel.Entidade);
        }

        [HttpDelete]
        [Authorize(Policy = "Sinalizacao.Edit")]
        public IActionResult Delete(int id)
        {
            ICollection<MensagemViewModel> mensagens = _service.Delete(id);
            SetMensagens(mensagens);
            ICollection<SinalizacaoViewModel> sinalizacoesViewModel = _mapper.Map<ICollection<SinalizacaoViewModel>>(_service.Search().Entidade);
            return PartialView("ListSinalizacoes", sinalizacoesViewModel);
        }
        
    }
}
