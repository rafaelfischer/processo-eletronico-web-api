﻿using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPP.Controllers
{
    public class RascunhoController : BaseController
    {   
        private IRascunhoProcessoService _service;
        private IRascunhoProcessoMunicipioService _municipioService;
        private IRascunhoProcessoSinalizacaoService _sinalizacaoService;
        private IRascunhoProcessoAnexoService _anexoService;
        private IOrganogramaAppService _organogramaService;

        public RascunhoController(
            IRascunhoProcessoService service, 
            IRascunhoProcessoMunicipioService municipio, 
            IRascunhoProcessoSinalizacaoService sinalizacao,
            IRascunhoProcessoAnexoService anexoService,
            IOrganogramaAppService organogramaService)
        {
            _service = service;
            _municipioService = municipio;
            _sinalizacaoService = sinalizacao;
            _anexoService = anexoService;
            _organogramaService = organogramaService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<RascunhoProcessoViewModel> rascunhosPorOrganizacao = _service.GetRascunhosProcessoPorOrganizacao();
            return View("RascunhosPorOrganizacao", rascunhosPorOrganizacao);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Editar(int? id)
        {
            RascunhoProcessoViewModel rascunho = _service.EditRascunhoProcesso(id);
            return View("Editar", rascunho);
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditarBasico(RascunhoProcessoViewModel rascunho)
        {
            _service.UpdateRascunhoProcesso(rascunho.Id, rascunho);
            _sinalizacaoService.PostSinalizacao(rascunho.Id, rascunho.Sinalizacoes);

            RascunhoProcessoViewModel rascunhoAtualizado = _service.EditRascunhoProcesso(rascunho.Id);
            return PartialView("RascunhoBasico", rascunhoAtualizado);
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

        [HttpGet]
        [Authorize]
        public IActionResult Excluir(int id)
        {
            _service.DeleteRascunhoProcesso(id);

            return RedirectToAction("Index");

            //IEnumerable<GetRascunhoProcessoViewModel> rascunhosPorOrganizacao = _service.GetRascunhosOrganizacao();
            //return PartialView("RascunhosPorOrganizacao", rascunhosPorOrganizacao);
        }

        [HttpPost]
        public async Task<IActionResult> UploadAnexo(IList<IFormFile> files, int idRascunho)
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
                        AnexoViewModel anexo = new AnexoViewModel() {
                            ConteudoString = s,
                            Nome = file.FileName,
                            MimeType = file.ContentType
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
        
        [HttpPost]
        [Authorize]
        public IActionResult FormInteressado(int tipoInteressado)
        {
            switch (tipoInteressado)
            {
                case 1:
                    return PartialView("RascunhoInteressadoOrgaos", GetOrganizacoesPorPatriarca());                    
                case 2:
                    return PartialView("RascunhoInteressadoPJ");
                case 3:
                    return PartialView("RascunhoInteressadoPF");
                default:
                    return Content("Informe um tipo válido de interessado."); 
            }            
        }

        [HttpPost]
        [Authorize]
        public IActionResult GetUnidadesPorOrganizacao(string guidOrganizacao)
        {
            IEnumerable<UnidadeViewModel> unidades = _organogramaService.GetUniadesPorOrganizacao(guidOrganizacao);
            return PartialView("RascunhoInteressadoOrgaoUnidades", unidades);
        }

        [NonAction]        
        private IEnumerable<OrganizacaoViewModel>  GetOrganizacoesPorPatriarca()
        {
            IEnumerable<OrganizacaoViewModel> organiozacoes = _organogramaService.GetOrganizacoesPorPatriarca();
            return organiozacoes;
        }

        [HttpPost]
        [Authorize]
        public IActionResult IncluirInteressado(string guidOrganizacao, string guidUnidade)
        {
            if (string.IsNullOrEmpty(guidUnidade))
            {
                var organizacao = _organogramaService.GetOrganizacao(guidOrganizacao);
                return Json(organizacao);
            }
            else
            {
                var unidade = _organogramaService.GetUnidade(guidUnidade);
                return Json(unidade);
            }            
        }        

        [HttpPost]
        [Authorize]
        public IActionResult FormInteressadoPF()
        {
            return PartialView("RascunhoInteressadoPF");
        }
    }
}
