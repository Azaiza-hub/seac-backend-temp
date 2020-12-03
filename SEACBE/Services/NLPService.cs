using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
//using System.Web.Http;
using Newtonsoft.Json;
using SEACBE.Models;
using SEACBE.Repositories;

namespace SEACBE.Services
{
    public class NLPService
    {
        private readonly string URL_API_IA = "http://www.google.com";//URL, completar con la URL de la api (es la URL de arriba que aparece en la línea 31)
        private readonly HttpClient ClienteHttp_IA;
        
        public NLPService()
        {
            ClienteHttp_IA = new HttpClient();
            ClienteHttp_IA.BaseAddress = new Uri(URL_API_IA);
        }

        public  string ObtenerSentimentalismo(string descripcion)
        {
            var desc = JsonConvert.SerializeObject(descripcion);
            var buffer = System.Text.Encoding.UTF8.GetBytes(desc);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var responseTask = ClienteHttp_IA.PostAsync("urldelaapi", byteContent); //esto se concatena a la URL de arriba
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                string senti = readTask.Result;
                return senti;
            }
            else
            {
                throw new Exception("No se pudo conectar a la API");
            }
        }

    }
}
