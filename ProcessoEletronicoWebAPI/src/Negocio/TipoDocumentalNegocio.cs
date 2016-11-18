using AutoMapper;
using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;


namespace ProcessoEletronicoService.Negocio
{
    public class TipoDocumentalNegocio : ITipoDocumentalNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<TipoDocumental> repositorioTiposDocumentais;


        public TipoDocumentalNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioTiposDocumentais = repositorios.TiposDocumentais;
        }

        public List<TipoDocumentalModeloNegocio> Listar(int idOrganizacaoPatriarca, int idAtividade)
        {
            IQueryable<TipoDocumental> query;

            query = repositorioTiposDocumentais.Include(td => td.Atividade);
            query = query.Where(td => td.Atividade.Funcao.PlanoClassificacao.OrganizacaoProcesso.IdOrganizacao == idOrganizacaoPatriarca);

            if (idAtividade > 0)
            {
                query = query.Where(td => td.IdAtividade == idAtividade);
            }

            List<TipoDocumental> tiposDocumentais = query.ToList();

            return Mapper.Map<List<TipoDocumental>, List<TipoDocumentalModeloNegocio>>(tiposDocumentais);
        }

        
    }
}
