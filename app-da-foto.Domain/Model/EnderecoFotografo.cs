using app_da_foto.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_da_Foto.Models
{
    public class EnderecoFotografo
    {
        public Endereco Endereco { get; set; }
        public Fotografo Fotografo { get; set; }
    }
}
