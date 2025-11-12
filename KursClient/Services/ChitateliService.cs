
using KursClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace KursClient.Services
{
    public class ChitateliService : BaseService<Chitateli>
    {
        public override bool Add(Chitateli obj)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(Chitateli obj)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<Chitateli>> GetAll()
        {
            HttpClient httpClient=new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization",
                "Bearer " + RegisterUser.access_token);
            return (await httpClient.GetFromJsonAsync<List<Chitateli>>("https://localhost:7229/api/Chitateli"))!;
        }


        public override List<Chitateli> Search(string str)
        {
            throw new NotImplementedException();
        }

        public override bool Update(Chitateli obj)
        {
            throw new NotImplementedException();
        }
    }
}
