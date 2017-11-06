﻿using Apresentacao.APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services.Base
{
    public interface IRascunhoProcessoSinalizacaoService
    {
        List<SinalizacaoViewModel> GetSinalizacoes(int idRascunho);
        void PostSinalizacao(int idRascunho, IList<SinalizacaoViewModel> sinalizacoes);
    }
}
