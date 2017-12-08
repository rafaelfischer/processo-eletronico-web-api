using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPP.Controllers
{
    public class RascunhoAnexoDespachoController : BaseController
    {
        private IOrganogramaAppService _organogramaService;
        private IRascunhoDespachoAppAnexoService _anexoRascunhoDespachoAppService;

        public RascunhoAnexoDespachoController(
            IOrganogramaAppService organogramaService,
            IRascunhoDespachoAppAnexoService anexoRascunhoDespachoAppService)
        {
            _organogramaService = organogramaService;
            _anexoRascunhoDespachoAppService = anexoRascunhoDespachoAppService;
        }

        [HttpGet]
        public IActionResult Search(int idRascunhoDespacho)
        {
            ResultViewModel<ICollection<AnexoRascunhoDespachoViewModel>> result = _anexoRascunhoDespachoAppService.Search(idRascunhoDespacho);
            ListaAnexosRascunhoDespacho listaAnexosRascunho = new ListaAnexosRascunhoDespacho() { IdRascunhoDespacho = idRascunhoDespacho, Anexos = result.Entidade };

            SetMensagens(result.Mensagens);            

            return PartialView("ListaAnexosRascunhoDespacho", listaAnexosRascunho);
        }

        public IActionResult Add(int idRascunhoDespacho, AnexoRascunhoDespachoViewModel anexoRascunhoDespacho)
        {
            ResultViewModel<AnexoRascunhoDespachoViewModel> result = _anexoRascunhoDespachoAppService.Add(idRascunhoDespacho, anexoRascunhoDespacho);
            SetMensagens(result.Mensagens);

            return View("Update", result.Entidade);
        }

        [HttpGet]
        public IActionResult Update(int idRascunhoDespacho, int id)
        {
            ResultViewModel<AnexoRascunhoDespachoViewModel> result = _anexoRascunhoDespachoAppService.Search(idRascunhoDespacho, id);

            SetMensagens(result.Mensagens);

            return View("Update", result.Entidade);
        }

        [HttpPost]
        public IActionResult Update(int idRascunhoDespacho, AnexoRascunhoDespachoViewModel rascunhoDespacho)
        {
            int id = rascunhoDespacho.Id;

            ResultViewModel<AnexoRascunhoDespachoViewModel> result = _anexoRascunhoDespachoAppService.Update(idRascunhoDespacho, id, rascunhoDespacho);
            SetMensagens(result.Mensagens);

            return Search(idRascunhoDespacho);
        }

        public IActionResult Delete(int idRascunhoDespacho, int id)
        {
            ResultViewModel<AnexoRascunhoDespachoViewModel> result = _anexoRascunhoDespachoAppService.Delete(idRascunhoDespacho, id);
            SetMensagens(result.Mensagens);

            return Search(idRascunhoDespacho);
        }

        [HttpPost]
        public async Task<IActionResult> UploadAnexo(IList<IFormFile> files, int idRascunhoDespacho, string descricaoAnexos, int? idTipoDocumental)
        {
            long totalBytes = files.Sum(f => f.Length);

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);
                        var fileBytes = ms.ToArray();
                        string s = Convert.ToBase64String(fileBytes);
                        AnexoRascunhoDespachoViewModel anexo = new AnexoRascunhoDespachoViewModel()
                        {
                            ConteudoString = s,
                            Nome = file.FileName,
                            MimeType = file.ContentType,
                            Descricao = descricaoAnexos,
                            TipoDocumental = idTipoDocumental.HasValue ? new TipoDocumentalViewModel { Id = idTipoDocumental.Value } : null
                        };
                        _anexoRascunhoDespachoAppService.Add(idRascunhoDespacho, anexo);
                    }
                }
            }

            return Search(idRascunhoDespacho);
        }

        [HttpPost]
        public IActionResult EditList(int idRascunhoDespacho)
        {
            ResultViewModel<ICollection<AnexoRascunhoDespachoViewModel>> result = _anexoRascunhoDespachoAppService.Search(idRascunhoDespacho);
            return PartialView("RascunhoAnexoDespachoEditarLista", new ListaAnexosRascunhoDespacho { IdRascunhoDespacho = idRascunhoDespacho, Anexos = result.Entidade });
        }

        [HttpPost]
        public IActionResult UpdateList(int idRascunhoDespacho, List<AnexoRascunhoDespachoViewModel> anexos)
        {
            ResultViewModel<AnexoRascunhoDespachoViewModel> result;

            foreach (var anexo in anexos)
            {
                result = _anexoRascunhoDespachoAppService.Update(idRascunhoDespacho, anexo.Id, anexo);
                SetMensagens(result.Mensagens);
            }

            return Search(idRascunhoDespacho);

        }

        [HttpGet]
        public IActionResult DownloadAnexo(int idRascunhoDespacho, int id)
        {
            try
            {
                ResultViewModel<AnexoRascunhoDespachoViewModel> result = _anexoRascunhoDespachoAppService.Search(idRascunhoDespacho, id);
                AnexoRascunhoDespachoViewModel anexo = result.Entidade;

                byte[] fileBase64 = Convert.FromBase64String(anexo.ConteudoString);
                string file = Encoding.UTF8.GetString(fileBase64, 0, fileBase64.Length);
                var conteudo = file.Split(',');

                if (conteudo.Length == 2)
                {
                    return File(Convert.FromBase64String(conteudo[1]), "application/octet-stream", anexo.Nome);
                }
                else
                {
                    return File(Convert.FromBase64String(anexo.ConteudoString), "application/octet-stream", anexo.Nome);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
