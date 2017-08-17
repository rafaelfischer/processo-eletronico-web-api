using ProcessoEletronicoService.Negocio.Rascunho.Processo.Base;
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
    public class EmailInteressadoPessoaFisicaNegocio : IEmailInteressadoPessoaFisicaNegocio
    {
        private IRepositorioGenerico<EmailRascunho> _repositorioEmailsRascunho;
        private IRepositorioGenerico<InteressadoPessoaFisicaRascunho> _repositorioInteressadosPessoaFisicaRascunho;
        private EmailValidacao _validacao;
        private InteressadoPessoaFisicaValidacao _interessadoPessoaFisicaValidacao;
        private RascunhoProcessoValidacao _rascunhoProcessoValidacao;
        private UsuarioValidacao _usuarioValidacao;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public EmailInteressadoPessoaFisicaNegocio(IProcessoEletronicoRepositorios repositorios, IMapper mapper, EmailValidacao validacao, InteressadoPessoaFisicaValidacao interessadoPessoaFisicaValidacao, RascunhoProcessoValidacao rascunhoProcessoValidacao)
        {
            _repositorioEmailsRascunho = repositorios.EmailsRascunho;
            _repositorioInteressadosPessoaFisicaRascunho = repositorios.InteressadosPessoaFisicaRascunho;
            _unitOfWork = repositorios.UnitOfWork;
            _validacao = validacao;
            _interessadoPessoaFisicaValidacao = interessadoPessoaFisicaValidacao;
            _rascunhoProcessoValidacao = rascunhoProcessoValidacao;
            _mapper = mapper;
        }

        public IList<EmailModeloNegocio> Get(int idRascunhoProcesso, int idInteressadoPessoaFisica)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _interessadoPessoaFisicaValidacao.Exists(idRascunhoProcesso, idInteressadoPessoaFisica);

            return _mapper.Map<IList<EmailModeloNegocio>>(_repositorioEmailsRascunho
                                              .Where(e => e.IdInteressadoPessoaFisicaRascunho == idInteressadoPessoaFisica
                                                       && e.InteressadoPessoaFisicaRascunho.IdRascunhoProcesso == idRascunhoProcesso)).ToList();

        }

        public EmailModeloNegocio Get(int idRascunhoProcesso, int idInteressadoPessoaFisica, int id)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _interessadoPessoaFisicaValidacao.Exists(idRascunhoProcesso, idInteressadoPessoaFisica);

            EmailRascunho email = _repositorioEmailsRascunho
                            .Where(e => e.IdInteressadoPessoaFisicaRascunho == idInteressadoPessoaFisica 
                                     && e.InteressadoPessoaFisicaRascunho.IdRascunhoProcesso == idRascunhoProcesso
                                     && e.Id == id)
                                     .SingleOrDefault();

            _validacao.Exists(email);
            return _mapper.Map<EmailModeloNegocio>(email);
        }

        public EmailModeloNegocio Post(int idRascunhoProcesso, int idInteressadoPessoaFisica, EmailModeloNegocio emailModeloNegocio)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _interessadoPessoaFisicaValidacao.Exists(idRascunhoProcesso, idInteressadoPessoaFisica);

            _validacao.IsFilled(emailModeloNegocio);
            _validacao.IsValid(emailModeloNegocio);

            EmailRascunho emailRascunho = new EmailRascunho();
            _mapper.Map(emailModeloNegocio, emailRascunho);
            emailRascunho.IdInteressadoPessoaFisicaRascunho = idInteressadoPessoaFisica;
            _repositorioEmailsRascunho.Add(emailRascunho);

            _unitOfWork.Save();
            return Get(idRascunhoProcesso, idInteressadoPessoaFisica, emailRascunho.Id);
        }

        public void Patch(int idRascunhoProcesso, int idInteressadoPessoaFisica, int id, EmailModeloNegocio emailModeloNegocio)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _interessadoPessoaFisicaValidacao.Exists(idRascunhoProcesso, idInteressadoPessoaFisica);

            EmailRascunho email = _repositorioEmailsRascunho
                            .Where(e => e.IdInteressadoPessoaFisicaRascunho == idInteressadoPessoaFisica
                                     && e.InteressadoPessoaFisicaRascunho.IdRascunhoProcesso == idRascunhoProcesso
                                     && e.Id == id)
                                     .SingleOrDefault();

            _validacao.Exists(email);
            _validacao.IsFilled(emailModeloNegocio);
            _validacao.IsValid(emailModeloNegocio);
            MapEmail(emailModeloNegocio, email);
            _unitOfWork.Save();
        }

        public void Delete(int idRascunhoProcesso, int idInteressadoPessoaFisica, int id)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _interessadoPessoaFisicaValidacao.Exists(idRascunhoProcesso, idInteressadoPessoaFisica);

            EmailRascunho email = _repositorioEmailsRascunho
                            .Where(e => e.IdInteressadoPessoaFisicaRascunho == idInteressadoPessoaFisica
                                     && e.InteressadoPessoaFisicaRascunho.IdRascunhoProcesso == idRascunhoProcesso
                                     && e.Id == id)
                                     .SingleOrDefault();

            _validacao.Exists(email);
            _repositorioEmailsRascunho.Remove(email);
            _unitOfWork.Save();
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
