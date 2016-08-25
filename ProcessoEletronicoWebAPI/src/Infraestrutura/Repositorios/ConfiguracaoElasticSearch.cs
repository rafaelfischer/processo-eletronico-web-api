using Nest;
using Elasticsearch.Net;
using System;
using ProcessoEletronicoService.Infraestrutura.Repositorios.Modelos;

namespace ProcessoEletronicoService.Infraestrutura.Repositorios
{
    public static class ConfiguracaoElasticSearch
    {
        public static string urlElastic = "http://es-dev.labs.prodest.dcpr.es.gov.br";

        public static ElasticClient abrirConexao(string url)
        {
            Uri node = new Uri(url);
            ConnectionSettings configuracoes = new ConnectionSettings(node)
                .DefaultIndex("processoeletronico")
                .MapDefaultTypeIndices(m => m.Add(typeof(ProcessoRepositorio), "processoeletronico"));

            ElasticClient conexaoElastic = new ElasticClient(configuracoes);


            return conexaoElastic;
        }

        public static ElasticLowLevelClient abrirConexaoLowLevel(string url)
        {
            Uri node = new Uri(url);
            ConnectionConfiguration configuracoes = new ConnectionConfiguration(node);
            ElasticLowLevelClient conexaoElastic = new ElasticLowLevelClient(configuracoes);

            return conexaoElastic;
        }


    }
}
