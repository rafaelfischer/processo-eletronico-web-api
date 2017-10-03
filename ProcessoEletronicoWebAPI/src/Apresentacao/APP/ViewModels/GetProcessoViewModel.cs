using System;

namespace Apresentacao.APP.ViewModels
{
    public class GetProcessoViewModel
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Resumo { get; set; }
        public string NomeOrganizacaoAutuadora { get; set; }
        public string SiglaOrganizacaoAutuadora { get; set; }        
        public string NomeUnidadeAutuadora { get; set; }
        public string SiglaUnidadeAutuadora { get; set; }
        public DateTime DataAutuacao { get; set; }
        public DateTime DataUltimoTramite { get; set; }
        public AtividadeViewModel Atividade { get; set; }

        public string NomeSiglaOrganizacao
        {
            get
            {
                return SiglaOrganizacaoAutuadora + " - " + NomeOrganizacaoAutuadora;
            }
        }

        public string NomeSiglaUnidade
        {
            get
            {
                return SiglaUnidadeAutuadora + " - " + NomeUnidadeAutuadora;
            }
        }

        public string SiglasOrganizacaoUnidade
        {
            get
            {
                return SiglaOrganizacaoAutuadora + " - " + SiglaUnidadeAutuadora;
            }
        }
    }
}
