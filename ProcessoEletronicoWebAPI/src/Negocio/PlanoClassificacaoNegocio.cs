using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Restrito.Validacao;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProcessoEletronicoService.Negocio.Validacao;

namespace ProcessoEletronicoService.Negocio
{
    public class PlanoClassificacaoNegocio : BaseNegocio, IPlanoClassificacaoNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<PlanoClassificacao> repositorioPlanosClassificacao;
        private IRepositorioGenerico<OrganizacaoProcesso> repositorioOrganizacoesProcesso;
        PlanoClassificacaoValidacao planoClassificacaoValidacao;

        public PlanoClassificacaoNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioPlanosClassificacao = repositorios.PlanosClassificacao;
            repositorioOrganizacoesProcesso = repositorios.OrganizacoesProcesso;
            planoClassificacaoValidacao = new PlanoClassificacaoValidacao(repositorios);
        }
        
        public List<PlanoClassificacaoModeloNegocio> Pesquisar(string guidOrganizacao)
        {
            planoClassificacaoValidacao.GuidValido(guidOrganizacao);

            Guid gOrganizacao = new Guid(guidOrganizacao);

            OrganizacaoOrganogramaModelo organizacaoPatriarca = PesquisarOrganizacaoPatriarca(gOrganizacao);

            planoClassificacaoValidacao.OrganizacaoPatriarcaExistente(organizacaoPatriarca);
            planoClassificacaoValidacao.GuidValido(organizacaoPatriarca.guid);

            var planosClassificacao = repositorioPlanosClassificacao.Where(pc => pc.OrganizacaoProcesso.GuidOrganizacao.Equals(new Guid(organizacaoPatriarca.guid))
                                                                              && (pc.GuidOrganizacao.Equals(gOrganizacao)
                                                                              ||  !pc.AreaFim))
                                                                    .Include(pc => pc.OrganizacaoProcesso)
                                                                    .ToList();

            return Mapper.Map<List<PlanoClassificacao>, List<PlanoClassificacaoModeloNegocio>>(planosClassificacao);
        }

        public PlanoClassificacaoModeloNegocio Pesquisar(int id)
        {
            PlanoClassificacao planoClassificacao = repositorioPlanosClassificacao.Where(pc => pc.Id == id).Include(op => op.OrganizacaoProcesso).SingleOrDefault();

            planoClassificacaoValidacao.NaoEncontrado(planoClassificacao);

            return Mapper.Map<PlanoClassificacao, PlanoClassificacaoModeloNegocio>(planoClassificacao);
        }

        public List<PlanoClassificacaoModeloNegocio> Pesquisar()
        {
            var planosClassificacao = repositorioPlanosClassificacao.Where(pc => pc.GuidOrganizacao.Equals(UsuarioGuidOrganizacao))
                                                                    .Include(pc => pc.OrganizacaoProcesso)
                                                                    .ToList();

            return Mapper.Map<List<PlanoClassificacao>, List<PlanoClassificacaoModeloNegocio>>(planosClassificacao);
        }

        public PlanoClassificacaoModeloNegocio Inserir(PlanoClassificacaoModeloNegocio planoClassificacaoModeloNegocio)
        {
            
            planoClassificacaoValidacao.Preenchido(planoClassificacaoModeloNegocio);
            planoClassificacaoValidacao.Valido(planoClassificacaoModeloNegocio);
            planoClassificacaoValidacao.Permissao(planoClassificacaoModeloNegocio, UsuarioGuidOrganizacao);
            InformacoesPadrao(planoClassificacaoModeloNegocio);

            PlanoClassificacao planoClassificacao = new PlanoClassificacao();
            Mapper.Map(planoClassificacaoModeloNegocio, planoClassificacao);


            repositorioPlanosClassificacao.Add(planoClassificacao);
            unitOfWork.Save(); 

            return Pesquisar(planoClassificacao.Id);
        }

        private void InformacoesPadrao(PlanoClassificacaoModeloNegocio planoClassificacao)
        {
            string organizacaoPatriarca = UsuarioGuidOrganizacaoPatriarca.ToString("D");
            planoClassificacao.OrganizacaoProcesso = new OrganizacaoProcessoModeloNegocio { Id = repositorioOrganizacoesProcesso.Where(op => op.GuidOrganizacao.Equals(UsuarioGuidOrganizacaoPatriarca)).Single().Id };
        }

        public void Excluir(int id)
        {
            planoClassificacaoValidacao.PossivelExcluir(id);
            PlanoClassificacao planoClassificacao = repositorioPlanosClassificacao.Where(pc => pc.Id == id).Single();
            repositorioPlanosClassificacao.Remove(planoClassificacao);
            unitOfWork.Save();       
        }
    }
}
