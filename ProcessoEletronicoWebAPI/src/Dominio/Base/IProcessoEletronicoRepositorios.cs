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
        IRepositorioGenerico<Processo> Processos { get; }
    }
}