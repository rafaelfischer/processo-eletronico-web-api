using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao
{
    public class ContatoValidacao : IBaseValidation<ContatoModeloNegocio, ContatoRascunho>, IBaseCollectionValidation<ContatoModeloNegocio>
    {
        private IRepositorioGenerico<ContatoRascunho> _repositorioRascunhosContato;
        private IRepositorioGenerico<TipoContato> _repositorioTiposContato;

        public ContatoValidacao(IProcessoEletronicoRepositorios repositorios)
        {
            _repositorioRascunhosContato = repositorios.ContatosRascunho;
            _repositorioTiposContato = repositorios.TiposContato;
        }

        public void Exists(ContatoRascunho contato)
        {
            if (contato == null)
            {
                throw new RecursoNaoEncontradoException("Contato não encontrado.");
            }
        }

        public void ExistsInInteressadoPessoaFisica(int idRascunhoProcesso, int idInteressadoPessoaFisica, int id)
        {
            if (_repositorioRascunhosContato.Where(c => c.Id == id && c.IdInteressadoPessoaFisicaRascunho == idInteressadoPessoaFisica && c.InteressadoPessoaFisicaRascunho.IdRascunhoProcesso == idRascunhoProcesso).SingleOrDefault() == null)
            {
                throw new RecursoNaoEncontradoException("Contato não encontrado");
            }
        }
        
        public void IsFilled(IEnumerable<ContatoModeloNegocio> contatos)
        {
            if (contatos != null)
            {
                foreach (ContatoModeloNegocio contato in contatos)
                {
                    IsFilled(contato);
                }
            }
        }

        public void IsFilled(ContatoModeloNegocio contato)
        {
        }

        public void IsValid(IEnumerable<ContatoModeloNegocio> contatos)
        {
            if (contatos != null)
            {
                foreach (ContatoModeloNegocio contato in contatos)
                {
                    IsFilled(contato);
                }
            }
        }

        public void IsValid(ContatoModeloNegocio contato)
        {
            TipoContatoIsValid(contato);
        }

        private void TipoContatoIsValid(ContatoModeloNegocio contato)
        {
            if (contato.TipoContato != null && contato.TipoContato.Id > 0)
            {
                if (_repositorioTiposContato.Where(tc => tc.Id == contato.TipoContato.Id).SingleOrDefault() == null)
                {
                    throw new RecursoNaoEncontradoException("Tipo de Contato não encontrado");
                }
            }
        }
    }
}
