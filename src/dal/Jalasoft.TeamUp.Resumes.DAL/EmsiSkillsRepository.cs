namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.Utils.Exceptions;
    using Newtonsoft.Json;
    using RestSharp;

    public class EmsiSkillsRepository : ISkillsRepository
    {
        public static IRestResponse<EmsiToken> Token()
        {
            string clientId = "crb5oodm6s07qo3i";
            string clientSecret = "EjK7DwSH";
            string scope = "emsi_open";

            var client = new RestClient("https://auth.emsicloud.com/connect/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", "client_id=" + clientId + "&client_secret=" + clientSecret + "&grant_type=client_credentials&scope=" + scope, ParameterType.RequestBody);
            IRestResponse<EmsiToken> response = client.Execute<EmsiToken>(request);
            return response;
        }

        public IRestResponse<Root> GetEmsiSkills(string skill)
        {
            string typeId = "ST1";
            string typeId2 = "ST2";
            string typeId3 = "ST3";

            int limit = 10;

            var client = new RestClient("https://emsiservices.com/skills/versions/latest/skills?q=" + skill + "&typeIds=" + typeId + "%2C" + typeId2 + "%2C" + typeId3 + "&fields=id%2Cname&limit=" + limit);
            var request = new RestRequest(Method.GET);

            var token = Token();

            string bearerToken = $"Bearer {token.Data.Access_token}";

            request.AddHeader("Authorization", bearerToken);
            IRestResponse<Root> response = client.Execute<Root>(request);

            return response;
        }

        public IEnumerable<Skill> GetSkills(string name)
        {
            try
            {
                var result = this.GetEmsiSkills(name);
                var response = JsonConvert.DeserializeObject<Root>(result.Content);
                List<Skill> listSkills = new List<Skill>();
                foreach (var data in response.Data)
                {
                    Skill skills = new Skill
                    {
                        Id = data.Id,
                        Name = data.Name
                    };
                    listSkills.Add(skills);
                }

                return listSkills;
            }
            catch (Exception ex)
            {
                throw new ResumeException(ErrorsTypes.ServerError, ex);
            }
        }
    }
}
