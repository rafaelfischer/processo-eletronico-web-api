using ProcessoEletronicoService.Apresentacao.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using AutoMapper;

namespace ProcessoEletronicoService.Apresentacao
{
    public class AtividadeWorkService : IAtividadeWorkService
    {
        IAtividadeNegocio atividadeNegocio;

        public AtividadeWorkService(IAtividadeNegocio atividadeNegocio)
        {
            this.atividadeNegocio = atividadeNegocio;
        }

        public IEnumerable<AtividadeModelo> Pesquisar(int idFuncao)
        {
            List<AtividadeModeloNegocio> atividades = atividadeNegocio.Pesquisar(idFuncao);

            return Mapper.Map<List<AtividadeModeloNegocio>, List<AtividadeModelo>>(atividades);
        }
    }
}
