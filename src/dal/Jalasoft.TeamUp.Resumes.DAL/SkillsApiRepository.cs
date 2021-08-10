namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Caching;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Microsoft.Extensions.Caching.Memory;
    using Newtonsoft.Json;
    using RestSharp;

    public class SkillsApiRepository : ISkillsRepository
    {
        private IMemoryCache cache;

        public SkillsApiRepository(IMemoryCache memoryCache)
        {
            this.cache = memoryCache;
        }

        public IEnumerable<Skill> GetSkills(string name)
        {
            string emsiSkills = this.SkillsCacheManager(name);
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
            string clientId = "otela8rzydlupz9t";
            string clientSecret = "Bv1Bg7aT";
            string scope = "emsi_open";

            var client = new RestClient("https://auth.emsicloud.com/connect/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", "client_id=" + clientId + "&client_secret=" + clientSecret + "&grant_type=client_credentials&scope=" + scope, ParameterType.RequestBody);
            IRestResponse<EmsiToken> response = client.Execute<EmsiToken>(request);
            return response;
        }

        private string GetEmsiSkills(string token, string skill)
        {
            string typeId = "ST1";
            string typeId2 = "ST2";
            string typeId3 = "ST3";

            var client = new RestClient("https://emsiservices.com/skills/versions/latest/skills?q=" + skill + "&typeIds=" + typeId + "%2C" + typeId2 + "%2C" + typeId3 + "&fields=id%2Cname");
            var request = new RestRequest(Method.GET);

            IRestResponse<Root> response = client.Execute<Root>(request);
            return response.Content;
        }

        private string SkillsCacheManager(string skill)
        {
            string key = "Token";
            if (!this.cache.TryGetValue(key, out string accessToken))
            {
                var dataToken = this.PostEmsiToken();
                accessToken = dataToken.Data.Access_token;
                int expiresIn = dataToken.Data.Expires_in;

                MemoryCacheEntryOptions cacheExpirationOptions = new MemoryCacheEntryOptions();

                cacheExpirationOptions.AbsoluteExpiration = DateTime.Now.AddSeconds(expiresIn);
                this.cache.Set(key, dataToken.Data.Access_token, cacheExpirationOptions);
            }
            else
            {
                accessToken = this.cache.Get("Token").ToString();
            }

            string emsiSkills = this.GetEmsiSkills(accessToken, skill);
            return emsiSkills;
        }
    }
}
