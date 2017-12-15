using Apresentacao.APP.Services.Base;
using Apresentacao.APP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPP.Controllers
{
    public class ProcessosController : BaseController
    {
        private IProcessoService _service;
        private IProcessoAnexoService _anexoService;
        
        public ProcessosController(IProcessoService service, IProcessoAnexoService anexoService)
        {
            _service = service;
            _anexoService = anexoService;
        }

        [HttpGet]        
        public IActionResult Index()
        {   
            return View();
        }

        [HttpGet]
        public IActionResult ConsultaProcesso()
        {
            return View(null);
        }

        [HttpGet]
        public IActionResult Search(int id)
        {
            ResultViewModel<GetProcessoViewModel> getProcessoResult = _service.Search(id);

            return View("VisualizacaoProcesso", getProcessoResult.Entidade);
        }

        [HttpPost]
        public IActionResult ConsultaProcessoPorNumero(string numero)
        {
            ResultViewModel<GetProcessoViewModel> getProcesso = _service.GetProcessoPorNumero(numero);
            return View("VisualizacaoProcesso", getProcesso.Entidade);
        }        

        [HttpGet]
        public IActionResult SearchByOrganizacao()
        {
            IEnumerable<GetProcessoBasicoViewModel> processosPorOrganizacao = _service.GetProcessosOrganizacao();
            return View("ProcessosPorOrganizacao", processosPorOrganizacao);
        }

        [HttpGet]
        public IActionResult DownloadAnexo(int id)
        {
            try
            {
                AnexoViewModel anexo = _anexoService.Search(id);
                string file = Encoding.UTF8.GetString(anexo.Conteudo, 0, anexo.Conteudo.Length);
                var conteudo = file.Split(',');

                if (conteudo.Length == 2)
                {
                    return File(conteudo[1], "application/octet-stream", anexo.Nome);
                }
                else
                {
                    return File(anexo.Conteudo, "application/octet-stream", anexo.Nome);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }


    }
}
