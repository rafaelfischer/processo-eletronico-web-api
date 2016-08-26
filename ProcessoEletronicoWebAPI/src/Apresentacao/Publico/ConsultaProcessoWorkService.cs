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
            processo = consultaProcesso.ConsultarPorNumero(numeroProcesso);

            return new ProcessoApresentacao(processo.numero, processo.digito, processo.resumo, processo.assunto.descricao, processo.dataAutuacao, processo.orgaoAutuacao.sigla);
        
        }
    }
}
