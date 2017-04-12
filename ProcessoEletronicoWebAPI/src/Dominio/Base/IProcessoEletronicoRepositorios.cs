using ProcessoEletronicoService.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessoEletronicoService.Dominio.Base
{
    public interface IProcessoEletronicoRepositorios : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }

        IRepositorioGenerico<Anexo> Anexos { get; }
        IRepositorioGenerico<Atividade> Atividades { get; }
        IRepositorioGenerico<Email> Emails { get; }
        IRepositorioGenerico<Contato> Contatos { get; }
        IRepositorioGenerico<Despacho> Despachos { get; }
        IRepositorioGenerico<DestinacaoFinal> DestinacoesFinais { get; }
        IRepositorioGenerico<Funcao> Funcoes { get; }
        IRepositorioGenerico<InteressadoPessoaFisica> InteressadosPessoaFisica { get; }
        IRepositorioGenerico<InteressadoPessoaJuridica> InteressadosPessoaJuridica { get; }
        IRepositorioGenerico<MunicipioRascunhoProcesso> MunicipiosRascunhoProcesso { get; }
        IRepositorioGenerico<PlanoClassificacao> PlanosClassificacao { get; }
        IRepositorioGenerico<OrganizacaoProcesso> OrganizacoesProcesso { get; }
        IRepositorioGenerico<Processo> Processos { get; }
        IRepositorioGenerico<RascunhoProcesso> RascunhosProcesso { get; }
        IRepositorioGenerico<SinalizacaoRascunhoProcesso> SinalizacoesRascunhoProcesso { get; }
        IRepositorioGenerico<TipoContato> TiposContato { get; }
        IRepositorioGenerico<TipoDocumental> TiposDocumentais { get; }
        IRepositorioGenerico<Sinalizacao> Sinalizacoes { get; }
    }
}
