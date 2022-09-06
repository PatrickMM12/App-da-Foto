using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace App_da_Foto.Services
{
    class Service
    {
        protected HttpClient _client;
        protected string BaseApiUrl = "https://appdafotoapi.azurewebsites.net/";

        public Service()
        {
            _client = new HttpClient();
        }
    }
}
