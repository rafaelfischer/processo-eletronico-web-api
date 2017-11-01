using Apresentacao.APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services.Base
{
    public interface IRascunhoProcessoInteressadoService
    {
        InteressadoPessoaJuridicaViewModel PostInteressadoPJ(int idRascunho, OrganizacaoViewModel organizacaoInteressada);
        InteressadoPessoaFisicaViewModel PostInteressadoPF(int idRascunho, InteressadoPessoaFisicaViewModel interessado);
        List<InteressadoPessoaFisicaViewModel> GetInteressadosPF(int idRascunho);
        List<InteressadoPessoaJuridicaViewModel> GetInteressadosPJ(int idRascunho);
    }
}
