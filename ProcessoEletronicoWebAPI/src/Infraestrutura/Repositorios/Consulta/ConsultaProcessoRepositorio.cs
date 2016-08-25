using Nest;
using ProcessoEletronicoService.Infraestrutura.Repositorios.Base;
using ProcessoEletronicoService.Infraestrutura.Repositorios.Modelos;
using System;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Infraestrutura.Repositorios.Consulta
{
    public class ConsultaProcessoRepositorio : IConsultaProcessoRepositorio
    {
        public List<IHit<ProcessoRepositorio>> ConsultarProcessoPorNumero(string numeroProcesso)
        {
            ProcessoRepositorio processo = new ProcessoRepositorio();
            ElasticClient client = new ElasticClient();
            client = ConfiguracaoElasticSearch.abrirConexao(ConfiguracaoElasticSearch.urlElastic);

            var resultado = client.Search<ProcessoRepositorio>(s => s.Query(q => q.Term(p => p.numero, numeroProcesso)));
            
            return resultado.HitsMetaData.Hits;

        }
    }
}
