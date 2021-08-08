namespace Jalasoft.TeamUp.Resumes.DAL.Interfaces
{
    using Jalasoft.TeamUp.Resumes.Models;

    public interface ISkillsRepository
    {
        public Skill[] GetSkills(string name);
    }
}
