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
using ProcessoEletronicoService.Negocio.Comum;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao;
using ProcessoEletronicoService.Negocio.Comum.Validacao;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo
{
    public class RascunhoProcessoNegocio : BaseNegocio, IRascunhoProcessoNegocio
    {
        IUnitOfWork unitOfWork;
        IRepositorioGenerico<Email> repositorioEmails;
        IRepositorioGenerico<Contato> repositorioContatos;
        IRepositorioGenerico<InteressadoPessoaFisica> repositorioInteressadosPessoaFisica;
        IRepositorioGenerico<InteressadoPessoaJuridica> repositorioInteressadosPessoaJuridica;
        IRepositorioGenerico<MunicipioRascunhoProcesso> repositorioMunicipiosRascunhoProcesso;
        IRepositorioGenerico<RascunhoProcesso> repositorioRascunhosProcesso;
        IRepositorioGenerico<OrganizacaoProcesso> repositorioOrganizacoesProcesso;
        IRepositorioGenerico<Anexo> repositorioAnexos;

        RascunhoProcessoValidacao rascunhoProcessoValidacao;
        UsuarioValidacao usuarioValidacao;
        InteressadoPessoaFisicaNegocio interessadoPessoaFisicaNegocio;
        InteressadoPessoaJuridicaNegocio interessadoPessoaJuridicaNegocio;
        MunicipioRascunhoProcessoNegocio municipioRascunhoProcessoNegocio;
        SinalizacaoRascunhoProcessoNegocio sinalizacaoRascunhoProcessoNegocio;

        private IMapper _mapper;

        public RascunhoProcessoNegocio(IProcessoEletronicoRepositorios repositorios, IMapper mapper)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioEmails = repositorios.Emails;
            repositorioContatos = repositorios.Contatos;
            repositorioInteressadosPessoaFisica = repositorios.InteressadosPessoaFisica;
            repositorioInteressadosPessoaJuridica = repositorios.InteressadosPessoaJuridica;
            repositorioMunicipiosRascunhoProcesso = repositorios.MunicipiosRascunhoProcesso;
            repositorioRascunhosProcesso = repositorios.RascunhosProcesso;
            repositorioOrganizacoesProcesso = repositorios.OrganizacoesProcesso;
            rascunhoProcessoValidacao = new RascunhoProcessoValidacao(repositorios);
            usuarioValidacao = new UsuarioValidacao();
            //interessadoPessoaFisicaNegocio = new InteressadoPessoaFisicaNegocio(repositorios, mapper);
            interessadoPessoaJuridicaNegocio = new InteressadoPessoaJuridicaNegocio(repositorios);
            municipioRascunhoProcessoNegocio = new MunicipioRascunhoProcessoNegocio(repositorios);
            sinalizacaoRascunhoProcessoNegocio = new SinalizacaoRascunhoProcessoNegocio(repositorios);
            _mapper = mapper;
        }

        public RascunhoProcessoModeloNegocio Pesquisar(int id)
        {

            RascunhoProcesso rascunhoProcesso = repositorioRascunhosProcesso.Where(p => p.Id == id)
                                               .Include(p => p.Anexos).ThenInclude(td => td.TipoDocumental)
                                               .Include(p => p.InteressadosPessoaFisica).ThenInclude(ipf => ipf.Contatos).ThenInclude(c => c.TipoContato)
                                               .Include(p => p.InteressadosPessoaFisica).ThenInclude(ipf => ipf.Emails)
                                               .Include(p => p.InteressadosPessoaJuridica).ThenInclude(ipf => ipf.Contatos).ThenInclude(c => c.TipoContato)
                                               .Include(p => p.InteressadosPessoaJuridica).ThenInclude(ipf => ipf.Emails)
                                               .Include(p => p.MunicipiosRascunhoProcesso)
                                               .Include(p => p.SinalizacoesRascunhoProcesso).ThenInclude(sp => sp.Sinalizacao)
                                               .Include(p => p.Atividade).ThenInclude(a => a.Funcao).ThenInclude(f => f.PlanoClassificacao)
                                               .SingleOrDefault();

            rascunhoProcessoValidacao.NaoEncontrado(rascunhoProcesso);
            return _mapper.Map<RascunhoProcesso, RascunhoProcessoModeloNegocio>(rascunhoProcesso);
        }

        public List<RascunhoProcessoModeloNegocio> PesquisarRascunhosProcessoNaOrganizacao(Guid guidOrganizacao)
        {
            List<RascunhoProcesso> rascunhos = repositorioRascunhosProcesso.Where(rp => rp.GuidOrganizacao.Equals(guidOrganizacao)).ToList();
            return Mapper.Map<List<RascunhoProcesso>, List<RascunhoProcessoModeloNegocio>>(rascunhos);

        }
        public RascunhoProcessoModeloNegocio Salvar(RascunhoProcessoModeloNegocio rascunhoProcessoNegocio)
        {
            usuarioValidacao.Autenticado(UsuarioCpf, UsuarioNome);
            usuarioValidacao.PossuiOrganizaoPatriarca(UsuarioGuidOrganizacaoPatriarca);
            usuarioValidacao.PodeSalvarProcessoNaOrganizacao(rascunhoProcessoNegocio, UsuarioGuidOrganizacao);

            /*Validações*/
            rascunhoProcessoValidacao.Valido(rascunhoProcessoNegocio, UsuarioGuidOrganizacao);

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
            
            repositorioRascunhosProcesso.Add(rascunhoProcesso);
            unitOfWork.Save();

            return Pesquisar(rascunhoProcesso.Id);
        }

        public void Alterar(int id, RascunhoProcessoModeloNegocio rascunhoProcessoNegocio)
        {
            RascunhoProcesso rascunhoProcesso = repositorioRascunhosProcesso.Where(rp => rp.Id.Equals(id)).SingleOrDefault();
            rascunhoProcessoValidacao.NaoEncontrado(rascunhoProcesso);

            rascunhoProcessoValidacao.Preenchido(rascunhoProcessoNegocio);
            usuarioValidacao.Autenticado(UsuarioCpf, UsuarioNome);
            usuarioValidacao.PossuiOrganizaoPatriarca(UsuarioGuidOrganizacaoPatriarca);
            usuarioValidacao.PodeSalvarProcessoNaOrganizacao(rascunhoProcessoNegocio, UsuarioGuidOrganizacao);

            /*Validações*/
            rascunhoProcessoValidacao.Valido(rascunhoProcessoNegocio, UsuarioGuidOrganizacao);

            MapAlteracaoRascunhoProcesso(rascunhoProcessoNegocio, rascunhoProcesso);
            unitOfWork.Save();
        }
        
        public void Excluir(int id)
        {
            RascunhoProcesso rascunhoProcesso = repositorioRascunhosProcesso.Where(rp => rp.Id.Equals(id)).Include(p => p.Anexos)
                                               .Include(p => p.InteressadosPessoaFisica).ThenInclude(ipf => ipf.Contatos)
                                               .Include(p => p.InteressadosPessoaFisica).ThenInclude(ipf => ipf.Emails)
                                               .Include(p => p.InteressadosPessoaJuridica).ThenInclude(ipf => ipf.Contatos)
                                               .Include(p => p.InteressadosPessoaJuridica).ThenInclude(ipf => ipf.Emails)
                                               .Include(p => p.MunicipiosRascunhoProcesso)
                                               .Include(p => p.SinalizacoesRascunhoProcesso)
                                               .SingleOrDefault();
            rascunhoProcessoValidacao.NaoEncontrado(rascunhoProcesso);

            interessadoPessoaFisicaNegocio.Delete(rascunhoProcesso.InteressadosPessoaFisica);
            interessadoPessoaJuridicaNegocio.Excluir(rascunhoProcesso.InteressadosPessoaJuridica);
            municipioRascunhoProcessoNegocio.Excluir(rascunhoProcesso.MunicipiosRascunhoProcesso);
            sinalizacaoRascunhoProcessoNegocio.Excluir(rascunhoProcesso.SinalizacoesRascunhoProcesso);

            repositorioRascunhosProcesso.Remove(rascunhoProcesso);
            unitOfWork.Save();

        }

        #region Preenchimento de Informações relacionadas com GUID
        private void InformacoesOrganizacao(RascunhoProcesso rascunhoProcesso)
        {
            OrganizacaoOrganogramaModelo organizacao = PesquisarOrganizacao(rascunhoProcesso.GuidOrganizacao);

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
            UnidadeOrganogramaModelo unidade = PesquisarUnidade(rascunhoProcesso.GuidUnidade);

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
                    MunicipioOrganogramaModelo municipioOrganograma = PesquisarMunicipio(municipio.GuidMunicipio);

                    if (municipioOrganograma == null)
                    {
                        throw new RequisicaoInvalidaException("Municipio não encontrado no Organograma.");
                    }

                    municipio.Nome = municipioOrganograma.nome;
                    municipio.Uf = municipioOrganograma.uf;

                }
            }
        }

        private void InformacoesMunicipioInteressadoPessoaFisica(RascunhoProcesso rascunhoProcesso)
        {
            if (rascunhoProcesso.InteressadosPessoaFisica != null)
            {
                foreach (InteressadoPessoaFisica interessado in rascunhoProcesso.InteressadosPessoaFisica)
                {
                    MunicipioOrganogramaModelo municipioOrganograma = PesquisarMunicipio(interessado.GuidMunicipio);

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
                foreach (InteressadoPessoaJuridica interessado in rascunhoProcesso.InteressadosPessoaJuridica)
                {
                    MunicipioOrganogramaModelo municipioOrganograma = PesquisarMunicipio(interessado.GuidMunicipio);

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
            var organizacaoProcesso = repositorioOrganizacoesProcesso.Where(o => o.GuidOrganizacao.Equals(UsuarioGuidOrganizacaoPatriarca))
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
