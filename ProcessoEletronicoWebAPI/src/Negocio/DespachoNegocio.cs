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

namespace ProcessoEletronicoService.Negocio
{
    public class DespachoNegocio : BaseNegocio, IDespachoNegocio
    {
        IUnitOfWork unitOfWork;
        IRepositorioGenerico<Anexo> repositorioAnexos;
        IRepositorioGenerico<Despacho> repositorioDespachos;
        IRepositorioGenerico<Processo> repositorioProcessos;

        DespachoValidacao despachoValidacao;
        AnexoValidacao anexoValidacao;
        UsuarioValidacao usuarioValidacao;

        public DespachoNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioDespachos = repositorios.Despachos;
            repositorioProcessos = repositorios.Processos;
            repositorioAnexos = repositorios.Anexos;
            despachoValidacao = new DespachoValidacao(repositorios);
            anexoValidacao = new AnexoValidacao(repositorios);
            usuarioValidacao = new UsuarioValidacao();
        }
        

        public List<DespachoModeloNegocio> PesquisarDespachosUsuario()
        {
            IQueryable<Despacho> query;
            query = repositorioDespachos;

            query = query.Where(d => d.IdUsuarioDespachante.Equals(UsuarioCpf)).Include(p => p.Processo);

            return Mapper.Map<List<Despacho>, List<DespachoModeloNegocio>>(query.ToList());
        }

        public DespachoModeloNegocio PesquisarDespacho(int idDespacho)
        {
            Despacho despacho = repositorioDespachos.Where(d => d.Id == idDespacho)
                                                    .Include(p => p.Processo)
                                                    .Include(a => a.Anexos).ThenInclude(td => td.TipoDocumental)
                                                    .SingleOrDefault();

            despachoValidacao.Existe(despacho);

            //Limpando conteúdo dos anexos para não ser enviado dentro do despacho
            if (despacho.Anexos != null)
            {
                LimparConteudoAnexos(despacho.Anexos);
            }


            return Mapper.Map<Despacho, DespachoModeloNegocio>(despacho);
        }

        public DespachoModeloNegocio Despachar(int idProcesso, DespachoModeloNegocio despachoNegocio)
        {

            /*
            TODO: VERIFICAR SE O USUÁRIO TEM PERMISSÃO PARA EFETUAR O DESPACHO
            (cruzar informações do usuário com o local (organição/unidade) em que o processo se encontra.
            **Informações do usuário são do acesso cidadão** 
            */
            //despachoValidacao.Permissao(despachoNegocio)

            /*Verificar se o usuário tem permissão para realizar o despacho na organização em que ele se encontra*/




            //Obter id da atividade do processo para validação dos anexos do despacho
            int idAtividadeProcesso;
            try
            {
                idAtividadeProcesso = repositorioProcessos.Where(p => p.Id == idProcesso).Select(s => s.IdAtividade).SingleOrDefault();
            }
            catch (Exception)
            {
                idAtividadeProcesso = 0;
            }

            despachoValidacao.Preenchido(despachoNegocio);
            despachoValidacao.Valido(idOrganizacaoProcesso, idProcesso, idAtividadeProcesso, despachoNegocio);

            Despacho despacho = new Despacho();
            PreparaInsercaoDespacho(despachoNegocio, idProcesso);
            Mapper.Map(despachoNegocio, despacho);

            repositorioDespachos.Add(despacho);
            unitOfWork.Save();

            return PesquisarDespacho(despacho.Id, idProcesso, idOrganizacaoProcesso);
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
    }
}
