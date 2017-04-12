using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Dominio.Modelos
{
    public class RascunhoProcesso
    {
        public RascunhoProcesso()
        {
            Anexos = new HashSet<Anexo>();
            InteressadosPessoaFisica = new HashSet<InteressadoPessoaFisica>();
            InteressadosPessoaJuridica = new HashSet<InteressadoPessoaJuridica>();
            SinalizacoesRascunhoProcesso = new HashSet<SinalizacaoRascunhoProcesso>();
        }

        public int Id { get; set; }
        public string Resumo { get; set; }
        public int? IdAtividade { get; set; }
        public string NomeOrganizacao { get; set; }
        public string SiglaOrganizacao { get; set; }
        public string NomeUnidade { get; set; }
        public string SiglaUnidade { get; set; }
        public int IdOrganizacaoProcesso { get; set; }
        public Guid GuidOrganizacao { get; set; }
        public Guid GuidUnidade { get; set; }

        public virtual ICollection<Anexo> Anexos { get; set; }
        public virtual Atividade Atividade { get; set; }
        public virtual ICollection<InteressadoPessoaFisica> InteressadosPessoaFisica { get; set; }
        public virtual ICollection<InteressadoPessoaJuridica> InteressadosPessoaJuridica { get; set; }
        public virtual ICollection<MunicipioRascunhoProcesso> MunicipiosRascunhoProcesso { get; set; }
        public virtual OrganizacaoProcesso OrganizacaoRascunhoProcesso { get; set; }
        public virtual ICollection<SinalizacaoRascunhoProcesso> SinalizacoesRascunhoProcesso { get; set; }
    }
}
