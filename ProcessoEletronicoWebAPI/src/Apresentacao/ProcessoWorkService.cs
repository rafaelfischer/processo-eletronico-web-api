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

        public DespachoProcessoGetModelo Despachar(int idOrganizacao, int idProcesso, DespachoProcessoModeloPost despachoPost)
        {

            DespachoModeloNegocio despachoNegocio = new DespachoModeloNegocio();
            Mapper.Map(despachoPost, despachoNegocio);

            despachoNegocio = this.processoNegocio.Despachar(idOrganizacao, idProcesso, despachoNegocio);

            return Mapper.Map<DespachoModeloNegocio, DespachoProcessoGetModelo>(despachoNegocio);
            

        }

        public void Excluir()
        {
            throw new NotImplementedException();
        }

        public void Listar()
        {
            throw new NotImplementedException();
        }

        public ProcessoCompletoModelo Pesquisar(string numero)
        {
            var processos = processoNegocio.Pesquisar(numero);

            var p = Mapper.Map<ProcessoModeloNegocio, ProcessoCompletoModelo>(processos);

            return p;
        }

        public ProcessoCompletoModelo Pesquisar(int idOrganizacaoProcesso, int idProcesso)
        {
            var processos = processoNegocio.Pesquisar(idOrganizacaoProcesso, idProcesso);

            var p = Mapper.Map<ProcessoModeloNegocio, ProcessoCompletoModelo>(processos);

            return p;
        }

        public DespachoProcessoGetModelo PesquisarDespacho(int idDespacho, int idProcesso, int idOrganizacaoProcesso)
        {
            DespachoModeloNegocio despachoNegocio = processoNegocio.PesquisarDespacho(idDespacho, idProcesso, idOrganizacaoProcesso);

            return Mapper.Map<DespachoModeloNegocio, DespachoProcessoGetModelo>(despachoNegocio);
        }

        public List<ProcessoModelo> PesquisarProcessosNaUnidade(int idOrganizacaoProcesso, int idUnidade)
        {
            var processos = processoNegocio.PesquisarProcessoNaUnidade(idOrganizacaoProcesso, idUnidade);

            return Mapper.Map<List<ProcessoModeloNegocio>, List<ProcessoModelo>>(processos);
        }

        public List<ProcessoModelo> PesquisarProcessosNaOrganizacao(int idOrganizacaoProcesso, int idOrganizacao)
        {
            var processos = processoNegocio.PesquisarProcessoNaOrganizacao(idOrganizacaoProcesso, idOrganizacao);

            return Mapper.Map<List<ProcessoModeloNegocio>, List<ProcessoModelo>>(processos);
        }
    }
}
