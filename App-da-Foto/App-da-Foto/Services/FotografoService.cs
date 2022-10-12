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
    public class FotografoService : IFotografoService<Fotografo>
    {
        protected HttpClient _client;
        protected string BaseApiUrl = "https://appdafotoapi.azurewebsites.net";

        public FotografoService()
        {
            _client = new HttpClient();
        }
        public async Task<IEnumerable<Fotografo>> ObterFotografos()
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Fotografo");

            IEnumerable<Fotografo> fotografos = null;
            if (response.IsSuccessStatusCode)
            {
                fotografos = await response.Content.ReadAsAsync<IEnumerable<Fotografo>>();
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return fotografos;
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                await Shell.Current.DisplayAlert("Erro", response.ToString(), "Ok");
                
            }
            return await Task.FromResult(fotografos.Where(a =>
                a.Nome.ToLower().Contains("".ToLower()) &&
                a.Especialidade.ToLower().Contains("".ToLower()))
                .OrderBy(item => item.Nome).ToList());
        }

        public async Task<ResponseService<IEnumerable<Fotografo>>> ObterFotografos(string nome, string especialidade)
        {
            if(nome == null || nome == "")
            {
                nome = "v@zio";
            }
            if (especialidade == null)
            {
                especialidade = "v@zio";
            }

            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Fotografo/busca?nome={nome}&especialidade={especialidade}");
            ResponseService<IEnumerable<Fotografo>> responseService = new ResponseService<IEnumerable<Fotografo>>()
            {
                IsSuccess = response.IsSuccessStatusCode,
                StatusCode = (int)response.StatusCode
            };
            if (response.IsSuccessStatusCode)
            {
                responseService.Data = await response.Content.ReadAsAsync<IEnumerable<Fotografo>>();
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                String problemResponse = await response.RequestMessage.Content.ReadAsStringAsync();
                String errors = JsonConvert.DeserializeObject<String>(problemResponse);

                responseService.Errors.Add(errors);
            }
            return responseService;
        }

        public async Task<ResponseService<Fotografo>> ObterFotografo(string email, string senha)
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Fotografo/login?email={email}&senha={senha}");

            ResponseService<Fotografo> responseService = new ResponseService<Fotografo>
            {
                IsSuccess = response.IsSuccessStatusCode,
                StatusCode = (int)response.StatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                responseService.Data = await response.Content.ReadAsAsync<Fotografo>();
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

        public async Task<ResponseService<FotografoCompleto>> ObterFotografo(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Fotografo/{id}");

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
                String problemResponse = await response.RequestMessage.Content.ReadAsStringAsync();
                String errors = JsonConvert.DeserializeObject<String>(problemResponse);

                responseService.Errors.Add(errors);
            }
            return responseService;
        }

        public async Task<ResponseService<Fotografo>> AdicionarFotografo(Fotografo fotografo)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync($"{BaseApiUrl}/api/Fotografo", fotografo);

            ResponseService<Fotografo> responseService = new ResponseService<Fotografo>
            {
                IsSuccess = response.IsSuccessStatusCode,
                StatusCode = (int)response.StatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                responseService.Data = await response.Content.ReadAsAsync<Fotografo>();
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

        public async Task<ResponseService<Fotografo>> AtualizarFotografo(Fotografo fotografo)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"{BaseApiUrl}/api/Fotografo/{fotografo.Id}", fotografo);

            ResponseService<Fotografo> responseService = new ResponseService<Fotografo>
            {
                IsSuccess = response.IsSuccessStatusCode,
                StatusCode = (int)response.StatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                responseService.Data = await response.Content.ReadAsAsync<Fotografo>();
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
