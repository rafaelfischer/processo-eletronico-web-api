using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Infraestrutura.Repositorios;
using ProcessoEletronicoService.Infraestrutura.Mapeamento;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;

namespace ProcessoEletronicoService.Negocio
{
    public class Autuacao : IAutuacao
    {
        IUnitOfWork unitOfWork;
        IRepositorioGenerico<Processo> repositorioProcessos;

        public Autuacao(IProcessoEletronicoRepositorios repositorios)
        {
            this.unitOfWork = repositorios.UnitOfWork;
            this.repositorioProcessos = repositorios.Processos;
        }

        public string Autuar(int numeroProcesso)
        {
            var processo = repositorioProcessos.Where(p => p.Numero == numeroProcesso)
                                               .SingleOrDefault();

            if (processo != null)
                return "Processo de número " + processo.Resumo + " autuado com sucesso!";
            else
                return "Processo de número " + numeroProcesso + " autuado com sucesso!";
        }
    }
}
