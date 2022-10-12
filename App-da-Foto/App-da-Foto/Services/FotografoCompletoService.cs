using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using Newtonsoft.Json;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App_da_Foto.Services
{
    public class FotografoCompletoService : IFotografoCompletoService<FotografoCompleto>
    {
        protected HttpClient _client;
        protected string BaseApiUrl = "https://appdafotoapi.azurewebsites.net";

        public FotografoCompletoService()
        {
            _client = new HttpClient();
        }

        public async Task<ResponseService<FotografoCompleto>> ObterFotografo(int idFotografo)
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Fotografo/{idFotografo}");

            ResponseService<FotografoCompleto> responseService = new ResponseService<FotografoCompleto>
            {
                IsSuccess = response.IsSuccessStatusCode,
                StatusCode = (int)response.StatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                responseService.Data = await response.Content.ReadAsAsync<FotografoCompleto>();
            }
            else if (response.RequestMessage.Content == null)
            {
                return responseService;
            }
            else
            {
                String problemResponse = await response.Content.ReadAsStringAsync();
                String errors = JsonConvert.DeserializeObject<String>(problemResponse);

                responseService.Errors.Add(errors);
            }
            return responseService;
        }

        public async Task<ResponseService<FotografoCompleto>> AtualizarFotografo(FotografoCompleto fotografo)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync($"{BaseApiUrl}/api/Fotografo", fotografo);

            ResponseService<FotografoCompleto> responseService = new ResponseService<FotografoCompleto>
            {
                IsSuccess = response.IsSuccessStatusCode,
                StatusCode = (int)response.StatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                responseService.Data = await response.Content.ReadAsAsync<FotografoCompleto>();
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
                String problemResponse = await response.Content.ReadAsStringAsync();
                String errors = JsonConvert.DeserializeObject<String>(problemResponse);

                responseService.Errors.Add(errors);

            }
            return responseService;
        }
    }
}
