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
    public class AtividadeWorkService : BaseWorkService, IAtividadeWorkService
    {
        IAtividadeNegocio atividadeNegocio;
        
        public override void RaiseUsuarioAlterado()
        {
            atividadeNegocio.Usuario = Usuario;
        }

        public AtividadeWorkService(IAtividadeNegocio atividadeNegocio)
        {
            this.atividadeNegocio = atividadeNegocio;
        }

        public AtividadeProcessoGetModelo Pesquisar(int id)
        {
            AtividadeModeloNegocio atividade = atividadeNegocio.Pesquisar(id);

            return Mapper.Map<AtividadeModeloNegocio, AtividadeProcessoGetModelo>(atividade);
        }

        public IEnumerable<AtividadeModelo> PesquisarPorFuncao(int idFuncao)
        {
            List<AtividadeModeloNegocio> atividades = atividadeNegocio.PesquisarPorFuncao(idFuncao);

            return Mapper.Map<List<AtividadeModeloNegocio>, List<AtividadeModelo>>(atividades);
        }

        public IEnumerable<AtividadeProcessoGetModelo> Pesquisar()
        {
            List<AtividadeModeloNegocio> atividades = atividadeNegocio.Pesquisar();

            return Mapper.Map<List<AtividadeModeloNegocio>, List<AtividadeProcessoGetModelo>>(atividades);
        }

        public AtividadeProcessoGetModelo Inserir(AtividadeModeloPost atividade)
        {
            AtividadeModeloNegocio atividadeModeloNegocio = new AtividadeModeloNegocio();
            Mapper.Map(atividade, atividadeModeloNegocio);

            atividadeModeloNegocio = atividadeNegocio.Inserir(atividadeModeloNegocio);

            return Mapper.Map<AtividadeModeloNegocio, AtividadeProcessoGetModelo>(atividadeModeloNegocio);
        }

        public void Excluir(int id)
        {
            atividadeNegocio.Excluir(id);
        }
    }
}
