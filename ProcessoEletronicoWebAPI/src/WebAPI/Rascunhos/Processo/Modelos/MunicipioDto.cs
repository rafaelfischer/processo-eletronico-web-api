namespace ProcessoEletronicoService.WebAPI.Rascunhos.Processo.Modelos
{
    public class GetMunicipioDto
    {
        public int Id { get; set; }
        public string Uf { get; set; }
        public string Nome { get; set; }
        public string GuidMunicipio { get; set; }
    }

    public class PostMunicipioDto
    {
        public string GuidMunicipio { get; set; }
    }

    public class PatchMunicipioDto
    {
        public string GuidMunicipio { get; set; }
    }

}
