using System;
using Nest;

namespace ProcessoEletronicoService.Infraestrutura.Repositorios.Modelos
{
    [ElasticsearchType(IdProperty = "numero", Name = "processo")]
    public class ProcessoRepositorio 
    {
        public ProcessoRepositorio()
        {
            assunto = new AssuntoRepositorio();
            orgaoAutuacao = new OrgaoRepositorio();
        }

        [String(Name = "numero")]
        public string numero { get; set; }
        [String(Name = "digito")]
        public string digito { get; set; }
        [String(Name = "resumo")]
        public string resumo { get; set; }
        [Nested(IncludeInParent = true, Name = "assunto")]
        public AssuntoRepositorio assunto { get; set; }
        [Date(Name = "data_autuacao")]
        public DateTime dataAutuacao { get; set; }
        [Nested(IncludeInParent = true, Name = "orgao_autuacao")]
        public OrgaoRepositorio orgaoAutuacao { get; set; }

    }



}
