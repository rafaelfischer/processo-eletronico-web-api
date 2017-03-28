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
    public class RascunhoProcessoValidacao
    {
        IRepositorioGenerico<Atividade> repositorioAtividades;
        IRepositorioGenerico<RascunhoProcesso> repositorioRascunhosProcesso;

        public RascunhoProcessoValidacao(IProcessoEletronicoRepositorios repositorios)
        {
            this.repositorioAtividades = repositorios.Atividades;
            this.repositorioRascunhosProcesso = repositorios.RascunhosProcesso;
        }

        public void NaoEncontrado(RascunhoProcesso rascunhoProcesso)
        {
            if (rascunhoProcesso == null)
            {
                throw new RecursoNaoEncontradoException("Rascunho de processo não encontrado");
            }
        }

        #region Preenchimento de campos obrigatórios
        public void Preenchido(RascunhoProcessoModeloNegocio rascunhoProcesso)
        {
        }
        #endregion

        #region Validação dos campos

        public void Valido(RascunhoProcessoModeloNegocio rascunhoProcesso, Guid guidOrganizacaoUsuario)
        {
            if (rascunhoProcesso.Atividade != null && rascunhoProcesso.Atividade.Id > 0)
            {
                if (repositorioAtividades.Where(a => a.Id == rascunhoProcesso.Atividade.Id).Where(pc => pc.Funcao.PlanoClassificacao.GuidOrganizacao.Equals(guidOrganizacaoUsuario)).SingleOrDefault() == null)
                {
                    throw new RecursoNaoEncontradoException("Atividade não encontrada.");
                }
            }
        }
        #endregion

    }
}
