using app_da_foto.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app_da_foto.Domain.Model
{
    public class Contato
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        public string TipoTelefone { get; set; }

        public string Instagram { get; set; }

        public int AcessosInstagram { get; set; }

        public int IdFotografo { get; set; }

        [ForeignKey("IdFotografo")]
        public Fotografo Fotografo { get; set; }
    }
}
