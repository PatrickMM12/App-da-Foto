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
    public interface IFotografoService<T>
    {
        Task<IEnumerable<Fotografo>> ObterFotografos();

        Task<ResponseService<IEnumerable<Fotografo>>> ObterFotografos(string nome, string especialidade);

        Task<ResponseService<Fotografo>> ObterFotografo(string email, string senha);

        Task<ResponseService<FotografoCompleto>> ObterFotografo(int id);

        Task<ResponseService<Fotografo>> AdicionarFotografo(Fotografo fotografo);

        Task<ResponseService<Fotografo>> AtualizarFotografo(Fotografo fotografo);
    }
}
