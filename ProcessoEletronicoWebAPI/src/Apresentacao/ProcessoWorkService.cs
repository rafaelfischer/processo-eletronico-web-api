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
    public class ProcessoWorkService : BaseWorkService, IProcessoWorkService
    {
        private IProcessoNegocio processoNegocio;

        public ProcessoWorkService(IProcessoNegocio processoNegocio)
        {
            this.processoNegocio = processoNegocio;
            //this.processoNegocio.Usuario = Usuario;
        }

        public override void RaiseUsuarioAlterado()
        {
            processoNegocio.Usuario = Usuario;
        }

        public ProcessoCompletoModelo Autuar(ProcessoModeloPost processo)
        {
            ProcessoModeloNegocio processoNegocio = new ProcessoModeloNegocio();
            Mapper.Map(processo, processoNegocio);

            processoNegocio = this.processoNegocio.Autuar(processoNegocio);

            return Mapper.Map<ProcessoModeloNegocio, ProcessoCompletoModelo>(processoNegocio);
        }
        
        
        public ProcessoCompletoModelo Pesquisar(string numero)
        {
            var processos = processoNegocio.Pesquisar(numero);

            var p = Mapper.Map<ProcessoModeloNegocio, ProcessoCompletoModelo>(processos);

            return p;
        }

        public ProcessoCompletoModelo Pesquisar(int id)
        {
            var processos = processoNegocio.Pesquisar(id);

            var p = Mapper.Map<ProcessoModeloNegocio, ProcessoCompletoModelo>(processos);

            return p;
        }
        
        public List<ProcessoModelo> PesquisarProcessosNaUnidade(string guidUnidade)
        {
            var processos = processoNegocio.PesquisarProcessoNaUnidade(guidUnidade);

            return Mapper.Map<List<ProcessoModeloNegocio>, List<ProcessoModelo>>(processos);
        }

        public List<ProcessoModelo> PesquisarProcessosNaOrganizacao(string guidOrganizacao)
        {
            var processos = processoNegocio.PesquisarProcessoNaOrganizacao(guidOrganizacao);

            return Mapper.Map<List<ProcessoModeloNegocio>, List<ProcessoModelo>>(processos);
        }

        public List<ProcessoModelo> PesquisarProcessosDespachadosUsuario()
        {
            List<ProcessoModeloNegocio> processosModeloNegocio = new List<ProcessoModeloNegocio>();
            processosModeloNegocio = processoNegocio.PesquisarProcessosDespachadosUsuario();

            return Mapper.Map<List<ProcessoModeloNegocio>, List<ProcessoModelo>>(processosModeloNegocio);
        }
    }
}
