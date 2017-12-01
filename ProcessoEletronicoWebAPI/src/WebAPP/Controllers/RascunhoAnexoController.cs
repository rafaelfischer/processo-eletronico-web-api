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
using System.Text;

namespace WebAPP.Controllers
{
    public class RascunhoAnexoController : BaseController
    {   
        private IRascunhoProcessoAnexoService _anexoService;
        private IProcessoService _processoService;

        public RascunhoAnexoController(
            IProcessoService processoService,
            IRascunhoProcessoAnexoService anexoService)
        {
            _processoService = processoService;
            _anexoService = anexoService;            
        }

        [HttpPost]
        public IActionResult ListarAnexos(int idRascunho)
        {
            ListaAnexosRascunho listaAnexosRascunho = new ListaAnexosRascunho { IdRascunho = idRascunho, Anexos = _anexoService.GetAnexos(idRascunho) };
            return PartialView("RascunhoAnexoLista", listaAnexosRascunho);
        }

        [HttpPost]
        public IActionResult EditarAnexosForm(int idRascunho, int idAtividade)
        {
            ICollection<AnexoViewModel> anexos = _anexoService.GetAnexos(idRascunho);
            ICollection<TipoDocumentalViewModel> tiposDocumentais = _processoService.GetTiposDocumentais(idAtividade);
            return PartialView("RascunhoAnexoEditarLista", new EditarAnexosRascunho { IdRascunho = idRascunho, Anexos = anexos, TiposDocumentais = tiposDocumentais });
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditarAnexos(int idRascunho, List<AnexoViewModel> anexos)
        {
            foreach(var anexo in anexos)
            {
                _anexoService.EditarAnexo(idRascunho, anexo);
            }

            ListaAnexosRascunho listaAnexosRascunho = new ListaAnexosRascunho { IdRascunho = idRascunho, Anexos = _anexoService.GetAnexos(idRascunho) };

            return PartialView("RascunhoAnexoLista", listaAnexosRascunho);            
        }

        [HttpPost]
        public IActionResult EditarAnexoForm(int idRascunho, int idAnexo, int idAtividade)
        {
            AnexoViewModel anexo = _anexoService.GetAnexo(idRascunho,idAnexo);
            IEnumerable<TipoDocumentalViewModel> tiposDocumentais = _processoService.GetTiposDocumentais(idAtividade);
            return PartialView("RascunhoAnexoEditar", new EditarAnexoRascunho { IdRascunho = idRascunho, Anexo = anexo, TiposDocumentais = tiposDocumentais });
        }

        [HttpPost]
        public IActionResult EditarAnexo(int idRascunho, AnexoViewModel anexo)
        {
            _anexoService.EditarAnexo(idRascunho, anexo);
            return PartialView("RascunhoAnexoLista", _anexoService.GetAnexos(idRascunho));
        }

        [HttpPost]
        public async Task<IActionResult> UploadAnexo(IList<IFormFile> files, int idRascunho, int? idTipoDocumental, string descricaoAnexos)
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
                            Descricao = descricaoAnexos,
                            TipoDocumental = idTipoDocumental.HasValue ? new TipoDocumentalViewModel { Id = idTipoDocumental.Value } : null
                        };
                        _anexoService.PostAnexo(idRascunho, anexo);
                    }
                }
            }

            ListaAnexosRascunho listaAnexosRascunho = new ListaAnexosRascunho { IdRascunho = idRascunho, Anexos = _anexoService.GetAnexos(idRascunho) };

            return PartialView("RascunhoAnexoLista", listaAnexosRascunho);
        }

        [HttpPost]
        public IActionResult ExcluirAnexo(int idRascunho, int idAnexo)
        {
            _anexoService.DeleteAnexo(idRascunho, idAnexo);

            ListaAnexosRascunho listaAnexosRascunho = new ListaAnexosRascunho { IdRascunho = idRascunho, Anexos = _anexoService.GetAnexos(idRascunho) };

            return PartialView("RascunhoAnexoLista", listaAnexosRascunho);
        }


        [HttpGet]
        public IActionResult DownloadAnexo(int idRascunho, int idAnexo)
        {
            try
            {
                AnexoViewModel anexo = _anexoService.GetAnexo(idRascunho, idAnexo);

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
