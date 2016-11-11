using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Infraestrutura.Comum;

namespace ProcessoEletronicoService.Negocio.Restrito.Validacao
{
    public class PlanoClassificacaoValidacao
    {
        IRepositorioGenerico<PlanoClassificacao> repositorioPlanosClassificacao;

        public PlanoClassificacaoValidacao(IRepositorioGenerico<PlanoClassificacao> repositorioPlanosClassificacao)
        {
            this.repositorioPlanosClassificacao = repositorioPlanosClassificacao;
        }

        internal void NaoEncontrado(List<PlanoClassificacao> planosClassificacao)
        {
            if (planosClassificacao == null || planosClassificacao.Count <= 0)
                throw new ProcessoEletronicoNaoEncontradoException("Plano de classificação não encontrado.");
        }

    }
}

