using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using System;

namespace Negocio.Comum.Validacao
{
    public class GuidValidacao
    {

        private readonly string mensagemErro = "Formato do identificador inválido";

        public void IsValid(string guidString)
        {
            try
            {
                Guid guid = Guid.Parse(guidString);
            }
            catch (FormatException)
            {
                throw new RequisicaoInvalidaException($"{mensagemErro} : {guidString}");
            }
        }

        public Guid ValidateAndReturnGuid(string guidString)
        {
            Guid guid = new Guid();
            if (!Guid.TryParse(guidString, out guid))
            {
                throw new RequisicaoInvalidaException("Formato do identificador inválido.");
            }

            return guid;
        }
    }
}
