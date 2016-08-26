using Nest;
using ProcessoEletronicoService.Infraestrutura.Repositorios.Base;
using ProcessoEletronicoService.Infraestrutura.Repositorios.Modelos;
using System;
using System.Linq;
using System.Collections.Generic;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;

namespace ProcessoEletronicoService.Infraestrutura.Repositorios.Consulta
{
    public class ConsultaProcessoRepositorio : IConsultaProcessoRepositorio
    {

        public ProcessoRepositorio ConsultarProcessoPorNumero(string numeroProcesso)
        {
            List<ProcessoRepositorio> resultados = new List<ProcessoRepositorio>();
            ElasticClient client = new ElasticClient();

            client = ConfiguracaoElasticSearch.abrirConexao(ConfiguracaoElasticSearch.urlElastic);
            var resultadoElastic = client.Search<ProcessoRepositorio>(s => s.Query(q => q.Term(p => p.numero, numeroProcesso)));

            //Dado o número do processo, deve ser encontrado ou 0 ou 1. Se achar mais de um processo com esse número, é uma exceção inesperada que será tratada no Controller.
            var processoResultado = resultadoElastic.HitsMetaData.Hits.Select(hit => hit.Source).SingleOrDefault();

            return processoResultado;

        }
    }
}
