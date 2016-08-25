using ProcessoEletronicoService.Negocio.Modelos;

namespace ProcessoEletronicoService.Negocio.Publico.Base
{
    public interface IConsultaProcesso
    {
        ProcessoNegocio ConsultarPorNumero(string numeroProcesso);
    }
}
