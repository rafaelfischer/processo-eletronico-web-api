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
    public class FuncaoValidacao
    {

        IRepositorioGenerico<Atividade> repositorioAtividades;
        IRepositorioGenerico<Funcao> repositorioFuncoes;
        IRepositorioGenerico<PlanoClassificacao> repositorioPlanosClassificacao;
        IRepositorioGenerico<Processo> repositorioProcessos;
        
        public FuncaoValidacao(IProcessoEletronicoRepositorios repositorios)
        {
            repositorioAtividades = repositorios.Atividades;
            repositorioFuncoes = repositorios.Funcoes;
            repositorioPlanosClassificacao = repositorios.PlanosClassificacao;
            repositorioProcessos = repositorios.Processos;
            
        }
        
        internal void Preenchido(FuncaoModeloNegocio funcaoModeloNegocio)
        {
            CodigoPreenchido(funcaoModeloNegocio);
            DescricaoPreenchida(funcaoModeloNegocio);
            PlanoClassificacaoPreenchido(funcaoModeloNegocio);
        }
        
        internal void NaoEncontrado(Funcao funcao)
        {
            if (funcao == null)
            {
                throw new RecursoNaoEncontradoException("Função não encontrada.");
            }
        }

        private void CodigoPreenchido(FuncaoModeloNegocio funcaoModeloNegocio)
        {
            if (string.IsNullOrWhiteSpace(funcaoModeloNegocio.Codigo))
            {
                throw new RequisicaoInvalidaException("Código não preenchido.");
            }
        }

       

        private void DescricaoPreenchida(FuncaoModeloNegocio funcaoModeloNegocio)
        {
            if (string.IsNullOrWhiteSpace(funcaoModeloNegocio.Descricao))
            {
                throw new RequisicaoInvalidaException("Descrição não preenchida.");
            }
        }

       

        private void PlanoClassificacaoPreenchido(FuncaoModeloNegocio funcaoModeloNegocio)
        {
            if (funcaoModeloNegocio.PlanoClassificacao == null || funcaoModeloNegocio.PlanoClassificacao.Id <= 0)
            {
                throw new RequisicaoInvalidaException("Plano de classificação da função não informado.");
            }


        }

        internal void Valido(FuncaoModeloNegocio funcaoModeloNegocio, Guid guidOrganizacaoUsuario)
        {
            PlanoClassificacaoExistente(funcaoModeloNegocio, guidOrganizacaoUsuario);
            CodigoExistente(funcaoModeloNegocio);
            DescricaoExistente(funcaoModeloNegocio);
            FuncaoPaiExistente(funcaoModeloNegocio, guidOrganizacaoUsuario);
         
        }

        private void CodigoExistente(FuncaoModeloNegocio funcaoModeloNegocio)
        {
            //Dentro de um mesmo plano de classificação, não pode haver funções com códigos iguais
            if (repositorioFuncoes.Where(f => f.Codigo == funcaoModeloNegocio.Codigo).Where(f => f.PlanoClassificacao.Id == funcaoModeloNegocio.PlanoClassificacao.Id).SingleOrDefault() != null) {
                throw new RequisicaoInvalidaException("Já existe uma função com esse código dentro do plano de classificação informado.");
            }
        }

        private void DescricaoExistente(FuncaoModeloNegocio funcaoModeloNegocio)
        {
            if (repositorioFuncoes.Where(f => f.Descricao == funcaoModeloNegocio.Descricao).Where(f => f.PlanoClassificacao.Id == funcaoModeloNegocio.PlanoClassificacao.Id).SingleOrDefault() != null) {
                throw new RequisicaoInvalidaException("Já existe uma função com essa descrição dentro do plano de classificação informado.");
            }
        }

        private void FuncaoPaiExistente(FuncaoModeloNegocio funcaoModeloNegocio, Guid guidOrganizacaoUsuario)
        {
            if (funcaoModeloNegocio.FuncaoPai != null)
            {
                if (funcaoModeloNegocio.FuncaoPai.Id > 0)
                {
                    if (repositorioFuncoes.Where(f => f.Id == funcaoModeloNegocio.FuncaoPai.Id).Where(pc => pc.PlanoClassificacao.GuidOrganizacao.Equals(guidOrganizacaoUsuario)).SingleOrDefault() == null)
                    {
                        throw new RecursoNaoEncontradoException("Função pai não encontrada.");
                    }
                }
            }
        }

        private void PlanoClassificacaoExistente(FuncaoModeloNegocio funcaoModeloNegocio, Guid guidOrganizacaoUsuario)
        {
            if (repositorioPlanosClassificacao.Where(pc => pc.Id == funcaoModeloNegocio.PlanoClassificacao.Id).Where(pc => pc.GuidOrganizacao == guidOrganizacaoUsuario).SingleOrDefault() == null)
            {
                throw new RecursoNaoEncontradoException("Plano de classificação não existe");
            }
        }

        internal void PossivelExcluir(int id, Guid usuarioGuidOrganizacao)
        {
            Existe(id, usuarioGuidOrganizacao);
            ExistemFilhas(id);
            ExistemProcessos(id);
            ExistemAtividades(id);
        }

        private void Existe(int id, Guid guidOrganizacaoUsuario)
        {
            if (repositorioFuncoes.Where(f => f.Id == id).Where(pc => pc.PlanoClassificacao.GuidOrganizacao.Equals(guidOrganizacaoUsuario)).SingleOrDefault() == null)
            {
                throw new RecursoNaoEncontradoException("Função não encontrada.");
            }
        }

        private void ExistemFilhas(int id)
        {
            if (repositorioFuncoes.Where(f => f.IdFuncaoPai == id).ToList().Count > 0)
            {
                throw new RequisicaoInvalidaException("Existes subfunções associadas a essa função.");
            }
        }

        private void ExistemProcessos(int id)
        {
            if (repositorioProcessos.Where(p => p.Atividade.Funcao.Id == id).ToList().Count > 0)
            {
                throw new RequisicaoInvalidaException("Existem processos associados a essa função.");
            }
        }

        private void ExistemAtividades(int id)
        {
            if (repositorioAtividades.Where(a => a.IdFuncao == id).ToList().Count > 0)
            {
                throw new RequisicaoInvalidaException("Existem atividades associadas a essa função.");
            }
        }


    }
}
