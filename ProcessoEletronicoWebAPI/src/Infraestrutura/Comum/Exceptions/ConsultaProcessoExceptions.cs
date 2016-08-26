using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Infraestrutura.Comum.Exceptions
{
    public class ProcessoNaoEncontradoException : Exception
    {
        public ProcessoNaoEncontradoException() : base() { }
        public ProcessoNaoEncontradoException(string mensagem) : base(mensagem) { }
        public ProcessoNaoEncontradoException(string mensagem, Exception inner) : base(mensagem, inner) { }
    }

    public class NumeroProcessoInvalidoException : Exception
    {
        public NumeroProcessoInvalidoException() : base() { }
        public NumeroProcessoInvalidoException(string mensagem) : base(mensagem) { }
        public NumeroProcessoInvalidoException(string mensagem, Exception inner) : base(mensagem, inner) { }
    }



}
