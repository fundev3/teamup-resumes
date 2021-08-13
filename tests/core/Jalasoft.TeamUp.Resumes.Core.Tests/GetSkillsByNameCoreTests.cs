namespace Jalasoft.TeamUp.Resumes.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Moq;
    using Xunit;

    public class GetSkillsByNameCoreTests
    {
        private readonly Mock<ISkillsRepository> mockRepository;
        private readonly SkillsService skillsService;

        public GetSkillsByNameCoreTests()
        {
            this.mockRepository = new Mock<ISkillsRepository>();
            this.skillsService = new SkillsService(this.mockRepository.Object);
        }

        public static IEnumerable<Skill> GetTestSkills()
        {
            var skills = new List<Skill>
            {
                new Skill
                {
                    Id = 1,
                    Name = ".NET Framework"
                },
                new Skill
                {
                    Id = 2,
                    Name = "Cisco Networking"
                },
                new Skill
                {
                    Id = 3,
                    Name = "Visual Basic .NET (Programming Language)"
                },
            };
            return skills;
        }

        [Fact]
        public void GetSkillByName_ExistName_SkillsArray()
        {
            // Arrange
            var stubSkillList = GetTestSkills();
            this.mockRepository.Setup(repository => repository.GetSkills(".NET")).Returns(stubSkillList);

            // Act
            var result = this.skillsService.GetSkills(".NET");

            // Assert
            Assert.Equal(3, result.Length);
        }
    }
}
