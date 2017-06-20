using ProcessoEletronicoService.Negocio.Rascunho.Proceso.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao;
using ProcessoEletronicoService.Negocio.Comum.Validacao;
using ProcessoEletronicoService.Negocio.Comum.Base;
using AutoMapper;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo
{
    public class EmailInteressadoPessoaJuridicaNegocio : IEmailInteressadoPessoaJuridicaNegocio
    {
        private IRepositorioGenerico<EmailRascunho> _repositorioEmailsRascunho;
        private IRepositorioGenerico<InteressadoPessoaJuridicaRascunho> _repositorioInteressadosPessoaJuridicaRascunho;
        private EmailValidacao _validacao;
        private InteressadoPessoaJuridicaValidacao _interessadoPessoaJuridicaValidacao;
        private RascunhoProcessoValidacao _rascunhoProcessoValidacao;
        private UsuarioValidacao _usuarioValidacao;
        private ICurrentUserProvider _user;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public EmailInteressadoPessoaJuridicaNegocio(IProcessoEletronicoRepositorios repositorios, ICurrentUserProvider user, IMapper mapper, EmailValidacao validacao, InteressadoPessoaJuridicaValidacao interessadoPessoaJuridicaValidacao, RascunhoProcessoValidacao rascunhoProcessoValidacao)
        {
            _repositorioEmailsRascunho = repositorios.EmailsRascunho;
            _repositorioInteressadosPessoaJuridicaRascunho = repositorios.InteressadosPessoaJuridicaRascunho;
            _unitOfWork = repositorios.UnitOfWork;
            _validacao = validacao;
            _interessadoPessoaJuridicaValidacao = interessadoPessoaJuridicaValidacao;
            _rascunhoProcessoValidacao = rascunhoProcessoValidacao;
            _mapper = mapper;
        }

        public IList<EmailModeloNegocio> Get(int idRascunhoProcesso, int idInteressadoPessoaJuridica)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _interessadoPessoaJuridicaValidacao.Exists(idRascunhoProcesso, idInteressadoPessoaJuridica);

            return _mapper.Map<IList<EmailModeloNegocio>>(_repositorioEmailsRascunho
                                              .Where(e => e.IdInteressadoPessoaJuridicaRascunho == idInteressadoPessoaJuridica
                                                       && e.InteressadoPessoaJuridicaRascunho.IdRascunhoProcesso == idRascunhoProcesso)).ToList();

        }

        public EmailModeloNegocio Get(int idRascunhoProcesso, int idInteressadoPessoaJuridica, int id)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _interessadoPessoaJuridicaValidacao.Exists(idRascunhoProcesso, idInteressadoPessoaJuridica);

            EmailRascunho email = _repositorioEmailsRascunho
                            .Where(e => e.IdInteressadoPessoaJuridicaRascunho == idInteressadoPessoaJuridica 
                                     && e.InteressadoPessoaJuridicaRascunho.IdRascunhoProcesso == idRascunhoProcesso
                                     && e.Id == id)
                                     .SingleOrDefault();

            _validacao.Exists(email);
            return _mapper.Map<EmailModeloNegocio>(email);
        }

        public EmailModeloNegocio Post(int idRascunhoProcesso, int idInteressadoPessoaJuridica, EmailModeloNegocio emailModeloNegocio)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _interessadoPessoaJuridicaValidacao.Exists(idRascunhoProcesso, idInteressadoPessoaJuridica);

            _validacao.IsFilled(emailModeloNegocio);
            _validacao.IsValid(emailModeloNegocio);

            EmailRascunho emailRascunho = new EmailRascunho();
            _mapper.Map(emailModeloNegocio, emailRascunho);
            emailRascunho.IdInteressadoPessoaJuridicaRascunho = idInteressadoPessoaJuridica;
            _repositorioEmailsRascunho.Add(emailRascunho);

            _unitOfWork.Save();
            return Get(idRascunhoProcesso, idInteressadoPessoaJuridica, emailRascunho.Id);
        }

        public void Patch(int idRascunhoProcesso, int idInteressadoPessoaJuridica, int id, EmailModeloNegocio emailModeloNegocio)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _interessadoPessoaJuridicaValidacao.Exists(idRascunhoProcesso, idInteressadoPessoaJuridica);

            EmailRascunho email = _repositorioEmailsRascunho
                            .Where(e => e.IdInteressadoPessoaJuridicaRascunho == idInteressadoPessoaJuridica
                                     && e.InteressadoPessoaJuridicaRascunho.IdRascunhoProcesso == idRascunhoProcesso
                                     && e.Id == id)
                                     .SingleOrDefault();

            _validacao.Exists(email);
            _validacao.IsFilled(emailModeloNegocio);
            _validacao.IsValid(emailModeloNegocio);
            MapEmail(emailModeloNegocio, email);
            _unitOfWork.Save();
        }

        public void Delete(int idRascunhoProcesso, int idInteressadoPessoaJuridica, int id)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _interessadoPessoaJuridicaValidacao.Exists(idRascunhoProcesso, idInteressadoPessoaJuridica);

            EmailRascunho email = _repositorioEmailsRascunho
                            .Where(e => e.IdInteressadoPessoaJuridicaRascunho == idInteressadoPessoaJuridica
                                     && e.InteressadoPessoaJuridicaRascunho.IdRascunhoProcesso == idRascunhoProcesso
                                     && e.Id == id)
                                     .SingleOrDefault();

            _validacao.Exists(email);
            _repositorioEmailsRascunho.Remove(email);
        }

        public void Delete(EmailRascunho email)
        {
            if (email != null)
            {
                _repositorioEmailsRascunho.Remove(email);
            }
        }

        public void Delete(ICollection<EmailRascunho> emails)
        {
            if (emails != null)
            {
                foreach(EmailRascunho email in emails)
                {
                    Delete(email);
                }
            }
        }

        private void MapEmail(EmailModeloNegocio emailModeloNegocio, EmailRascunho email)
        {
            email.Endereco = emailModeloNegocio.Endereco;
        }
    }
}
