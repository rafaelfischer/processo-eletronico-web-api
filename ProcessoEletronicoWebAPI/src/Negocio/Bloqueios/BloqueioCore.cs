using AutoMapper;
using Negocio.Bloqueios.Base;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Negocio.Bloqueios
{
    public class BloqueioCore : IBloqueioCore
    {
        private IRepositorioGenerico<Bloqueio> _repositorioBloqueios;
        private IRepositorioGenerico<Processo> _repositorioProcessos;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private ICurrentUserProvider _user;
        private BloqueioValidation _validation;
        private ProcessoValidacao _processoValidation;

        public BloqueioCore(IProcessoEletronicoRepositorios repositorios, IMapper mapper, ICurrentUserProvider user, BloqueioValidation validation, ProcessoValidacao processoValidation)
        {
            _repositorioBloqueios = repositorios.Bloqueios;
            _repositorioProcessos = repositorios.Processos;
            _mapper = mapper;
            _unitOfWork = repositorios.UnitOfWork;
            _user = user;
            _validation = validation;
            _processoValidation = processoValidation;
        }

        public IList<BloqueioModel> GetBloqueiosOfProcesso(int idProcesso)
        {
            IList<Bloqueio> bloqueios = _repositorioBloqueios.Where(b => b.IdProcesso == idProcesso).ToList();
            return _mapper.Map<IList<BloqueioModel>>(bloqueios);
        }

        public BloqueioModel GetSingleBloqueio(int idProcesso, int id)
        {
            Bloqueio bloqueio = _repositorioBloqueios.Where(b => b.IdProcesso == idProcesso && b.Id == id).SingleOrDefault();
            _validation.Exists(bloqueio);

            return _mapper.Map<BloqueioModel>(bloqueio);
        }

        public BloqueioModel InsertBloqueioIntoProcesso(int idProcesso, BloqueioModel bloqueioModel)
        {
            Processo processo = _repositorioProcessos.SingleOrDefault(p => p.Id == idProcesso);
            _processoValidation.NaoEncontrado(processo);

            _validation.IsProcessoWithBloqueio(idProcesso);

            FillDateAndUserInformation(bloqueioModel);
            Bloqueio bloqueio = new Bloqueio();
            _mapper.Map(bloqueioModel, bloqueio);

            bloqueio.IdProcesso = idProcesso;
            _repositorioBloqueios.Add(bloqueio);
            _unitOfWork.Save();

            return GetSingleBloqueio(idProcesso, bloqueio.Id);
        }
        
        public void DeleteBloqueioOfProcesso(int idProcesso, int id)
        {
            Bloqueio bloqueio = _repositorioBloqueios.Where(b => b.IdProcesso == idProcesso && b.Id == id).SingleOrDefault();
            _validation.Exists(bloqueio);
            _validation.IsRemoveBloqueioPossible(bloqueio);

            bloqueio.DataFim = DateTime.Now;
            _unitOfWork.Save();
        }

        private void FillDateAndUserInformation(BloqueioModel bloqueioModel)
        {
            bloqueioModel.DataInicio = DateTime.Now;
            bloqueioModel.CpfUsuario = _user.UserCpf;
            bloqueioModel.NomeUsuario = _user.UserNome;
            bloqueioModel.NomeSistema = _user.UserSistema;
        }
    }
}
