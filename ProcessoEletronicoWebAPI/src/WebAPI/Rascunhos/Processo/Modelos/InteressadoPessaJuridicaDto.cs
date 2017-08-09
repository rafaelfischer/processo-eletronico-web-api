using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Modelos
{
    public class GetInteressadoPessoaJuridicaDto
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string Sigla { get; set; }
        public string GuidMunicipio { get; set; }
        public string NomeUnidade { get; set; }
        public string SiglaUnidade { get; set; }
        public List<GetContatoDto> Contatos { get; set; }
        public List<GetEmailDto> Emails { get; set; }

    }

    public class PostInteressadoPessoaJuridicaDto
    {
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string Sigla { get; set; }
        public string GuidMunicipio { get; set; }
        public string NomeUnidade { get; set; }
        public string SiglaUnidade { get; set; }
        public List<PostContatoDto> Contatos { get; set; }
        public List<PostEmailDto> Emails { get; set; }
    }

    public class PatchInteressadoPessoaJuridicaDto
    {
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string Sigla { get; set; }
        public string GuidMunicipio { get; set; }
        public string NomeUnidade { get; set; }
        public string SiglaUnidade { get; set; }

    }
}
