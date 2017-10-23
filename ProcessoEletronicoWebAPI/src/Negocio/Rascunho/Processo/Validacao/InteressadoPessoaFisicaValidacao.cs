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
    public class InteressadoPessoaFisicaValidacao : IBaseValidation<InteressadoPessoaFisicaModeloNegocio, InteressadoPessoaFisicaRascunho>, IBaseCollectionValidation<InteressadoPessoaFisicaModeloNegocio>
    {
        private IRepositorioGenerico<InteressadoPessoaFisicaRascunho> _repositorioInteressadosPessoaFisicaRascunho;

        public InteressadoPessoaFisicaValidacao(IProcessoEletronicoRepositorios repositorios)
        {
            _repositorioInteressadosPessoaFisicaRascunho = repositorios.InteressadosPessoaFisicaRascunho;
        }

        public void Exists(InteressadoPessoaFisicaRascunho interessadoPessoaFisica)
        {
            if (interessadoPessoaFisica == null)
            {
                throw new RecursoNaoEncontradoException("Interessado Pessoa Física não encontrado.");
            }
        }

        public void Exists(int idRascunhoProcesso, int id)
        {
            if (_repositorioInteressadosPessoaFisicaRascunho.Where(i => i.Id == id && i.IdRascunhoProcesso == idRascunhoProcesso).SingleOrDefault() == null)
            {
                throw new RecursoNaoEncontradoException("Interessado Pessoa Física não encontrado.");
            }
        }

        public void IsFilled(IEnumerable<InteressadoPessoaFisicaModeloNegocio> interessadosPessoaFisicaNegocio)
        {
            if (interessadosPessoaFisicaNegocio != null)
            {
                foreach (InteressadoPessoaFisicaModeloNegocio interessado in interessadosPessoaFisicaNegocio)
                {
                    IsFilled(interessado);
                }
            }
        }

        public void IsFilled(InteressadoPessoaFisicaModeloNegocio interessadoPessoaFisicaNegocio)
        {
        }

        public void IsValid(IEnumerable<InteressadoPessoaFisicaModeloNegocio> interessadosPessoaFisicaNegocio)
        {
            if (interessadosPessoaFisicaNegocio != null)
            {
                foreach (InteressadoPessoaFisicaModeloNegocio interessado in interessadosPessoaFisicaNegocio)
                {
                    IsValid(interessado);
                }
            }
        }

        public void IsValid(InteressadoPessoaFisicaModeloNegocio interessadoPessoaFisicaNegocio)
        {
            GuidMunicipioValido(interessadoPessoaFisicaNegocio);
        }

        private void GuidMunicipioValido(InteressadoPessoaFisicaModeloNegocio interesadoPessoaFisicaNegocio)
        {
            if (interesadoPessoaFisicaNegocio.GuidMunicipio != null)
            {
                try
                {
                    Guid guid = new Guid(interesadoPessoaFisicaNegocio.GuidMunicipio);
                }
                catch (Exception)
                {
                    throw new RequisicaoInvalidaException("Guid do Município Inválido");
                }
            }
        }
    }
}
