using System;
using Nest;
using Elasticsearch.Net;
using ProcessoEletronicoService.Infraestrutura.Repositorios.Base;
using ProcessoEletronicoService.Infraestrutura.Repositorios;
using Newtonsoft.Json;


namespace ProcessoEletronicoService.Infraestrutura.Repositorios.Consulta
{
    public class ConsultaProcessoRepositorio : IConsultaProcessoRepositorio
    {
        public string ConsultarProcessoPorNumero(string numeroProcesso)
        {
            return "Consultar Processo Por Numero - Elastic Search";
            
        }
    }
}
