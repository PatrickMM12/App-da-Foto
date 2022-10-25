using app_da_foto.Domain.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app_da_foto.Domain.Model
{
    public class Foto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NomeArquivo { get; set; }

        [Required]
        public byte[] Imagem { get; set; }

        public string MIME { get; set; }


        public string HoraUpload { get; set; }

        [Required]
        public string Perfil { get; set; }

        [Required]
        public int IdFotografo { get; set; }

        [ForeignKey("IdFotografo")]
        public Fotografo Fotografo { get; set; }
    }
}
