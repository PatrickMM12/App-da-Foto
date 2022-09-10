using System;
using System.ComponentModel.DataAnnotations;

namespace app_da_foto.Domain.Model
{
    public class Fotografo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Nome { get; set; }

        [Required]
        public string Especialidade { get; set; }

        [MaxLength(1)]
        public string Sexo { get; set; }

        public DateTime Nascimento { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(2)]
        public string Senha { get; set; }
    }
}
