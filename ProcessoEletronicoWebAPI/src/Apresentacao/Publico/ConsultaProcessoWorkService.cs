using ProcessoEletronicoService.Apresentacao.Publico.Base;
using ProcessoEletronicoService.Negocio.Publico.Base;
using Newtonsoft.Json;

namespace ProcessoEletronicoService.Apresentacao.Publico
{
    public class ConsultaProcessoWorkService : IConsultaProcessoWorkService
    {

        private IConsultaProcesso consultaProcesso;

        public ConsultaProcessoWorkService(IConsultaProcesso consultaProcesso)
        {
            this.consultaProcesso = consultaProcesso;
        }

        public string ConsultarPorNumero(string numeroProcesso)
        {
            return  JsonConvert.SerializeObject(consultaProcesso.ConsultarPorNumero(numeroProcesso));
        
        }
    }
}
