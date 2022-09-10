using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace App_da_Foto.Services
{
    public class Service
    {
        protected HttpClient _client;
        protected string BaseApiUrl = "https://appdafotoapi.azurewebsites.net/";

        public Service()
        {
            _client = new HttpClient();
        }
    }
}
