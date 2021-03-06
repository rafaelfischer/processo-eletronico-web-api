﻿using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Validacao
{
    public class ProcessoValidacao
    {
        IRepositorioGenerico<Processo> repositorioProcessos;
        IRepositorioGenerico<Atividade> repositorioAtividades;
        InteressadoPessoaFisicaValidacao interessadoPessoaFisicaValidacao;
        InteressadoPessoaJuridicaValidacao interessadoPessoaJuridicaValidacao;
        MunicipioValidacao municipioValidacao;
        SinalizacaoValidacao sinalizacaoValidacao;
        AnexoValidacao anexoValidacao;

        public ProcessoValidacao(IProcessoEletronicoRepositorios repositorios)
        {
            repositorioProcessos = repositorios.Processos;
            repositorioAtividades = repositorios.Atividades;
            interessadoPessoaFisicaValidacao = new InteressadoPessoaFisicaValidacao(repositorios);
            interessadoPessoaJuridicaValidacao = new InteressadoPessoaJuridicaValidacao(repositorios);
            municipioValidacao = new MunicipioValidacao();
            sinalizacaoValidacao = new SinalizacaoValidacao(repositorios);
            anexoValidacao = new AnexoValidacao(repositorios);
        }

        public void NaoEncontrado(Processo processo)
        {
            if (processo == null)
            {
                throw new RecursoNaoEncontradoException("Processo não encontrado.");
            }
        }

        #region Preechimento dos campos obrigatórios

        public void Preenchido(ProcessoModeloNegocio processo)
        {
            /*Preenchimentos dos campos do processo*/
            AtividadePreenchida(processo);
            ResumoPreechido(processo);
            GuidOrganizacaoAutuadoraPreenchido(processo);
            GuidUnidadeAutuadoraPreenchida(processo);
            InteressadoPreenchido(processo);
            MunicipioPreenchido(processo);

            /*Preenchimento de objetos associados ao processo*/
            interessadoPessoaFisicaValidacao.Preenchido(processo.InteressadosPessoaFisica);
            interessadoPessoaJuridicaValidacao.Preenchido(processo.InteressadosPessoaJuridica);
            anexoValidacao.Preenchido(processo.Anexos);
            municipioValidacao.Preenchido(processo.Municipios);
            sinalizacaoValidacao.IdValido(processo.Sinalizacoes);

        }

        /*Atividade*/
        internal void AtividadePreenchida(ProcessoModeloNegocio processo)
        {

            if (processo.Atividade == null)
            {
                throw new RequisicaoInvalidaException("Dados Básicos: Atividade não preenchida.");
            }

            if (processo.Atividade.Id <= 0)
            {
                throw new RequisicaoInvalidaException("Atividade não preenchida.");
            }
        }

        /*Campos texto*/

        internal void ResumoPreechido(ProcessoModeloNegocio processo)
        {
            if (string.IsNullOrWhiteSpace(processo.Resumo))
            {
                throw new RequisicaoInvalidaException("Dados Básicos: Resumo não preenchido.");
            }
        }

        /*Interessados*/

        internal void InteressadoPreenchido(ProcessoModeloNegocio processo)
        {
            int countInteressadoPessoaFisica = 0;
            int countInteressadoPessoaJuridica = 0;

            if (processo.InteressadosPessoaFisica != null)
            {
                countInteressadoPessoaFisica = processo.InteressadosPessoaFisica.Count;
            }

            if (processo.InteressadosPessoaJuridica != null)
            {
                countInteressadoPessoaJuridica = processo.InteressadosPessoaJuridica.Count;
            }

            if (countInteressadoPessoaFisica + countInteressadoPessoaJuridica == 0)
            {
                throw new RequisicaoInvalidaException("Interessados: O processo deve possuir ao menos um interessado.");
            }
        }

        /*Municípios*/
        internal void MunicipioPreenchido(ProcessoModeloNegocio processo)
        {
            if (processo.Municipios.Count == 0)
            {
                throw new RequisicaoInvalidaException("Abrangência: Pelo menos um município deve ser informado.");
            }
        }

        /*Órgao Autuador*/
        internal void GuidOrganizacaoAutuadoraPreenchido(ProcessoModeloNegocio processo)
        {
            if (string.IsNullOrWhiteSpace(processo.GuidOrganizacaoAutuadora))
            {
                throw new RequisicaoInvalidaException("Dados Básicos: Identificador do Organização autuadora não preenchido.");
            }
        }

        /*Unidade Autuadora*/
        internal void GuidUnidadeAutuadoraPreenchida(ProcessoModeloNegocio processo)
        {
            if (string.IsNullOrWhiteSpace(processo.GuidUnidadeAutuadora))
            {
                throw new RequisicaoInvalidaException("Dados Básicos: Identificador da Unidade Autuadora não preenchido.");
            }
        }

        #endregion

        #region Validação dos campos

        public void Valido(ProcessoModeloNegocio processo)
        {
            AtividadeExistente(processo);
            interessadoPessoaFisicaValidacao.Valido(processo.InteressadosPessoaFisica);
            interessadoPessoaJuridicaValidacao.Valido(processo.InteressadosPessoaJuridica);
            sinalizacaoValidacao.SinalizacaoExistente(processo.Sinalizacoes);
            anexoValidacao.Valido(processo.Anexos, processo.Atividade.Id);
            municipioValidacao.Valido(processo.Municipios);
            GuidOrganizacaoValido(processo);
            GuidUnidadeValido(processo);

        }

        internal void AtividadeExistente(ProcessoModeloNegocio processo)
        {
            if (repositorioAtividades.Where(a => a.Id == processo.Atividade.Id).SingleOrDefault() == null)
            {
                throw new RecursoNaoEncontradoException("Atividade não existente.");
            }

        }

        private void GuidOrganizacaoValido(ProcessoModeloNegocio processo)
        {
            try
            {
                Guid guid = new Guid(processo.GuidOrganizacaoAutuadora);
            }
            catch (FormatException)
            {
                throw new RequisicaoInvalidaException("Guid da Organização autuadora inválido.");
            }
        }

        private void GuidUnidadeValido(ProcessoModeloNegocio processo)
        {
            try
            {
                Guid guid = new Guid(processo.GuidUnidadeAutuadora);
            }
            catch (Exception)
            {
                throw new RequisicaoInvalidaException("Guid da Unidade autuadora inválido.");
            }
        }

        internal void NumeroValido(string numero)
        {
            if (numero == null)
                throw new RequisicaoInvalidaException("Número do processo inválido.");

            string[] primeiraDivisao = numero.Split('-');
            if (primeiraDivisao == null || primeiraDivisao.Length != 2)
                throw new RequisicaoInvalidaException("Número do processo inválido.");

            string[] segundaDivisao = primeiraDivisao[1].Split('.');
            if (segundaDivisao == null || segundaDivisao.Length != 5)
                throw new RequisicaoInvalidaException("Número do processo inválido.");
        }

        internal void SequencialValido(string stringSequencial)
        {
            int sequencial;
            if (!Int32.TryParse(stringSequencial, out sequencial))
                throw new RequisicaoInvalidaException("Número do processo inválido.");
        }

        internal void DigitoVerificadorValido(string stringDigitoVerificador)
        {
            int digitoVerificador;
            if (!Int32.TryParse(stringDigitoVerificador, out digitoVerificador))
                throw new RequisicaoInvalidaException("Número do processo inválido.");
        }

        internal void DigitoVerificadorValido(byte digitoVerificadorRecebido, byte digitoVerificadorGerado)
        {
            if (digitoVerificadorRecebido != digitoVerificadorGerado)
                throw new RequisicaoInvalidaException("Número do processo inválido.");
        }

        internal void AnoValido(string stringAno)
        {
            short ano;
            if (!Int16.TryParse(stringAno, out ano))
                throw new RequisicaoInvalidaException("Número do processo inválido.");
        }

        internal void DigitoPoderValido(string stringDigitoPoder)
        {
            byte digitoPoder;
            if (!Byte.TryParse(stringDigitoPoder, out digitoPoder))
                throw new RequisicaoInvalidaException("Número do processo inválido.");
        }

        internal void DigitoEsferaValido(string stringDigitoEsfera)
        {
            byte digitoEsfera;
            if (!Byte.TryParse(stringDigitoEsfera, out digitoEsfera))
                throw new RequisicaoInvalidaException("Número do processo inválido.");
        }

        internal void DigitoOrganizacaoValido(string stringDigitoOrganizacao)
        {
            short digitoOrganizacao;
            if (!Int16.TryParse(stringDigitoOrganizacao, out digitoOrganizacao))
                throw new RequisicaoInvalidaException("Número do processo inválido.");
        }

        internal void AtividadePertenceAOrganizacaoPatriarca(ProcessoModeloNegocio processoNegocio, Guid usuarioGuidOrganizacaoPatriarca)
        {
            Atividade atividade = repositorioAtividades.Where(a => a.Id == processoNegocio.Atividade.Id
                                                                && a.Funcao.PlanoClassificacao.OrganizacaoProcesso.GuidOrganizacao.Equals(usuarioGuidOrganizacaoPatriarca))
                                                       .SingleOrDefault();

            if (atividade == null)
                throw new RequisicaoInvalidaException("A atividade informada não pertence à organização patriarca da organização autuadora.");
        }

        internal void SinalizacoesPertencemAOrganizacaoPatriarca(ProcessoModeloNegocio processoNegocio, Guid usuarioGuidOrganizacaoPatriarca)
        {
            if (processoNegocio.Sinalizacoes != null)
            {
                foreach (SinalizacaoModeloNegocio sinalizacao in processoNegocio.Sinalizacoes)
                {
                    sinalizacaoValidacao.SinalizacoesPertencemAOrganizacaoPatriarca(sinalizacao, usuarioGuidOrganizacaoPatriarca);
                }
            }
        }

        internal void AtividadePertenceAOrganizacao(ProcessoModeloNegocio processoNegocio)
        {
            Atividade atividade = repositorioAtividades.Where(a => a.Id == processoNegocio.Atividade.Id
                                                                && (a.Funcao.PlanoClassificacao.GuidOrganizacao.Equals(new Guid(processoNegocio.GuidOrganizacaoAutuadora))
                                                                || !a.Funcao.PlanoClassificacao.AreaFim))
                                                       .SingleOrDefault();

            if (atividade == null)
                throw new RequisicaoInvalidaException("A atividade informada não pertence à organização autuadora nem é de uma área meio.");
        }

        internal void UnidadePertenceAOrganizacao(Guid guidOrganizacaoUnidade, Guid guidOrganizacaoAutuadora)
        {
            if (!guidOrganizacaoUnidade.Equals(guidOrganizacaoAutuadora))
                throw new RequisicaoInvalidaException("A unidade autuadora informada não pertence à organização autuadora.");
        }

        #endregion

    }
}
