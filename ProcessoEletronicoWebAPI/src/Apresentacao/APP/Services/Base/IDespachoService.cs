﻿using Apresentacao.APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao.APP.Services.Base
{
    public interface IDespachoService
    {
        ResultViewModel<GetDespachoViewModel> Search(int idDespacho);
        ResultViewModel<GetDespachoViewModel> Despachar(int idProcesso, int idRascunhoDespacho);
    }
}
