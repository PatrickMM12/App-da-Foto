using app_da_foto.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace app_da_foto.Domain.Model
{
    public class FotografoCompleto
    {
        public Fotografo Fotografo { get; set; }
        public Endereco Endereco { get; set; }
        public Contato Contato { get; set; }
        public Foto FotoPerfil { get; set; }
    }
}
