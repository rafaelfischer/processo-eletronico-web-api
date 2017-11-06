using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Apresentacao.APP.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Apresentacao.APP.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPP.Controllers
{
    public class RascunhoAnexoController : BaseController
    {   
        private IRascunhoProcessoAnexoService _anexoService;     

        public RascunhoAnexoController(            
            IRascunhoProcessoAnexoService anexoService)
        {   
            _anexoService = anexoService;            
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditarAnexo(RascunhoProcessoViewModel rascunho)
        {
            long size = rascunho.files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in rascunho.files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        formFile.CopyToAsync(stream);
                    }
                }
            }

            return PartialView("RascunhoAnexoLista", _anexoService.GetAnexos(rascunho.Id));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UploadAnexo(IList<IFormFile> files, int idRascunho, int idTipoDocumental)
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
                        AnexoViewModel anexo = new AnexoViewModel()
                        {
                            ConteudoString = s,
                            Nome = file.FileName,
                            MimeType = file.ContentType,
                            TipoDocumental = new TipoDocumentalViewModel { Id = idTipoDocumental }
                        };
                        _anexoService.PostAnexo(idRascunho, anexo);
                    }
                }
            }

            return PartialView("RascunhoAnexoLista", _anexoService.GetAnexos(idRascunho));
        }

        [HttpPost]
        [Authorize]
        public IActionResult ExcluirAnexo(int idRascunho, int idAnexo)
        {
            _anexoService.DeleteAnexo(idRascunho, idAnexo);

            return PartialView("RascunhoAnexoLista", _anexoService.GetAnexos(idRascunho));
        }
    }
}
