using System.Collections.Generic;

namespace Negocio.Bloqueios.Base
{
    public interface IBloqueioCore
    {
        IList<BloqueioModel> GetBloqueiosOfProcesso(int idProcesso);
        BloqueioModel GetSingleBloqueio(int idProcesso, int id);
        BloqueioModel InsertBloqueioIntoProcesso(int idProcesso, BloqueioModel bloqueioModel);
        void DeleteBloqueioOfProcesso(int idProcesso, int id);
        void DeleteBloqueioOfProcessoIfExists(int idProcesso);
    }
}
