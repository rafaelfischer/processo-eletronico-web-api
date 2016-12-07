﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Modelos
{
    public class DespachoModeloNegocio
    {

        public int Id { get; set; }
        public string Texto { get; set; }
        public int IdProcesso { get; set; }
        public int IdOrganizacaoDestino { get; set; }
        public string NomeOrganizacaoDestino { get; set; }
        public string SiglaOrganizacaoDestino { get; set; }
        public int IdUnidadeDestino { get; set; }
        public string NomeUnidadeDestino { get; set; }
        public string SiglaUnidadeDestino { get; set; }
        public string IdUsuarioDespachante { get; set; }
        public string NomeUsuarioDespachante { get; set; }
        public DateTime DataHoraDespacho { get; set; }
        

        public List<AnexoModeloNegocio> Anexos { get; set; }
        public ProcessoModeloNegocio Processo { get; set; }
    }
}
