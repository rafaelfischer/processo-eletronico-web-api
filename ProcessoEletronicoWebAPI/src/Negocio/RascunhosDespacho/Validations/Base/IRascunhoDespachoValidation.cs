﻿using Negocio.RascunhosDespacho.Models;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Comum.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.RascunhosDespacho.Validations.Base
{
    public interface IRascunhoDespachoValidation : IBaseValidation<RascunhoDespachoModel, RascunhoDespacho>
    {
        void IsRascunhoDespachoOfUser(RascunhoDespacho rascunhoDespacho);
    }
}
