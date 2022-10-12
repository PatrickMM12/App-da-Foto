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
    public interface IEnderecoService<T>
    {
        Task<IEnumerable<Endereco>> ObterEnderecos();

        Task<ResponseService<Endereco>> AdicionarEndereco(Endereco endereco);

        Task<ResponseService<Endereco>> AtualizarEndereco(Endereco endereco);

        Task<EnderecoWeb> BuscarCep(string cep);
    }
}
