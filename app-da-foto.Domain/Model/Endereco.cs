using app_da_foto.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app_da_foto.Domain.Model
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Logradouro { get; set; }

        [Required]
        public string Numero { get; set; }

        public string Complemento { get; set; }

        [Required]
        public string Bairro { get; set; }

        [Required]
        public string Cidade { get; set; }

        [Required]
        public string Estado { get; set; }

        [Required]
        public string Cep { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; } 

        [Required]
        public int IdFotografo { get; set; }

        [ForeignKey("IdFotografo")]
        public Fotografo Fotografo { get; set; }
    }
}
