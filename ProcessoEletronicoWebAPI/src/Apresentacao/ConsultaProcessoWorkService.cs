using AutoMapper;
using ProcessoEletronicoService.Apresentacao.Publico.Base;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using System;

namespace ProcessoEletronicoService.Apresentacao.Publico
{
    public class ConsultaProcessoWorkService : IConsultaProcessoWorkService
    {

        private IProcessoNegocio processoNegocio;

        public ConsultaProcessoWorkService(IProcessoNegocio processoNegocio)
        {
            this.processoNegocio = processoNegocio;
        }

        public ProcessoModelo ConsultarPorNumero(string numeroProcesso)
        {
            throw new NotImplementedException();
        }

        /*
        public ProcessoApresentacao ConsultarPorNumero(string numeroProcesso)
        {
            ProcessoNegocio processo = new ProcessoNegocio();
            ProcessoApresentacao processoApresentacao = new ProcessoApresentacao();

            processo = consultaProcesso.ConsultarPorNumero(numeroProcesso);
            processoApresentacao = Mapper.Map<ProcessoNegocio, ProcessoApresentacao>(processo);

            return processoApresentacao;


        }
        */
    }
}
