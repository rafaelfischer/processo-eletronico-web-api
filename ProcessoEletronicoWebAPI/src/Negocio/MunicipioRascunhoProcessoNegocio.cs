using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using System;
using System.Collections.Generic;
using ProcessoEletronicoService.Negocio.Comum;

namespace ProcessoEletronicoService.Negocio
{
    public class MunicipioRascunhoProcessoNegocio : BaseNegocio, IMunicipioRascunhoProcesso
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<MunicipioRascunhoProcesso> repositorioMunicipiosRascunhoProcesso;

        public MunicipioRascunhoProcessoNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioMunicipiosRascunhoProcesso = repositorios.MunicipiosRascunhoProcesso;
        }

        public void Excluir(ICollection<MunicipioRascunhoProcesso> municipiosRascunhoProcesso)
        {
            if (municipiosRascunhoProcesso != null)
            {
                foreach(var municipioRascunhoProcesso in municipiosRascunhoProcesso)
                {
                    Excluir(municipioRascunhoProcesso);
                }
            }
        }

        public void Excluir(MunicipioRascunhoProcesso municipioRascunhoProcesso)
        {
            if (municipioRascunhoProcesso != null)
            {
                repositorioMunicipiosRascunhoProcesso.Remove(municipioRascunhoProcesso);
            }
        }
    }
}
