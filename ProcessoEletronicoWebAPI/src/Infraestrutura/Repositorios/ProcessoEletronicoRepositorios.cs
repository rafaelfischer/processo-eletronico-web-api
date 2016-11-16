using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Mapeamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Infraestrutura.Repositorios
{
    public class ProcessoEletronicoRepositorios : IProcessoEletronicoRepositorios
    {
        public ProcessoEletronicoRepositorios()
        {
            UnitOfWork = new EFUnitOfWork(new ProcessoEletronicoContext());

            Funcoes = UnitOfWork.MakeGenericRepository<Funcao>();
            PlanosClassificacao = UnitOfWork.MakeGenericRepository<PlanoClassificacao>();
            Processos = UnitOfWork.MakeGenericRepository<Processo>();
            TiposDocumentais = UnitOfWork.MakeGenericRepository<TipoDocumental>();
            Sinalizacoes = UnitOfWork.MakeGenericRepository<Sinalizacao>();
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public IRepositorioGenerico<Funcao> Funcoes { get; private set; }
        public IRepositorioGenerico<PlanoClassificacao> PlanosClassificacao { get; private set; }
        public IRepositorioGenerico<Processo> Processos { get; private set; }
        public IRepositorioGenerico<TipoDocumental> TiposDocumentais { get; private set; }
        public IRepositorioGenerico<Sinalizacao> Sinalizacoes { get; private set; }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
