using AutoMapper;
using ProcessoEletronicoService.Apresentacao.Publico.Base;
using ProcessoEletronicoService.Negocio.Publico.Base;
using ProcessoEletronicoService.Apresentacao.Publico.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;

namespace ProcessoEletronicoService.Apresentacao.Publico
{
    public class ConsultaProcessoWorkService : IConsultaProcessoWorkService
    {

        private IConsultaProcesso consultaProcesso;

        public ConsultaProcessoWorkService(IConsultaProcesso consultaProcesso)
        {
            this.consultaProcesso = consultaProcesso;
        }

        public ProcessoApresentacao ConsultarPorNumero(string numeroProcesso)
        {
            ProcessoNegocio processo = new ProcessoNegocio();
            ProcessoApresentacao processoApresentacao = new ProcessoApresentacao();

            processo = consultaProcesso.ConsultarPorNumero(numeroProcesso);
            processoApresentacao = Mapper.Map<ProcessoNegocio, ProcessoApresentacao>(processo);

            return processoApresentacao;


        }
    }
}
