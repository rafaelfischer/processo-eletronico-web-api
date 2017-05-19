using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao
{
    public class InteressadoPessoaFisicaValidacao
    {

        private IRepositorioGenerico<InteressadoPessoaFisica> _repositorioInteressadosPessoaFisica;

        public InteressadoPessoaFisicaValidacao(IProcessoEletronicoRepositorios repositorios)
        {
            _repositorioInteressadosPessoaFisica = repositorios.InteressadosPessoaFisica;
        }

        public void NaoEncontrado(InteressadoPessoaFisica interessadoPessoaFisica)
        {
            if (interessadoPessoaFisica == null)
            {
                throw new RecursoNaoEncontradoException("Interessado Pessoa Física não encontrado.");
            }
        }

        public void Preenchido(InteressadoPessoaFisicaModeloNegocio interessadoPessoaFisicaNegocio)
        {
            NomePreenchido(interessadoPessoaFisicaNegocio);
            GuidMunicipioPreenchido(interessadoPessoaFisicaNegocio);
        }

        private void NomePreenchido(InteressadoPessoaFisicaModeloNegocio interessadoPessoaFisicaNegocio)
        {
            if (string.IsNullOrWhiteSpace(interessadoPessoaFisicaNegocio.Nome))
            {
                throw new RequisicaoInvalidaException("Nome do interessado pessoa física deve ser informado.");
            }
        }

        private void GuidMunicipioPreenchido(InteressadoPessoaFisicaModeloNegocio interessadoPessoaFisicaNegocio)
        {
            if (string.IsNullOrWhiteSpace(interessadoPessoaFisicaNegocio.GuidMunicipio))
            {
                throw new RequisicaoInvalidaException("Guid do município do interessado pessoa física deve ser informado.");
            }
        }

        public void Valido(InteressadoPessoaFisicaModeloNegocio interessadoPessoaFisicaNegocio)
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
