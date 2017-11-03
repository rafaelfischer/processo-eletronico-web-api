using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao;
using AutoMapper;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo
{
    public class SinalizacaoNegocio : ISinalizacaoNegocio
    {
        private IRepositorioGenerico<SinalizacaoRascunhoProcesso> _repositorioSinalizacoesRascunhoProcesso;
        private IRepositorioGenerico<Sinalizacao> _repositorioSinalizacoes;
        private IRepositorioGenerico<RascunhoProcesso> _repositorioRascunhos;
        private SinalizacaoValidacao _validacao;
        private RascunhoProcessoValidacao _rascunhoProcessoValidacao;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public SinalizacaoNegocio(IProcessoEletronicoRepositorios repositorios, SinalizacaoValidacao validacao, RascunhoProcessoValidacao rascunhoProcessoValidacao, IMapper mapper)
        {
            _repositorioSinalizacoesRascunhoProcesso = repositorios.SinalizacoesRascunhoProcesso;
            _repositorioSinalizacoes = repositorios.Sinalizacoes;
            _repositorioRascunhos = repositorios.RascunhosProcesso;
            _validacao = validacao;
            _rascunhoProcessoValidacao = rascunhoProcessoValidacao;
            _unitOfWork = repositorios.UnitOfWork;
            _mapper = mapper;
        }

        public IList<SinalizacaoModeloNegocio> Get(int idRascunhoProcesso)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            return _mapper.Map<IList<SinalizacaoModeloNegocio>>(_repositorioSinalizacoes.Where(m => m.SinalizacaoRascunhoProcesso.Any(srp => srp.IdRascunhoProcesso == idRascunhoProcesso))).ToList();
        }

        public SinalizacaoModeloNegocio Get(int idRascunhoProcesso, int idSinalizacao)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);

            Sinalizacao sinalizacao = _repositorioSinalizacoes.Where(m => m.SinalizacaoRascunhoProcesso.Any(srp => srp.IdRascunhoProcesso == idRascunhoProcesso && srp.IdSinalizacao == idSinalizacao)).SingleOrDefault();

            _validacao.Exists(sinalizacao);
            return _mapper.Map<SinalizacaoModeloNegocio>(sinalizacao);
        }

        public IList<SinalizacaoModeloNegocio> Post(int idRascunhoProcesso, IList<int> idsSinalizacoes)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _validacao.IsValid(idsSinalizacoes);
            foreach (int idSinalizacao in idsSinalizacoes)
            {
                //Incluir sinalização no rascunho (caso já não esteja incluso)
                if (!_validacao.ExistsInRascunhoProcesso(idRascunhoProcesso, idSinalizacao))
                {
                    _repositorioSinalizacoesRascunhoProcesso.Add(new SinalizacaoRascunhoProcesso { IdRascunhoProcesso = idRascunhoProcesso, IdSinalizacao = idSinalizacao });
                }
            }

            _unitOfWork.Save();
            return Get(idRascunhoProcesso);
        }

        public IList<SinalizacaoModeloNegocio> Put(int idRascunhoProcesso, IList<int> idsSinalizacoes)
        {
            DeleteAll(idRascunhoProcesso);
            _validacao.IsValid(idsSinalizacoes);

            foreach (int idSinalizacao in idsSinalizacoes)
            {
               _repositorioSinalizacoesRascunhoProcesso.Add(new SinalizacaoRascunhoProcesso { IdRascunhoProcesso = idRascunhoProcesso, IdSinalizacao = idSinalizacao });
            }

            _unitOfWork.Save();
            return Get(idRascunhoProcesso);
        }

        public void DeleteAll(int idRascunhoProcesso)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);

            IEnumerable<SinalizacaoRascunhoProcesso> sinalizacoesRascunho = _repositorioSinalizacoesRascunhoProcesso.Where(r => r.IdRascunhoProcesso == idRascunhoProcesso).ToList();
            _repositorioSinalizacoesRascunhoProcesso.RemoveRange(sinalizacoesRascunho);
            _unitOfWork.Save();
        }

        public void Delete(int idRascunhoProcesso, int idSinalizacao)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);

            SinalizacaoRascunhoProcesso sinalizacaoRascunhoProcesso = _repositorioSinalizacoesRascunhoProcesso.Where(srp => srp.IdRascunhoProcesso == idRascunhoProcesso && srp.IdSinalizacao == idSinalizacao).SingleOrDefault();
            _validacao.Exists(sinalizacaoRascunhoProcesso);

            _repositorioSinalizacoesRascunhoProcesso.Remove(sinalizacaoRascunhoProcesso);
            _unitOfWork.Save();
        }

        public void Delete(SinalizacaoRascunhoProcesso sinalizacaoRascunhoProcesso)
        {
            if (sinalizacaoRascunhoProcesso != null)
            {
                _repositorioSinalizacoesRascunhoProcesso.Remove(sinalizacaoRascunhoProcesso);
            }
        }

        public void Delete(ICollection<SinalizacaoRascunhoProcesso> sinalizacoesRascunhoProcesso)
        {
            if (sinalizacoesRascunhoProcesso != null)
            {
                foreach (SinalizacaoRascunhoProcesso sinalizacaoRascunhoProcesso in sinalizacoesRascunhoProcesso)
                {
                    Delete(sinalizacaoRascunhoProcesso);
                }
            }
        }
    }
}
