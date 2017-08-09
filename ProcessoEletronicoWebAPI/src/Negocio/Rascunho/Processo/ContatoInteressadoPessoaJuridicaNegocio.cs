using ProcessoEletronicoService.Negocio.Comum;
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
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo
{
    public class ContatoInteressadoPessoaJuridicaNegocio : BaseNegocio, IContatoInteressadoPessoaJuridicaNegocio
    {
        private IRepositorioGenerico<ContatoRascunho> _repositorioContatosRascunho;
        private IRepositorioGenerico<InteressadoPessoaJuridica> _repositorioInteressadosPessoaJuridica;
        private IRepositorioGenerico<RascunhoProcesso> _repositorioRascunhosProcesso;
        private ContatoValidacao _validacao;
        private InteressadoPessoaJuridicaValidacao _interessadoPessoaJuridicaValidacao;
        private RascunhoProcessoValidacao _rascunhoProcessoValidacao;
        private UsuarioValidacao _usuarioValidacao;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public ContatoInteressadoPessoaJuridicaNegocio(IProcessoEletronicoRepositorios repositorios, IMapper mapper, ContatoValidacao validacao, InteressadoPessoaJuridicaValidacao interessadoPessoaJuridicaValidacao, RascunhoProcessoValidacao rascunhoProcessoValidacao, UsuarioValidacao usuarioValidacao)
        {
            _repositorioContatosRascunho = repositorios.ContatosRascunho;
            _repositorioInteressadosPessoaJuridica = repositorios.InteressadosPessoaJuridica;
            _repositorioRascunhosProcesso = repositorios.RascunhosProcesso;
            _validacao = validacao;
            _interessadoPessoaJuridicaValidacao = interessadoPessoaJuridicaValidacao;
            _rascunhoProcessoValidacao = rascunhoProcessoValidacao;
            _usuarioValidacao = usuarioValidacao;
            _mapper = mapper;
            _unitOfWork = repositorios.UnitOfWork;
        }

        public IList<ContatoModeloNegocio> Get(int idRascunhoProcesso, int idInteressadoPessoaJuridica)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _interessadoPessoaJuridicaValidacao.Exists(idRascunhoProcesso, idInteressadoPessoaJuridica);

            IList<ContatoRascunho> contatos = _repositorioContatosRascunho.Where(c => c.IdInteressadoPessoaJuridicaRascunho.HasValue && c.IdInteressadoPessoaJuridicaRascunho.Value == idInteressadoPessoaJuridica)
                                                .Include(t => t.TipoContato).ToList();
            return _mapper.Map<IList<ContatoModeloNegocio>>(contatos);
        }

        public ContatoModeloNegocio Get(int idRascunhoProcesso, int idInteressadoPessoaJuridica, int id)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _interessadoPessoaJuridicaValidacao.Exists(idRascunhoProcesso, idInteressadoPessoaJuridica);

            ContatoRascunho contato = _repositorioContatosRascunho.Where(c => c.IdInteressadoPessoaJuridicaRascunho == idInteressadoPessoaJuridica && c.Id == id).Include(t => t.TipoContato).SingleOrDefault();
            _validacao.Exists(contato);
            return _mapper.Map<ContatoModeloNegocio>(contato);

        }

        public ContatoModeloNegocio Post(int idRascunhoProcesso, int idInteressadoPessoaJuridica, ContatoModeloNegocio contatoModeloNegocio)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _interessadoPessoaJuridicaValidacao.Exists(idRascunhoProcesso, idInteressadoPessoaJuridica);

            _validacao.IsFilled(contatoModeloNegocio);
            _validacao.IsValid(contatoModeloNegocio);

            ContatoRascunho contatoRascunho = new ContatoRascunho();
            _mapper.Map(contatoModeloNegocio, contatoRascunho);
            contatoRascunho.IdInteressadoPessoaJuridicaRascunho = idInteressadoPessoaJuridica;
            _repositorioContatosRascunho.Add(contatoRascunho);

            _unitOfWork.Save();
            return Get(idRascunhoProcesso, idInteressadoPessoaJuridica, contatoRascunho.Id);

        }
        public void Patch(int idRascunhoProcesso, int idInteressadoJuridica, int id, ContatoModeloNegocio contatoModeloNegocio)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _interessadoPessoaJuridicaValidacao.Exists(idRascunhoProcesso, idInteressadoJuridica);

            ContatoRascunho contato = _repositorioContatosRascunho.Where(c => c.IdInteressadoPessoaJuridicaRascunho == idInteressadoJuridica && c.Id == id).SingleOrDefault();
            _validacao.Exists(contato);
            _validacao.IsFilled(contatoModeloNegocio);
            _validacao.IsValid(contatoModeloNegocio);
            MapContato(contatoModeloNegocio, contato);
            _unitOfWork.Save();
        }

        public void Delete(int idRascunhoProcesso, int idInteressadoPessoaJuridica, int id)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _interessadoPessoaJuridicaValidacao.Exists(idRascunhoProcesso, idInteressadoPessoaJuridica);

            ContatoRascunho contatoRascunho = _repositorioContatosRascunho.Where(c => c.Id == id && c.IdInteressadoPessoaJuridicaRascunho == idInteressadoPessoaJuridica).SingleOrDefault();
            _validacao.Exists(contatoRascunho);

            _repositorioContatosRascunho.Remove(contatoRascunho);
            _unitOfWork.Save();

        }
        public void Delete(ContatoRascunho contato)
        {
            if (contato != null)
            {
                _repositorioContatosRascunho.Remove(contato);
            }
        }

        public void Delete(ICollection<ContatoRascunho> contatos)
        {
            if (contatos != null)
            {
                foreach (ContatoRascunho contato in contatos)
                {
                    Delete(contato);
                }
            }
        }

        private void MapContato(ContatoModeloNegocio contatoModeloNegocio, ContatoRascunho contato)
        {
            contato.Telefone = contatoModeloNegocio.Telefone;
            contato.IdTipoContato = contatoModeloNegocio.TipoContato != null ? contatoModeloNegocio.TipoContato.Id : (int?)null;
        }
    }
}
