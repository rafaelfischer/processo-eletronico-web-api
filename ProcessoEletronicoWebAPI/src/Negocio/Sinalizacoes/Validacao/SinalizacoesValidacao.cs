using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProcessoEletronicoService.Negocio.Sinalizacoes.Validacao
{
    public class SinalizacoesValidacao
    {
        private IRepositorioGenerico<Sinalizacao> _repositorioSinalizacoes;
        private IRepositorioGenerico<SinalizacaoProcesso> _repositorioSinalizacoesProcessos;
        private IRepositorioGenerico<SinalizacaoRascunhoProcesso> _repositorioSinalizacoesRascunhosProcessos;
        private ICurrentUserProvider _user;

        private readonly string[] _validImageMimeTypes = { "image/gif", "image/png", "image/jpeg", "image/bmp", "image/webp" };
        private readonly string _colorPattern = @"^(([0-9a-fA-F]{2}){3}|([0-9a-fA-F]){3})$";
        public SinalizacoesValidacao(IProcessoEletronicoRepositorios repositorios, ICurrentUserProvider user)
        {
            _repositorioSinalizacoes = repositorios.Sinalizacoes;
            _repositorioSinalizacoesProcessos = repositorios.SinalizacoesProcesso;
            _repositorioSinalizacoesRascunhosProcessos = repositorios.SinalizacoesRascunhoProcesso;
            _user = user;
        }

        public void Exists(Sinalizacao sinalizacao)
        {
            if (sinalizacao == null)
            {
                throw new RecursoNaoEncontradoException("Sinalização não encontrada");
            }
        }

        #region Preechimento de campos obrigatórios
        public void IsFilled(SinalizacaoModeloNegocio sinalizacaoModeloNegocio)
        {
            DescricaoIsFilled(sinalizacaoModeloNegocio);
        }

        private void DescricaoIsFilled(SinalizacaoModeloNegocio sinalizacaoModeloNegocio)
        {
            if (string.IsNullOrWhiteSpace(sinalizacaoModeloNegocio.Descricao))
            {
                throw new RequisicaoInvalidaException("Descrição não informada");
            }
        }

        //Ambos Imagem e MimeType devem estar preenchidos ou vazios. Caso um esteja preenchido e o outro não, o modelo está inválido
        private void ImagemAndMimeTypeBothFilledOrBothEmpty(SinalizacaoModeloNegocio sinalizacaoModeloNegocio)
        {
            if (!string.IsNullOrWhiteSpace(sinalizacaoModeloNegocio.ImagemBase64String) && string.IsNullOrWhiteSpace(sinalizacaoModeloNegocio.MimeType))
            {
                throw new RequisicaoInvalidaException("Imagem informada, mas seu tipo não foi preenchido");
            }

            if (string.IsNullOrWhiteSpace(sinalizacaoModeloNegocio.ImagemBase64String) && !string.IsNullOrWhiteSpace(sinalizacaoModeloNegocio.MimeType))
            {
                throw new RequisicaoInvalidaException("Tipo da imagem informada, mas seu conteúdo não foi preenchido");
            }

        }
        #endregion

        public void IsValid(SinalizacaoModeloNegocio sinalizacaoModeloNegocio)
        {
            DescricaoAlreadyExists(sinalizacaoModeloNegocio);
            CorIsValid(sinalizacaoModeloNegocio);
            ImagemIsValid(sinalizacaoModeloNegocio);
            MimeTypeIsValid(sinalizacaoModeloNegocio);
        }

        private void DescricaoAlreadyExists(SinalizacaoModeloNegocio sinalizacaoModeloNegocio)
        {
            if (_repositorioSinalizacoes.Where(s => s.Id != sinalizacaoModeloNegocio.Id && s.Descricao.ToLower().Equals(sinalizacaoModeloNegocio.Descricao.ToLower())).SingleOrDefault() != null)
            {
                throw new RequisicaoInvalidaException("Já existe uma sinalização com esta descrição");
            }
        }

        private void CorIsValid(SinalizacaoModeloNegocio sinalizacaoModeloNegocio)
        {
            if (!string.IsNullOrWhiteSpace(sinalizacaoModeloNegocio.Cor))
            {
                Regex colorRegex = new Regex(_colorPattern);

                if (!colorRegex.IsMatch(sinalizacaoModeloNegocio.Cor))
                {
                    throw new RequisicaoInvalidaException("Formato da representação cor inválido");
                }
            }
        }

        private void ImagemIsValid(SinalizacaoModeloNegocio sinalizacaoModeloNegocio)
        {
            if (!string.IsNullOrWhiteSpace(sinalizacaoModeloNegocio.ImagemBase64String))
            {
                try
                {
                    byte[] byteArray = Convert.FromBase64String(sinalizacaoModeloNegocio.ImagemBase64String);
                }
                catch (FormatException)
                {
                    throw new RequisicaoInvalidaException("Formato da imagem inválido");
                }
            }
        }

        private void MimeTypeIsValid(SinalizacaoModeloNegocio sinalizacaoModeloNegocio)
        {
            if (!string.IsNullOrWhiteSpace(sinalizacaoModeloNegocio.MimeType))
            {
                if (!_validImageMimeTypes.Contains(sinalizacaoModeloNegocio.MimeType))
                {
                    throw new RequisicaoInvalidaException("O arquivo enviado como imagem não é do tipo imagem");
                }
            }
        }

        public void IdValido(List<SinalizacaoModeloNegocio> sinalizacoes)
        {
            if (sinalizacoes != null)
            {
                foreach (SinalizacaoModeloNegocio sinalizacao in sinalizacoes)
                {
                    IdValido(sinalizacao);
                }
            }
        }

        public void IdValido(SinalizacaoModeloNegocio sinalizacao)
        {
            if (sinalizacao.Id <= 0)
            {
                throw new RequisicaoInvalidaException("Id da sinalização inválido.");
            }
        }

        public void SinalizacaoExistente(List<SinalizacaoModeloNegocio> sinalizacoes)
        {
            if (sinalizacoes != null)
            {
                foreach (SinalizacaoModeloNegocio sinalizacao in sinalizacoes)
                {
                    SinalizacaoExistente(sinalizacao);
                }
            }
        }

        public void SinalizacaoExistente(SinalizacaoModeloNegocio sinalizacao)
        {
            if (_repositorioSinalizacoes.Where(s => s.Id == sinalizacao.Id).SingleOrDefault() == null)
            {
                throw new RecursoNaoEncontradoException("Sinalização inexistente");
            }
        }

        public void ExistsInProcesso(Sinalizacao sinalizacao)
        {
            if (_repositorioSinalizacoesProcessos.Where(sp => sp.IdSinalizacao == sinalizacao.Id).ToList().Count > 0)
            {
                throw new RequisicaoInvalidaException($"A sinalização {sinalizacao.Descricao} não pode ser alterada ou excluída, pois está associada a pelo menos um processo");
            }
        }

        public void ExistsInRascunhoProcesso(Sinalizacao sinalizacao)
        {
            if (_repositorioSinalizacoesRascunhosProcessos.Where(sp => sp.IdSinalizacao == sinalizacao.Id).ToList().Count > 0)
            {
                throw new RequisicaoInvalidaException($"A sinalização {sinalizacao.Descricao} não pode ser alterada ou excluída, pois está associada a pelo menos um rascunho de processo");
            }
        }

        internal void SinalizacoesPertencemAOrganizacaoPatriarca(SinalizacaoModeloNegocio sinalizacaoNegocio, Guid usuarioGuidOrganizacaoPatriarca)
        {
            Sinalizacao sinalizacao = _repositorioSinalizacoes.Where(s => s.Id == sinalizacaoNegocio.Id
                                                                      && s.OrganizacaoProcesso.GuidOrganizacao.Equals(usuarioGuidOrganizacaoPatriarca))
                                                             .SingleOrDefault();

            if (sinalizacao == null)
                throw new RequisicaoInvalidaException("A Sinalização informada não pertence à organização patriarca da organização autuadora.");
        }
    }
}
