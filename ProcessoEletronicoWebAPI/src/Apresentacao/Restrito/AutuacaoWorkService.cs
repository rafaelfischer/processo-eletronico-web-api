using ProcessoEletronicoService.Apresentacao.Base;
using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao.Processo
{
    public class AutuacaoWorkService : IAutuacaoWorkService
    {
        private IAutuacao autuacao;

        public AutuacaoWorkService(IAutuacao autuacao)
        {
            this.autuacao = autuacao;
        }

        public string Autuar()
        {
            return autuacao.Autuar();
        }
    }
}
