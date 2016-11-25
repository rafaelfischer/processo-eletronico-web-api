using AutoMapper;
using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Infraestrutura.Repositorios;
using ProcessoEletronicoService.Infraestrutura.Mapeamento;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Validacao;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;

namespace ProcessoEletronicoService.Negocio.Restrito
{
    public class ProcessoNegocio : IProcessoNegocio
    {
        IUnitOfWork unitOfWork;
        IRepositorioGenerico<Processo> repositorioProcessos;
        IRepositorioGenerico<OrganizacaoProcesso> repositorioOrganizacoesProcesso;

        ProcessoValidacao processoValidacao;
        InteressadoPessoaFisicaValidacao interessadoPessoaFisicaValidacao;
        IRepositorioGenerico<Despacho> repositorioDespachos;

        public ProcessoNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            this.unitOfWork = repositorios.UnitOfWork;
            this.repositorioProcessos = repositorios.Processos;
            this.repositorioOrganizacoesProcesso = repositorios.OrganizacoesProcesso;
            this.processoValidacao = new ProcessoValidacao(repositorios);
            this.interessadoPessoaFisicaValidacao = new InteressadoPessoaFisicaValidacao(repositorios);
            this.repositorioDespachos = repositorios.Despachos;
        }

        public void Listar()
        {
            throw new NotImplementedException();
        }

        public ProcessoModeloNegocio Pesquisar(int idOrganizacaoProcesso, int idProcesso)
        {
            var processo = repositorioProcessos.Where(p => p.IdOrganizacaoProcesso == idOrganizacaoProcesso
                                                        && p.Id == idProcesso)
                                               .Include(p => p.Anexos)
                                               .Include(p => p.Despachos)
                                               .Include(p => p.InteressadosPessoaFisica).ThenInclude(ipf => ipf.Contatos).ThenInclude(c => c.TipoContato)
                                               .Include(p => p.InteressadosPessoaFisica).ThenInclude(ipf => ipf.Emails)
                                               .Include(p => p.InteressadosPessoaJuridica).ThenInclude(ipf => ipf.Contatos).ThenInclude(c => c.TipoContato)
                                               .Include(p => p.InteressadosPessoaJuridica).ThenInclude(ipf => ipf.Emails)
                                               .Include(p => p.MunicipiosProcesso)
                                               .Include(p => p.SinalizacoesProcesso).ThenInclude(sp => sp.Sinalizacao)
                                               .Include(p => p.Atividade).ThenInclude(a => a.Funcao).ThenInclude(f => f.PlanoClassificacao)
                                               .Include(p => p.OrganizacaoProcesso)
                                               .SingleOrDefault();

            var p1 = Mapper.Map<Processo, ProcessoModeloNegocio>(processo);

            return p1;
        }

        public ProcessoModeloNegocio Pesquisar(string numero)
        {
            processoValidacao.NumeroValido(numero);

            int sequencial = ObterSequencial(numero);
            byte digitoVerificadorRecebido = ObterDigitoVerificador(numero);
            short ano = ObterAno(numero);
            byte digitoPoder = ObterDigitoPoder(numero);
            byte digitoEsfera = ObterDigitoEsfera(numero);
            short digitoOrganizacao = ObterDigitoOrganizacao(numero);

            byte digitoVerificadorGerado = (byte)DigitoVerificador(sequencial);
            processoValidacao.DigitoVerificadorValido(digitoVerificadorRecebido, digitoVerificadorGerado);

            var processo = repositorioProcessos.Where(p => p.Sequencial == sequencial
                                                        && p.DigitoVerificador == digitoVerificadorRecebido
                                                        && p.Ano == ano
                                                        && p.DigitoPoder == digitoPoder
                                                        && p.DigitoEsfera == digitoEsfera
                                                        && p.DigitoOrganizacao == digitoOrganizacao)
                                                   .Include(p => p.Anexos)
                                                   .Include(p => p.Despachos)
                                                   .Include(p => p.InteressadosPessoaFisica).ThenInclude(ipf => ipf.Contatos).ThenInclude(c => c.TipoContato)
                                                   .Include(p => p.InteressadosPessoaFisica).ThenInclude(ipf => ipf.Emails)
                                                   .Include(p => p.InteressadosPessoaJuridica).ThenInclude(ipf => ipf.Contatos).ThenInclude(c => c.TipoContato)
                                                   .Include(p => p.InteressadosPessoaJuridica).ThenInclude(ipf => ipf.Emails)
                                                   .Include(p => p.MunicipiosProcesso)
                                                   .Include(p => p.SinalizacoesProcesso).ThenInclude(sp => sp.Sinalizacao)
                                                   .Include(p => p.Atividade).ThenInclude(a => a.Funcao).ThenInclude(f => f.PlanoClassificacao)
                                                   .Include(p => p.OrganizacaoProcesso)
                                                   .SingleOrDefault();

            var p1 = Mapper.Map<Processo, ProcessoModeloNegocio>(processo);

            return p1;
        }

        public List<ProcessoModeloNegocio> PesquisarProcessoNaUnidade(int idOrganizacaoProcesso, int idUnidade)
        {
            var processosSemDespachoNaUnidade = repositorioProcessos.Where(p => p.IdOrganizacaoProcesso == idOrganizacaoProcesso
                                                                             && p.IdUnidadeAutuadora == idUnidade
                                                                             && !p.Despachos.Any())
                                                                    .Include(p => p.OrganizacaoProcesso)
                                                                    .Include(p => p.Atividade);

            var ultimosDespachosDosProcessos = repositorioDespachos.Where(d => d.Processo.IdOrganizacaoProcesso == idOrganizacaoProcesso)
                                                                   .GroupBy(d => d.IdProcesso)
                                                                   .Select(d => new { IdProcesso = d.Key, DataHoraDespacho = d.Max(gbd => gbd.DataHoraDespacho) });

            var idsUltimosDespachosParaUnidade = repositorioDespachos.Where(d => d.Processo.IdOrganizacaoProcesso == idOrganizacaoProcesso
                                                                         && d.IdUnidadeDestino == idUnidade)
                                                                     .Join(ultimosDespachosDosProcessos,
                                                                            d => d.IdProcesso,
                                                                            ud => ud.IdProcesso,
                                                                            (d, ud) => new { Despacho = d, DespachoProcesso = ud })
                                                                     .Where(d => d.Despacho.DataHoraDespacho == d.DespachoProcesso.DataHoraDespacho)
                                                                     .Select(d => d.Despacho.Id);

            var processosDespachadosParaUnidade = repositorioProcessos.Where(p => p.IdOrganizacaoProcesso == idOrganizacaoProcesso
                                                                               && p.Despachos.Any(d => idsUltimosDespachosParaUnidade.Contains(d.Id)))
                                                                      .Include(p => p.OrganizacaoProcesso)
                                                                      .Include(p => p.Atividade);

            var processosNaUnidade = processosDespachadosParaUnidade.Union(processosSemDespachoNaUnidade.Include(p => p.OrganizacaoProcesso)
                                                                                                        .Include(p => p.Atividade))
                                                                  .OrderBy(p => p.Sequencial)
                                                                  .ThenBy(p => p.Ano)
                                                                  .ThenBy(p => p.DigitoPoder)
                                                                  .ThenBy(p => p.DigitoEsfera)
                                                                  .ThenBy(p => p.DigitoOrganizacao)
                                                                  .Include(p => p.OrganizacaoProcesso)
                                                                  .Include(p => p.Atividade)
                                                                  .ToList();

            var r = Mapper.Map<List<Processo>, List<ProcessoModeloNegocio>>(processosNaUnidade);
            return r;
        }

        public ProcessoModeloNegocio Autuar(ProcessoModeloNegocio processoNegocio, int IdOrganizacao)
        {
            /*Validações*/
            processoValidacao.Preenchido(processoNegocio);
            processoValidacao.Valido(processoNegocio);

            /*Mapeamento para inserção*/
            Processo processo = new Processo();
            processo = Mapper.Map<ProcessoModeloNegocio, Processo>(processoNegocio);

            InformacaoPadrao(processo, IdOrganizacao);

            /*Gera número do processo*/
            NumeracaoProcesso(processo, IdOrganizacao);

            repositorioProcessos.Add(processo);
            unitOfWork.Save();

            return Pesquisar(IdOrganizacao, processo.Id);

        }

        public void Despachar()
        {
            throw new NotImplementedException();
        }

        public void Excluir()
        {
            throw new NotImplementedException();
        }

        private int ObterSequencial(string numero)
        {
            //O formato do número é SEQUENCIAL-DD.AAAA.P.E.OOOO
            //O número é dividido em duas partes pelo caracter "-" (hífen)
            //O sequencial é a primeira parte da divisão
            string stringSequencial = numero.Split('-')[0];

            processoValidacao.SequencialValido(stringSequencial);

            return Convert.ToInt32(stringSequencial);
        }

        private byte ObterDigitoVerificador(string numero)
        {
            //O formato do número é SEQUENCIAL-DD.AAAA.P.E.OOOO
            //O número é dividido em duas partes pelo caracter "-" (hífen) após isso dividido em cinco partes pelo caracter "." (ponto)
            //O digito verificador é a primeira parte da divisão por pontos
            string stringDigitoVerificador = numero.Split('-')[1].Split('.')[0];

            processoValidacao.DigitoVerificadorValido(stringDigitoVerificador);

            return Convert.ToByte(stringDigitoVerificador);
        }

        private short ObterAno(string numero)
        {
            //O formato do número é SEQUENCIAL-DD.AAAA.P.E.OOOO
            //O número é dividido em duas partes pelo caracter "-" (hífen) após isso dividido em cinco partes pelo caracter "." (ponto)
            //O ano é a segunda parte da divisão por pontos
            string stringAno = numero.Split('-')[1].Split('.')[1];

            processoValidacao.AnoValido(stringAno);

            return Convert.ToInt16(stringAno);
        }

        private byte ObterDigitoPoder(string numero)
        {
            //O formato do número é SEQUENCIAL-DD.AAAA.P.E.OOOO
            //O número é dividido em duas partes pelo caracter "-" (hífen) após isso dividido em cinco partes pelo caracter "." (ponto)
            //O digito poder é a terceira parte da divisão por pontos
            string stringDigitoPoder = numero.Split('-')[1].Split('.')[2];

            processoValidacao.DigitoPoderValido(stringDigitoPoder);

            return Convert.ToByte(stringDigitoPoder);
        }

        private byte ObterDigitoEsfera(string numero)
        {
            //O formato do número é SEQUENCIAL-DD.AAAA.P.E.OOOO
            //O número é dividido em duas partes pelo caracter "-" (hífen) após isso dividido em cinco partes pelo caracter "." (ponto)
            //O digito esfera é a quarta parte da divisão por pontos
            string stringDigitoEsfera = numero.Split('-')[1].Split('.')[3];

            processoValidacao.DigitoEsferaValido(stringDigitoEsfera);

            return Convert.ToByte(stringDigitoEsfera);
        }

        private short ObterDigitoOrganizacao(string numero)
        {
            //O formato do número é SEQUENCIAL-DD.AAAA.P.E.OOOO
            //O número é dividido em duas partes pelo caracter "-" (hífen) após isso dividido em cinco partes pelo caracter "." (ponto)
            //O digito organização é a quinta parte da divisão por pontos
            string stringDigitoOrganizacao = numero.Split('-')[1].Split('.')[4];

            processoValidacao.DigitoOrganizacaoValido(stringDigitoOrganizacao);

            return Convert.ToInt16(stringDigitoOrganizacao);
        }

        private void InformacaoPadrao(Processo processo, int idOrganizacao)
        {
            processo.IdOrganizacaoProcesso = idOrganizacao;
            processo.DataAutuacao = DateTime.Now;
        }

        private void NumeracaoProcesso(Processo processo, int idOrganizacao)
        {

            processo.DigitoOrganizacao = repositorioOrganizacoesProcesso.Where(o => o.IdOrganizacao == idOrganizacao).Select(s => s.NumeroOrganiacao).SingleOrDefault();
            processo.DigitoPoder = (byte)repositorioOrganizacoesProcesso.Where(o => o.IdOrganizacao == idOrganizacao).Select(o => o.DigitoPoder.Id).SingleOrDefault();
            processo.DigitoEsfera = (byte)repositorioOrganizacoesProcesso.Where(o => o.IdOrganizacao == idOrganizacao).Select(o => o.DigitoEsfera.Id).SingleOrDefault();
            processo.Ano = (short)DateTime.Now.Year;

            processo.Sequencial = repositorioProcessos.Where(p => p.Ano == processo.Ano
                                                 && p.DigitoEsfera == processo.DigitoEsfera
                                                 && p.DigitoPoder == processo.DigitoPoder
                                                 && p.DigitoOrganizacao == processo.DigitoOrganizacao)
                                                 .GroupBy(g => new { g.Ano, g.DigitoEsfera, g.DigitoPoder, g.DigitoOrganizacao }).Select(p => p.Max(s => s.Sequencial)).FirstOrDefault() + 1;


            processo.DigitoVerificador = (byte)DigitoVerificador(processo.Sequencial);

        }

        private int DigitoVerificador(int numero)
        {
            /*Digito Verificador base 11*/

            int i = 0;
            int soma = 0;
            int digitoVerificador = 0;
            string numeroString = numero.ToString();

            for (i = 0; i < numeroString.Length; i++)
            {
                soma += (int)Char.GetNumericValue(numeroString[numeroString.Length - 1 - i]) * (i + 2);
            }

            digitoVerificador = 11 - (soma % 11);

            return digitoVerificador;

        }
    }
}
