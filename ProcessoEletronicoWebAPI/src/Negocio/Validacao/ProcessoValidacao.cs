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


        public ProcessoValidacao(IProcessoEletronicoRepositorios repositorios)
        {
            this.repositorioProcessos = repositorios.Processos;
            this.repositorioAtividades = repositorios.Atividades;
            interessadoPessoaFisicaValidacao = new InteressadoPessoaFisicaValidacao(repositorios);
            interessadoPessoaJuridicaValidacao = new InteressadoPessoaJuridicaValidacao(repositorios);
            municipioValidacao = new MunicipioValidacao();
            sinalizacaoValidacao = new SinalizacaoValidacao(repositorios);
        }

        #region Preechimento dos campos obrigatórios

        public void Preenchido(ProcessoModeloNegocio processo)
        {
            /*Preenchimentos dos campos do processo*/
            AtividadePreenchida(processo);
            ResumoPreechido(processo);
            InteressadoPreenchido(processo);
            MunicipioPreenchido(processo);
            IdOrgaoAutuadorPreenchido(processo);
            OrgaoAutuadorPreenchido(processo);
            SiglaOrgaoAutuadorPreenchido(processo);
            IdUnidadeAutuadoraPreenchida(processo);
            UnidadeAutuadoraPreenchida(processo);
            SiglaUnidadeAutuadoraPreenchida(processo);
            IdUsuarioAutuadorPreenchido(processo);
            UsuarioAutuadoroPreenchido(processo);

            /*Preenchimento de objetos associados ao processo*/
            interessadoPessoaFisicaValidacao.Preenchido(processo.InteressadosPessoaFisica);
            interessadoPessoaJuridicaValidacao.Preenchido(processo.InteressadosPessoaJuridica);
            municipioValidacao.Preenchido(processo.MunicipiosProcesso);
            sinalizacaoValidacao.IdValido(processo.Sinalizacoes);

            
            //Validar Anexo
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
            if (processo.InteressadosPessoaFisica.Count + processo.InteressadosPessoaJuridica.Count == 0)
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
        internal void IdOrgaoAutuadorPreenchido(ProcessoModeloNegocio processo)
        {
            if (processo.IdOrgaoAutuador <= 0)
            {
                throw new RequisicaoInvalidaException("Identificador do Órgão Autuador não preenchido.");
            }
        }

        internal void OrgaoAutuadorPreenchido(ProcessoModeloNegocio processo)
        {
            if (string.IsNullOrWhiteSpace(processo.NomeOrgaoAutuador))
            {
                throw new RequisicaoInvalidaException("Órgão Autuador não preenchido.");
            }
        }

        internal void SiglaOrgaoAutuadorPreenchido(ProcessoModeloNegocio processo)
        {
            if (string.IsNullOrWhiteSpace(processo.SiglaOrgaoAutuador))
            {
                throw new RequisicaoInvalidaException("Sigla do Órgão Autuador não preenchida.");
            }
        }

        /*Unidade Autuadora*/
        internal void IdUnidadeAutuadoraPreenchida(ProcessoModeloNegocio processo)
        {
            if (processo.IdUnidadeAutuadora <= 0)
            {
                throw new RequisicaoInvalidaException("Identificador da Unidade Autuadora não preenchido.");
            }
        }

        internal void UnidadeAutuadoraPreenchida(ProcessoModeloNegocio processo)
        {
            if (string.IsNullOrWhiteSpace(processo.NomeUnidadeAutuadora))
            {
                throw new RequisicaoInvalidaException("Unidade Autuadora não preenchida.");
            }
        }
        internal void SiglaUnidadeAutuadoraPreenchida(ProcessoModeloNegocio processo)
        {
            if (string.IsNullOrWhiteSpace(processo.SiglaUnidadeAutuadora))
            {
                throw new RequisicaoInvalidaException("Sigla da Unidade Autuadora não preenchida.");
            }
        }


        /*Usuário Autuador*/
        internal void IdUsuarioAutuadorPreenchido(ProcessoModeloNegocio processo)
        {
            if (string.IsNullOrWhiteSpace(processo.IdUsuarioAutuador))
            {
                throw new RequisicaoInvalidaException("Identificador da Usuário Autuador não preenchido.");
            }
        }

        internal void UsuarioAutuadoroPreenchido(ProcessoModeloNegocio processo)
        {
            if (string.IsNullOrWhiteSpace(processo.NomeUsuarioAutuador))
            {
                throw new RequisicaoInvalidaException("Usuário Autuador não preenchido.");
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
            UfMunicipioValida(processo);
            SiglaOrgaoAutuadorValido(processo);
            SiglaUnidadeAutuadoraValida(processo);

        }

        internal void AtividadeExistente(ProcessoModeloNegocio processo)
        {
            if (repositorioAtividades.Where(a => a.Id == processo.Atividade.Id).SingleOrDefault() == null)
            {
                throw new RecursoNaoEncontradoException("Atividade não existente.");
            }

        }

        internal void UfMunicipioValida (ProcessoModeloNegocio processo)
        {
           foreach (MunicipioProcessoModeloNegocio municipio in processo.MunicipiosProcesso)
            {
                if (municipio.Uf.Length != 2)
                {
                    throw new RequisicaoInvalidaException("Uf do município deve conter 2 dígitos");
                }
            }
        }

        internal void SiglaOrgaoAutuadorValido(ProcessoModeloNegocio processo)
        {
            if (processo.SiglaOrgaoAutuador.Length > 10)
            {
                throw new RequisicaoInvalidaException("Sigla do órgão autuador deve conter no máximo 10 caracteres");
            }
        }

        internal void SiglaUnidadeAutuadoraValida(ProcessoModeloNegocio processo)
        {
            if (processo.SiglaUnidadeAutuadora.Length > 10)
            {
                throw new RequisicaoInvalidaException("Sigla da unidade autuadora deve conter no máximo 10 caracteres");
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
