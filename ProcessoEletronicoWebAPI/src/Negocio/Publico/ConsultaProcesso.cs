using ProcessoEletronicoService.Negocio.Publico.Base;
using ProcessoEletronicoService.Infraestrutura.Repositorios.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using Nest;
using ProcessoEletronicoService.Infraestrutura.Repositorios.Modelos;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Publico
{
    public class ConsultaProcesso : IConsultaProcesso
    {
        private IConsultaProcessoRepositorio repositorioProcesso;

        public ConsultaProcesso(IConsultaProcessoRepositorio repositorioProcesso)
        {
            this.repositorioProcesso = repositorioProcesso;
        }

        public ProcessoNegocio ConsultarPorNumero(string numeroProcesso)
        {
            if (ProcessoValidacoes.ValidarNumeroProcesso(numeroProcesso))
            {
                ProcessoRepositorio processoRepositorio = new ProcessoRepositorio();

                List<IHit<ProcessoRepositorio>> hitsProcessoRepositorio = new List<IHit<ProcessoRepositorio>>();
                hitsProcessoRepositorio = repositorioProcesso.ConsultarProcessoPorNumero(numeroProcesso);

                if (hitsProcessoRepositorio.Count == 0)
                {
                    //Retorna um processo vazio caso a lista de resultados seja vazia
                    return new ProcessoNegocio(true);

                }
                else if (hitsProcessoRepositorio.Count > 1)
                {
                    //Caso retorne mais de um processo, retorna nulo
                    return null;
                }
                else
                {
                    processoRepositorio = hitsProcessoRepositorio[0].Source;
                    return new ProcessoNegocio(processoRepositorio.numero,
                                                processoRepositorio.digito,
                                                processoRepositorio.resumo, 
                                                processoRepositorio.assunto.id, 
                                                processoRepositorio.assunto.descricao, 
                                                processoRepositorio.dataAutuacao, 
                                                processoRepositorio.orgaoAutuacao.id, 
                                                processoRepositorio.orgaoAutuacao.sigla);
                }

            }
            else
            {
               
                //Requisição é inválida
                return null;
            }

          
        }
    }
}
