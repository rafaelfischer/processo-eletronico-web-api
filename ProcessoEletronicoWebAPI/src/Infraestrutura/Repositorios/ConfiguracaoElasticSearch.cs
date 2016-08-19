using Nest;
using Elasticsearch.Net;
using System;

namespace ProcessoEletronicoService.Infraestrutura.Repositorios
{
    public static class ConfiguracaoElasticSearch
    {
        public static string urlElastic = "http://es-dev.labs.prodest.dcpr.es.gov.br";
                
        public static ElasticClient abrirConexao (string url, string indicePadrao = "processoeletronico")
        {
            Uri node = new Uri(url);
            ConnectionSettings configuracoes = new ConnectionSettings(node).DefaultIndex(indicePadrao);
            ElasticClient conexaoElastic = new ElasticClient(configuracoes);

            return conexaoElastic;
        }

        public static ElasticLowLevelClient abrirConexaoLowLevel (string url, string indicePadrao = "/processoeletronico")
        {
            Uri node = new Uri(url + indicePadrao);
            ConnectionConfiguration configuracoes = new ConnectionConfiguration(node);
            ElasticLowLevelClient conexaoElastic = new ElasticLowLevelClient(configuracoes);
            
            return conexaoElastic;
        }

    }
}
