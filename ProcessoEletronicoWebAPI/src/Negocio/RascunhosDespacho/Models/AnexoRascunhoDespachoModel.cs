namespace Negocio.RascunhosDespacho.Models
{
    public class AnexoRascunhoDespachoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public byte[] Conteudo { get; set; }
        public string ConteudoString { get; set; }
        public string MimeType { get; set; }
    }
}
