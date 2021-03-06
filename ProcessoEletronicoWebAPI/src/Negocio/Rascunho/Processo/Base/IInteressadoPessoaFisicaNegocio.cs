﻿using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System.Collections.Generic;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Base
{
    public interface IInteressadoPessoaFisicaNegocio
    {
        IList<InteressadoPessoaFisicaModeloNegocio> Get(int idRascunhoProcesso);
        InteressadoPessoaFisicaModeloNegocio Get(int idRascunhoProcesso, int id);
        InteressadoPessoaFisicaModeloNegocio Post(int idRascunhoProcesso, InteressadoPessoaFisicaModeloNegocio interessadoPessoaFisicaNegocio);
        void Patch(int idRascunhoProcesso, int id, InteressadoPessoaFisicaModeloNegocio interessadoPessoaFisicaNegocio);
        void Delete(int idRascunhoProcesso, int id);

        void Delete(ICollection<InteressadoPessoaFisicaRascunho> interessadosPessoaFisica);
        void Delete(InteressadoPessoaFisicaRascunho interessadoPessoaFisica);
    }
}
