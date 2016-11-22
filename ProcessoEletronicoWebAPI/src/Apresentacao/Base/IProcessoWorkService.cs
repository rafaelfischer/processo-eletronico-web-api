using ProcessoEletronicoService.Apresentacao.Modelos;

namespace ProcessoEletronicoService.Apresentacao.Base
{
    public interface IProcessoWorkService
    {
        void Listar();
        void Pesquisar(int id);
        void Pesquisar(string numeroProcesso);
        void Autuar(ProcessoModeloPost processoPost);
        void Despachar();
        void Excluir();
        
    }
}
