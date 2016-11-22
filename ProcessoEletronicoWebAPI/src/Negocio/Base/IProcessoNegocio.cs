using ProcessoEletronicoService.Negocio.Modelos;

namespace ProcessoEletronicoService.Negocio.Base
{
    public interface IProcessoNegocio
    {
        void Listar();
        void Pesquisar(int id);
        void Pesquisar(string numeroProcesso);
        void Autuar(ProcessoModeloNegocio processoNegocio);
        void Despachar();
        void Excluir();
    }
}
