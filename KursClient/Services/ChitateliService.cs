
using KursClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace KursClient.Services
{
    public class ChitateliService : BaseService<Chitateli>
    {
        private HttpClient httpClient;
        public ChitateliService() 
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization",
               "Bearer " + RegisterUser.access_token);
        }
        public override async Task Add(Chitateli obj)
        {
            JsonContent content = JsonContent.Create(obj);
            using var response = await httpClient.PostAsync("https://localhost:7229/api/Chitateli", content);
            string responseText = await response.Content.ReadAsStringAsync();
            if(responseText != null)
            {
                Chitateli resp = JsonSerializer.Deserialize<Chitateli>(responseText!)!;
                if (resp != null)
                {
                    MessageBox.Show("Читатель успешно создан!");
                }
            }
            else
            {
                MessageBox.Show(responseText);
            }
        }

        public override Task Delete(Chitateli obj)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<Chitateli>> GetAll()
        {
            return (await httpClient.GetFromJsonAsync<List<Chitateli>>("https://localhost:7229/api/Chitateli"))!;
        }


        public override Task<List<Chitateli>> Search(string str)
        {
            throw new NotImplementedException();
        }

        public override Task Update(Chitateli obj)
        {
            throw new NotImplementedException();
        }
    }
}
