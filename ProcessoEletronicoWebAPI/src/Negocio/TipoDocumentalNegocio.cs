using AutoMapper;
using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using Microsoft.EntityFrameworkCore;
using ProcessoEletronicoService.Negocio.Restrito.Validacao;

namespace ProcessoEletronicoService.Negocio
{
    public class TipoDocumentalNegocio : BaseNegocio, ITipoDocumentalNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<TipoDocumental> repositorioTiposDocumentais;
        private TipoDocumentalValidacao tipoDocumentalValidacao;


        public TipoDocumentalNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioTiposDocumentais = repositorios.TiposDocumentais;
            tipoDocumentalValidacao = new TipoDocumentalValidacao(repositorios);
        }

        public TipoDocumentalModeloNegocio Pesquisar(int id)
        {
            TipoDocumental tipoDocumental = repositorioTiposDocumentais.Where(td => td.Id == id).Include(td => td.Atividade).Include(td => td.DestinacaoFinal).SingleOrDefault();

            tipoDocumentalValidacao.NaoEncontrado(tipoDocumental);

            return Mapper.Map<TipoDocumental, TipoDocumentalModeloNegocio>(tipoDocumental);
        }

        public List<TipoDocumentalModeloNegocio> PesquisarPorAtividade(int idAtividade)
        {
            var tiposDocumentais = repositorioTiposDocumentais.Where(td => td.IdAtividade == idAtividade)
                                                              .Include(td => td.Atividade)
                                                              .ToList();

            return Mapper.Map<List<TipoDocumental>, List<TipoDocumentalModeloNegocio>>(tiposDocumentais);
        }

        public TipoDocumentalModeloNegocio Inserir (TipoDocumentalModeloNegocio tipoDocumentalModeloNegocio)
        {
            tipoDocumentalValidacao.Preenchido(tipoDocumentalModeloNegocio);
            tipoDocumentalValidacao.Valido(tipoDocumentalModeloNegocio, UsuarioGuidOrganizacao);

            TipoDocumental tipoDocumental = new TipoDocumental();
            Mapper.Map(tipoDocumentalModeloNegocio, tipoDocumental);

            repositorioTiposDocumentais.Add(tipoDocumental);
            unitOfWork.Save();

            return Pesquisar(tipoDocumental.Id);

        }

        public void Excluir(int id)
        {
            tipoDocumentalValidacao.PossivelExcluir(id);
            TipoDocumental tipoDocumental = repositorioTiposDocumentais.Where(td => td.Id == id).Single();

            repositorioTiposDocumentais.Remove(tipoDocumental);
            unitOfWork.Save();
        }
    }
}
