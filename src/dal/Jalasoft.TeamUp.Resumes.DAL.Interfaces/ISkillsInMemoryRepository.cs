namespace Jalasoft.TeamUp.Resumes.DAL.Interfaces
{
    using Jalasoft.TeamUp.Resumes.Models;

    public interface ISkillsInMemoryRepository
    {
        public Skill[] GetSkills(string name);
    }
}
