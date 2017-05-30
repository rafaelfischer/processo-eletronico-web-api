using ProcessoEletronicoService.Negocio.Comum;
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
using AutoMapper;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Comum.Base;
using Microsoft.EntityFrameworkCore;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo
{
    public class ContatoInteressadoPessoaFisicaNegocio : BaseNegocio, IContatoInteressadoPessoaFisicaNegocio
    {
        private IRepositorioGenerico<ContatoRascunho> _repositorioContatosRascunho;
        private IRepositorioGenerico<InteressadoPessoaFisica> _repositorioInteressadosPessoaFisica;
        private IRepositorioGenerico<RascunhoProcesso> _repositorioRascunhosProcesso;
        private ContatoValidacao _validacao;
        private InteressadoPessoaFisicaValidacao _interessadoPessoaFisicaValidacao;
        private RascunhoProcessoValidacao _rascunhoProcessoValidacao;
        private UsuarioValidacao _usuarioValidacao;
        private ICurrentUserProvider _user;
                
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;


        public ContatoInteressadoPessoaFisicaNegocio(IProcessoEletronicoRepositorios repositorios, IMapper mapper, ContatoValidacao validacao, RascunhoProcessoValidacao rascunhoProcessoValidacao, InteressadoPessoaFisicaValidacao interessadoPessoaFisicaValidacao , UsuarioValidacao usuarioValidacao, ICurrentUserProvider user)
        {
            _repositorioContatosRascunho = repositorios.ContatosRascunho;
            _repositorioInteressadosPessoaFisica = repositorios.InteressadosPessoaFisica;
            _repositorioRascunhosProcesso = repositorios.RascunhosProcesso;
            _validacao = validacao;
            _rascunhoProcessoValidacao = rascunhoProcessoValidacao;
            _interessadoPessoaFisicaValidacao = interessadoPessoaFisicaValidacao;
            _usuarioValidacao = usuarioValidacao;
            _user = user;
            _mapper = mapper;
            _unitOfWork = repositorios.UnitOfWork;
        }

        public IList<ContatoModeloNegocio> Get(int idRascunhoProcesso, int idInteressadoPessoaFisica)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _interessadoPessoaFisicaValidacao.Exists(idRascunhoProcesso, idInteressadoPessoaFisica);
            
            IList<ContatoRascunho> contatos = _repositorioContatosRascunho.Where(c => c.IdInteressadoPessoaFisicaRascunho.HasValue && c.IdInteressadoPessoaFisicaRascunho.Value == idInteressadoPessoaFisica)
                                                .Include(t => t.TipoContato).ToList();
            return _mapper.Map<IList<ContatoModeloNegocio>>(contatos);
        }

        public ContatoModeloNegocio Get(int idRascunhoProcesso, int idInteressadoPessoaFisica, int id)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _interessadoPessoaFisicaValidacao.Exists(idRascunhoProcesso, idInteressadoPessoaFisica);
            _validacao.ExistsInInteressadoPessoaFisica(idRascunhoProcesso, idInteressadoPessoaFisica, id);
            
            ContatoRascunho contato = _repositorioContatosRascunho.Where(c => c.IdInteressadoPessoaFisicaRascunho == idInteressadoPessoaFisica && c.Id == id).Include(t => t.TipoContato).SingleOrDefault();
            return _mapper.Map<ContatoModeloNegocio>(contato);

        }
        
        public ContatoModeloNegocio Post(int idRascunhoProcesso, int idInteressadoPessoaFisica, ContatoModeloNegocio contatoModeloNegocio)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _interessadoPessoaFisicaValidacao.Exists(idRascunhoProcesso, idInteressadoPessoaFisica);

            _validacao.IsFilled(contatoModeloNegocio);
            _validacao.IsValid(contatoModeloNegocio);

            ContatoRascunho contatoRascunho = new ContatoRascunho();
            _mapper.Map(contatoModeloNegocio, contatoRascunho);
            contatoRascunho.IdInteressadoPessoaFisicaRascunho = idInteressadoPessoaFisica;
            _repositorioContatosRascunho.Add(contatoRascunho);

            _unitOfWork.Save();
            return Get(idRascunhoProcesso, idInteressadoPessoaFisica, contatoRascunho.Id);

        }
        public void Patch(int idRascunhoProcesso, int idInteressadoPessoaFisica, int id, ContatoModeloNegocio contatoModeloNegocio)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _interessadoPessoaFisicaValidacao.Exists(idRascunhoProcesso, idInteressadoPessoaFisica);

            ContatoRascunho contato = _repositorioContatosRascunho.Where(c => c.IdInteressadoPessoaFisicaRascunho == idInteressadoPessoaFisica && c.Id == id).SingleOrDefault();
            _validacao.Exists(contato);
            _validacao.IsFilled(contatoModeloNegocio);
            _validacao.IsValid(contatoModeloNegocio);
            MapContato(contatoModeloNegocio, contato);
            _unitOfWork.Save();
        }

        public void Delete (int idRascunhoProcesso, int idInteressadoPessoaFisica, int id)
        {
            _rascunhoProcessoValidacao.Exists(idRascunhoProcesso);
            _interessadoPessoaFisicaValidacao.Exists(idRascunhoProcesso, idInteressadoPessoaFisica);

            ContatoRascunho contatoRascunho = _repositorioContatosRascunho.Where(c => c.Id == id && c.IdInteressadoPessoaFisicaRascunho == idInteressadoPessoaFisica).SingleOrDefault();
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
            contato.IdTipoContato = contatoModeloNegocio.TipoContato != null ? contatoModeloNegocio.TipoContato.Id : (int?) null; 
        }
    }
}
