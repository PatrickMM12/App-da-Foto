using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using Newtonsoft.Json;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App_da_Foto.Services
{
    public class EnderecoService : IEnderecoService<Endereco>
    {
        protected HttpClient _client;
        protected string BaseApiUrl = "https://appdafotoapi.azurewebsites.net";

        public EnderecoService()
        {
            _client = new HttpClient();
        }
        public async Task<IEnumerable<Endereco>> ObterEnderecos()
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Endereco/todos/");

            IEnumerable<Endereco> enderecos = null;
            if (response.IsSuccessStatusCode)
            {
                enderecos = await response.Content.ReadAsAsync<IEnumerable<Endereco>>();
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return enderecos;
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                await Shell.Current.DisplayAlert("Erro", response.ToString(), "Ok");
            }
            return await Task.FromResult(enderecos);
        }

        public async Task<IEnumerable<EnderecoFotografo>> ObterEnderecosPorLocalizacao(double latitudeSul, double latitudeNorte, double longitudeOeste, double longitudeLeste)
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Endereco/por-localizacao?latitudeSul={latitudeSul}&latitudeNorte={latitudeNorte}&longitudeOeste={longitudeOeste}&longitudeLeste={longitudeLeste}");

            IEnumerable<EnderecoFotografo> enderecos = null;

            if (response.IsSuccessStatusCode)
            {
                enderecos = await response.Content.ReadAsAsync<IEnumerable<EnderecoFotografo>>();
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return enderecos;
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                await Shell.Current.DisplayAlert("Erro", response.ToString(), "Ok");

            }
            return await Task.FromResult(enderecos);
        }

        public async Task<ResponseService<Endereco>> AdicionarEndereco(Endereco endereco)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync($"{BaseApiUrl}/api/Endereco", endereco);

            ResponseService<Endereco> responseService = new ResponseService<Endereco>
            {
                IsSuccess = response.IsSuccessStatusCode,
                StatusCode = (int)response.StatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                responseService.Data = await response.Content.ReadAsAsync<Endereco>();
            }
            else if (responseService.StatusCode == 400)
            {
                await Shell.Current.DisplayAlert("Erro!", "E-mail já Cadastrado!", "Ok");
            }
            else if (response.RequestMessage.Content == null)
            {
                return responseService;
            }
            else
            {
                String problemResponse = await response.RequestMessage.Content.ReadAsStringAsync();
                String errors = JsonConvert.DeserializeObject<String>(problemResponse);

                responseService.Errors.Add(errors);
            }
            return responseService;
        }

        public async Task<ResponseService<Endereco>> AtualizarEndereco(Endereco endereco)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseApiUrl}/api/Endereco/{endereco.IdFotografo}", endereco);

            ResponseService<Endereco> responseService = new ResponseService<Endereco>
            {
                IsSuccess = response.IsSuccessStatusCode,
                StatusCode = (int)response.StatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                responseService.Data = await response.Content.ReadAsAsync<Endereco>();
            }
            else if (response.RequestMessage.Content == null)
            {
                return responseService;
            }
            else
            {
                String problemResponse = await response.RequestMessage.Content.ReadAsStringAsync();
                String errors = JsonConvert.DeserializeObject<String>(problemResponse);

                responseService.Errors.Add(errors);
            }
            return responseService;
        }

        public async Task<EnderecoWeb> BuscarCep(string cep)
        {
            var json = await _client.GetStringAsync($"https://viacep.com.br/ws/{cep}/json/");
            var dados = JsonConvert.DeserializeObject<EnderecoWeb>(json);
            return dados;
        }

    }
}
