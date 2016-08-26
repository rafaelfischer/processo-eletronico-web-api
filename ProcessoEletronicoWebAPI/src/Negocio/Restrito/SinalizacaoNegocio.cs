using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Restrito.Base;
using ProcessoEletronicoService.Negocio.Restrito.Validacao;
using System.Collections.Generic;
using System.Linq;

namespace ProcessoEletronicoService.Negocio.Restrito
{
    public class SinalizacaoNegocio : ISinalizacaoNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<Sinalizacao> repositorio;
        private SinalizacaoValidacao validacao;

        public SinalizacaoNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorio = repositorios.Sinalizacoes;
            validacao = new SinalizacaoValidacao(repositorio);
        }

        public void Excluir(int id)
        {
            validacao.IdExistente(id);

            var sinalizacao = repositorio.Single(td => td.Id == id);

            repositorio.Remove(sinalizacao);

            unitOfWork.Save();
        }

        public List<SinalizacaoModeloNegocio> Obter()
        {
            var sinalizacoes = repositorio.Select(td => new SinalizacaoModeloNegocio { Id = td.Id, Descricao = td.Descricao })
                                                              .ToList();

            return sinalizacoes;
        }

        public SinalizacaoModeloNegocio Obter(int id)
        {
            var sinalizacao = repositorio.Select(td => new SinalizacaoModeloNegocio { Id = td.Id, Descricao = td.Descricao })
                                                            .SingleOrDefault(td => td.Id == id);

            return sinalizacao;
        }

        public SinalizacaoModeloNegocio Incluir(SinalizacaoModeloNegocio sinalizacao)
        {
            validacao.SinalizacaoValida(sinalizacao);

            validacao.DescricaoValida(sinalizacao.Descricao);

            validacao.DescricaoExistente(sinalizacao.Descricao);

            Sinalizacao sn = new Sinalizacao();

            sn.Descricao = sinalizacao.Descricao;

            repositorio.Add(sn);

            unitOfWork.Save();

            sinalizacao.Id = sn.Id;

            return sinalizacao;
        }

        public void Alterar(int id, SinalizacaoModeloNegocio sinalizacao)
        {
            validacao.SinalizacaoValida(sinalizacao);

            validacao.IdValido(id);
            validacao.IdValido(sinalizacao.Id);

            validacao.IdAlteracaoValido(id, sinalizacao);

            validacao.IdExistente(id);

            validacao.DescricaoValida(sinalizacao.Descricao);

            validacao.DescricaoExistente(sinalizacao.Descricao);

            Sinalizacao sn = repositorio.Where(t => t.Id == sinalizacao.Id).Single();

            sn.Descricao = sinalizacao.Descricao;

            unitOfWork.Save();
        }
    }
}
