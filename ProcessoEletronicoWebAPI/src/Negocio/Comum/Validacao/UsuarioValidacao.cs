using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Modelos;
using System;

namespace ProcessoEletronicoService.Negocio.Comum.Validacao
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
            Guid guidOrganizacaoAutuadora = new Guid(processoNegocio.GuidOrganizacaoAutuadora);
            if (!usuarioGuidOrganizacao.Equals(guidOrganizacaoAutuadora))
                throw new RequisicaoInvalidaException("Usuário não possui permissão para autuar processo para a organização informada.");
        }

        internal void PodeSalvarProcessoNaOrganizacao(RascunhoProcessoModeloNegocio rascunhoProcessoNegocio, Guid usuarioGuidOrganizacao)
        {
            Guid guidOrganizacao = new Guid(rascunhoProcessoNegocio.GuidOrganizacao);

            if (!usuarioGuidOrganizacao.Equals(guidOrganizacao))
                throw new RequisicaoInvalidaException("Usuário não possui permissão para salvar processo para a organização informada.");
        }
    }
}
