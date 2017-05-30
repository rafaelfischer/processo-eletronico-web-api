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

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo
{
    public class ContatoInteressadoPessoaJuridicaNegocio : BaseNegocio, IContatoInteressadoPessoaJuridicaNegocio
    {
        private IRepositorioGenerico<ContatoRascunho> _repositorioContatosRascunho;
        private IRepositorioGenerico<InteressadoPessoaJuridica> _repositorioInteressadosPessoaJuridica;
        private IRepositorioGenerico<RascunhoProcesso> _repositorioRascunhosProcesso;
        private ContatoValidacao _validacao;
        //private InteressadoPessoaJuridicaValidacao _interessadoPessoaFisicaValidacao;
        private RascunhoProcessoValidacao _rascunhoProcessoValidacao;
        private UsuarioValidacao _usuarioValidacao;

        private IContatoInteressadoPessoaJuridicaNegocio _contatoNegocio;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;


        public ContatoInteressadoPessoaJuridicaNegocio(IProcessoEletronicoRepositorios repositorios, IMapper mapper, ContatoValidacao validacao, RascunhoProcessoValidacao rascunhoProcessoValidacao, UsuarioValidacao usuarioValidacao, IContatoInteressadoPessoaJuridicaNegocio contatoNegocio)
        {
            _repositorioInteressadosPessoaJuridica = repositorios.InteressadosPessoaJuridica;
            _repositorioRascunhosProcesso = repositorios.RascunhosProcesso;
            _validacao = validacao;
            //_interessadoPessoaJuridicaValidacao = interessadoPessoaJuridicaValidacao;
            _rascunhoProcessoValidacao = rascunhoProcessoValidacao;
            _usuarioValidacao = usuarioValidacao;
            _contatoNegocio = contatoNegocio;
            _mapper = mapper;
            _unitOfWork = repositorios.UnitOfWork;
        }

        public IList<ContatoModeloNegocio> Get(int idRascunhoProcesso, int idInteressado)
        {
            throw new NotImplementedException();
        }

        public ContatoModeloNegocio Get(int idRascunhoProcesso, int idInteressado, int id)
        {
            throw new NotImplementedException();
        }
        
        public ContatoModeloNegocio Post(int idRascunhoProcesso, int idInteressado, ContatoModeloNegocio contatoModeloNegocio)
        {
            throw new NotImplementedException();
        }
        public void Patch(int idRascunhoProcesso, int idInteressado, int id, ContatoModeloNegocio contatoModeloNegocio)
        {
            throw new NotImplementedException();
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

        public void Delete(int idRascunhoProcesso, int idInteressado, int id)
        {
            throw new NotImplementedException();
        }
    }
}
