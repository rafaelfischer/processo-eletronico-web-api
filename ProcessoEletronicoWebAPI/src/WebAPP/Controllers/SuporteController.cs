using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace WebAPP.Controllers
{
    public class SuporteController : BaseController
    {        
        private IOrganogramaAppService _organogramaService;
        private IProcessoService _processoService;

        public SuporteController(
            IOrganogramaAppService municipioService,
            IProcessoService processoService)
        {
            _organogramaService = municipioService;
            _processoService = processoService;
        }

        [HttpGet]        
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
        public IActionResult GetMunicipiosPorUF(string uf)
        {
            IEnumerable<MunicipioViewModel> municipios = _organogramaService.GetMunicipios(uf);
            return Json(municipios);
        }

        [HttpGet]        
        public IActionResult GetTiposDocumentais(int idAtividade)
        {
            IEnumerable<TipoDocumentalViewModel> tiposDocumentais = _processoService.GetTiposDocumentais(idAtividade);
            return Json(tiposDocumentais);
        }

        [HttpGet]        
        public IActionResult GetOrganizacoesPorPatriarca()
        {
            IEnumerable<OrganizacaoViewModel> organizacoes = _organogramaService.GetOrganizacoesPorPatriarca();
            return Json(organizacoes);
        }

        [HttpGet]
        public IActionResult GetUnidadesPorOrganizacao(string guidOrganizacao)
        {
            IEnumerable<UnidadeViewModel> unidades = _organogramaService.GetUniadesPorOrganizacao(guidOrganizacao);
            return Json(unidades);
        }
    }    
}
