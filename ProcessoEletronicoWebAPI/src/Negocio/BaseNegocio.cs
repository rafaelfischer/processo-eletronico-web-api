using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio
{
    public class BaseNegocio : IBaseNegocio
    {
        private Dictionary<string, string> usuario;
        private string usuarioCpf;
        private string usuarioNome;
        private Guid usuarioGuidOrganizacao;
        private Guid usuarioGuidOrganizacaoPatriarca;

        public Dictionary<string, string> Usuario
        {
            get
            {
                return usuario;
            }

            set
            {
                usuario = value;
            }
        }

        public string UsuarioCpf
        {
            get
            {
                if (usuarioCpf == null)
                    usuarioCpf = Usuario["cpf"];

                return usuarioCpf;
            }
        }
        public string UsuarioNome
        {
            get
            {
                if (usuarioNome == null)
                    usuarioNome = Usuario["nome"];

                return usuarioNome;
            }
        }
        public Guid UsuarioGuidOrganizacao
        {
            get
            {
                if (usuarioGuidOrganizacao.Equals(Guid.Empty))
                {
                    string stringGuidOrganizacao = Usuario["guidOrganizacao"];

                    if (stringGuidOrganizacao != null)
                        usuarioGuidOrganizacao = new Guid(stringGuidOrganizacao);
                }

                return usuarioGuidOrganizacao;
            }
        }
        public Guid UsuarioGuidOrganizacaoPatriarca
        {
            get
            {
                if (usuarioGuidOrganizacaoPatriarca.Equals(Guid.Empty))
                {
                    string stringGuidOrganizacaoPatriarca = Usuario["guidOrganizacaoPatriarca"];

                    if (stringGuidOrganizacaoPatriarca != null)
                        usuarioGuidOrganizacaoPatriarca = new Guid(stringGuidOrganizacaoPatriarca);
                }

                return usuarioGuidOrganizacaoPatriarca;
            }
        }
    }
}
