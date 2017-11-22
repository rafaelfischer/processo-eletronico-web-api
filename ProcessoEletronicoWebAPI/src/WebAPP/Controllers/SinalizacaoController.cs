﻿using Apresentacao.APP.Services.Base;
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
            return View("Sinalizacao", sinalizacoesViewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Update(int? Id)
        {
            return PartialView("UpdateSinalizacao");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(SinalizacaoViewModel sinalizacaoForm)
        {
            _service.Add(sinalizacaoForm);
            ICollection<SinalizacaoViewModel> sinalizacoesViewModel = _mapper.Map<ICollection<SinalizacaoViewModel>>(_service.Search());
            return PartialView("ListSinalizacoes", sinalizacoesViewModel);
        }

       
    }
}
