using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Comum
{
    public class ProcessoEletronicoServiceRestritoException : Exception
    {
        public ProcessoEletronicoServiceRestritoException() : base() { }

        public ProcessoEletronicoServiceRestritoException(string mensagem) : base(mensagem) { }

        public ProcessoEletronicoServiceRestritoException(string mensagem, Exception ex) : base(mensagem, ex) { }
    }

    public class ProcessoEletronicoServicePublicoException : Exception
    {
        public ProcessoEletronicoServicePublicoException() : base() { }

        public ProcessoEletronicoServicePublicoException(string mensagem) : base(mensagem) { }

        public ProcessoEletronicoServicePublicoException(string mensagem, Exception ex) : base(mensagem, ex) { }
    }
}
