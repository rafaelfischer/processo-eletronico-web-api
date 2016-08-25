using Nest;

namespace ProcessoEletronicoService.Infraestrutura.Repositorios.Modelos
{
    public class OrgaoRepositorio
    {
        [Number(Name = "id")]
        public int id { get; set; }
        [String (Name = "sigla")]
        public string sigla { get; set; }
    }
}
