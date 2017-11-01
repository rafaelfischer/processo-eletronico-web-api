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
            Anexos = new HashSet<AnexoRascunho>();
            InteressadosPessoaFisica = new HashSet<InteressadoPessoaFisicaRascunho>();
            InteressadosPessoaJuridica = new HashSet<InteressadoPessoaJuridicaRascunho>();
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
        public Guid? GuidUnidade { get; set; }

        public virtual ICollection<AnexoRascunho> Anexos { get; set; }
        public virtual Atividade Atividade { get; set; }
        public virtual ICollection<InteressadoPessoaFisicaRascunho> InteressadosPessoaFisica { get; set; }
        public virtual ICollection<InteressadoPessoaJuridicaRascunho> InteressadosPessoaJuridica { get; set; }
        public virtual List<MunicipioRascunhoProcesso> MunicipiosRascunhoProcesso { get; set; }
        public virtual OrganizacaoProcesso OrganizacaoRascunhoProcesso { get; set; }
        public virtual ICollection<SinalizacaoRascunhoProcesso> SinalizacoesRascunhoProcesso { get; set; }
    }
}
