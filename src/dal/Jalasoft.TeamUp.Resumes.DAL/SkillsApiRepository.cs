namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Newtonsoft.Json;
    using RestSharp;

    public class SkillsApiRepository : ISkillsRepository
    {
        public IEnumerable<Skill> GetSkills(string name)
        {
            string emsiSkills = this.SkillsManager(name);
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

        private IRestResponse<EmsiToken> PostEmsiToken()
        {
            var client = new RestClient("https://auth.emsicloud.com/connect/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"client_id={Constants.Constants.ClientId}&client_secret={Constants.Constants.ClientSecret}&grant_type=client_credentials&scope={Constants.Constants.Scope}", ParameterType.RequestBody);
            IRestResponse<EmsiToken> response = client.Execute<EmsiToken>(request);
            return response;
        }

        private string GetEmsiSkills(string token, string skill)
        {
            var client = new RestClient($"https://emsiservices.com/skills/versions/latest/skills?typeIds= {Constants.Constants.HardSkills}%2C{Constants.Constants.SoftSkills}%2C{Constants.Constants.Certification}&fields=id%2Cname&q={skill}");
            var request = new RestRequest(Method.GET);
            string bearerToken = $"Bearer {token}";
            request.AddHeader("Authorization", bearerToken);
            IRestResponse<Root> response = client.Execute<Root>(request);
            return response.Content;
        }

        private string SkillsManager(string skill)
        {
            var dataToken = this.PostEmsiToken();
            string emsiSkills = this.GetEmsiSkills(dataToken.Data.Access_token, skill);
            return emsiSkills;
        }
    }
}
