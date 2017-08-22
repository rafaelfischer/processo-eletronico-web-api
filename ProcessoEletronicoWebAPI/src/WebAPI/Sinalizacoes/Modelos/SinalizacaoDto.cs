using System.ComponentModel.DataAnnotations;

namespace ProcessoEletronicoService.WebAPI.Sinalizacoes.Modelos
{

    public class GetSinalizacaoDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Cor { get; set; }
        public string Imagem { get; set; }
        public string MimeType { get; set; }
    }

    public class GetSinalizacaoNoImagemDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Cor { get; set; }
    }
    
    public class PostSinalizacaoDto
    {
        [Required]
        public string Descricao { get; set; }
        public string Cor { get; set; }
        public string Imagem { get; set; }
        public string MimeType { get; set; }
    }

    public class PatchSinalizacaoDto
    {
        [Required]
        public string Descricao { get; set; }
        public string Cor { get; set; }
        public string Imagem { get; set; }
        public string MimeType { get; set; }
    }

}
