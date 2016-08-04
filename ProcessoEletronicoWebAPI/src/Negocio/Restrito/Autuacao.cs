using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Infraestrutura.Repositorios;
using ProcessoEletronicoService.Infraestrutura.Mapeamento;
using ProcessoEletronicoService.Dominio.Base;

namespace ProcessoEletronicoService.Negocio
{
    public class Autuacao : IAutuacao
    {

        public string Autuar(int numeroProcesso)
        {
            //processoRepositorio.AutuarProcesso(numeroProcesso);
            return "Processo de número " + numeroProcesso + " autuado com sucesso!";
        }
    }
}
