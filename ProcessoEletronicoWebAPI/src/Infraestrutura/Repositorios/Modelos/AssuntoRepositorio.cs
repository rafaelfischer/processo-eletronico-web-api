using Nest;

namespace ProcessoEletronicoService.Infraestrutura.Repositorios.Modelos
{
    public class AssuntoRepositorio
    {
        [Number(Name = "id")]
        public int id { get; set; }
        [String(Name = "descricao")]
        public string descricao { get; set; }
    }
}
