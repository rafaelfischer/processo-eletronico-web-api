using ProcessoEletronicoService.Negocio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessoEletronicoService.Infraestrutura.Repositorios;
using ProcessoEletronicoService.Infraestrutura.Mapeamento;
using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Negocio.Modelos;
using ProcessoEletronicoService.Negocio.Validacao;

namespace ProcessoEletronicoService.Negocio.Restrito
{
    public class ProcessoNegocio : IProcessoNegocio
    {
        IUnitOfWork unitOfWork;
        IRepositorioGenerico<Processo> repositorioProcessos;
        ProcessoValidacao processoValidacao;
        InteressadoPessoaFisicaValidacao interessadoPessoaFisicaValidacao;
        

        public ProcessoNegocio(IProcessoEletronicoRepositorios repositorios)
        {
            this.unitOfWork = repositorios.UnitOfWork;
            this.repositorioProcessos = repositorios.Processos;
            this.processoValidacao = new ProcessoValidacao(repositorios);
            this.interessadoPessoaFisicaValidacao = new InteressadoPessoaFisicaValidacao(repositorios);
        }

        public void Listar()
        {
            throw new NotImplementedException();
        }

        public void Pesquisar(int id)
        {
            throw new NotImplementedException();
        }

        public void Pesquisar(string numeroProcesso)
        {
            throw new NotImplementedException();
        }

        public void Autuar(ProcessoModeloNegocio processoNegocio)
        {
            processoValidacao.Preenchido(processoNegocio);
            processoValidacao.Valido(processoNegocio);
            throw new NotImplementedException("Processo Válido");
        }

        public void Despachar()
        {
            throw new NotImplementedException();
        }

        public void Excluir()
        {
            throw new NotImplementedException();
        }
    }
}
