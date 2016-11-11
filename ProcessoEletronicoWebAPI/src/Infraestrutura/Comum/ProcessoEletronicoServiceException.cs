using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Infraestrutura.Comum
{
    public class ProcessoEletronicoServiceException : Exception
    {
        public ProcessoEletronicoServiceException(string mensagem) : base(mensagem) { }

        public ProcessoEletronicoServiceException(string mensagem, Exception ex) : base(mensagem, ex) { }
    }

    public class ProcessoEletronicoNaoEncontradoException : ProcessoEletronicoServiceException
    {
        public ProcessoEletronicoNaoEncontradoException(string mensagem) : base(mensagem) { }

        public ProcessoEletronicoNaoEncontradoException(string mensagem, Exception ex) : base(mensagem, ex) { }
    }

    public class ProcessoEletronicoRequisicaoInvalidaException : ProcessoEletronicoServiceException
    {
        public ProcessoEletronicoRequisicaoInvalidaException(string mensagem) : base(mensagem) { }

        public ProcessoEletronicoRequisicaoInvalidaException(string mensagem, Exception ex) : base(mensagem, ex) { }
    }
}
