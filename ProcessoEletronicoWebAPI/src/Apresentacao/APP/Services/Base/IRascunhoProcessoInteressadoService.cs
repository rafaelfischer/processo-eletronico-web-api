using Apresentacao.APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services.Base
{
    public interface IRascunhoProcessoInteressadoService
    {
        InteressadoPessoaJuridicaViewModel PostInteressadoPJ(int idRascunho, InteressadoPessoaJuridicaViewModel interessado);
        InteressadoPessoaJuridicaViewModel PostInteressadoPJOrganograma(int idRascunho, OrganizacaoViewModel organizacaoInteressada);
        InteressadoPessoaJuridicaViewModel PostInteressadoPJOrganograma(int idRascunho, OrganizacaoViewModel organizacaoInteressada, UnidadeViewModel unidadeInteressada);
        InteressadoPessoaFisicaViewModel PostInteressadoPF(int idRascunho, InteressadoPessoaFisicaViewModel interessado);
        List<InteressadoPessoaFisicaViewModel> GetInteressadosPF(int idRascunho);
        List<InteressadoPessoaJuridicaViewModel> GetInteressadosPJ(int idRascunho);
        void ExcluirInteressadoPJ(int idRascunho, int idInteressadoPJ);
    }
}
