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
    public class RascunhoProcessoWorkService : BaseWorkService, IRascunhoProcessoWorkService
    {
        private IRascunhoProcessoNegocio rascunhoProcessoNegocio;

        public RascunhoProcessoWorkService(IRascunhoProcessoNegocio rascunhoProcessoNegocio)
        {
            this.rascunhoProcessoNegocio = rascunhoProcessoNegocio;
        }

        public override void RaiseUsuarioAlterado()
        {
            rascunhoProcessoNegocio.Usuario = Usuario;
        }

        public RascunhoProcessoCompletoModelo Pesquisar(int id)
        {
            RascunhoProcessoModeloNegocio rascunhoProcesso = rascunhoProcessoNegocio.Pesquisar(id);
            return Mapper.Map<RascunhoProcessoModeloNegocio, RascunhoProcessoCompletoModelo>(rascunhoProcesso);
        }

        public List<RascunhoProcessoModelo> PesquisarRascunhosProcessoNaOrganizacao(string guidOrganizacao)
        {
            Guid guid = new Guid(guidOrganizacao);
            List<RascunhoProcessoModeloNegocio> rascunhosProcesso = rascunhoProcessoNegocio.PesquisarRascunhosProcessoNaOrganizacao(guid);
            return Mapper.Map<List<RascunhoProcessoModeloNegocio>, List<RascunhoProcessoModelo>>(rascunhosProcesso);
        }

        public RascunhoProcessoCompletoModelo Salvar(RascunhoProcessoModeloPost rascunhoProcesso)
        {
            RascunhoProcessoModeloNegocio rascunhoProcessoNegocio = new RascunhoProcessoModeloNegocio();
            Mapper.Map(rascunhoProcesso, rascunhoProcessoNegocio);
            rascunhoProcessoNegocio = this.rascunhoProcessoNegocio.Salvar(rascunhoProcessoNegocio);
            return Mapper.Map<RascunhoProcessoModeloNegocio, RascunhoProcessoCompletoModelo>(rascunhoProcessoNegocio);
        }

        public RascunhoProcessoCompletoModelo Alterar(RascunhoProcessoModeloPatch rascunhoProcessoPatch)
        {
            throw new NotImplementedException();
        }
        
        public void Excluir(int id)
        {
            rascunhoProcessoNegocio.Excluir(id);
        }

        
    }
}
