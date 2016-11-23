using AutoMapper;
using ProcessoEletronicoService.Apresentacao.Base;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Apresentacao
{
    public class ProcessoWorkService : IProcessoWorkService
    {
        private IProcessoNegocio processoNegocio;

        public ProcessoWorkService(IProcessoNegocio processoNegocio)
        {
            this.processoNegocio = processoNegocio;
        }

        public void Autuar()
        {
            throw new NotImplementedException();
        }

        public void Despachar()
        {
            throw new NotImplementedException();
        }

        public void Excluir()
        {
            throw new NotImplementedException();
        }

        public void Listar()
        {
            throw new NotImplementedException();
        }

        public void Pesquisar(string numeroProcesso)
        {
            throw new NotImplementedException();
        }

        public void Pesquisar(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProcessoModelo> Pesquisar(int idOrganizacaoProcesso, int idUnidade)
        {
            var processos = processoNegocio.Pesquisar(idOrganizacaoProcesso, idUnidade);

            var p = Mapper.Map<List<ProcessoModeloNegocio>, List<ProcessoModelo>>(processos);

            return p;
        }
    }
}
