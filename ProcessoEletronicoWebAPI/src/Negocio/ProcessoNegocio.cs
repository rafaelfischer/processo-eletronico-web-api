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
using ProcessoEletronicoService.Negocio.Comum.Validacao;
using ProcessoEletronicoService.Negocio.Comum.Base;
using Negocio.Notificacoes.Base;
using Prodest.ProcessoEletronico.Integration.Organograma.Base;
using Prodest.ProcessoEletronico.Integration.Organograma.Models;

namespace ProcessoEletronicoService.Negocio
{
    public class ProcessoNegocio : IProcessoNegocio
    {
        private IUnitOfWork _unitOfWork;
        private ICurrentUserProvider _user;
        private IMapper _mapper;
        private IRepositorioGenerico<Processo> _repositorioProcessos;
        private IRepositorioGenerico<OrganizacaoProcesso> _repositorioOrganizacoesProcesso;
        private IRepositorioGenerico<Anexo> _repositorioAnexos;
        private IRepositorioGenerico<Despacho> _repositorioDespachos;
        private IRepositorioGenerico<RascunhoProcesso> _repositorioRascunhosProcesso;
        private INotificacoesService _notificacoesService;

        private ProcessoValidacao _validacao;
        private DespachoValidacao _despachoValidacao;
        private AnexoValidacao _anexoValidacao;
        private UsuarioValidacao _usuarioValidacao;
        private IOrganizacaoService _organizacaoService;
        private IUnidadeService _unidadeService;
        private IMunicipioService _municipioService;
        private Rascunho.Processo.Validacao.RascunhoProcessoValidacao _rascunhoProcessoValidacao;


        public ProcessoNegocio(IProcessoEletronicoRepositorios repositorios, ICurrentUserProvider user, INotificacoesService notificacoesService, IMapper mapper, IOrganizacaoService organizacaoService, IUnidadeService unidadeService, IMunicipioService municipioService, Rascunho.Processo.Validacao.RascunhoProcessoValidacao rascunhoProcessoValidacao)
        {
            _unitOfWork = repositorios.UnitOfWork;
            _user = user;
            _mapper = mapper;
            _organizacaoService = organizacaoService;
            _unidadeService = unidadeService;
            _municipioService = municipioService;
            _repositorioDespachos = repositorios.Despachos;
            _repositorioProcessos = repositorios.Processos;
            _repositorioOrganizacoesProcesso = repositorios.OrganizacoesProcesso;
            _repositorioAnexos = repositorios.Anexos;
            _repositorioRascunhosProcesso = repositorios.RascunhosProcesso;
            _notificacoesService = notificacoesService;
            _validacao = new ProcessoValidacao(repositorios);
            _despachoValidacao = new DespachoValidacao(repositorios);
            _anexoValidacao = new AnexoValidacao(repositorios);
            _rascunhoProcessoValidacao = rascunhoProcessoValidacao;
            _usuarioValidacao = new UsuarioValidacao();
        }

        public ProcessoModeloNegocio Pesquisar(int id)
        {
            Processo processo = _repositorioProcessos.Where(p => p.Id == id)
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

            _validacao.NaoEncontrado(processo);

            //Ordenando os despachos
            processo.Despachos = processo.Despachos.OrderByDescending(d => d.DataHoraDespacho).ToList();

            //Limpando contedo dos anexos para não enviar na resposta da consulta de processos
            if (processo.Anexos != null)
            {
                LimparConteudoAnexos(processo.Anexos);
            }

            return _mapper.Map<ProcessoModeloNegocio>(processo);

        }
        
        public ProcessoModeloNegocio Pesquisar(string numero)
        {
            _validacao.NumeroValido(numero);

            int sequencial = ObterSequencial(numero);
            byte digitoVerificadorRecebido = ObterDigitoVerificador(numero);
            short ano = ObterAno(numero);
            byte digitoPoder = ObterDigitoPoder(numero);
            byte digitoEsfera = ObterDigitoEsfera(numero);
            short digitoOrganizacao = ObterDigitoOrganizacao(numero);

            byte digitoVerificadorGerado = (byte)DigitoVerificador(sequencial);
            _validacao.DigitoVerificadorValido(digitoVerificadorRecebido, digitoVerificadorGerado);

            var processo = _repositorioProcessos.Where(p => p.Sequencial == sequencial
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

            _validacao.NaoEncontrado(processo);

            //Limpando contedo dos anexos para não enviar na resposta da consulta de processos
            if (processo.Anexos != null)
            {
                LimparConteudoAnexos(processo.Anexos);
            }

            return _mapper.Map<ProcessoModeloNegocio>(processo);

        }

        public ProcessoModeloNegocio PesquisarSemDespachos(string numero)
        {
            _validacao.NumeroValido(numero);

            int sequencial = ObterSequencial(numero);
            byte digitoVerificadorRecebido = ObterDigitoVerificador(numero);
            short ano = ObterAno(numero);
            byte digitoPoder = ObterDigitoPoder(numero);
            byte digitoEsfera = ObterDigitoEsfera(numero);
            short digitoOrganizacao = ObterDigitoOrganizacao(numero);

            byte digitoVerificadorGerado = (byte)DigitoVerificador(sequencial);
            _validacao.DigitoVerificadorValido(digitoVerificadorRecebido, digitoVerificadorGerado);

            var processo = _repositorioProcessos.Where(p => p.Sequencial == sequencial
                                                        && p.DigitoVerificador == digitoVerificadorRecebido
                                                        && p.Ano == ano
                                                        && p.DigitoPoder == digitoPoder
                                                        && p.DigitoEsfera == digitoEsfera
                                                        && p.DigitoOrganizacao == digitoOrganizacao)
                                                   .Include(p => p.Anexos).ThenInclude(a => a.TipoDocumental)
                                                   .Include(p => p.InteressadosPessoaFisica).ThenInclude(ipf => ipf.Contatos).ThenInclude(c => c.TipoContato)
                                                   .Include(p => p.InteressadosPessoaFisica).ThenInclude(ipf => ipf.Emails)
                                                   .Include(p => p.InteressadosPessoaJuridica).ThenInclude(ipf => ipf.Contatos).ThenInclude(c => c.TipoContato)
                                                   .Include(p => p.InteressadosPessoaJuridica).ThenInclude(ipf => ipf.Emails)
                                                   .Include(p => p.MunicipiosProcesso)
                                                   .Include(p => p.SinalizacoesProcesso).ThenInclude(sp => sp.Sinalizacao)
                                                   .Include(p => p.Atividade).ThenInclude(a => a.Funcao).ThenInclude(f => f.PlanoClassificacao)
                                                   .Include(p => p.OrganizacaoProcesso)
                                                   .SingleOrDefault();

            _validacao.NaoEncontrado(processo);

            //Limpando contedo dos anexos para não enviar na resposta da consulta de processos
            if (processo.Anexos != null)
            {
                LimparConteudoAnexos(processo.Anexos);
            }

            return _mapper.Map<ProcessoModeloNegocio>(processo);

        }

        public ProcessoModeloNegocio PesquisarSimplificado(string numero)
        {
            _validacao.NumeroValido(numero);

            int sequencial = ObterSequencial(numero);
            byte digitoVerificadorRecebido = ObterDigitoVerificador(numero);
            short ano = ObterAno(numero);
            byte digitoPoder = ObterDigitoPoder(numero);
            byte digitoEsfera = ObterDigitoEsfera(numero);
            short digitoOrganizacao = ObterDigitoOrganizacao(numero);

            byte digitoVerificadorGerado = (byte)DigitoVerificador(sequencial);
            _validacao.DigitoVerificadorValido(digitoVerificadorRecebido, digitoVerificadorGerado);

            var processo = _repositorioProcessos.Where(p => p.Sequencial == sequencial
                                                        && p.DigitoVerificador == digitoVerificadorRecebido
                                                        && p.Ano == ano
                                                        && p.DigitoPoder == digitoPoder
                                                        && p.DigitoEsfera == digitoEsfera
                                                        && p.DigitoOrganizacao == digitoOrganizacao)
                                                   .Include(p => p.Atividade)
                                                   .SingleOrDefault();

            _validacao.NaoEncontrado(processo);

            return _mapper.Map<ProcessoModeloNegocio>(processo);

        }

        public List<ProcessoModeloNegocio> PesquisarProcessosNaUnidade(string guidUnidade)
        {
            var processosSemDespachoNaUnidade = _repositorioProcessos.Where(p => p.GuidUnidadeAutuadora.Equals(new Guid(guidUnidade))
                                                                             && !p.Despachos.Any())
                                                                    .Include(p => p.OrganizacaoProcesso)
                                                                    .Include(p => p.Atividade)
                                                                    .Include(p => p.Despachos);

            var ultimosDespachosDosProcessos = _repositorioDespachos.GroupBy(d => d.IdProcesso)
                                                                   .Select(d => new { IdProcesso = d.Key, DataHoraDespacho = d.Max(gbd => gbd.DataHoraDespacho) });

            var idsUltimosDespachosParaUnidade = _repositorioDespachos.Where(d => d.GuidUnidadeDestino.Equals(new Guid(guidUnidade)))
                                                                     .Join(ultimosDespachosDosProcessos,
                                                                            d => d.IdProcesso,
                                                                            ud => ud.IdProcesso,
                                                                            (d, ud) => new { Despacho = d, DespachoProcesso = ud })
                                                                     .Where(d => d.Despacho.DataHoraDespacho == d.DespachoProcesso.DataHoraDespacho)
                                                                     .Select(d => d.Despacho.Id);

            var processosDespachadosParaUnidade = _repositorioProcessos.Where(p => p.Despachos.Any(d => idsUltimosDespachosParaUnidade.Contains(d.Id)))
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

            return _mapper.Map<List<ProcessoModeloNegocio>>(processosNaUnidade);
        }

        public List<ProcessoModeloNegocio> PesquisarProcessosDespachadosUsuario()
        {
            IQueryable<Processo> query;

            query = _repositorioProcessos;
            query = query.Where(p => p.Despachos.Any(d => d.IdUsuarioDespachante.Equals(_user.UserCpf)))
                         .Include(d => d.Despachos);

            return _mapper.Map<List<ProcessoModeloNegocio>>(query.ToList());
        }

        public ProcessoModeloNegocio Autuar(ProcessoModeloNegocio processoNegocio)
        {
            _usuarioValidacao.Autenticado(_user.UserCpf, _user.UserNome);
            _usuarioValidacao.PossuiOrganizaoPatriarca(_user.UserGuidOrganizacaoPatriarca);
            _usuarioValidacao.PodeAutuarProcessoNaOrganizacao(processoNegocio, _user.UserGuidOrganizacao);

            /*Validações*/
            _validacao.Preenchido(processoNegocio);
            _validacao.Valido(processoNegocio);

            /*Mapeamento para inserção*/
            Processo processo = new Processo();
            processo = _mapper.Map<Processo>(processoNegocio);

            /*Preenchimento das informações que possuem GUID*/
            InformacoesOrganizacao(processo);
            InformacoesUnidade(processo);
            InformacoesMunicipio(processo);
            InformacoesMunicipioInteressadoPessoaFisica(processo);
            InformacoesMunicipioInteressadoPessoaJuridica(processo);

            /*Informações padrão, como a data de atuação*/
            InformacaoPadrao(processo);
            
            _validacao.AtividadePertenceAOrganizacaoPatriarca(processoNegocio, _user.UserGuidOrganizacaoPatriarca);
            _validacao.AtividadePertenceAOrganizacao(processoNegocio);
            _validacao.SinalizacoesPertencemAOrganizacaoPatriarca(processoNegocio, _user.UserGuidOrganizacaoPatriarca);

            /*Gera número do processo*/
            NumeracaoProcesso(processo);

            _repositorioProcessos.Add(processo);
            _unitOfWork.Save();
            _notificacoesService.Insert(processo);

            return Pesquisar(processo.Id);
        }

        public ProcessoModeloNegocio Post(int idRascunhoProcesso)
        {
            RascunhoProcesso rascunhoProcesso = _repositorioRascunhosProcesso.Where(p => p.Id == idRascunhoProcesso)
                                               .Include(p => p.Anexos).ThenInclude(td => td.TipoDocumental)
                                               .Include(p => p.InteressadosPessoaFisica).ThenInclude(ipf => ipf.ContatosRascunho).ThenInclude(c => c.TipoContato)
                                               .Include(p => p.InteressadosPessoaFisica).ThenInclude(ipf => ipf.EmailsRascunho)
                                               .Include(p => p.InteressadosPessoaJuridica).ThenInclude(ipf => ipf.ContatosRascunho).ThenInclude(c => c.TipoContato)
                                               .Include(p => p.InteressadosPessoaJuridica).ThenInclude(ipf => ipf.EmailsRascunho)
                                               .Include(p => p.MunicipiosRascunhoProcesso)
                                               .Include(p => p.SinalizacoesRascunhoProcesso).ThenInclude(sp => sp.Sinalizacao)
                                               .SingleOrDefault();

            _rascunhoProcessoValidacao.Exists(rascunhoProcesso);

            ProcessoModeloNegocio processo = _mapper.Map<ProcessoModeloNegocio>(rascunhoProcesso);

            return Autuar(processo);          
            
        }
        
        public List<ProcessoModeloNegocio> PesquisarProcessosNaOrganizacao(string guidOrganizacao)
        {
            Guid gOrganizacao = new Guid(guidOrganizacao);

            var processosNaOrganizacao = _repositorioProcessos.Where(p => p.OrganizacaoProcesso.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca)
                                                                    
                                                                     //Processos sem despacho que foram autuados na organização
                                                                     && (p.GuidOrganizacaoAutuadora.Equals(gOrganizacao)
                                                                     && !p.Despachos.Any())

                                                                     //Processos cujo destino do ultimo despacho é a organização 
                                                                     || (p.Despachos.Any()
                                                                      && p.Despachos.OrderBy(d => d.DataHoraDespacho).Last().GuidOrganizacaoDestino.Equals(gOrganizacao)))
                                                             
                                                             .Include(p => p.OrganizacaoProcesso)
                                                             .Include(p => p.Atividade)
                                                             .OrderBy(p => p.Sequencial)
                                                             .ThenBy(p => p.Ano)
                                                             .ThenBy(p => p.DigitoPoder)
                                                             .ThenBy(p => p.DigitoEsfera)
                                                             .ThenBy(p => p.DigitoOrganizacao)
                                                             .ToList();

            return _mapper.Map<List<ProcessoModeloNegocio>>(processosNaOrganizacao);
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

            _validacao.SequencialValido(stringSequencial);

            return Convert.ToInt32(stringSequencial);
        }

        private byte ObterDigitoVerificador(string numero)
        {
            //O formato do número é SEQUENCIAL-DD.AAAA.P.E.OOOO
            //O número é dividido em duas partes pelo caracter "-" (hífen) após isso dividido em cinco partes pelo caracter "." (ponto)
            //O digito verificador é a primeira parte da divisão por pontos
            string stringDigitoVerificador = numero.Split('-')[1].Split('.')[0];

            _validacao.DigitoVerificadorValido(stringDigitoVerificador);

            return Convert.ToByte(stringDigitoVerificador);
        }

        private short ObterAno(string numero)
        {
            //O formato do número é SEQUENCIAL-DD.AAAA.P.E.OOOO
            //O número é dividido em duas partes pelo caracter "-" (hífen) após isso dividido em cinco partes pelo caracter "." (ponto)
            //O ano é a segunda parte da divisão por pontos
            string stringAno = numero.Split('-')[1].Split('.')[1];

            _validacao.AnoValido(stringAno);

            return Convert.ToInt16(stringAno);
        }

        private byte ObterDigitoPoder(string numero)
        {
            //O formato do número é SEQUENCIAL-DD.AAAA.P.E.OOOO
            //O número é dividido em duas partes pelo caracter "-" (hífen) após isso dividido em cinco partes pelo caracter "." (ponto)
            //O digito poder é a terceira parte da divisão por pontos
            string stringDigitoPoder = numero.Split('-')[1].Split('.')[2];

            _validacao.DigitoPoderValido(stringDigitoPoder);

            return Convert.ToByte(stringDigitoPoder);
        }

        private byte ObterDigitoEsfera(string numero)
        {
            //O formato do número é SEQUENCIAL-DD.AAAA.P.E.OOOO
            //O número é dividido em duas partes pelo caracter "-" (hífen) após isso dividido em cinco partes pelo caracter "." (ponto)
            //O digito esfera é a quarta parte da divisão por pontos
            string stringDigitoEsfera = numero.Split('-')[1].Split('.')[3];

            _validacao.DigitoEsferaValido(stringDigitoEsfera);

            return Convert.ToByte(stringDigitoEsfera);
        }

        private short ObterDigitoOrganizacao(string numero)
        {
            //O formato do número é SEQUENCIAL-DD.AAAA.P.E.OOOO
            //O número é dividido em duas partes pelo caracter "-" (hífen) após isso dividido em cinco partes pelo caracter "." (ponto)
            //O digito organização é a quinta parte da divisão por pontos
            string stringDigitoOrganizacao = numero.Split('-')[1].Split('.')[4];

            _validacao.DigitoOrganizacaoValido(stringDigitoOrganizacao);

            return Convert.ToInt16(stringDigitoOrganizacao);
        }

        private void InformacoesUnidade(Processo processo)
        {
            Unidade unidade = _unidadeService.Search(processo.GuidUnidadeAutuadora).ResponseObject;

            if (unidade == null)
            {
                throw new RequisicaoInvalidaException("Unidade de destino não encontrada no Organograma.");
            }
            
            _validacao.UnidadePertenceAOrganizacao(new Guid(unidade.Organizacao.Guid), processo.GuidOrganizacaoAutuadora);

            processo.GuidUnidadeAutuadora = new Guid(unidade.Guid);
            processo.NomeUnidadeAutuadora = unidade.Nome;
            processo.SiglaUnidadeAutuadora = unidade.Sigla;
        }

        private void InformacoesOrganizacao(Processo processo)
        {
            Organizacao organizacao = _organizacaoService.Search(processo.GuidOrganizacaoAutuadora).ResponseObject;

            if (organizacao == null)
            {
                throw new RequisicaoInvalidaException("Organização autuadora não encontrada no Organograma.");
            }

            processo.GuidOrganizacaoAutuadora = new Guid(organizacao.Guid);
            processo.NomeOrganizacaoAutuadora = organizacao.RazaoSocial;
            processo.SiglaOrganizacaoAutuadora = organizacao.Sigla;

        }
        private void InformacoesMunicipio(Processo processo)
        {
            foreach (MunicipioProcesso municipioProcesso in processo.MunicipiosProcesso)
            {
                Municipio municipio = _municipioService.Search(municipioProcesso.GuidMunicipio).ResponseObject;

                if (municipio == null)
                {
                    throw new RequisicaoInvalidaException("Municipio não encontrado no Organograma.");
                }

                municipioProcesso.Nome = municipio.Nome;
                municipioProcesso.Uf = municipio.Uf;

            }
        }

        private void InformacoesMunicipioInteressadoPessoaFisica(Processo processo)
        {
            if (processo.InteressadosPessoaFisica != null)
            {
                foreach (InteressadoPessoaFisica interessado in processo.InteressadosPessoaFisica)
                {
                    Municipio municipio = _municipioService.Search(interessado.GuidMunicipio).ResponseObject;

                    if (municipio == null)
                    {
                        throw new RequisicaoInvalidaException("Municipio do interessado pessoa física não encontrado no Organograma.");
                    }

                    interessado.NomeMunicipio = municipio.Nome;
                    interessado.UfMunicipio = municipio.Uf;
                }
            }
        }

        private void InformacoesMunicipioInteressadoPessoaJuridica(Processo processo)
        {
            if (processo.InteressadosPessoaJuridica != null)
            {
                foreach (InteressadoPessoaJuridica interessado in processo.InteressadosPessoaJuridica)
                {
                    Municipio municipio = _municipioService.Search(interessado.GuidMunicipio).ResponseObject;

                    if (municipio == null)
                    {
                        throw new RequisicaoInvalidaException("Municipio do interessado pessoa jurídica não encontrado no Organograma.");
                    }

                    interessado.NomeMunicipio = municipio.Nome;
                    interessado.UfMunicipio = municipio.Uf;
                }
            }
        }

        private void InformacaoPadrao(Processo processo)
        {
            int idOrganizacaoProcesso = _repositorioOrganizacoesProcesso.Where(p => p.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca))
                                                            .Single().Id;

            processo.IdOrganizacaoProcesso = idOrganizacaoProcesso;
            processo.DataAutuacao = DateTime.Now;
            processo.IdUsuarioAutuador = _user.UserCpf;
            processo.NomeUsuarioAutuador = _user.UserNome;
        }

        private void NumeracaoProcesso(Processo processo)
        {
            int idOrganizacaoProcesso = _repositorioOrganizacoesProcesso.Where(p => p.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca))
                                                            .Single()
                                                            .Id;

            processo.DigitoOrganizacao = _repositorioOrganizacoesProcesso.Where(o => o.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca)).Select(s => s.DigitoOrganizacao).SingleOrDefault();
            processo.DigitoPoder = (byte)_repositorioOrganizacoesProcesso.Where(o => o.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca)).Select(o => o.DigitoPoder.Id).SingleOrDefault();
            processo.DigitoEsfera = (byte)_repositorioOrganizacoesProcesso.Where(o => o.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca)).Select(o => o.DigitoEsfera.Id).SingleOrDefault();
            processo.Ano = (short)DateTime.Now.Year;

            processo.Sequencial = _repositorioProcessos.Where(p => p.Ano == processo.Ano
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

        private void PreparaInsercaoDespacho(Despacho despacho, int idProcesso)
        {
            //Processo do despacho
            despacho.IdProcesso = idProcesso;

            //Preenche processo dos anexos
            if (despacho.Anexos != null)
            {
                foreach (Anexo anexo in despacho.Anexos)
                {
                    anexo.IdProcesso = idProcesso;
                }
            }

            //Data/hora atual do despacho
            despacho.DataHoraDespacho = DateTime.Now;
        }
        
    }
}
