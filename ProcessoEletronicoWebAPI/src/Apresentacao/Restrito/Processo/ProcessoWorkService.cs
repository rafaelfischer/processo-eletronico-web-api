using ProcessoEletronicoService.Apresentacao.Base;
using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Processo
{
    public class ProcessoWorkService : IProcessoWorkService
    {
        private IAutuacao autuacao;

        public ProcessoWorkService(IAutuacao autuacao)
        {
            this.autuacao = autuacao;
        }

        public string Autuar()
        {
            return autuacao.Autuar();
        }
    }
}
