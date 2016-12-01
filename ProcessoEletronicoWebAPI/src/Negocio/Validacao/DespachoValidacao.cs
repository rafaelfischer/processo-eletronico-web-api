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
    public class DespachoValidacao
    {
        IRepositorioGenerico<Processo> repositorioProcessos;
        IRepositorioGenerico<Despacho> repositorioDespachos;
        AnexoValidacao anexoValidacao;


        public DespachoValidacao(IProcessoEletronicoRepositorios repositorios)
        {
            this.repositorioProcessos = repositorios.Processos;
            this.repositorioDespachos = repositorios.Despachos;
            anexoValidacao = new AnexoValidacao(repositorios);
        }

        #region Preechimento dos campos obrigatórios

        public void Preenchido(DespachoModeloNegocio despacho)
        {
            /*Preenchimentos dos campos do processo*/

            /*Preenchimento de objetos associados ao processo*/
            anexoValidacao.Preenchido(despacho.Anexos);

        }
        
        #endregion

        #region Validação dos campos

        public void Valido(DespachoModeloNegocio processo)
        {
           
        }
        
        #endregion

    }
}
