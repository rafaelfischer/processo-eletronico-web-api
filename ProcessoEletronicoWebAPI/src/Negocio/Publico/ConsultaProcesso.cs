using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Negocio.Publico.Base;
using ProcessoEletronicoService.Infraestrutura.Repositorios.Base;


namespace ProcessoEletronicoService.Negocio.Publico
{
    public class ConsultaProcesso : IConsultaProcesso
    {

        private IConsultaProcessoRepositorio repositorioProcesso;

        public ConsultaProcesso(IConsultaProcessoRepositorio repositorioProcesso)
        {
            this.repositorioProcesso = repositorioProcesso;
        }
            
        public string ConsultarPorNumero(string numeroProcesso)
        {
            return repositorioProcesso.ConsultarProcessoPorNumero(numeroProcesso);
        }
    }
}
