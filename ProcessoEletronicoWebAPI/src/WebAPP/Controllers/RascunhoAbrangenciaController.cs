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
    public class RascunhoAbrangenciaController : BaseController
    {   
        private IRascunhoProcessoMunicipioService _municipioService;        
        private IOrganogramaAppService _organogramaService;        

        public RascunhoAbrangenciaController(            
            IRascunhoProcessoMunicipioService municipio,
            IOrganogramaAppService organogramaService
            )
        {            
            _municipioService = municipio;            
            _organogramaService = organogramaService;            
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditarMunicipio(RascunhoProcessoViewModel rascunho)
        {
            if (rascunho.MunicipiosRascunhoProcesso != null)
            {
                foreach (var municipio in rascunho.MunicipiosRascunhoProcesso)
                {
                    _municipioService.PostMunicipioPorIdRascunho(rascunho.Id, municipio);
                }
            }

            return PartialView("RascunhoMunicipioLista", _municipioService.GetMunicipiosPorIdRascunho(rascunho.Id));
        }
    }
}
