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
        private IRascunhoProcessoAbrangenciaService _municipioService;        
        private IOrganogramaAppService _organogramaService;        

        public RascunhoAbrangenciaController(            
            IRascunhoProcessoAbrangenciaService municipio,
            IOrganogramaAppService organogramaService
            )
        {            
            _municipioService = municipio;            
            _organogramaService = organogramaService;            
        }

        [HttpPost]
        public IActionResult EditarMunicipio(RascunhoProcessoViewModel rascunho)
        {
            List<string> municipios = new List<string>();

            if (rascunho.MunicipiosRascunhoProcesso != null)
            {
                foreach (var municipio in rascunho.MunicipiosRascunhoProcesso)
                {
                    municipios.Add(municipio.GuidMunicipio);
                }

                return PartialView("RascunhoMunicipioLista", _municipioService.UpdateMunicipioPorIdRascunho(rascunho.Id, municipios));                
            }
            else
            {
                _municipioService.DeleteAllMunicipio(rascunho.Id);
                return PartialView("RascunhoMunicipioLista", _municipioService.GetMunicipiosPorIdRascunho(rascunho.Id));
            }

            
        }
    }
}
