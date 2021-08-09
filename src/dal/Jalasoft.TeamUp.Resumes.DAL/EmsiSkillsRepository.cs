namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Newtonsoft.Json;
    using RestSharp;

    public class EmsiSkillsRepository : ISkillsRepository
    {
        public static string Token()
        {
            string clientId = "otela8rzydlupz9t";
            string clientSecret = "Bv1Bg7aT";
            string scope = "emsi_open";

            var client = new RestClient("https://auth.emsicloud.com/connect/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", "client_id=" + clientId + "&client_secret=" + clientSecret + "&grant_type=client_credentials&scope=" + scope, ParameterType.RequestBody);
            IRestResponse<EmsiToken> response = client.Execute<EmsiToken>(request);
            string token = response.Data.Access_token;
            return token;
        }

        public string GetEmsiSkills(string token, string skill)
        {
            string typeId = "ST1";
            string typeId2 = "ST2";
            string typeId3 = "ST3";

            var client = new RestClient("https://emsiservices.com/skills/versions/latest/skills?q=" + skill + "&typeIds=" + typeId + "%2C" + typeId2 + "%2C" + typeId3 + "&fields=id%2Cname");
            var request = new RestRequest(Method.GET);

            string bearerToken = $"Bearer {token}";
            request.AddHeader("Authorization", bearerToken);
            IRestResponse<Root> response = client.Execute<Root>(request);
            return response.Content;
        }

        public IEnumerable<Skill> GetSkills(string name)
        {
            string token = Token();
            string emsiSkills = this.GetEmsiSkills(token, name);
            var response = JsonConvert.DeserializeObject<Root>(emsiSkills);
            List<Skill> listSkills = new List<Skill>();
            foreach (var data in response.Data)
            {
                Skill skill = new Skill
                {
                    Id = data.Id,
                    Name = data.Name
                };
                listSkills.Add(skill);
            }

            return listSkills;
        }
    }
}
