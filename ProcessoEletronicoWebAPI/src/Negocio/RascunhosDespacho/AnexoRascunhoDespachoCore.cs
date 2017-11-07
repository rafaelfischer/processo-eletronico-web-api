using AutoMapper;
using Negocio.RascunhosDespacho.Base;
using Negocio.RascunhosDespacho.Models;
using Negocio.RascunhosDespacho.Validations.Base;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Negocio.RascunhosDespacho
{
    public class AnexoRascunhoDespachoCore : IAnexoRascunhoDespachoCore
    {
        private IMapper _mapper;
        private IAnexoRascunhoDespachoValidation _validation;
        private IRascunhoDespachoValidation _rascunhoDespachovalidation;
        private IRepositorioGenerico<AnexoRascunho> _repositorioAnexosRascunho;
        private IRepositorioGenerico<RascunhoDespacho> _repositorioRascunhosDespacho;
        private IUnitOfWork _unitOfWork;

        public AnexoRascunhoDespachoCore(IProcessoEletronicoRepositorios repositorios, IMapper mapper, IAnexoRascunhoDespachoValidation validation,  IRascunhoDespachoValidation rascunhoDespachoValidation)
        {
            _repositorioAnexosRascunho = repositorios.AnexosRascunho;
            _repositorioRascunhosDespacho = repositorios.RascunhosDespacho;
            _validation = validation;
            _rascunhoDespachovalidation = rascunhoDespachoValidation;
            _mapper = mapper;
            _unitOfWork = repositorios.UnitOfWork;
        }

        public AnexoRascunhoDespachoModel Add(int idRascunhoDespacho, AnexoRascunhoDespachoModel anexoRascunhoDespachoModel)
        {
            RascunhoDespacho rascunhoDespacho = _repositorioRascunhosDespacho.Where(r => r.Id == idRascunhoDespacho).SingleOrDefault();
            _rascunhoDespachovalidation.Exists(rascunhoDespacho);
            _rascunhoDespachovalidation.IsRascunhoDespachoOfUser(rascunhoDespacho);

            _validation.IsFilled(anexoRascunhoDespachoModel);
            _validation.IsValid(anexoRascunhoDespachoModel);

            AnexoRascunho anexoRascunho = _mapper.Map<AnexoRascunho>(anexoRascunhoDespachoModel);

            rascunhoDespacho.AnexosRascunho.Add(anexoRascunho);
            _unitOfWork.Save();

            return Search(idRascunhoDespacho, anexoRascunho.Id);
        }

        public void Delete(int idRascunhoDespacho, int id)
        {
            RascunhoDespacho rascunhoDespacho = _repositorioRascunhosDespacho.Where(r => r.Id == idRascunhoDespacho).SingleOrDefault();
            _rascunhoDespachovalidation.Exists(rascunhoDespacho);
            _rascunhoDespachovalidation.IsRascunhoDespachoOfUser(rascunhoDespacho);

            AnexoRascunho anexoRascunho = _repositorioAnexosRascunho.Where(a => a.Id == id && a.IdRascunhoDespacho == idRascunhoDespacho).SingleOrDefault();
            _validation.Exists(anexoRascunho);

            _repositorioAnexosRascunho.Remove(anexoRascunho);
            _unitOfWork.Save();
        }

        public AnexoRascunhoDespachoModel Search(int idRascunhoDespacho, int id)
        {
            AnexoRascunho anexoRascunho = _repositorioAnexosRascunho.Where(a => a.Id == id && a.IdRascunhoDespacho == idRascunhoDespacho).SingleOrDefault();
            _validation.Exists(anexoRascunho);

            return _mapper.Map<AnexoRascunhoDespachoModel>(anexoRascunho);
        }

        public IEnumerable<AnexoRascunhoDespachoModel> Search(int idRascunhoDespacho)
        {
            IEnumerable<AnexoRascunho> anexosRascunho = _repositorioAnexosRascunho.Where(a => a.IdRascunhoDespacho == idRascunhoDespacho).ToList();
            return _mapper.Map<IEnumerable<AnexoRascunhoDespachoModel>>(anexosRascunho);
        }

        public void Update(int idRascunhoDespacho, int id, AnexoRascunhoDespachoModel rascunhoDespacho)
        {
            throw new NotImplementedException();
        }

    }
}
