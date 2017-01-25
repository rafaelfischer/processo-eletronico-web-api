using AutoMapper;
using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using Microsoft.EntityFrameworkCore;
using ProcessoEletronicoService.Negocio.Validacao;

namespace ProcessoEletronicoService.Negocio
{
    public class DestinacaoFinalNegocio : BaseNegocio, IDestinacaoFinalNegocio
    {
        private IUnitOfWork unitOfWork;
        private IRepositorioGenerico<DestinacaoFinal> repositorioDestinacoesFinais;
        private DestinacaoFinalValidacao destinacaoFinalValidacao;


        public DestinacaoFinalNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            unitOfWork = repositorios.UnitOfWork;
            repositorioDestinacoesFinais = repositorios.DestinacoesFinais;
            destinacaoFinalValidacao = new DestinacaoFinalValidacao(repositorios);
        }

        public List<DestinacaoFinalModeloNegocio> Listar()
        {
            List<DestinacaoFinal> destinacoesFinais = repositorioDestinacoesFinais.OrderBy(df => df.Descricao).ToList();

            return Mapper.Map<List<DestinacaoFinal>, List<DestinacaoFinalModeloNegocio>>(destinacoesFinais);
        }

        public DestinacaoFinalModeloNegocio Pesquisar(int id)
        {
            DestinacaoFinal destinacaoFinal = repositorioDestinacoesFinais.Where(df => df.Id.Equals(id)).SingleOrDefault();
            destinacaoFinalValidacao.NaoEncontrado(destinacaoFinal);

            return Mapper.Map<DestinacaoFinal, DestinacaoFinalModeloNegocio>(destinacaoFinal);

        }

        public DestinacaoFinalModeloNegocio Inserir(DestinacaoFinalModeloNegocio destinacaoFinalModeloNegocio)
        {
            destinacaoFinalValidacao.Preenchido(destinacaoFinalModeloNegocio);
            destinacaoFinalValidacao.Valido(destinacaoFinalModeloNegocio);

            DestinacaoFinal destinacaoFinal = new DestinacaoFinal();
            Mapper.Map(destinacaoFinalModeloNegocio, destinacaoFinal);

            repositorioDestinacoesFinais.Add(destinacaoFinal);
            unitOfWork.Save();

            return Pesquisar(destinacaoFinal.Id);
            
        }

        public void Excluir(int id)
        {
            destinacaoFinalValidacao.PossivelExcluir(id);

            DestinacaoFinal destinacaoFinal = repositorioDestinacoesFinais.Where(df => df.Id.Equals(id)).Single();

            repositorioDestinacoesFinais.Remove(destinacaoFinal);
            unitOfWork.Save();
        }
    }
}
