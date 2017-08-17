using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao;
using ProcessoEletronicoService.Negocio.Comum.Validacao;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
using ProcessoEletronicoService.Negocio.Comum.Base;
using static ProcessoEletronicoService.Negocio.Comum.Validacao.OrganogramaValidacao;

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
        private OrganogramaValidacao _organogramaValidacao;
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
                                       OrganogramaValidacao organogramaValidacao, SinalizacaoValidacao sinalizacaoValidacao, 
                                       UsuarioValidacao usuarioValidacao, IAnexoNegocio anexoNegocio, 
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
            _organogramaValidacao = organogramaValidacao;
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
                                               .Include(p => p.Anexos)
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
            List<RascunhoProcesso> rascunhos = _repositorioRascunhosProcesso.Where(rp => rp.GuidOrganizacao.Equals(guidOrganizacao)).ToList();
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
            _anexoValidacao.IsValid(rascunhoProcessoNegocio.Anexos, rascunhoProcessoNegocio.Atividade != null ? rascunhoProcessoNegocio.Atividade.Id : (int?) null);
            _interessadoPessoaFisicaValidacao.IsValid(rascunhoProcessoNegocio.InteressadosPessoaFisica);
            _interessadoPessoaJuridicaValidacao.IsValid(rascunhoProcessoNegocio.InteressadosPessoaJuridica);
            _municipioValidacao.IsValid(rascunhoProcessoNegocio.MunicipiosRascunhoProcesso);
            _sinalizacaoValidacao.IsValid(rascunhoProcessoNegocio.Sinalizacoes.Select(s => s.Id).ToList());
            
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
            RascunhoProcesso rascunhoProcesso = _repositorioRascunhosProcesso.Where(rp => rp.Id.Equals(id)).SingleOrDefault();
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
            _anexoValidacao.IsValid(rascunhoProcessoNegocio.Anexos, rascunhoProcessoNegocio.Atividade != null ? rascunhoProcessoNegocio.Atividade.Id : (int?)null);
            _interessadoPessoaFisicaValidacao.IsValid(rascunhoProcessoNegocio.InteressadosPessoaFisica);
            _interessadoPessoaJuridicaValidacao.IsValid(rascunhoProcessoNegocio.InteressadosPessoaJuridica);
            _municipioValidacao.IsValid(rascunhoProcessoNegocio.MunicipiosRascunhoProcesso);
            _sinalizacaoValidacao.IsValid(rascunhoProcessoNegocio.Sinalizacoes.Select(s => s.Id).ToList());
            
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
            OrganizacaoOrganogramaModelo organizacao = _organogramaValidacao.PesquisarOrganizacao(rascunhoProcesso.GuidOrganizacao);

            if (organizacao == null)
            {
                throw new RequisicaoInvalidaException("Organização não encontrada no Organograma.");
            }

            rascunhoProcesso.GuidOrganizacao = new Guid(organizacao.guid);
            rascunhoProcesso.NomeOrganizacao = organizacao.razaoSocial;
            rascunhoProcesso.SiglaOrganizacao = organizacao.sigla;
        }

        private void InformacoesUnidade(RascunhoProcesso rascunhoProcesso)
        {
            UnidadeOrganogramaModelo unidade = _organogramaValidacao.PesquisarUnidade(rascunhoProcesso.GuidUnidade);

            if (unidade == null)
            {
                throw new RequisicaoInvalidaException("Unidade não encontrada no Organograma.");
            }

            rascunhoProcesso.GuidUnidade = new Guid(unidade.guid);
            rascunhoProcesso.NomeUnidade = unidade.nome;
            rascunhoProcesso.SiglaUnidade = unidade.sigla;
        }

        private void InformacoesMunicipio(RascunhoProcesso rascunhoProcesso)
        {
            if (rascunhoProcesso.MunicipiosRascunhoProcesso != null && rascunhoProcesso.MunicipiosRascunhoProcesso.Count > 0)
            {

                foreach (MunicipioRascunhoProcesso municipio in rascunhoProcesso.MunicipiosRascunhoProcesso)
                {
                    if (municipio.GuidMunicipio.HasValue)
                    {
                        MunicipioOrganogramaModelo municipioOrganograma = _organogramaValidacao.PesquisarMunicipio(municipio.GuidMunicipio.Value);

                        if (municipioOrganograma == null)
                        {
                            throw new RequisicaoInvalidaException("Municipio não encontrado no Organograma.");
                        }

                        municipio.Nome = municipioOrganograma.nome;
                        municipio.Uf = municipioOrganograma.uf;
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
                    MunicipioOrganogramaModelo municipioOrganograma = _organogramaValidacao.PesquisarMunicipio(interessado.GuidMunicipio.Value);

                    if (municipioOrganograma == null)
                    {
                        throw new RequisicaoInvalidaException("Municipio do interessado pessoa física não encontrado no Organograma.");
                    }

                    interessado.NomeMunicipio = municipioOrganograma.nome;
                    interessado.UfMunicipio = municipioOrganograma.uf;
                }
            }
        }

        private void InformacoesMunicipioInteressadoPessoaJuridica(RascunhoProcesso rascunhoProcesso)
        {
            if (rascunhoProcesso.InteressadosPessoaJuridica != null)
            {
                foreach (InteressadoPessoaJuridicaRascunho interessado in rascunhoProcesso.InteressadosPessoaJuridica)
                {
                    MunicipioOrganogramaModelo municipioOrganograma = _organogramaValidacao.PesquisarMunicipio(interessado.GuidMunicipio.Value);

                    if (municipioOrganograma == null)
                    {
                        throw new RequisicaoInvalidaException("Municipio do interessado pessoa jurídica não encontrado no Organograma.");
                    }

                    interessado.NomeMunicipio = municipioOrganograma.nome;
                    interessado.UfMunicipio = municipioOrganograma.uf;
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
            rascunhoProcesso.IdAtividade = rascunhoProcessoNegocio.Atividade != null ? rascunhoProcessoNegocio.Atividade.Id : (int?)null;
            rascunhoProcesso.Resumo = rascunhoProcessoNegocio.Resumo;
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

    }
}
