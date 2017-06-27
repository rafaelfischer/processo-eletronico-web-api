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
        private IRepositorioGenerico<Email> _repositorioEmails;
        private IRepositorioGenerico<Contato> _repositorioContatos;
        private IRepositorioGenerico<InteressadoPessoaFisica> _repositorioInteressadosPessoaFisica;
        private IRepositorioGenerico<InteressadoPessoaJuridica> _repositorioInteressadosPessoaJuridica;
        private IRepositorioGenerico<MunicipioRascunhoProcesso> _repositorioMunicipiosRascunhoProcesso;
        private IRepositorioGenerico<RascunhoProcesso> _repositorioRascunhosProcesso;
        private IRepositorioGenerico<OrganizacaoProcesso> _repositorioOrganizacoesProcesso;
        private IRepositorioGenerico<Anexo> _repositorioAnexos;

        private RascunhoProcessoValidacao _validacao;
        private InteressadoPessoaFisicaValidacao _interessadoPessoaFisicaValidacao;
        private UsuarioValidacao _usuarioValidacao;
        private OrganogramaValidacao _organogramaValidacao;
        private IInteressadoPessoaFisicaNegocio _interessadoPessoaFisicaNegocio;
        //private IInteressadoPessoaJuridicaNegocio _interessadoPessoaJuridicaNegocio;
        private MunicipioRascunhoProcessoNegocio _municipioRascunhoProcessoNegocio;
        private SinalizacaoRascunhoProcessoNegocio _sinalizacaoRascunhoProcessoNegocio;

        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private ICurrentUserProvider _user;

        public RascunhoProcessoNegocio(IProcessoEletronicoRepositorios repositorios, IMapper mapper, ICurrentUserProvider user, RascunhoProcessoValidacao validacao, InteressadoPessoaFisicaValidacao interessadoPessoaFisicaValidacao, IInteressadoPessoaFisicaNegocio interessadoPessoaFisicaNegocio, UsuarioValidacao usuarioValidacao, OrganogramaValidacao organogramaValidacao)
        {
            _repositorioEmails = repositorios.Emails;
            _repositorioContatos = repositorios.Contatos;
            _repositorioInteressadosPessoaFisica = repositorios.InteressadosPessoaFisica;
            _repositorioInteressadosPessoaJuridica = repositorios.InteressadosPessoaJuridica;
            _repositorioMunicipiosRascunhoProcesso = repositorios.MunicipiosRascunhoProcesso;
            _repositorioRascunhosProcesso = repositorios.RascunhosProcesso;
            _repositorioOrganizacoesProcesso = repositorios.OrganizacoesProcesso;
            _validacao = validacao;
            _interessadoPessoaFisicaValidacao = interessadoPessoaFisicaValidacao;
            _usuarioValidacao = usuarioValidacao;
            _organogramaValidacao = organogramaValidacao;
            _interessadoPessoaFisicaNegocio = interessadoPessoaFisicaNegocio;
            //_interessadoPessoaJuridicaNegocio = interessadoPessoaJuridicaNegocio;
            _municipioRascunhoProcessoNegocio = new MunicipioRascunhoProcessoNegocio(repositorios);
            _sinalizacaoRascunhoProcessoNegocio = new SinalizacaoRascunhoProcessoNegocio(repositorios);
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
            return _mapper.Map<RascunhoProcesso, RascunhoProcessoModeloNegocio>(rascunhoProcesso);
        }

        public List<RascunhoProcessoModeloNegocio> Get(Guid guidOrganizacao)
        {
            List<RascunhoProcesso> rascunhos = _repositorioRascunhosProcesso.Where(rp => rp.GuidOrganizacao.Equals(guidOrganizacao)).ToList();
            return Mapper.Map<List<RascunhoProcesso>, List<RascunhoProcessoModeloNegocio>>(rascunhos);

        }
        public RascunhoProcessoModeloNegocio Post(RascunhoProcessoModeloNegocio rascunhoProcessoNegocio)
        {
            /*Validações*/
            _validacao.IsFilled(rascunhoProcessoNegocio);
            _interessadoPessoaFisicaValidacao.IsFilled(rascunhoProcessoNegocio.InteressadosPessoaFisica);

            _validacao.IsValid(rascunhoProcessoNegocio);
            _interessadoPessoaFisicaValidacao.IsValid(rascunhoProcessoNegocio.InteressadosPessoaFisica);

            //Municipios, Sinalizacoes, Anexos

            _usuarioValidacao.Autenticado(_user.UserCpf, _user.UserNome);
            _usuarioValidacao.PossuiOrganizaoPatriarca(_user.UserGuidOrganizacaoPatriarca);
            _usuarioValidacao.PodeSalvarProcessoNaOrganizacao(rascunhoProcessoNegocio, _user.UserGuidOrganizacao);

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

            _validacao.IsFilled(rascunhoProcessoNegocio);
            _usuarioValidacao.Autenticado(_user.UserCpf, _user.UserNome);
            _usuarioValidacao.PossuiOrganizaoPatriarca(_user.UserGuidOrganizacaoPatriarca);
            _usuarioValidacao.PodeSalvarProcessoNaOrganizacao(rascunhoProcessoNegocio, _user.UserGuidOrganizacao);

            /*Validações*/
            _validacao.IsValid(rascunhoProcessoNegocio);

            MapAlteracaoRascunhoProcesso(rascunhoProcessoNegocio, rascunhoProcesso);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            RascunhoProcesso rascunhoProcesso = _repositorioRascunhosProcesso.Where(rp => rp.Id == id).SingleOrDefault();
            _validacao.Exists(rascunhoProcesso);

            _interessadoPessoaFisicaNegocio.Delete(rascunhoProcesso.InteressadosPessoaFisica);
            //_interessadoPessoaJuridicaNegocio.Excluir(rascunhoProcesso.InteressadosPessoaJuridica);
            _municipioRascunhoProcessoNegocio.Excluir(rascunhoProcesso.MunicipiosRascunhoProcesso);
            _sinalizacaoRascunhoProcessoNegocio.Excluir(rascunhoProcesso.SinalizacoesRascunhoProcesso);

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

    }
}
