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
    public class FotoService : IFotoService<Foto>
    {
        protected HttpClient _client;
        protected string BaseApiUrl = "https://appdafotoapi.azurewebsites.net";

        public FotoService()
        {
            _client = new HttpClient();
        }
        public async Task<IEnumerable<Foto>> ObterFotos()
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Foto");

            IEnumerable<Foto> fotos = null;
            if (response.IsSuccessStatusCode)
            {
                fotos = await response.Content.ReadAsAsync<IEnumerable<Foto>>();
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return fotos;
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                await Shell.Current.DisplayAlert("Erro", response.ToString(), "Ok");
                
            }
            return await Task.FromResult(fotos);
        }

        public async Task<ResponseService<Foto>> AdicionarFoto(Foto foto)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync($"{BaseApiUrl}/api/Foto", foto);

            ResponseService<Foto> responseService = new ResponseService<Foto>
            {
                IsSuccess = response.IsSuccessStatusCode,
                StatusCode = (int)response.StatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                responseService.Data = await response.Content.ReadAsAsync<Foto>();
            }
            else if (response.RequestMessage.Content == null)
            {
                return responseService;
            }
            else
            {
                String problemResponse = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<ResponseService<Foto>>(problemResponse);

                responseService.Errors = errors.Errors;
            }
            return responseService;
        }

        public async Task<ResponseService<Foto>> AtualizarFoto(Foto foto)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseApiUrl}/api/Foto/{foto.IdFotografo}", foto);

            ResponseService<Foto> responseService = new ResponseService<Foto>
            {
                IsSuccess = response.IsSuccessStatusCode,
                StatusCode = (int)response.StatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                responseService.Data = await response.Content.ReadAsAsync<Foto>();
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
