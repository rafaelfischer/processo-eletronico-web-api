using ProcessoEletronicoService.Dominio.Base;
using ProcessoEletronicoService.Dominio.Modelos;
using ProcessoEletronicoService.Infraestrutura.Mapeamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Infraestrutura.Repositorios
{
    public class ProcessoEletronicoRepositorios : IProcessoEletronicoRepositorios
    {
        public ProcessoEletronicoRepositorios()
        {
            UnitOfWork = new EFUnitOfWork(new ProcessoEletronicoContext());

            Anexos = UnitOfWork.MakeGenericRepository<Anexo>();
            Bloqueios = UnitOfWork.MakeGenericRepository<Bloqueio>();
            AnexosRascunho = UnitOfWork.MakeGenericRepository<AnexoRascunho>();
            Atividades = UnitOfWork.MakeGenericRepository<Atividade>();
            Contatos = UnitOfWork.MakeGenericRepository<Contato>();
            ContatosRascunho = UnitOfWork.MakeGenericRepository<ContatoRascunho>();
            Despachos = UnitOfWork.MakeGenericRepository<Despacho>();
            Emails = UnitOfWork.MakeGenericRepository<Email>();
            EmailsRascunho = UnitOfWork.MakeGenericRepository<EmailRascunho>();
            DestinacoesFinais = UnitOfWork.MakeGenericRepository<DestinacaoFinal>();
            Funcoes = UnitOfWork.MakeGenericRepository<Funcao>();
            InteressadosPessoaFisica = UnitOfWork.MakeGenericRepository<InteressadoPessoaFisica>();
            InteressadosPessoaFisicaRascunho = UnitOfWork.MakeGenericRepository<InteressadoPessoaFisicaRascunho>();
            InteressadosPessoaJuridica = UnitOfWork.MakeGenericRepository<InteressadoPessoaJuridica>();
            InteressadosPessoaJuridicaRascunho = UnitOfWork.MakeGenericRepository<InteressadoPessoaJuridicaRascunho>();
            MunicipiosRascunhoProcesso = UnitOfWork.MakeGenericRepository<MunicipioRascunhoProcesso>();
            Notificacoes = UnitOfWork.MakeGenericRepository<Notificacao>();
            PlanosClassificacao = UnitOfWork.MakeGenericRepository<PlanoClassificacao>();
            OrganizacoesProcesso = UnitOfWork.MakeGenericRepository<OrganizacaoProcesso>();
            Processos = UnitOfWork.MakeGenericRepository<Processo>();
            RascunhosProcesso = UnitOfWork.MakeGenericRepository<RascunhoProcesso>();
            SinalizacoesRascunhoProcesso = UnitOfWork.MakeGenericRepository<SinalizacaoRascunhoProcesso>();
            SinalizacoesProcesso = UnitOfWork.MakeGenericRepository<SinalizacaoProcesso>();
            TiposContato = UnitOfWork.MakeGenericRepository<TipoContato>();
            TiposDocumentais = UnitOfWork.MakeGenericRepository<TipoDocumental>();
            Sinalizacoes = UnitOfWork.MakeGenericRepository<Sinalizacao>();

        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public IRepositorioGenerico<Anexo> Anexos { get; private set; }
        public IRepositorioGenerico<Bloqueio> Bloqueios { get; private set; }
        public IRepositorioGenerico<AnexoRascunho> AnexosRascunho { get; private set; }
        public IRepositorioGenerico<Atividade> Atividades { get; private set; }
        public IRepositorioGenerico<Contato> Contatos { get; private set; }
        public IRepositorioGenerico<ContatoRascunho> ContatosRascunho { get; private set; }
        public IRepositorioGenerico<Despacho> Despachos { get; private set; }
        public IRepositorioGenerico<Email> Emails { get; private set; }
        public IRepositorioGenerico<EmailRascunho> EmailsRascunho { get; }
        public IRepositorioGenerico<DestinacaoFinal> DestinacoesFinais { get; private set; }
        public IRepositorioGenerico<Funcao> Funcoes { get; private set; }
        public IRepositorioGenerico<InteressadoPessoaFisica> InteressadosPessoaFisica { get; private set; }
        public IRepositorioGenerico<InteressadoPessoaFisicaRascunho> InteressadosPessoaFisicaRascunho { get; private set; }
        public IRepositorioGenerico<InteressadoPessoaJuridica> InteressadosPessoaJuridica { get; private set; }
        public IRepositorioGenerico<InteressadoPessoaJuridicaRascunho> InteressadosPessoaJuridicaRascunho { get; private set; }
        public IRepositorioGenerico<MunicipioRascunhoProcesso> MunicipiosRascunhoProcesso { get; private set; }
        public IRepositorioGenerico<Notificacao> Notificacoes { get; private set; }
        public IRepositorioGenerico<OrganizacaoProcesso> OrganizacoesProcesso { get; private set; }
        public IRepositorioGenerico<PlanoClassificacao> PlanosClassificacao { get; private set; }
        public IRepositorioGenerico<Processo> Processos { get; private set; }
        public IRepositorioGenerico<RascunhoProcesso> RascunhosProcesso { get; private set; }
        public IRepositorioGenerico<SinalizacaoProcesso> SinalizacoesProcesso { get; private set; }
        public IRepositorioGenerico<SinalizacaoRascunhoProcesso> SinalizacoesRascunhoProcesso { get; private set; }
        public IRepositorioGenerico<TipoContato> TiposContato { get; private set; }
        public IRepositorioGenerico<TipoDocumental> TiposDocumentais { get; private set; }
        public IRepositorioGenerico<Sinalizacao> Sinalizacoes { get; private set; }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
