using ProcessoEletronicoService.Dominio.Base;
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
            this.repositorioProcessos = repositorios.Processos;
            this.repositorioAtividades = repositorios.Atividades;
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
            InteressadoPreenchido(processo);
            MunicipioPreenchido(processo);
            GuidOrganizacaoAutuadoraPreenchido(processo);
            GuidUnidadeAutuadoraPreenchida(processo);

            /*Preenchimento de objetos associados ao processo*/
            interessadoPessoaFisicaValidacao.Preenchido(processo.InteressadosPessoaFisica);
            interessadoPessoaJuridicaValidacao.Preenchido(processo.InteressadosPessoaJuridica);
            anexoValidacao.Preenchido(processo.Anexos);
            municipioValidacao.Preenchido(processo.MunicipiosProcesso);
            sinalizacaoValidacao.IdValido(processo.Sinalizacoes);

        }

        /*Atividade*/
        internal void AtividadePreenchida(ProcessoModeloNegocio processo)
        {

            if (processo.Atividade == null)
            {
                throw new RequisicaoInvalidaException("Atividade não preenchida.");
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
                throw new RequisicaoInvalidaException("Resumo não preenchido.");
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
                throw new RequisicaoInvalidaException("O processo deve possuir ao menos um interessado.");
            }
        }

        /*Municípios*/
        internal void MunicipioPreenchido(ProcessoModeloNegocio processo)
        {
            if (processo.MunicipiosProcesso.Count == 0)
            {
                throw new RequisicaoInvalidaException("Município não preenchido.");
            }
        }

        /*Órgao Autuador*/
        internal void GuidOrganizacaoAutuadoraPreenchido(ProcessoModeloNegocio processo)
        {
            if (string.IsNullOrWhiteSpace(processo.GuidOrganizacaoAutuadora))
            {
                throw new RequisicaoInvalidaException("Identificador do organização autuadora não preenchido.");
            }
        }

        /*Unidade Autuadora*/
        internal void GuidUnidadeAutuadoraPreenchida(ProcessoModeloNegocio processo)
        {
            if (string.IsNullOrWhiteSpace(processo.GuidUnidadeAutuadora))
            {
                throw new RequisicaoInvalidaException("Identificador da Unidade Autuadora não preenchido.");
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
            UfMunicipioValida(processo);
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

        internal void UfMunicipioValida(ProcessoModeloNegocio processo)
        {
            foreach (MunicipioProcessoModeloNegocio municipio in processo.MunicipiosProcesso)
            {
                if (municipio.Uf.Length != 2)
                {
                    throw new RequisicaoInvalidaException("Uf do município deve conter 2 dígitos");
                }
            }
        }

        private void GuidOrganizacaoValido(ProcessoModeloNegocio processo)
        {
            try
            {
                Guid guid = new Guid(processo.GuidOrganizacaoAutuadora);
            }
            catch (Exception)
            {
                throw new RequisicaoInvalidaException("Guid da Organização autuadora inválido");
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
                throw new RequisicaoInvalidaException("Guid da Unidade autuadora inválido");
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


        #endregion

    }
}
