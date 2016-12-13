using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProcessoEletronicoService.Negocio.Validacao
{
    public class UsuarioValidacao
    {
        public void Autenticado(string cpfUsuario, string nomeUsuario)
        {
            if (string.IsNullOrWhiteSpace(cpfUsuario) || string.IsNullOrWhiteSpace(nomeUsuario))
            {
                throw new RequisicaoInvalidaException("Usuário não autenticado.");
            }
        }

        public void PossuiOrganizaoPatriarca (Guid organizacaoPatriarca)
            
        {
            if (organizacaoPatriarca.Equals(Guid.Empty))
            {
                throw new RequisicaoInvalidaException("Usuário não possui organização patriarca.");
            }
        }

        internal void PodeAutuarProcessoNaOrganizacao(ProcessoModeloNegocio processoNegocio, Guid usuarioGuidOrganizacao)
        {

            if (!usuarioGuidOrganizacao.Equals(new Guid(processoNegocio.GuidOrganizacaoAutuadora)))
                throw new RequisicaoInvalidaException("Usuário não possui permissão para autuar processo para a organização informada.");
        }
    }
}
