using KursClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KursClient.Services
{
    public class AuthService
    {
        private HttpClient client = new HttpClient();
        public async Task<String> Register(Person person)
        {
            JsonContent content = JsonContent.Create(person);
            using var response = await client.PostAsync("https://localhost:7229/register", content);
            string responseText = await response.Content.ReadAsStringAsync();
            if (responseText != "")
            {
                return $"Пользователь {person.Email} успешно создан";
            }
            return $"Пользователь {person.Email} существует!";
        }
        public async Task<Response> SignIn(Person person)
        {
            JsonContent content = JsonContent.Create(person);
            using var response = await client.PostAsync("https://localhost:7229/login", content);
            string responseText = await response.Content.ReadAsStringAsync();
            if(responseText !="")
            {
                Response resp = JsonSerializer.Deserialize<Response>(responseText)!;
                return resp;
            }
            return null!;
        }
    }
}
