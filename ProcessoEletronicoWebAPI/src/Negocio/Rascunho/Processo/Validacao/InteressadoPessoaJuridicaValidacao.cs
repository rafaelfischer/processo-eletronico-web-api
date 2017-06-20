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
    public class InteressadoPessoaJuridicaValidacao : IBaseValidation<InteressadoPessoaJuridicaModeloNegocio, InteressadoPessoaJuridicaRascunho>, IBaseCollectionValidation<InteressadoPessoaJuridicaModeloNegocio>
    {
        private IRepositorioGenerico<InteressadoPessoaJuridicaRascunho> _repositorioInteressadosPessoaJuridicaRascunho;

        public InteressadoPessoaJuridicaValidacao(IProcessoEletronicoRepositorios repositorios)
        {
            _repositorioInteressadosPessoaJuridicaRascunho = repositorios.InteressadosPessoaJuridicaRascunho;
        }

        public void Exists(InteressadoPessoaJuridicaRascunho interessadoPessoaJuridica)
        {
            if (interessadoPessoaJuridica == null)
            {
                throw new RecursoNaoEncontradoException("Interessado Pessoa Jurídica não encontrado.");
            }
        }

        public void Exists(int idRascunhoProcesso, int id)
        {
            if (_repositorioInteressadosPessoaJuridicaRascunho.Where(i => i.Id == id && i.IdRascunhoProcesso == idRascunhoProcesso).SingleOrDefault() == null)
            {
                throw new RecursoNaoEncontradoException("Interessado Pessoa Juridica não encontrado.");
            }
        }
        
        public void IsFilled(IEnumerable<InteressadoPessoaJuridicaModeloNegocio> interessadosPessoaJuridicaNegocio)
        {
            if (interessadosPessoaJuridicaNegocio != null)
            {
                foreach (InteressadoPessoaJuridicaModeloNegocio interessado in interessadosPessoaJuridicaNegocio)
                {
                    IsFilled(interessado);
                }
            }
        }

        public void IsFilled(InteressadoPessoaJuridicaModeloNegocio interessadoPessoaJuridicaNegocio)
        {
        }

        public void IsValid(IEnumerable<InteressadoPessoaJuridicaModeloNegocio> interessadosPessoaJuridicaNegocio)
        {
            foreach (InteressadoPessoaJuridicaModeloNegocio interessado in interessadosPessoaJuridicaNegocio)
            {
                IsValid(interessado);
            }
        }

        public void IsValid(InteressadoPessoaJuridicaModeloNegocio interessadoPessoaJuridicaNegocio)
        {
            GuidMunicipioValido(interessadoPessoaJuridicaNegocio);
        }

        private void GuidMunicipioValido(InteressadoPessoaJuridicaModeloNegocio interesadoPessoaJuridicaNegocio)
        {
            if (interesadoPessoaJuridicaNegocio.GuidMunicipio != null)
            {
                try
                {
                    Guid guid = new Guid(interesadoPessoaJuridicaNegocio.GuidMunicipio);
                }
                catch (Exception)
                {
                    throw new RequisicaoInvalidaException($"Guid do Município Inválido ({interesadoPessoaJuridicaNegocio.GuidMunicipio})");
                }
            }
        }
    }
}
