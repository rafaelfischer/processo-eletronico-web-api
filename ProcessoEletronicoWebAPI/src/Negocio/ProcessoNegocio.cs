using AutoMapper;
using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Validacao;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Comum;
using ProcessoEletronicoService.Negocio.Comum.Validacao;
using ProcessoEletronicoService.Negocio.Comum.Base;
using static ProcessoEletronicoService.Negocio.Comum.Validacao.OrganogramaValidacao;

namespace ProcessoEletronicoService.Negocio
{
    public class ProcessoNegocio : IProcessoNegocio
    {
        IUnitOfWork unitOfWork;
        private ICurrentUserProvider _user;
        IRepositorioGenerico<Processo> repositorioProcessos;
        IRepositorioGenerico<OrganizacaoProcesso> repositorioOrganizacoesProcesso;
        IRepositorioGenerico<Anexo> repositorioAnexos;

        ProcessoValidacao processoValidacao;
        DespachoValidacao despachoValidacao;
        AnexoValidacao anexoValidacao;
        UsuarioValidacao usuarioValidacao;
        private OrganogramaValidacao _organogramaValidacao;
        IRepositorioGenerico<Despacho> repositorioDespachos;

        public ProcessoNegocio(IProcessoEletronicoRepositorios repositorios, ICurrentUserProvider user, OrganogramaValidacao organogramaValidacao)
        {
            unitOfWork = repositorios.UnitOfWork;
            _user = user;
            _organogramaValidacao = organogramaValidacao;
            repositorioDespachos = repositorios.Despachos;
            repositorioProcessos = repositorios.Processos;
            repositorioOrganizacoesProcesso = repositorios.OrganizacoesProcesso;
            repositorioAnexos = repositorios.Anexos;
            processoValidacao = new ProcessoValidacao(repositorios);
            despachoValidacao = new DespachoValidacao(repositorios);
            anexoValidacao = new AnexoValidacao(repositorios);
            usuarioValidacao = new UsuarioValidacao();
        }

        public ProcessoModeloNegocio Pesquisar(int id)
        {
            var processo = repositorioProcessos.Where(p => p.Id == id)
                                               .Include(p => p.Anexos).ThenInclude(td => td.TipoDocumental)
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

            processoValidacao.NaoEncontrado(processo);

            //Ordenando os despachos
            processo.Despachos = processo.Despachos.OrderByDescending(d => d.DataHoraDespacho).ToList();

            //Limpando contedo dos anexos para não enviar na resposta da consulta de processos
            if (processo.Anexos != null)
            {
                LimparConteudoAnexos(processo.Anexos);
            }

            return Mapper.Map<Processo, ProcessoModeloNegocio>(processo);

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
                                                   .Include(p => p.Anexos).ThenInclude(a => a.TipoDocumental)
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

            processoValidacao.NaoEncontrado(processo);

            //Limpando contedo dos anexos para não enviar na resposta da consulta de processos
            if (processo.Anexos != null)
            {
                LimparConteudoAnexos(processo.Anexos);
            }

            return Mapper.Map<Processo, ProcessoModeloNegocio>(processo);

        }

        public List<ProcessoModeloNegocio> PesquisarProcessoNaUnidade(string guidUnidade)
        {
            var processosSemDespachoNaUnidade = repositorioProcessos.Where(p => p.GuidUnidadeAutuadora.Equals(new Guid(guidUnidade))
                                                                             && !p.Despachos.Any())
                                                                    .Include(p => p.OrganizacaoProcesso)
                                                                    .Include(p => p.Atividade)
                                                                    .Include(p => p.Despachos);

            var ultimosDespachosDosProcessos = repositorioDespachos.GroupBy(d => d.IdProcesso)
                                                                   .Select(d => new { IdProcesso = d.Key, DataHoraDespacho = d.Max(gbd => gbd.DataHoraDespacho) });

            var idsUltimosDespachosParaUnidade = repositorioDespachos.Where(d => d.GuidUnidadeDestino.Equals(new Guid(guidUnidade)))
                                                                     .Join(ultimosDespachosDosProcessos,
                                                                            d => d.IdProcesso,
                                                                            ud => ud.IdProcesso,
                                                                            (d, ud) => new { Despacho = d, DespachoProcesso = ud })
                                                                     .Where(d => d.Despacho.DataHoraDespacho == d.DespachoProcesso.DataHoraDespacho)
                                                                     .Select(d => d.Despacho.Id);

            var processosDespachadosParaUnidade = repositorioProcessos.Where(p => p.Despachos.Any(d => idsUltimosDespachosParaUnidade.Contains(d.Id)))
                                                                      .Include(p => p.OrganizacaoProcesso)
                                                                      .Include(p => p.Atividade)
                                                                      .Include(p => p.Despachos);

            var processosNaUnidade = processosDespachadosParaUnidade.Union(processosSemDespachoNaUnidade.Include(p => p.OrganizacaoProcesso)
                                                                                                        .Include(p => p.Atividade)
                                                                                                        .Include(p => p.Despachos))
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

        public List<ProcessoModeloNegocio> PesquisarProcessosDespachadosUsuario()
        {
            IQueryable<Processo> query;

            query = repositorioProcessos;
            query = query.Where(p => p.Despachos.Any(d => d.IdUsuarioDespachante.Equals(_user.UserCpf)))
                         .Include(d => d.Despachos);

            return Mapper.Map<List<Processo>, List<ProcessoModeloNegocio>>(query.ToList());
        }

        public ProcessoModeloNegocio Autuar(ProcessoModeloNegocio processoNegocio)
        {
            usuarioValidacao.Autenticado(_user.UserCpf, _user.UserCpf);
            usuarioValidacao.PossuiOrganizaoPatriarca(_user.UserGuidOrganizacaoPatriarca);
            usuarioValidacao.PodeAutuarProcessoNaOrganizacao(processoNegocio, _user.UserGuidOrganizacao);

            /*Validações*/
            processoValidacao.Preenchido(processoNegocio);
            processoValidacao.Valido(processoNegocio);

            /*Mapeamento para inserção*/
            Processo processo = new Processo();
            processo = Mapper.Map<ProcessoModeloNegocio, Processo>(processoNegocio);

            /*Preenchimento das informações que possuem GUID*/
            InformacoesOrganizacao(processo);
            InformacoesUnidade(processo);
            InformacoesMunicipio(processo);
            InformacoesMunicipioInteressadoPessoaFisica(processo);
            InformacoesMunicipioInteressadoPessoaJuridica(processo);

            /*Informações padrão, como a data de atuação*/
            InformacaoPadrao(processo);

            processoValidacao.AtividadePertenceAOrganizacaoPatriarca(processoNegocio, _user.UserGuidOrganizacaoPatriarca);

            processoValidacao.AtividadePertenceAOrganizacao(processoNegocio);

            processoValidacao.SinalizacoesPertencemAOrganizacaoPatriarca(processoNegocio, _user.UserGuidOrganizacaoPatriarca);

            /*Gera número do processo*/
            NumeracaoProcesso(processo);

            repositorioProcessos.Add(processo);
            unitOfWork.Save();

            return Pesquisar(processo.Id);
        }


        public void Excluir()
        {
            throw new NotImplementedException();
        }

        public List<ProcessoModeloNegocio> PesquisarProcessoNaOrganizacao(string guidOrganizacao)
        {
            Guid gOrganizacao = new Guid(guidOrganizacao);

            var processosSemDespachoNaOrganizacao = repositorioProcessos.Where(p => p.OrganizacaoProcesso.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca)
                                                                                 && p.GuidOrganizacaoAutuadora.Equals(gOrganizacao)
                                                                                && !p.Despachos.Any())
                                                                        .Include(p => p.OrganizacaoProcesso)
                                                                        .Include(p => p.Atividade)
                                                                    .Include(p => p.Despachos);

            var ultimosDespachosDosProcessos = repositorioDespachos.Where(d => d.Processo.OrganizacaoProcesso.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca))
                                                                   .GroupBy(d => d.IdProcesso)
                                                                   .Select(d => new { IdProcesso = d.Key, DataHoraDespacho = d.Max(gbd => gbd.DataHoraDespacho) });

            var idsUltimosDespachosParaOrganizacao = repositorioDespachos.Where(d => d.Processo.OrganizacaoProcesso.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca)
                                                                                  && d.GuidOrganizacaoDestino.Equals(gOrganizacao))
                                                                         .Join(ultimosDespachosDosProcessos,
                                                                                d => d.IdProcesso,
                                                                                ud => ud.IdProcesso,
                                                                                (d, ud) => new { Despacho = d, DespachoProcesso = ud })
                                                                         .Where(d => d.Despacho.DataHoraDespacho == d.DespachoProcesso.DataHoraDespacho)
                                                                         .Select(d => d.Despacho.Id);

            var processosDespachadosParaOrganizacao = repositorioProcessos.Where(p => p.OrganizacaoProcesso.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca)
                                                                                   && p.Despachos.Any(d => idsUltimosDespachosParaOrganizacao.Contains(d.Id)))
                                                                          .Include(p => p.OrganizacaoProcesso)
                                                                          .Include(p => p.Atividade)
                                                                    .Include(p => p.Despachos);

            var processosNaOrganizacao = processosDespachadosParaOrganizacao.Union(processosSemDespachoNaOrganizacao.Include(p => p.OrganizacaoProcesso)
                                                                                                                    .Include(p => p.Atividade)
                                                                                                                    .Include(p => p.Despachos))
                                                                            .Include(p => p.OrganizacaoProcesso)
                                                                            .Include(p => p.Atividade)
                                                                            .ToList();

            processosNaOrganizacao = processosNaOrganizacao.OrderBy(p => p.Sequencial)
                                                           .ThenBy(p => p.Ano)
                                                           .ThenBy(p => p.DigitoPoder)
                                                           .ThenBy(p => p.DigitoEsfera)
                                                           .ThenBy(p => p.DigitoOrganizacao)
                                                           .ToList();

            return Mapper.Map<List<Processo>, List<ProcessoModeloNegocio>>(processosNaOrganizacao);
        }

        private void LimparConteudoAnexos(ICollection<Anexo> anexos)
        {
            if (anexos != null)
            {
                foreach (Anexo anexo in anexos)
                {
                    anexo.Conteudo = null;
                }
            }
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

        private void InformacoesUnidade(Processo processo)
        {
            UnidadeOrganogramaModelo unidade = _organogramaValidacao.PesquisarUnidade(processo.GuidUnidadeAutuadora);

            if (unidade == null)
            {
                throw new RequisicaoInvalidaException("Unidade de destino não encontrada no Organograma.");
            }

            processoValidacao.UnidadePertenceAOrganizacao(new Guid(unidade.organizacao.guid), processo.GuidOrganizacaoAutuadora);

            processo.GuidUnidadeAutuadora = new Guid(unidade.guid);
            processo.NomeUnidadeAutuadora = unidade.nome;
            processo.SiglaUnidadeAutuadora = unidade.sigla;
        }

        private void InformacoesOrganizacao(Processo processo)
        {
            OrganizacaoOrganogramaModelo organizacao = _organogramaValidacao.PesquisarOrganizacao(processo.GuidOrganizacaoAutuadora);

            if (organizacao == null)
            {
                throw new RequisicaoInvalidaException("Organização autuadora não encontrada no Organograma.");
            }

            processo.GuidOrganizacaoAutuadora = new Guid(organizacao.guid);
            processo.NomeOrganizacaoAutuadora = organizacao.razaoSocial;
            processo.SiglaOrganizacaoAutuadora = organizacao.sigla;

        }
        private void InformacoesMunicipio(Processo processo)
        {
            foreach (MunicipioProcesso municipio in processo.MunicipiosProcesso)
            {
                MunicipioOrganogramaModelo municipioOrganograma = _organogramaValidacao.PesquisarMunicipio(municipio.GuidMunicipio);

                if (municipioOrganograma == null)
                {
                    throw new RequisicaoInvalidaException("Municipio não encontrado no Organograma.");
                }

                municipio.Nome = municipioOrganograma.nome;
                municipio.Uf = municipioOrganograma.uf;

            }
        }

        private void InformacoesMunicipioInteressadoPessoaFisica(Processo processo)
        {
            if (processo.InteressadosPessoaFisica != null)
            {
                foreach (InteressadoPessoaFisica interessado in processo.InteressadosPessoaFisica)
                {
                    MunicipioOrganogramaModelo municipioOrganograma = _organogramaValidacao.PesquisarMunicipio(interessado.GuidMunicipio);

                    if (municipioOrganograma == null)
                    {
                        throw new RequisicaoInvalidaException("Municipio do interessado pessoa física não encontrado no Organograma.");
                    }

                    interessado.NomeMunicipio = municipioOrganograma.nome;
                    interessado.UfMunicipio = municipioOrganograma.uf;
                }
            }
        }

        private void InformacoesMunicipioInteressadoPessoaJuridica(Processo processo)
        {
            if (processo.InteressadosPessoaJuridica != null)
            {
                foreach (InteressadoPessoaJuridica interessado in processo.InteressadosPessoaJuridica)
                {
                    MunicipioOrganogramaModelo municipioOrganograma = _organogramaValidacao.PesquisarMunicipio(interessado.GuidMunicipio);

                    if (municipioOrganograma == null)
                    {
                        throw new RequisicaoInvalidaException("Municipio do interessado pessoa jurídica não encontrado no Organograma.");
                    }

                    interessado.NomeMunicipio = municipioOrganograma.nome;
                    interessado.UfMunicipio = municipioOrganograma.uf;
                }
            }
        }

        private void InformacaoPadrao(Processo processo)
        {
            int idOrganizacaoProcesso = repositorioOrganizacoesProcesso.Where(p => p.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca))
                                                            .Single().Id;

            processo.IdOrganizacaoProcesso = idOrganizacaoProcesso;
            processo.DataAutuacao = DateTime.Now;
            processo.IdUsuarioAutuador = _user.UserCpf;
            processo.NomeUsuarioAutuador = _user.UserNome;
        }

        private void NumeracaoProcesso(Processo processo)
        {
            int idOrganizacaoProcesso = repositorioOrganizacoesProcesso.Where(p => p.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca))
                                                            .Single()
                                                            .Id;

            processo.DigitoOrganizacao = repositorioOrganizacoesProcesso.Where(o => o.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca)).Select(s => s.DigitoOrganizacao).SingleOrDefault();
            processo.DigitoPoder = (byte)repositorioOrganizacoesProcesso.Where(o => o.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca)).Select(o => o.DigitoPoder.Id).SingleOrDefault();
            processo.DigitoEsfera = (byte)repositorioOrganizacoesProcesso.Where(o => o.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca)).Select(o => o.DigitoEsfera.Id).SingleOrDefault();
            processo.Ano = (short)DateTime.Now.Year;

            processo.Sequencial = repositorioProcessos.Where(p => p.Ano == processo.Ano
                                                 && p.DigitoEsfera == processo.DigitoEsfera
                                                 && p.DigitoPoder == processo.DigitoPoder
                                                 && p.DigitoOrganizacao == processo.DigitoOrganizacao)
                                                 .GroupBy(g => new { g.Ano, g.DigitoEsfera, g.DigitoPoder, g.DigitoOrganizacao }).Select(p => p.Max(s => s.Sequencial)).FirstOrDefault() + 1;



            processo.DigitoVerificador = (byte)DigitoVerificador(processo.Sequencial);

        }

        private int DigitoVerificador(int numero, string digito = "")
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

            if (digitoVerificador == 11 || digitoVerificador == 10)
            {
                digitoVerificador = 0;
            }

            /*chamada recursiva para o segundo digito verificador*/
            if ((digito + digitoVerificador.ToString()).Count() < 2)
            {
                int numeroConcatenado = int.Parse(numero.ToString() + digitoVerificador.ToString());
                return DigitoVerificador(numeroConcatenado, digitoVerificador.ToString());
            }

            return int.Parse(digito + digitoVerificador.ToString());

        }

        private void PreparaInsercaoDespacho(DespachoModeloNegocio despacho, int idProcesso)
        {
            //Processo do despacho
            despacho.IdProcesso = idProcesso;

            //Preenche processo dos anexos
            if (despacho.Anexos != null)
            {
                foreach (AnexoModeloNegocio anexo in despacho.Anexos)
                {
                    anexo.IdProcesso = idProcesso;
                }
            }

            //Data/hora atual do despacho
            despacho.DataHoraDespacho = DateTime.Now;
        }

    }
}
