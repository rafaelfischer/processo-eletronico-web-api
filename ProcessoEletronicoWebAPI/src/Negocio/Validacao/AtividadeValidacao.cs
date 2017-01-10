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
    public class AtividadeValidacao
    {
        IRepositorioGenerico<Atividade> repositorioAtividades;
        IRepositorioGenerico<Processo> repositorioProcessos;
        IRepositorioGenerico<Funcao> repositorioFuncoes;

        public AtividadeValidacao(IProcessoEletronicoRepositorios repositorios)
        {
            repositorioAtividades = repositorios.Atividades;
            repositorioFuncoes = repositorios.Funcoes;
            repositorioProcessos = repositorios.Processos;
            
        }
        
        internal void Preenchido(AtividadeModeloNegocio atividadeModeloNegocio)
        {
            CodigoPreenchido(atividadeModeloNegocio);
            DescricaoPreenchida(atividadeModeloNegocio);
            FuncaoPreenchida(atividadeModeloNegocio);
        }
        
        internal void NaoEncontrado(Atividade atividade)
        {
            if (atividade == null)
            {
                throw new RecursoNaoEncontradoException("Atividade não encontrada.");
            }
        }

        private void CodigoPreenchido(AtividadeModeloNegocio atividadeModeloNegocio)
        {
            if (string.IsNullOrWhiteSpace(atividadeModeloNegocio.Codigo))
            {
                throw new RequisicaoInvalidaException("Código não preenchido.");
            }
        }
        
        private void DescricaoPreenchida(AtividadeModeloNegocio atividadeModeloNegocio)
        {
            if (string.IsNullOrWhiteSpace(atividadeModeloNegocio.Descricao))
            {
                throw new RequisicaoInvalidaException("Descrição não preenchida.");
            }
        }
        
        private void FuncaoPreenchida(AtividadeModeloNegocio atividadeModeloNegocio)
        {
            if (atividadeModeloNegocio.Funcao == null || atividadeModeloNegocio.Funcao.Id <= 0)
            {
                throw new RequisicaoInvalidaException("Função da atividade não informada.");
            }

        }

        internal void Valido(AtividadeModeloNegocio atividadeModeloNegocio, Guid guidOrganizacaoUsuario)
        {
            FuncaoExistente(atividadeModeloNegocio, guidOrganizacaoUsuario);
            CodigoExistente(atividadeModeloNegocio);
            DescricaoExistente(atividadeModeloNegocio);
        }

        private void CodigoExistente(AtividadeModeloNegocio atividadeModeloNegocio)
        {
            //Dentro de uma mesma função, não pode haver atividades com códigos iguais
            if (repositorioAtividades.Where(a => a.Codigo == atividadeModeloNegocio.Codigo).Where(a => a.Funcao.Id == atividadeModeloNegocio.Funcao.Id).SingleOrDefault() != null) {
                throw new RequisicaoInvalidaException("Já existe uma atividade com esse código dentro da função informada.");
            }
        }

        private void DescricaoExistente(AtividadeModeloNegocio atividadeModeloNegocio)
        {
            if (repositorioAtividades.Where(a => a.Descricao == atividadeModeloNegocio.Descricao).Where(a => a.Funcao.Id == atividadeModeloNegocio.Funcao.Id).SingleOrDefault() != null) {
                throw new RequisicaoInvalidaException("Já existe uma atividade com essa descrição dentro da função informada.");
            }
        }

        private void FuncaoExistente(AtividadeModeloNegocio atividadeModeloNegocio, Guid guidOrganizacaoUsuario)
        {
            if (repositorioFuncoes.Where(f => f.Id == atividadeModeloNegocio.Funcao.Id).Where(f => f.PlanoClassificacao.GuidOrganizacao == guidOrganizacaoUsuario).SingleOrDefault() == null)
            {
                throw new RecursoNaoEncontradoException("Função não existe.");
            }
        }

        internal void PossivelExcluir(int id, Guid usuarioGuidOrganizacao)
        {
            Existe(id, usuarioGuidOrganizacao);
            ExistemProcessos(id);
        }

        private void Existe(int id, Guid guidOrganizacaoUsuario)
        {
            if (repositorioAtividades.Where(a => a.Id == id).Where(pc => pc.Funcao.PlanoClassificacao.GuidOrganizacao.Equals(guidOrganizacaoUsuario)).SingleOrDefault() == null)
            {
                throw new RecursoNaoEncontradoException("Atividade não encontrada.");
            }
        }

       
        private void ExistemProcessos(int id)
        {
            if (repositorioProcessos.Where(p => p.Atividade.Id == id).ToList().Count > 0)
            {
                throw new RequisicaoInvalidaException("Existem processos associados a essa atividade.");
            }
        }
        
    }
}
