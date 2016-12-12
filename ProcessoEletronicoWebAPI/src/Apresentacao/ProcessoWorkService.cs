﻿using AutoMapper;
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

        public DespachoProcessoModeloGet Despachar(int idOrganizacao, int idProcesso, DespachoProcessoModeloPost despachoPost)
        {
            DespachoModeloNegocio despachoNegocio = new DespachoModeloNegocio();
            Mapper.Map(despachoPost, despachoNegocio);

            despachoNegocio = this.processoNegocio.Despachar(idOrganizacao, idProcesso, despachoNegocio);

            return Mapper.Map<DespachoModeloNegocio, DespachoProcessoModeloGet>(despachoNegocio);
            
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

        public DespachoProcessoModeloGet PesquisarDespacho(int idDespacho, int idProcesso, int idOrganizacaoProcesso)
        {
            DespachoModeloNegocio despachoNegocio = processoNegocio.PesquisarDespacho(idDespacho, idProcesso, idOrganizacaoProcesso);

            return Mapper.Map<DespachoModeloNegocio, DespachoProcessoModeloGet>(despachoNegocio);
        }

        public List<DespachoProcessoModeloCompleto> PesquisarDespachosUsuario(int idOrganizacao, string cpfUsuario)
        {
            List<DespachoModeloNegocio> despachosModeloNegocio = processoNegocio.PesquisarDespachosUsuario(idOrganizacao, cpfUsuario);

            return Mapper.Map<List<DespachoModeloNegocio>, List<DespachoProcessoModeloCompleto>>(despachosModeloNegocio);
        }

        public AnexoModeloGet PesquisarAnexo (int idOrganizacao, int idProcesso, int idDespacho, int idAnexo)
        {
            AnexoModeloNegocio anexoModeloNegocio = new AnexoModeloNegocio();
            anexoModeloNegocio = processoNegocio.PesquisarAnexo(idOrganizacao, idProcesso, idDespacho, idAnexo);

            return Mapper.Map<AnexoModeloNegocio, AnexoModeloGet>(anexoModeloNegocio);
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
