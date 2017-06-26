using System.Collections.Generic;

namespace ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Modelos
{
    public class GetInteressadoPessoaFisicaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string NomeMunicipio { get; set; }
        public string UfMunicipio { get; set; }
        public string GuidMunicipio { get; set; }
        public List<GetContatoDto> Contatos { get; set; }
        public List<GetEmailDto> Emails { get; set; }
    }

    public class PostInteressadoPessoaFisicaDto
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string GuidMunicipio { get; set; }
        public List<PostContatoDto> Contatos { get; set; }
        public List<PostEmailDto> Emails { get; set; }
    }

    public class PatchInteressadoPessoaFisicaDto
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string GuidMunicipio { get; set; }
    }
}
