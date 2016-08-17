using ProcessoEletronicoService.Apresentacao.Publico.Base;
using ProcessoEletronicoService.Negocio.Publico.Base;

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
            return this.consultaProcesso.ConsultarPorNumero(numeroProcesso);
        }
    }
}
