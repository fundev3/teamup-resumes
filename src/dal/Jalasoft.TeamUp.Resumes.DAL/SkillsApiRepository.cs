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
            string clientId = Constants.Constants.ClientId;
            string clientSecret = Constants.Constants.ClientSecret;
            string scope = Constants.Constants.Scope;

            var client = new RestClient("https://auth.emsicloud.com/connect/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"client_id={clientId}&client_secret={clientSecret}&grant_type=client_credentials&scope={scope}", ParameterType.RequestBody);
            IRestResponse<EmsiToken> response = client.Execute<EmsiToken>(request);
            return response;
        }

        private string GetEmsiSkills(string token, string skill)
        {
            string hardSkills = Constants.Constants.HardSkills;
            string softSkills = Constants.Constants.SoftSkills;
            string certification = Constants.Constants.Certification;

            var client = new RestClient($"https://emsiservices.com/skills/versions/latest/skills?typeIds= {hardSkills}%2C{softSkills}%2C{certification}&fields=id%2Cname&q={skill}");
            var request = new RestRequest(Method.GET);
            string bearerToken = $"Bearer {token}";
            request.AddHeader("Authorization", bearerToken);
            IRestResponse<Root> response = client.Execute<Root>(request);
            return response.Content;
        }

        private string SkillsManager(string skill)
        {
            var dataToken = this.PostEmsiToken();
            string accessToken = dataToken.Data.Access_token;

            string emsiSkills = this.GetEmsiSkills(accessToken, skill);
            return emsiSkills;
        }
    }
}
