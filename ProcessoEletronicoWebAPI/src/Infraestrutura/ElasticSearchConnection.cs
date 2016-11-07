using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Infraestrutura
{
    public class ElasticSearchConnection
    {
        public string urlElastic = "http://es-dev.labs.prodest.dcpr.es.gov.br";

        public ElasticClient AbrirConexao(string url)
        {
            Uri node = new Uri(url);
            ConnectionSettings configuracoes = new ConnectionSettings(node)
                .DefaultIndex("processoeletronico");
                //.MapDefaultTypeIndices(m => m.Add(typeof(ProcessoRepositorio), "processoeletronico"));

            ElasticClient conexaoElastic = new ElasticClient(configuracoes);


            return conexaoElastic;
        }

        public ElasticLowLevelClient AbrirConexaoLowLevel(string url)
        {
            Uri node = new Uri(url);
            ConnectionConfiguration configuracoes = new ConnectionConfiguration(node);
            ElasticLowLevelClient conexaoElastic = new ElasticLowLevelClient(configuracoes);

            return conexaoElastic;
        }
    }
}
