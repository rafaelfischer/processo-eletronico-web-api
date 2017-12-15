using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Negocio.Modelos.Patch;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Comum.Validacao;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao;
using Prodest.ProcessoEletronico.Integration.Organograma.Base;
using Prodest.ProcessoEletronico.Integration.Organograma.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo
{
    public class RascunhoProcessoNegocio : IRascunhoProcessoNegocio
    {
        //Repositórios
        private IRepositorioGenerico<RascunhoProcesso> _repositorioRascunhosProcesso;
        private IRepositorioGenerico<AnexoRascunho> _repositorioAnexos;
        private IRepositorioGenerico<InteressadoPessoaFisica> _repositorioInteressadosPessoaFisica;
        private IRepositorioGenerico<InteressadoPessoaJuridica> _repositorioInteressadosPessoaJuridica;
        private IRepositorioGenerico<MunicipioRascunhoProcesso> _repositorioMunicipios;
        private IRepositorioGenerico<SinalizacaoRascunhoProcesso> _repositorioSinalizacoes;
        private IRepositorioGenerico<OrganizacaoProcesso> _repositorioOrganizacoesProcesso;

        //Validadores
        private RascunhoProcessoValidacao _validacao;
        private AnexoValidacao _anexoValidacao;
        private InteressadoPessoaFisicaValidacao _interessadoPessoaFisicaValidacao;
        private InteressadoPessoaJuridicaValidacao _interessadoPessoaJuridicaValidacao;
        private MunicipioValidacao _municipioValidacao;
        private IOrganizacaoService _organizacaoService;
        private IUnidadeService _unidadeService;
        private IMunicipioService _municipioService;
        private SinalizacaoValidacao _sinalizacaoValidacao;
        private UsuarioValidacao _usuarioValidacao;

        //Classes de negócio
        private IAnexoNegocio _anexoNegocio;
        private IInteressadoPessoaFisicaNegocio _interessadoPessoaFisicaNegocio;
        private IInteressadoPessoaJuridicaNegocio _interessadoPessoaJuridicaNegocio;
        private IMunicipioNegocio _municipioNegocio;
        private ISinalizacaoNegocio _sinalizacaoNegocio;

        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private ICurrentUserProvider _user;

        public RascunhoProcessoNegocio(IProcessoEletronicoRepositorios repositorios, RascunhoProcessoValidacao validacao,
                                       AnexoValidacao anexoValidacao, InteressadoPessoaFisicaValidacao interessadoPessoaFisicaValidacao,
                                       InteressadoPessoaJuridicaValidacao interessadoPessoaJuridicaValidacao, MunicipioValidacao municipioValidacao,
                                       IOrganizacaoService organizacaoService, IUnidadeService unidadeService, IMunicipioService municipioService,
                                       SinalizacaoValidacao sinalizacaoValidacao, UsuarioValidacao usuarioValidacao, IAnexoNegocio anexoNegocio,
                                       IInteressadoPessoaFisicaNegocio interessadoPessoaFisicaNegocio, IInteressadoPessoaJuridicaNegocio interessadoPessoaJuridicaNegocio,
                                       ISinalizacaoNegocio sinalizacaoNegocio, IMunicipioNegocio municipioNegocio,
                                       IMapper mapper, ICurrentUserProvider user)
        {
            _repositorioRascunhosProcesso = repositorios.RascunhosProcesso;
            _repositorioAnexos = repositorios.AnexosRascunho;
            _repositorioInteressadosPessoaFisica = repositorios.InteressadosPessoaFisica;
            _repositorioInteressadosPessoaJuridica = repositorios.InteressadosPessoaJuridica;
            _repositorioMunicipios = repositorios.MunicipiosRascunhoProcesso;
            _repositorioSinalizacoes = repositorios.SinalizacoesRascunhoProcesso;

            _repositorioOrganizacoesProcesso = repositorios.OrganizacoesProcesso;

            _validacao = validacao;
            _anexoValidacao = anexoValidacao;
            _interessadoPessoaFisicaValidacao = interessadoPessoaFisicaValidacao;
            _interessadoPessoaJuridicaValidacao = interessadoPessoaJuridicaValidacao;
            _municipioValidacao = municipioValidacao;
            _organizacaoService = organizacaoService;
            _unidadeService = unidadeService;
            _municipioService = municipioService;
            _sinalizacaoValidacao = sinalizacaoValidacao;
            _usuarioValidacao = usuarioValidacao;

            _anexoNegocio = anexoNegocio;
            _interessadoPessoaFisicaNegocio = interessadoPessoaFisicaNegocio;
            _interessadoPessoaJuridicaNegocio = interessadoPessoaJuridicaNegocio;
            _municipioNegocio = municipioNegocio;
            _sinalizacaoNegocio = sinalizacaoNegocio;
            _unitOfWork = repositorios.UnitOfWork;
            _mapper = mapper;
            _user = user;
        }

        public RascunhoProcessoModeloNegocio Get(int id)
        {

            RascunhoProcesso rascunhoProcesso = _repositorioRascunhosProcesso.Where(p => p.Id == id)
                                               .Include(p => p.Anexos).ThenInclude(td => td.TipoDocumental)
                                               .Include(p => p.InteressadosPessoaFisica).ThenInclude(ipf => ipf.ContatosRascunho).ThenInclude(c => c.TipoContato)
                                               .Include(p => p.InteressadosPessoaFisica).ThenInclude(ipf => ipf.EmailsRascunho)
                                               .Include(p => p.InteressadosPessoaJuridica).ThenInclude(ipf => ipf.ContatosRascunho).ThenInclude(c => c.TipoContato)
                                               .Include(p => p.InteressadosPessoaJuridica).ThenInclude(ipf => ipf.EmailsRascunho)
                                               .Include(p => p.MunicipiosRascunhoProcesso)
                                               .Include(p => p.SinalizacoesRascunhoProcesso).ThenInclude(sp => sp.Sinalizacao)
                                               .Include(p => p.Atividade)
                                               .SingleOrDefault();
            _validacao.Exists(rascunhoProcesso);

            //Removendo o conteudo dos anexos
            DeleteConteudoAnexos(rascunhoProcesso);

            return _mapper.Map<RascunhoProcesso, RascunhoProcessoModeloNegocio>(rascunhoProcesso);
        }

        public List<RascunhoProcessoModeloNegocio> Get(Guid guidOrganizacao)
        {
            List<RascunhoProcesso> rascunhos = _repositorioRascunhosProcesso.Where(rp => rp.GuidOrganizacao.Equals(guidOrganizacao)).Include(a => a.Atividade).ToList();
            return Mapper.Map<List<RascunhoProcesso>, List<RascunhoProcessoModeloNegocio>>(rascunhos);

        }
        public RascunhoProcessoModeloNegocio Post(RascunhoProcessoModeloNegocio rascunhoProcessoNegocio)
        {
            //Autenticação do usuário
            _usuarioValidacao.Autenticado(_user.UserCpf, _user.UserNome);
            _usuarioValidacao.PossuiOrganizaoPatriarca(_user.UserGuidOrganizacaoPatriarca);
            _usuarioValidacao.PodeSalvarProcessoNaOrganizacao(rascunhoProcessoNegocio, _user.UserGuidOrganizacao);

            //Preechimento de campos obrigatórios
            _validacao.IsFilled(rascunhoProcessoNegocio);
            _anexoValidacao.IsFilled(rascunhoProcessoNegocio.Anexos);
            _interessadoPessoaFisicaValidacao.IsFilled(rascunhoProcessoNegocio.InteressadosPessoaFisica);
            _interessadoPessoaJuridicaValidacao.IsFilled(rascunhoProcessoNegocio.InteressadosPessoaJuridica);
            _municipioValidacao.IsFilled(rascunhoProcessoNegocio.MunicipiosRascunhoProcesso);
            //Como sinalizações são representadas como lista de inteiros, não faz sentido verificar o preechimento um a um, apenas sua validade.

            //Preenchimento correto dos campos
            _validacao.IsValid(rascunhoProcessoNegocio);
            _anexoValidacao.IsValid(rascunhoProcessoNegocio.Anexos, rascunhoProcessoNegocio.Atividade != null ? rascunhoProcessoNegocio.Atividade.Id : (int?)null);
            _interessadoPessoaFisicaValidacao.IsValid(rascunhoProcessoNegocio.InteressadosPessoaFisica);
            _interessadoPessoaJuridicaValidacao.IsValid(rascunhoProcessoNegocio.InteressadosPessoaJuridica);
            _municipioValidacao.IsValid(rascunhoProcessoNegocio.MunicipiosRascunhoProcesso);
            _sinalizacaoValidacao.IsValid(rascunhoProcessoNegocio.Sinalizacoes?.Select(s => s.Id).ToList());

            /*Mapeamento para inserção*/
            RascunhoProcesso rascunhoProcesso = new RascunhoProcesso();
            rascunhoProcesso = _mapper.Map<RascunhoProcessoModeloNegocio, RascunhoProcesso>(rascunhoProcessoNegocio);

            /*Preenchimento das informações que possuem GUID*/
            InformacoesOrganizacao(rascunhoProcesso);
            InformacoesUnidade(rascunhoProcesso);
            InformacoesMunicipio(rascunhoProcesso);
            InformacoesMunicipioInteressadoPessoaFisica(rascunhoProcesso);
            InformacoesMunicipioInteressadoPessoaJuridica(rascunhoProcesso);

            /*Informações padrão, como a data de atuação*/
            InformacaoPadrao(rascunhoProcesso);

            _repositorioRascunhosProcesso.Add(rascunhoProcesso);
            _unitOfWork.Save();

            return Get(rascunhoProcesso.Id);
        }

        public void Patch(int id, RascunhoProcessoModeloNegocio rascunhoProcessoNegocio)
        {
            RascunhoProcesso rascunhoProcesso = _repositorioRascunhosProcesso.Where(rp => rp.Id.Equals(id)).Include(a => a.Anexos).ThenInclude(td => td.TipoDocumental).SingleOrDefault();
            _validacao.Exists(rascunhoProcesso);

            //Autenticacao do usuário
            _usuarioValidacao.Autenticado(_user.UserCpf, _user.UserNome);
            _usuarioValidacao.PossuiOrganizaoPatriarca(_user.UserGuidOrganizacaoPatriarca);
            _usuarioValidacao.PodeSalvarProcessoNaOrganizacao(rascunhoProcessoNegocio, _user.UserGuidOrganizacao);

            _validacao.IsFilled(rascunhoProcessoNegocio);
            _anexoValidacao.IsFilled(rascunhoProcessoNegocio.Anexos);
            _interessadoPessoaFisicaValidacao.IsFilled(rascunhoProcessoNegocio.InteressadosPessoaFisica);
            _interessadoPessoaJuridicaValidacao.IsFilled(rascunhoProcessoNegocio.InteressadosPessoaJuridica);
            _municipioValidacao.IsFilled(rascunhoProcessoNegocio.MunicipiosRascunhoProcesso);
            //Como sinalizações são representadas como lista de inteiros, não faz sentido verificar o preechimento um a um, apenas sua validade.

            //Preenchimento correto dos campos
            _validacao.IsValid(rascunhoProcessoNegocio);
            ClearTiposDocumentaisAnexos(rascunhoProcessoNegocio, rascunhoProcesso);
            _anexoValidacao.IsValid(rascunhoProcessoNegocio.Anexos, rascunhoProcessoNegocio.Atividade?.Id);
            _interessadoPessoaFisicaValidacao.IsValid(rascunhoProcessoNegocio.InteressadosPessoaFisica);
            _interessadoPessoaJuridicaValidacao.IsValid(rascunhoProcessoNegocio.InteressadosPessoaJuridica);
            _municipioValidacao.IsValid(rascunhoProcessoNegocio.MunicipiosRascunhoProcesso);
            _sinalizacaoValidacao.IsValid(rascunhoProcessoNegocio.Sinalizacoes.Select(s => s.Id).ToList());

            MapAlteracaoRascunhoProcesso(rascunhoProcessoNegocio, rascunhoProcesso);
            InformacoesUnidade(rascunhoProcesso);
            _unitOfWork.Save();
        }

        public void Patch(int id, RascunhoProcessoPatchModel rascunhoProcessoPatchModel)
        {
            RascunhoProcesso rascunhoProcesso = _repositorioRascunhosProcesso.Where(rp => rp.Id.Equals(id)).Include(a => a.Anexos).ThenInclude(td => td.TipoDocumental).SingleOrDefault();
            _validacao.Exists(rascunhoProcesso);

            RascunhoProcessoModeloNegocio rascunhoProcessoNegocio = _mapper.Map<RascunhoProcessoModeloNegocio>(rascunhoProcesso);
            _mapper.Map(rascunhoProcessoPatchModel, rascunhoProcessoNegocio);
            
            //Autenticacao do usuário
            _usuarioValidacao.Autenticado(_user.UserCpf, _user.UserNome);
            _usuarioValidacao.PossuiOrganizaoPatriarca(_user.UserGuidOrganizacaoPatriarca);
            _usuarioValidacao.PodeSalvarProcessoNaOrganizacao(rascunhoProcessoNegocio, _user.UserGuidOrganizacao);

            _validacao.IsFilled(rascunhoProcessoNegocio);
            _anexoValidacao.IsFilled(rascunhoProcessoNegocio.Anexos);
            _interessadoPessoaFisicaValidacao.IsFilled(rascunhoProcessoNegocio.InteressadosPessoaFisica);
            _interessadoPessoaJuridicaValidacao.IsFilled(rascunhoProcessoNegocio.InteressadosPessoaJuridica);
            _municipioValidacao.IsFilled(rascunhoProcessoNegocio.MunicipiosRascunhoProcesso);
            //Como sinalizações são representadas como lista de inteiros, não faz sentido verificar o preechimento um a um, apenas sua validade.

            //Preenchimento correto dos campos
            _validacao.IsValid(rascunhoProcessoNegocio);
            ClearTiposDocumentaisAnexos(rascunhoProcessoNegocio, rascunhoProcesso);
            _anexoValidacao.IsValid(rascunhoProcessoNegocio.Anexos, rascunhoProcessoNegocio.Atividade?.Id);
            _interessadoPessoaFisicaValidacao.IsValid(rascunhoProcessoNegocio.InteressadosPessoaFisica);
            _interessadoPessoaJuridicaValidacao.IsValid(rascunhoProcessoNegocio.InteressadosPessoaJuridica);
            _municipioValidacao.IsValid(rascunhoProcessoNegocio.MunicipiosRascunhoProcesso);
            _sinalizacaoValidacao.IsValid(rascunhoProcessoNegocio.Sinalizacoes.Select(s => s.Id).ToList());

            InformacoesUnidade(rascunhoProcesso);
            MapAlteracaoRascunhoProcesso(rascunhoProcessoNegocio, rascunhoProcesso);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            RascunhoProcesso rascunhoProcesso = _repositorioRascunhosProcesso.Where(rp => rp.Id == id).Include(p => p.Anexos)
                                               .Include(p => p.InteressadosPessoaFisica).ThenInclude(ipf => ipf.ContatosRascunho)
                                               .Include(p => p.InteressadosPessoaFisica).ThenInclude(ipf => ipf.EmailsRascunho)
                                               .Include(p => p.InteressadosPessoaJuridica).ThenInclude(ipf => ipf.ContatosRascunho)
                                               .Include(p => p.InteressadosPessoaJuridica).ThenInclude(ipf => ipf.EmailsRascunho)
                                               .Include(p => p.MunicipiosRascunhoProcesso)
                                               .Include(p => p.SinalizacoesRascunhoProcesso)
                                               .Include(p => p.Atividade).SingleOrDefault();
            _validacao.Exists(rascunhoProcesso);

            _anexoNegocio.Delete(rascunhoProcesso.Anexos);
            _interessadoPessoaFisicaNegocio.Delete(rascunhoProcesso.InteressadosPessoaFisica);
            _interessadoPessoaJuridicaNegocio.Delete(rascunhoProcesso.InteressadosPessoaJuridica);
            _municipioNegocio.Delete(rascunhoProcesso.MunicipiosRascunhoProcesso);
            _sinalizacaoNegocio.Delete(rascunhoProcesso.SinalizacoesRascunhoProcesso);

            _repositorioRascunhosProcesso.Remove(rascunhoProcesso);
            _unitOfWork.Save();

        }

        #region Preenchimento de Informações relacionadas com GUID
        private void InformacoesOrganizacao(RascunhoProcesso rascunhoProcesso)
        {
            Organizacao organizacao = _organizacaoService.Search(rascunhoProcesso.GuidOrganizacao).ResponseObject;

            if (organizacao == null)
            {
                throw new RequisicaoInvalidaException("Organização não encontrada no Organograma.");
            }

            rascunhoProcesso.GuidOrganizacao = new Guid(organizacao.Guid);
            rascunhoProcesso.NomeOrganizacao = organizacao.RazaoSocial;
            rascunhoProcesso.SiglaOrganizacao = organizacao.Sigla;
        }

        private void InformacoesUnidade(RascunhoProcesso rascunhoProcesso)
        {
            if (rascunhoProcesso.GuidUnidade.HasValue)
            {
                Unidade unidade = _unidadeService.Search(rascunhoProcesso.GuidUnidade.Value).ResponseObject;

                if (unidade == null)
                {
                    throw new RequisicaoInvalidaException("Unidade não encontrada no Organograma.");
                }

                rascunhoProcesso.NomeUnidade = unidade.Nome;
                rascunhoProcesso.SiglaUnidade = unidade.Sigla;
            }
            else
            {
                rascunhoProcesso.NomeUnidade = null;
                rascunhoProcesso.SiglaUnidade = null;
            }
        }

        private void InformacoesMunicipio(RascunhoProcesso rascunhoProcesso)
        {
            if (rascunhoProcesso.MunicipiosRascunhoProcesso != null && rascunhoProcesso.MunicipiosRascunhoProcesso.Count > 0)
            {

                foreach (MunicipioRascunhoProcesso municipioRascunhoProcesso in rascunhoProcesso.MunicipiosRascunhoProcesso)
                {
                    if (municipioRascunhoProcesso.GuidMunicipio.HasValue)
                    {
                        Municipio municipio = _municipioService.Search(municipioRascunhoProcesso.GuidMunicipio.Value).ResponseObject;

                        if (municipio == null)
                        {
                            throw new RequisicaoInvalidaException("Municipio não encontrado no Organograma.");
                        }

                        municipioRascunhoProcesso.Nome = municipio.Nome;
                        municipioRascunhoProcesso.Uf = municipio.Uf;
                    }
                }
            }
        }

        private void InformacoesMunicipioInteressadoPessoaFisica(RascunhoProcesso rascunhoProcesso)
        {
            if (rascunhoProcesso.InteressadosPessoaFisica != null)
            {
                foreach (InteressadoPessoaFisicaRascunho interessado in rascunhoProcesso.InteressadosPessoaFisica)
                {
                    Municipio municipio = _municipioService.Search(interessado.GuidMunicipio.Value).ResponseObject;

                    if (municipio == null)
                    {
                        throw new RequisicaoInvalidaException("Municipio do interessado pessoa física não encontrado no Organograma.");
                    }

                    interessado.NomeMunicipio = municipio.Nome;
                    interessado.UfMunicipio = municipio.Uf;
                }
            }
        }

        private void InformacoesMunicipioInteressadoPessoaJuridica(RascunhoProcesso rascunhoProcesso)
        {
            if (rascunhoProcesso.InteressadosPessoaJuridica != null)
            {
                foreach (InteressadoPessoaJuridicaRascunho interessado in rascunhoProcesso.InteressadosPessoaJuridica)
                {
                    Municipio municipio = _municipioService.Search(interessado.GuidMunicipio.Value).ResponseObject;

                    if (municipio == null)
                    {
                        throw new RequisicaoInvalidaException("Municipio do interessado pessoa jurídica não encontrado no Organograma.");
                    }

                    interessado.NomeMunicipio = municipio.Nome;
                    interessado.UfMunicipio = municipio.Uf;
                }
            }
        }

        #endregion

        private void InformacaoPadrao(RascunhoProcesso rascunhoProcesso)
        {
            var organizacaoProcesso = _repositorioOrganizacoesProcesso.Where(o => o.GuidOrganizacao.Equals(_user.UserGuidOrganizacaoPatriarca))
                                                            .SingleOrDefault();

            int idOrganizacaoProcesso = organizacaoProcesso.Id;

            rascunhoProcesso.IdOrganizacaoProcesso = idOrganizacaoProcesso;
        }

        private void MapAlteracaoRascunhoProcesso(RascunhoProcessoModeloNegocio rascunhoProcessoNegocio, RascunhoProcesso rascunhoProcesso)
        {
            rascunhoProcesso.IdAtividade = rascunhoProcessoNegocio.Atividade != null && rascunhoProcessoNegocio.Atividade?.Id > 0 ? rascunhoProcessoNegocio.Atividade.Id : (int?)null;
            rascunhoProcesso.Resumo = rascunhoProcessoNegocio.Resumo;
            rascunhoProcesso.GuidUnidade = !string.IsNullOrWhiteSpace(rascunhoProcessoNegocio.GuidUnidade) ? new Guid(rascunhoProcessoNegocio.GuidUnidade) : (Guid?)null;
        }

        private void DeleteConteudoAnexos(RascunhoProcesso rascunhoProcesso)
        {
            if (rascunhoProcesso.Anexos != null)
            {
                foreach (AnexoRascunho anexo in rascunhoProcesso.Anexos)
                {
                    anexo.Conteudo = null;
                }
            }
        }

        private void ClearTiposDocumentaisAnexos(RascunhoProcessoModeloNegocio rascunhoProcessoNegocio, RascunhoProcesso rascunhoProcesso)
        {
            if ((rascunhoProcessoNegocio.Atividade?.Id != rascunhoProcesso.IdAtividade) || rascunhoProcessoNegocio.Atividade == null)
            {
                ClearTiposDocumentaisAnexosModeloNegocio(rascunhoProcessoNegocio);
                ClearTiposDocumentaisAnexosRepositorio(rascunhoProcesso);
            }
        }
        private void ClearTiposDocumentaisAnexosModeloNegocio(RascunhoProcessoModeloNegocio rascunhoProcessoNegocio)
        {
            if (rascunhoProcessoNegocio.Anexos != null)
            {
                foreach (AnexoModeloNegocio anexo in rascunhoProcessoNegocio.Anexos)
                {
                    anexo.TipoDocumental = null;
                }
            }
        }

        private void ClearTiposDocumentaisAnexosRepositorio(RascunhoProcesso rascunhoProcesso)
        {
            if (rascunhoProcesso.Anexos != null)
            {
                foreach (AnexoRascunho anexo in rascunhoProcesso.Anexos)
                {
                    anexo.TipoDocumental = null;
                }
            }
        }

    }
}
