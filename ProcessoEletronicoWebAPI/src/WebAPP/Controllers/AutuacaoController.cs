using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Apresentacao.APP.Services.Base;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPP.Controllers
{
    public class AutuacaoController : BaseController
    {
        private IProcessoService _service;

        public AutuacaoController(IProcessoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {            
            return View();
        }
    }
}
