using ProcessoEletronicoService.Negocio.Publico.Base;
using ProcessoEletronicoService.Infraestrutura.Repositorios.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Repositorios.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Publico.Validacao;
using AutoMapper;

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
                ProcessoNegocio processoNegocio = new ProcessoNegocio();
                
                processoNegocio = Mapper.Map<ProcessoRepositorio, ProcessoNegocio>(processo);

                return processoNegocio;

            }
        }
    }
}
