using ProcessoEletronicoService.Apresentacao.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Apresentacao.Modelos;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using AutoMapper;

namespace ProcessoEletronicoService.Apresentacao
{
    public class AnexoWorkService : BaseWorkService, IAnexoWorkService
    {
        IAnexoNegocio anexoNegocio;

        public AnexoWorkService(IAnexoNegocio anexoNegocio)
        {
            this.anexoNegocio = anexoNegocio;
        }

        public override void RaiseUsuarioAlterado()
        {
            anexoNegocio.Usuario = Usuario;
        }

        public AnexoModeloGet Pesquisar(int id)
        {
            AnexoModeloNegocio anexoModeloNegocio = new AnexoModeloNegocio();
            anexoModeloNegocio = anexoNegocio.Pesquisar(id);

            return Mapper.Map<AnexoModeloNegocio, AnexoModeloGet>(anexoModeloNegocio);
        }
        
    }
}
