using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Comum.Exceptions;
using ProcessoEletronicoService.Negocio.Comum.Base;
using ProcessoEletronicoService.Negocio.Comum.Validacao;
using ProcessoEletronicoService.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Negocio.Rascunho.Processo.Validacao
{
    public class SinalizacaoValidacao
    {
        private IRepositorioGenerico<Sinalizacao> _repositorioSinalizacoes;
        private IRepositorioGenerico<SinalizacaoRascunhoProcesso> _repositorioSinalizacoesRascunhoProcesso;
        public SinalizacaoValidacao(IProcessoEletronicoRepositorios repositorios)
        {
            _repositorioSinalizacoes = repositorios.Sinalizacoes;
            _repositorioSinalizacoesRascunhoProcesso = repositorios.SinalizacoesRascunhoProcesso;
        }

        public void Exists(Sinalizacao sinalizacao)
        {
            if (sinalizacao == null)
            {
                throw new RecursoNaoEncontradoException("Sinalização não encontrada");
            }
        }
        public void Exists(SinalizacaoRascunhoProcesso sinalizacaoRascunhoProcesso)
        {
            if (sinalizacaoRascunhoProcesso == null)
            {
                throw new RecursoNaoEncontradoException("Sinalização não encontrada no Rascunho do Processo");
            }
        }


        public bool ExistsInRascunhoProcesso(int idRascunhoProcesso, int idSinalizacao)
        {
            //Verificar se a sinalização está cadastrada no rascunho do Processo
            return _repositorioSinalizacoesRascunhoProcesso.Where(srp => srp.IdRascunhoProcesso == idRascunhoProcesso && srp.IdSinalizacao == idSinalizacao).SingleOrDefault() != null;
        }
        
        public void IsValid(IList<int> idSinalizacoes)
        {
            if (idSinalizacoes != null)
            {
                foreach (int idSinalizacao in idSinalizacoes)
                {
                    IsValid(idSinalizacao);
                }
            }
        }

        public void IsValid(int idSinalizacao)
        {
            if (_repositorioSinalizacoes.Where(s => s.Id == idSinalizacao).SingleOrDefault() == null)
            {
                throw new RecursoNaoEncontradoException($"Sinalização {idSinalizacao} não encontrada");
            }
        }
    }
}
