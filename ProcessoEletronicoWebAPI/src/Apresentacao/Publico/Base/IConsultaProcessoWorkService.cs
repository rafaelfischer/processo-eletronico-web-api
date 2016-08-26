using ProcessoEletronicoService.Apresentacao.Publico.Modelos;

namespace ProcessoEletronicoService.Apresentacao.Publico.Base
{
    public interface IConsultaProcessoWorkService
    {
        ProcessoApresentacao ConsultarPorNumero(string numeroProcesso);
    }
}
