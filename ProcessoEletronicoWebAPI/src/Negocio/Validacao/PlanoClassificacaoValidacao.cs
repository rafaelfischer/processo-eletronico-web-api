using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Modelos;
using Prodest.ProcessoEletronico.Integration.Organograma.Models;
using System;
using System.Linq;


namespace ProcessoEletronicoService.Negocio.Validacao
{
    public class PlanoClassificacaoValidacao
    {
        IRepositorioGenerico<PlanoClassificacao> repositorioPlanosClassificacao;
        IRepositorioGenerico<Funcao> repositorioFuncoes;
        IRepositorioGenerico<Processo> repositorioProcessos;

        public PlanoClassificacaoValidacao(IProcessoEletronicoRepositorios repositorios)
        {
            repositorioPlanosClassificacao = repositorios.PlanosClassificacao;
            repositorioFuncoes = repositorios.Funcoes;
            repositorioProcessos = repositorios.Processos;
        }

        internal void GuidValido(string guid)
        {
            try
            {
                Guid g = new Guid(guid);

                if (g.Equals(Guid.Empty))
                    throw new RequisicaoInvalidaException("Identificador inválido.");
            }
            catch (FormatException)
            {
                throw new RequisicaoInvalidaException("Formato do identificador inválido.");
            }
        }

        internal void OrganizacaoPatriarcaExistente(Organizacao organizacaoPatriarca)
        {
            if (organizacaoPatriarca == null)
                throw new RecursoNaoEncontradoException("Organização patriarca não encontrada.");

        }

        internal void Preenchido(PlanoClassificacaoModeloNegocio planoClassificacaoModeloNegocio)
        {
            CodigoPreenchido(planoClassificacaoModeloNegocio);
            DescricaoPreenchida(planoClassificacaoModeloNegocio);
            GuidOrganizacaoPreenchido(planoClassificacaoModeloNegocio);
        }

        private void CodigoPreenchido(PlanoClassificacaoModeloNegocio planoClassificacaoModeloNegocio)
        {
            if (string.IsNullOrWhiteSpace(planoClassificacaoModeloNegocio.Codigo))
            {
                throw new RequisicaoInvalidaException("Código não preenchido.");
            }
        }

        private void DescricaoPreenchida(PlanoClassificacaoModeloNegocio planoClassificacaoModeloNegocio)
        {
            if (string.IsNullOrWhiteSpace(planoClassificacaoModeloNegocio.Descricao))
            {
                throw new RequisicaoInvalidaException("Descrição não preenchida.");
            }
        }

        private void GuidOrganizacaoPreenchido(PlanoClassificacaoModeloNegocio planoClassificacaoModeloNegocio)
        {
            if (string.IsNullOrWhiteSpace(planoClassificacaoModeloNegocio.GuidOrganizacao))
            {
                throw new RequisicaoInvalidaException("Identificador da organização não informado.");
            }
        }

        internal void Valido(PlanoClassificacaoModeloNegocio planoClassificacaoModeloNegocio)
        {
            GuidValido(planoClassificacaoModeloNegocio.GuidOrganizacao);
            DescricaoExistente(planoClassificacaoModeloNegocio);
            CodigoExistente(planoClassificacaoModeloNegocio);
        }

       

        private void CodigoExistente(PlanoClassificacaoModeloNegocio planoClassificacaoModeloNegocio)
        {
            Guid guidOrganizacao = new Guid(planoClassificacaoModeloNegocio.GuidOrganizacao);
            
            /*Dentro de uma organização, não pode haver planos de classificação com o mesmo código.*/
            if (repositorioPlanosClassificacao.Where(pc => pc.Codigo.Equals(planoClassificacaoModeloNegocio.Codigo))
                                               .Where(pc => pc.GuidOrganizacao.Equals(guidOrganizacao))
                                               .SingleOrDefault() != null)
            {
                throw new RequisicaoInvalidaException("Já existe um plano de classificação com este código na organização informada.");
            }
        }

        private void DescricaoExistente(PlanoClassificacaoModeloNegocio planoClassificacaoModeloNegocio)
        {
            string planoClassificacaoUpper = planoClassificacaoModeloNegocio.Descricao.ToUpper();

            if (repositorioPlanosClassificacao.Where(pc => pc.Descricao.ToUpper().Equals(planoClassificacaoUpper)).SingleOrDefault() != null)
            {
                throw new RequisicaoInvalidaException("Já existe um plano de classificação com esta descrição.");
            }



        }
        
        internal void Permissao(PlanoClassificacaoModeloNegocio planoClassificacaoModeloNegocio, Guid usuarioGuidOrganizacao)
        {
            if (!usuarioGuidOrganizacao.Equals(new Guid(planoClassificacaoModeloNegocio.GuidOrganizacao)))
            {
                throw new RequisicaoInvalidaException("Usuário não possui permissão para inserir na organização informada.");
            }
        }

        internal void PossivelExcluir(int id)
        {
            Existe(id);
            ExisteProcesso(id);
            ExisteFuncao(id);
        }

        private void Existe(int id)
        {
            if (repositorioPlanosClassificacao.Where(pc => pc.Id == id).SingleOrDefault() == null)
            {
                throw new RecursoNaoEncontradoException("Plano de classificação não encontrado.");
            }
        }

        private void ExisteProcesso(int id)
        {
            if (repositorioProcessos.Where(p => p.Atividade.Funcao.PlanoClassificacao.Id == id).ToList().Count > 0)
            {
                throw new RequisicaoInvalidaException("Existem processos associados a esse plano de classificação.");
            }
        }

        private void ExisteFuncao(int id)
        {
            if (repositorioFuncoes.Where(f => f.IdPlanoClassificacao == id).ToList().Count > 0)
            {
                throw new RequisicaoInvalidaException("Existem funções associadas a este plano de classificação.");
            }
        }


        internal void NaoEncontrado(PlanoClassificacao planoClassificacao)
        {
            if (planoClassificacao == null)
            {
                throw new RecursoNaoEncontradoException("Plano de Classificação não encontrado.");
            }
        }
    }
}
