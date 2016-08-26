using ProcessoEletronicoService.Negocio.Publico.Base;
using ProcessoEletronicoService.Infraestrutura.Repositorios.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Repositorios.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Publico.Validacao;

namespace ProcessoEletronicoService.Negocio.Publico
{
    public class ConsultaProcesso : IConsultaProcesso
    {
        private IConsultaProcessoRepositorio repositorioProcesso;
        private const string msgErroProcessoNaoEncontrado = "Não há processo cadastrado com esse número.";

        public ConsultaProcesso(IConsultaProcessoRepositorio repositorioProcesso)
        {
            this.repositorioProcesso = repositorioProcesso;
        }

        public ProcessoNegocio ConsultarPorNumero(string numeroProcesso)
        {

            ProcessoValidacoes.ValidarNumeroProcesso(numeroProcesso);

            ProcessoRepositorio processo = new ProcessoRepositorio();
            processo = repositorioProcesso.ConsultarProcessoPorNumero(numeroProcesso);

            if (processo == null)
            {
                throw new ProcessoNaoEncontradoException(msgErroProcessoNaoEncontrado);

            }
            else
            {
                return new ProcessoNegocio(processo.numero, processo.digito, processo.resumo, processo.assunto.id, processo.assunto.descricao, 
                                            processo.dataAutuacao, processo.orgaoAutuacao.id, processo.orgaoAutuacao.sigla);
            }
        }
    }
}
