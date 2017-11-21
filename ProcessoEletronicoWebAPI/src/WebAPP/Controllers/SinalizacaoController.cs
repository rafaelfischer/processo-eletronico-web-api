using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [Authorize]
        public IActionResult GetSinalizacoes()
        {
            ICollection<SinalizacaoViewModel> sinalizacoesViewModel = _mapper.Map<ICollection<SinalizacaoViewModel>>(_service.Search());
            return View("ListSinalizacoes", sinalizacoesViewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Update()
        {
            return View("UpdateSinalizacao");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(SinalizacaoViewModel sinalizacaoForm)
        {
            return View("UpdateSinalizacao");
        }


    }
}
