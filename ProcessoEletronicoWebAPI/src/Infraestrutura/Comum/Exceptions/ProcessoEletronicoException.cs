using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Infraestrutura.Comum.Exceptions
{
    public class RecursoNaoEncontradoException : Exception
    {
        public RecursoNaoEncontradoException() : base() { }
        public RecursoNaoEncontradoException(string mensagem) : base(mensagem) { }
        public RecursoNaoEncontradoException(string mensagem, Exception inner) : base(mensagem, inner) { }
    }

    public class RequisicaoInvalidaException : Exception
    {
        public RequisicaoInvalidaException() : base() { }
        public RequisicaoInvalidaException(string mensagem) : base(mensagem) { }
        public RequisicaoInvalidaException(string mensagem, Exception inner) : base(mensagem, inner) { }
    }

    public class InsercaoProcessoException : Exception
    {
        public InsercaoProcessoException() : base() { }
        public InsercaoProcessoException(string mensagem) : base(mensagem) { }
        public InsercaoProcessoException(string mensagem, Exception inner) : base(mensagem, inner) { }
    }



}
