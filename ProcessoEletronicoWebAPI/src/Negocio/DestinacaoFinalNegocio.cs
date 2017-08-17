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
using ProcessoEletronicoService.Negocio.Validacao;
using ProcessoEletronicoService.Negocio.Comum;

namespace ProcessoEletronicoService.Negocio
{
    public class DestinacaoFinalNegocio : BaseNegocio, IDestinacaoFinalNegocio
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IRepositorioGenerico<DestinacaoFinal> _repositorioDestinacoesFinais;
        private DestinacaoFinalValidacao _validacao;
        
        public DestinacaoFinalNegocio(IProcessoEletronicoRepositorios repositorios, IMapper mapper)
        {
            _unitOfWork = repositorios.UnitOfWork;
            _mapper = mapper;
            _repositorioDestinacoesFinais = repositorios.DestinacoesFinais;
            _validacao = new DestinacaoFinalValidacao(repositorios);
        }

        public List<DestinacaoFinalModeloNegocio> Listar()
        {
            List<DestinacaoFinal> destinacoesFinais = _repositorioDestinacoesFinais.OrderBy(df => df.Descricao).ToList();
            return _mapper.Map<List<DestinacaoFinalModeloNegocio>>(destinacoesFinais);
        }

        public DestinacaoFinalModeloNegocio Pesquisar(int id)
        {
            DestinacaoFinal destinacaoFinal = _repositorioDestinacoesFinais.Where(df => df.Id.Equals(id)).SingleOrDefault();
            _validacao.NaoEncontrado(destinacaoFinal);
            return _mapper.Map<DestinacaoFinalModeloNegocio>(destinacaoFinal);
        }

        public DestinacaoFinalModeloNegocio Inserir(DestinacaoFinalModeloNegocio destinacaoFinalModeloNegocio)
        {
            _validacao.Preenchido(destinacaoFinalModeloNegocio);
            _validacao.Valido(destinacaoFinalModeloNegocio);

            DestinacaoFinal destinacaoFinal = new DestinacaoFinal();
            _mapper.Map(destinacaoFinalModeloNegocio, destinacaoFinal);

            _repositorioDestinacoesFinais.Add(destinacaoFinal);
            _unitOfWork.Save();

            return Pesquisar(destinacaoFinal.Id);
            
        }

        public void Excluir(int id)
        {
            _validacao.PossivelExcluir(id);

            DestinacaoFinal destinacaoFinal = _repositorioDestinacoesFinais.Where(df => df.Id.Equals(id)).Single();

            _repositorioDestinacoesFinais.Remove(destinacaoFinal);
            _unitOfWork.Save();
        }
    }
}
