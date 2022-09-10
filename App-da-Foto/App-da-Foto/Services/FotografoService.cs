using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App_da_Foto.Services
{
    public class FotografoService : Service
    {
        public async Task<ResponseService<Fotografo>> ObterFotografo(string email, string senha)
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Fotografo?email={email}&senha={senha}");

            ResponseService<Fotografo> responseService = new ResponseService<Fotografo>
            {
                IsSuccess = response.IsSuccessStatusCode,
                StatusCode = (int)response.StatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                responseService.Data = await response.Content.ReadAsAsync<Fotografo>();
            }
            else
            {
                String problemResponse = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<ResponseService<Fotografo>>(problemResponse);

                responseService.Errors = errors.Errors;
            }
            return responseService;
        }

        public async Task<IEnumerable<Fotografo>> ObterFotografos()
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Fotografo/todos");

            List<Fotografo> fotografos = null;
            if (response.IsSuccessStatusCode)
            {
                fotografos = await response.Content.ReadAsAsync<List<Fotografo>>();
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return fotografos;
            }
            return fotografos;
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
            else
            {
                String problemResponse = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<ResponseService<Fotografo>>(problemResponse);

                responseService.Errors = errors.Errors;
            }
            return responseService;
        }
    }
}
