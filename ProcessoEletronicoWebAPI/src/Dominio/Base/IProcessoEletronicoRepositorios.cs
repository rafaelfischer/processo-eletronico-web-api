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
        IRepositorioGenerico<Despacho> Despachos { get; }
        IRepositorioGenerico<DestinacaoFinal> DestinacoesFinais { get; }
        IRepositorioGenerico<Funcao> Funcoes { get; }
        IRepositorioGenerico<PlanoClassificacao> PlanosClassificacao { get; }
        IRepositorioGenerico<OrganizacaoProcesso> OrganizacoesProcesso { get; }
        IRepositorioGenerico<Processo> Processos { get; }
        IRepositorioGenerico<TipoContato> TiposContato { get; }
        IRepositorioGenerico<TipoDocumental> TiposDocumentais { get; }
        IRepositorioGenerico<Sinalizacao> Sinalizacoes { get; }
    }
}
