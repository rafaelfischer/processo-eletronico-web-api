using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Base;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Restrito.Validacao;
using System.Collections.Generic;
using System.Linq;

namespace ProcessoEletronicoService.Negocio
{
    public class TipoDocumentalNegocio : ITipoDocumentalNegocio
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ICurrentUserProvider _user;
        private IRepositorioGenerico<TipoDocumental> _repositorioTiposDocumentais;
        private TipoDocumentalValidacao _validacao;

        public TipoDocumentalNegocio(IProcessoEletronicoRepositorios repositorios, IMapper mapper, ICurrentUserProvider user)
        {
            _unitOfWork = repositorios.UnitOfWork;
            _mapper = mapper;
            _user = user;
            _repositorioTiposDocumentais = repositorios.TiposDocumentais;
            _validacao = new TipoDocumentalValidacao(repositorios);
        }

        public TipoDocumentalModeloNegocio Pesquisar(int id)
        {
            TipoDocumental tipoDocumental = _repositorioTiposDocumentais.Where(td => td.Id == id).Include(td => td.Atividade).Include(td => td.DestinacaoFinal).SingleOrDefault();
            _validacao.NaoEncontrado(tipoDocumental);

            return _mapper.Map<TipoDocumentalModeloNegocio>(tipoDocumental);
        }

        public List<TipoDocumentalModeloNegocio> PesquisarPorAtividade(int idAtividade)
        {
            var tiposDocumentais = _repositorioTiposDocumentais.Where(td => td.IdAtividade == idAtividade)
                                                              .Include(td => td.Atividade)
                                                              .ToList();

            return _mapper.Map<List<TipoDocumentalModeloNegocio>>(tiposDocumentais);
        }

        public TipoDocumentalModeloNegocio Inserir (TipoDocumentalModeloNegocio tipoDocumentalModeloNegocio)
        {
            _validacao.Preenchido(tipoDocumentalModeloNegocio);
            _validacao.Valido(tipoDocumentalModeloNegocio, _user.UserGuidOrganizacao);

            TipoDocumental tipoDocumental = new TipoDocumental();
            Mapper.Map(tipoDocumentalModeloNegocio, tipoDocumental);

            _repositorioTiposDocumentais.Add(tipoDocumental);
            _unitOfWork.Save();

            return Pesquisar(tipoDocumental.Id);

        }

        public void Excluir(int id)
        {
            _validacao.PossivelExcluir(id);
            TipoDocumental tipoDocumental = _repositorioTiposDocumentais.Where(td => td.Id == id).Single();

            _repositorioTiposDocumentais.Remove(tipoDocumental);
            _unitOfWork.Save();
        }
    }
}
