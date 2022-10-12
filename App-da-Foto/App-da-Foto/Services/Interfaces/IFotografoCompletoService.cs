using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Services.Interfaces
{
    public interface IFotografoCompletoService<T>
    {
        Task<ResponseService<FotografoCompleto>> ObterFotografo(int idFotografo);

        Task<ResponseService<FotografoCompleto>> AtualizarFotografo(FotografoCompleto fotografo);
    }
}
