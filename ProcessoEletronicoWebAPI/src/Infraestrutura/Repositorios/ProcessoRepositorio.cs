using ProcessoEletronicoService.Dominio.Base;
using System;
using ProcessoEletronicoService.Infraestrutura.Mapeamento;
using ProcessoEletronicoService.Dominio.Modelos;

namespace ProcessoEletronicoService.Infraestrutura.Repositorios
{
    public class ProcessoRepositorio : IProcessoRepositorio, IDisposable
    {

        private ProcessoEletronicoContext context;

        public ProcessoRepositorio(ProcessoEletronicoContext context)
        {
            this.context = context;
        }
        
        public void AutuarProcesso(int numeroProcesso)
        {

            Processo processo = new Processo { Numero = numeroProcesso };
            
            /*
            Criar processo genérico
            */

            this.context.Add(processo);
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
