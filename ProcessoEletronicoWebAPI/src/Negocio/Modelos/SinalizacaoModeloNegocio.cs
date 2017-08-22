namespace ProcessoEletronicoService.Negocio.Modelos
{
    public class SinalizacaoModeloNegocio
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Cor { get; set; }
        public byte[] Imagem { get; set; }
        public string ImagemBase64String { get; set; }
        public string MimeType { get; set; }
        public OrganizacaoProcessoModeloNegocio OrganizacaoProcesso { get; set; }
    }
}
