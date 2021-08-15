namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Caching;
    using System.Web;
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
            List<Skill> skills = new List<Skill>();
            if (response.Data.Count() == 0)
            {
                skills = null;
            }

            foreach (var data in response.Data)
            {
                var skill = new Skill
                {
                    Id = data.Id,
                    Name = data.Name
                };
                skills.Add(skill);
            }

            return skills;
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
            request.AddHeader("Authorization", $"Bearer {token}");
            IRestResponse<Root> response = client.Execute<Root>(request);
            return response.Content;
        }

        private string SkillsManager(string name)
        {
            var dataToken = this.PostEmsiToken();
            string emsiSkills = this.GetEmsiSkills(dataToken.Data.Access_token, name);
            return emsiSkills;
        }
    }
}
