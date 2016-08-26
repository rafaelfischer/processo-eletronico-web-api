using ProcessoEletronicoService.Apresentacao.Restrito.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Apresentacao.Restrito.Modelos;
using ProcessoEletronicoService.Negocio.Restrito.Base;
using ProcessoEletronicoService.Negocio.Modelos;

namespace ProcessoEletronicoService.Apresentacao.Restrito
{
    public class SinalizacaoWorkService : ISinalizacaoWorkService
    {
        private ISinalizacaoNegocio sinalizacaoNegocio;

        public SinalizacaoWorkService(ISinalizacaoNegocio sinalizacaoNegocio)
        {
            this.sinalizacaoNegocio = sinalizacaoNegocio;
        }

        public void Excluir(int id)
        {
            sinalizacaoNegocio.Excluir(id);
        }

        public List<SinalizacaoModelo> Obter()
        {
            var sinalizacoes = sinalizacaoNegocio.Obter().Select(td => new SinalizacaoModelo { Id = td.Id, Descricao = td.Descricao })
                                                                                .ToList();

            return sinalizacoes;
        }

        public SinalizacaoModelo Obter(int id)
        {
            var td = sinalizacaoNegocio.Obter(id);

            SinalizacaoModelo sinalizacao = null;
            if (td != null)
            {
                sinalizacao = new SinalizacaoModelo();
                sinalizacao.Id = td.Id;
                sinalizacao.Descricao = td.Descricao;
            }

            return sinalizacao;
        }

        public SinalizacaoModelo Incluir(SinalizacaoModelo sinalizacao)
        {
            //TODO: Colocar AutoMapper
            SinalizacaoModeloNegocio smn = new SinalizacaoModeloNegocio();

            smn = new SinalizacaoModeloNegocio();
            smn.Id = sinalizacao.Id;
            smn.Descricao = sinalizacao.Descricao;

            var td = sinalizacaoNegocio.Incluir(smn);

            sinalizacao.Id = td.Id;

            return sinalizacao;
        }

        public void Alterar(int id, SinalizacaoModelo sinalizacao)
        {
            //TODO: Colocar AutoMapper
            SinalizacaoModeloNegocio smn = new SinalizacaoModeloNegocio();

            smn = new SinalizacaoModeloNegocio();
            smn.Id = sinalizacao.Id;
            smn.Descricao = sinalizacao.Descricao;

            sinalizacaoNegocio.Alterar(id, smn);
        }
    }
}
