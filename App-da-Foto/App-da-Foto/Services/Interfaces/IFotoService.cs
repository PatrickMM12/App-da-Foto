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
    public interface IFotoService<T>
    {
        Task<IEnumerable<Foto>> ObterFotos();

        Task<ResponseService<Foto>> AdicionarFoto(Foto foto);

        Task<ResponseService<Foto>> AtualizarFoto(Foto foto);
    }
}
