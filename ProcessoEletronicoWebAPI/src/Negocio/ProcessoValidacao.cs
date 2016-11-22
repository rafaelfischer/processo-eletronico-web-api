using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio
{
    public class ProcessoValidacao
    {
        IRepositorioGenerico<Processo> repositorioProcessos;
        //CpfValidacao cpfValidacao;
        //CnpjValidacao cnpjValidacao;

        public ProcessoValidacao (IRepositorioGenerico<Processo> repositorioProcessos)
        {
            this.repositorioProcessos = repositorioProcessos;
        }

              


        internal void Preenchido(ProcessoModeloNegocio processo)
        {

        }

        

    }
}
