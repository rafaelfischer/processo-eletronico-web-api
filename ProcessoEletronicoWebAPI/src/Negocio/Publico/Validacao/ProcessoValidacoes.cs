using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;

namespace ProcessoEletronicoService.Negocio.Publico.Validacao
{

    public static class ProcessoValidacoes
    {

        private const string msgErroNumeroProcessoInvalido = "O número do processo deve possuir apenas números e ter entre 2 e 8 dígitos";

        public static void ValidarNumeroProcesso(string numeroProcesso)
        {
            int numero;
            if (!int.TryParse(numeroProcesso, out numero) || (numeroProcesso.Length < 2 || numeroProcesso.Length > 8))
            {
                throw new NumeroProcessoInvalidoException(msgErroNumeroProcessoInvalido);
            }
        }
    }
}
