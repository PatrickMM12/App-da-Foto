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
    public class ContatoService : IContatoService<Contato>
    {
        protected HttpClient _client;
        protected string BaseApiUrl = "https://appdafotoapi.azurewebsites.net";

        public ContatoService()
        {
            _client = new HttpClient();
        }
        public async Task<IEnumerable<Contato>> ObterContatos()
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Contato");

            IEnumerable<Contato> contatos = null;
            if (response.IsSuccessStatusCode)
            {
                contatos = await response.Content.ReadAsAsync<IEnumerable<Contato>>();
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return contatos;
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                await Shell.Current.DisplayAlert("Erro", response.ToString(), "Ok");
                
            }
            return await Task.FromResult(contatos);
        }

        public async Task<ResponseService<Contato>> AdicionarContato(Contato contato)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync($"{BaseApiUrl}/api/Contato", contato);

            ResponseService<Contato> responseService = new ResponseService<Contato>
            {
                IsSuccess = response.IsSuccessStatusCode,
                StatusCode = (int)response.StatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                responseService.Data = await response.Content.ReadAsAsync<Contato>();
            }
            else if (response.RequestMessage.Content == null)
            {
                return responseService;
            }
            else
            {
                String problemResponse = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<ResponseService<Contato>>(problemResponse);

                responseService.Errors = errors.Errors;
            }
            return responseService;
        }

        public async Task<ResponseService<Contato>> AtualizarContato(Contato contato)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseApiUrl}/api/Contato/{contato.IdFotografo}", contato);

            ResponseService<Contato> responseService = new ResponseService<Contato>
            {
                IsSuccess = response.IsSuccessStatusCode,
                StatusCode = (int)response.StatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                responseService.Data = await response.Content.ReadAsAsync<Contato>();
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
    }
}
