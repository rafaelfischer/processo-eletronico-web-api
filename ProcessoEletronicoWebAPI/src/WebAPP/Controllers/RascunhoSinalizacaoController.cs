using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Apresentacao.APP.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Apresentacao.APP.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPP.Controllers
{
    public class RascunhoSinalizacaoController : BaseController
    {   
        private IRascunhoProcessoSinalizacaoService _sinalizacaoService;      

        public RascunhoSinalizacaoController(            
            IRascunhoProcessoSinalizacaoService sinalizacao
            )
        {   
            _sinalizacaoService = sinalizacao;            
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditarSinalizacoes(RascunhoProcessoViewModel rascunho)
        {
            if (rascunho.Sinalizacoes != null && rascunho.Sinalizacoes.Count > 0)
                _sinalizacaoService.PostSinalizacao(rascunho.Id, rascunho.Sinalizacoes);

            RascunhoProcessoViewModel rascunhoAtualizado = new RascunhoProcessoViewModel { Sinalizacoes = _sinalizacaoService.GetSinalizacoes(rascunho.Id) };
            return PartialView("RascunhoSinalizacoes", rascunhoAtualizado);
        }
    }
}
