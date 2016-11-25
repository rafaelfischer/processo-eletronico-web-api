using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Validacao
{
    public class SinalizacaoValidacao
    {
        IRepositorioGenerico<Sinalizacao> repositorioSinalizacoes;

        public SinalizacaoValidacao(IProcessoEletronicoRepositorios repositorios)
        {
            this.repositorioSinalizacoes = repositorios.Sinalizacoes;
        }

        public void IdValido(List<SinalizacaoModeloNegocio> sinalizacoes)
        {
            foreach (SinalizacaoModeloNegocio sinalizacao in sinalizacoes)
            {
                IdValido(sinalizacao);
            }
        }

        public void IdValido(SinalizacaoModeloNegocio sinalizacao)
        {
            if (sinalizacao.Id <= 0)
            {
                throw new RequisicaoInvalidaException("Id da sinalização inválido.");
            }
        }

        public void SinalizacaoExistente(List<SinalizacaoModeloNegocio> sinalizacoes)
        {
            foreach (SinalizacaoModeloNegocio sinalizacao in sinalizacoes)
            {
                SinalizacaoExistente(sinalizacao);
            }
        }

        public void SinalizacaoExistente(SinalizacaoModeloNegocio sinalizacao)
        {
            if (repositorioSinalizacoes.Where(s => s.Id == sinalizacao.Id).SingleOrDefault() == null)
            {
                throw new RecursoNaoEncontradoException("Sinalização inexistente");
            }
        }


    }
}
