using App_da_Foto.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories
{
    public class FotografoRepositorio : IFotografoRepositorio<Fotografo>
    {
        HttpClient client = new HttpClient();

        public async Task<bool> AddFotografoAsync(Fotografo fotografo)
        {
            try
            {
                string url = "http://192.168.1.15:8082/api/Fotografos/";

                var uri = new Uri(url);

                var data = JsonConvert.SerializeObject(fotografo);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao incluir fotografo");
                }

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateFotografoAsync(Fotografo fotografo)
        {
            try
            {
                string url = "http://192.168.1.15:8082/api/Fotografos/{0}";

                var uri = new Uri(string.Format(url, fotografo.Id));

                var data = JsonConvert.SerializeObject(fotografo);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao atualizar fotografo");
                }

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteFotografoAsync(string id)
        {
            try
            {
                string url = "http://192.168.1.15:8082/api/Fotografos/{0}";

                var uri = new Uri(string.Format(url, id));

                await client.DeleteAsync(uri);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Fotografo> GetFotografoAsync(string id)
        {
            try
            {
                string url = "http://192.168.1.15:8082/api/Fotografos/{0}";
                var response = await client.GetStringAsync(url);
                var fotografos = JsonConvert.DeserializeObject<IEnumerable<Fotografo>>(response);

                return await Task.FromResult(fotografos.FirstOrDefault(f => f.Id == id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Fotografo>> GetFotografosAsync(bool forceRefresh = false)
        {
            try
            {
                string url = "http://192.168.1.15:8082/api/Fotografos/";
                var cts = new CancellationTokenSource();
                cts.CancelAfter(TimeSpan.FromSeconds(30));
                var response = await client.GetStringAsync(url);
                var fotografos = JsonConvert.DeserializeObject<IEnumerable<Fotografo>>(response);

                return await Task.FromResult(fotografos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}