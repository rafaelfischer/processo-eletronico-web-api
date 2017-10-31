using Apresentacao.APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services.Base
{
    public interface IRascunhoProcessoInteressadoService
    {
        InteressadoPessoaJuridicaViewModel PostInteressadoPJ(int idRascunho, InteressadoPessoaJuridicaViewModel interessado);
        InteressadoPessoaFisicaViewModel PostInteressadoPF(int idRascunho, InteressadoPessoaFisicaViewModel interessado);
    }
}
