using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Apresentacao.APP.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Apresentacao.APP.ViewModels;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPP.Controllers
{
    public class SuporteController : Controller
    {        
        private IOrganogramaAppService _organogramaService;

        public SuporteController(IOrganogramaAppService municipioService)
        {
            _organogramaService = municipioService;            
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUF()
        {
            string ufs = string.Empty;
            FileStream fileStream = new FileStream("Json/uf.json", FileMode.Open);

            using (StreamReader reader = new StreamReader(fileStream))
            {
                ufs = reader.ReadToEnd();
            }

            return Json(JsonConvert.DeserializeObject<List<UfViewModel>>(ufs));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetMunicipiosPorUF(string uf)
        {
            IEnumerable<MunicipioViewModel> municipios = _organogramaService.GetMunicipios(uf);
            return Json(municipios);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetOrganizacoesPorPatriarca()
        {
            IEnumerable<OrganizacaoViewModel> organizacoes = _organogramaService.GetOrganizacoesPorPatriarca();
            return Json(organizacoes);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUnidadesPorOrganizacao(string guidOrganizacao)
        {
            IEnumerable<UnidadeViewModel> unidades = _organogramaService.GetUniadesPorOrganizacao(guidOrganizacao);
            return Json(unidades);
        }
    }    
}
