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

        public ProcessoCompletoModelo Autuar(ProcessoModeloPost processo, int idOrganizacao)
        {
            ProcessoModeloNegocio processoNegocio = new ProcessoModeloNegocio();
            Mapper.Map(processo, processoNegocio);

            processoNegocio = this.processoNegocio.Autuar(processoNegocio, idOrganizacao);

            return Mapper.Map<ProcessoModeloNegocio, ProcessoCompletoModelo>(processoNegocio);
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

        public ProcessoCompletoModelo Pesquisar(int idOrganizacaoProcesso, int idProcesso)
        {
            var processos = processoNegocio.Pesquisar(idOrganizacaoProcesso, idProcesso);

            var p = Mapper.Map<ProcessoModeloNegocio, ProcessoCompletoModelo>(processos);

            return p;
        }

        public List<ProcessoModelo> PesquisarProcessosNaUnidade(int idOrganizacaoProcesso, int idUnidade)
        {
            var processos = processoNegocio.PesquisarProcessoNaUnidade(idOrganizacaoProcesso, idUnidade);

            return Mapper.Map<List<ProcessoModeloNegocio>, List<ProcessoModelo>>(processos);
        }
    }
}
