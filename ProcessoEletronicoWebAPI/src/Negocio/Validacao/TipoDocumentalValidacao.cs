using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Restrito.Validacao
{
    public class TipoDocumentalValidacao
    {
        IRepositorioGenerico<Anexo> repositorioAnexos;
        IRepositorioGenerico<Atividade> repositorioAtividades;
        IRepositorioGenerico<DestinacaoFinal> repositorioDestinacoesFinais;
        IRepositorioGenerico<TipoDocumental> repositorioTiposDocumentais;
        
        public TipoDocumentalValidacao(IProcessoEletronicoRepositorios repositorios)
        {
            repositorioAnexos = repositorios.Anexos;
            repositorioAtividades = repositorios.Atividades;
            repositorioDestinacoesFinais = repositorios.DestinacoesFinais;
            repositorioTiposDocumentais = repositorios.TiposDocumentais;
        }

        internal void Preenchido(TipoDocumentalModeloNegocio tipoDocumentalModeloNegocio)
        {
            AtividadePrenchida(tipoDocumentalModeloNegocio);
            CodigoPreenchido(tipoDocumentalModeloNegocio);
            DescricaoPrenchida(tipoDocumentalModeloNegocio);
            DestinacaoFinalPreenchida(tipoDocumentalModeloNegocio);
            PrazoGuardaCorrentePreenchido(tipoDocumentalModeloNegocio);
            PrazoGuardaIntermediariaPreenchido(tipoDocumentalModeloNegocio);
        }

        private void AtividadePrenchida(TipoDocumentalModeloNegocio tipoDocumentalModeloNegocio)
        {
            if (tipoDocumentalModeloNegocio.Atividade == null || tipoDocumentalModeloNegocio.Atividade.Id <= 0)
            {
                throw new RequisicaoInvalidaException("Atividade não preenchida.");
            }
        }

        private void CodigoPreenchido(TipoDocumentalModeloNegocio tipoDocumentalModeloNegocio)
        {
            if (string.IsNullOrWhiteSpace(tipoDocumentalModeloNegocio.Codigo))
            {
                throw new RequisicaoInvalidaException("Código não preenchido.");
            }
        }

        private void DescricaoPrenchida(TipoDocumentalModeloNegocio tipoDocumentalModeloNegocio)
        {
            if (string.IsNullOrWhiteSpace(tipoDocumentalModeloNegocio.Descricao))
            {
                throw new RequisicaoInvalidaException("Descrição não preenchida.");
            }
        }

        private void DestinacaoFinalPreenchida(TipoDocumentalModeloNegocio tipoDocumentalModeloNegocio)
        {
            if (tipoDocumentalModeloNegocio.DestinacaoFinal == null || tipoDocumentalModeloNegocio.DestinacaoFinal.Id <= 0)
            {
                throw new RequisicaoInvalidaException("Destinação final não preenchida.");
            }
        }

        private void PrazoGuardaCorrentePreenchido(TipoDocumentalModeloNegocio tipoDocumentalModeloNegocio)
        {
            //Prazo de guarda de fase corrente: dos prazos em anos ou o subjetivo, apenas um deles deve estar preenchido. Caso os dois estejam preenchidos ou nenhum deles, a requisição está inválida.
            if ((string.IsNullOrWhiteSpace(tipoDocumentalModeloNegocio.PrazoGuardaSubjetivoCorrente) && !tipoDocumentalModeloNegocio.PrazoGuardaAnosCorrente.HasValue) 
             || (!string.IsNullOrWhiteSpace(tipoDocumentalModeloNegocio.PrazoGuardaSubjetivoCorrente) && tipoDocumentalModeloNegocio.PrazoGuardaAnosCorrente.HasValue && tipoDocumentalModeloNegocio.PrazoGuardaAnosCorrente.Value > 0))
            {
                throw new RequisicaoInvalidaException("O prazo de guarda de fase corrente deve ser informado ou em anos ou de forma subjetiva.");
            }
        }

        private void PrazoGuardaIntermediariaPreenchido(TipoDocumentalModeloNegocio tipoDocumentalModeloNegocio)
        {
            //Prazo de guarda de fase intermediaria: dos prazos em anos ou o subjetivo, apenas um deles deve estar preenchido. Caso os dois estejam preenchidos ou nenhum deles, a requisição está inválida.
            if ((string.IsNullOrWhiteSpace(tipoDocumentalModeloNegocio.PrazoGuardaSubjetivoIntermediaria) && !tipoDocumentalModeloNegocio.PrazoGuardaAnosIntermediaria.HasValue)
             || (!string.IsNullOrWhiteSpace(tipoDocumentalModeloNegocio.PrazoGuardaSubjetivoIntermediaria) && tipoDocumentalModeloNegocio.PrazoGuardaAnosIntermediaria.HasValue && tipoDocumentalModeloNegocio.PrazoGuardaAnosIntermediaria.Value > 0))
            {
                throw new RequisicaoInvalidaException("O prazo de guarda de fase intermediára deve ser informado ou em anos ou de forma subjetiva.");
            }
        }

        internal void Valido (TipoDocumentalModeloNegocio tipoDocumentalModeloNegocio, Guid usuarioGuidOrganizacao)
        {
            AtividadeExiste(tipoDocumentalModeloNegocio, usuarioGuidOrganizacao);
            CodigoExiste(tipoDocumentalModeloNegocio);
            DescricaoExiste(tipoDocumentalModeloNegocio);
            DestinacaoFinalExiste(tipoDocumentalModeloNegocio);

        }


        private void AtividadeExiste(TipoDocumentalModeloNegocio tipoDocumentalModeloNegocio, Guid usuarioGuidOrganizacao)
        {
            if (repositorioAtividades.Where(a => a.Id == tipoDocumentalModeloNegocio.Atividade.Id)
                                     .Where(a => a.Funcao.PlanoClassificacao.GuidOrganizacao.Equals(usuarioGuidOrganizacao)).SingleOrDefault() == null)
            {
                throw new RecursoNaoEncontradoException("Atividade não encontrada.");
            }
        }

        private void CodigoExiste(TipoDocumentalModeloNegocio tipoDocumentalModeloNegocio)
        {
            if (repositorioTiposDocumentais.Where(td => td.Codigo.Equals(tipoDocumentalModeloNegocio.Codigo))
                                           .Where(td => td.Atividade.Id == tipoDocumentalModeloNegocio.Atividade.Id)
                                           .SingleOrDefault() != null)
            {
                throw new RequisicaoInvalidaException("Já existe um tipo documental com esse código na atividade informada.");
            }
        }


        //Dentro de uma mesma atividade, não pode haver tipos documentais com o mesmo nome.
        internal void DescricaoExiste(TipoDocumentalModeloNegocio tipoDocumentalModeloNegocio)
        {
            if (repositorioTiposDocumentais.Where(td => td.Descricao.Equals(tipoDocumentalModeloNegocio.Descricao))
                                           .Where(td => td.Atividade.Id == tipoDocumentalModeloNegocio.Atividade.Id)
                                           .SingleOrDefault() != null)
            {
                throw new RequisicaoInvalidaException("Já existe um tipo documental com esse nome na atividade informada.");
            }
        }

        private void DestinacaoFinalExiste (TipoDocumentalModeloNegocio tipoDocumentalModeloNegocio)
        {
            if (repositorioDestinacoesFinais.Where(df => df.Id == tipoDocumentalModeloNegocio.DestinacaoFinal.Id).SingleOrDefault() == null)
            {
                throw new RecursoNaoEncontradoException("Destinação Final não encontrada");
            }
        }

        internal void IdExistente(int id)
        {
            var tdocumental = repositorioTiposDocumentais.SingleOrDefault(td => td.Id == id);

            if (tdocumental == null)
                throw new RecursoNaoEncontradoException("Tipo Documental não encontrado.");
        }

        internal void PossivelExcluir(int id)
        {
            Existe(id);
            ExisteAnexo(id);
        }

        private void Existe(int id)
        {
            if (repositorioTiposDocumentais.Where(td => td.Id == id).SingleOrDefault() == null)
            {
                throw new RecursoNaoEncontradoException("Tipo Documental não encontrado.");
            }
        }

        private void ExisteAnexo(int id)
        {
            if (repositorioAnexos.Where(a => a.IdTipoDocumental == id).ToList().Count > 0)
            {
                throw new RequisicaoInvalidaException("Existem anexos associados a esse tipo documental.");
            }
        }

        internal void NaoEncontrado(TipoDocumental tipoDocumental)
        {
            if (tipoDocumental == null)
            {
                throw new RecursoNaoEncontradoException("Tipo Documental não encontrado.");
            }
        }
        
    }
}

