using SQLite;

namespace App_da_Foto.Models
{
    public class Fotografo
    {
        [PrimaryKey, AutoIncrement]
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Especialidade { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}