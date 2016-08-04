using System;

namespace ProcessoEletronicoService.Dominio.Base
{
    public interface IProcessoRepositorio : IDisposable
    {
        void AutuarProcesso(int numeroProcesso);
    }
}
