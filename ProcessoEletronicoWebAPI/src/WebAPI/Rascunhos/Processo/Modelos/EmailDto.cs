namespace ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Modelos
{
    public class GetEmailDto
    {
        public int Id { get; set; }
        public string Endereco { get; set; }
    }

    public class PostEmailDto
    {
        public string Endereco { get; set; }
    }

    public class PatchEmailDto
    {
        public string Endereco { get; set; }
    }
}
