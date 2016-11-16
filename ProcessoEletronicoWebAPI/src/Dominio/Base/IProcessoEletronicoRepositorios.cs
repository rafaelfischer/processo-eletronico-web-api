﻿using ProcessoEletronicoService.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Dominio.Base
{
    public interface IProcessoEletronicoRepositorios : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }

        IRepositorioGenerico<Funcao> Funcoes { get; }
        IRepositorioGenerico<PlanoClassificacao> PlanosClassificacao { get; }
        IRepositorioGenerico<Processo> Processos { get; }
        IRepositorioGenerico<TipoDocumental> TiposDocumentais { get; }
        IRepositorioGenerico<Sinalizacao> Sinalizacoes { get; }
    }
}
