using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
using Jalasoft.TeamUp.Resumes.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using Xunit;

namespace Jalasoft.TeamUp.Resumes.DAL.Test
{
    public class ResumeSQLRepositoryTests
    {
        [Fact]
        public void GetAll_Return_Information()
        {
            var mockDapper = new Mock<IDapperWrapper>();
            //mockDapper.Setup()

            var expectedConnectionString = @"Server=SERVERNAME;Database=TESTDB;Integrated Security=true;";
            var expectedQuery = "SELECT Name, Description, RuntimeMinutes, Year FROM Movies";
            var repo = new ResumeSQLRepository(mockDapper.Object, expectedConnectionString);
            var expectedResumes = new List<Resume>() { new Resume() { Title = "Test" } };

            mockDapper.Setup(t => t.Query<Resume>(It.Is<IDbConnection>(db => db.ConnectionString == expectedConnectionString), expectedQuery))
            .Returns(expectedResumes);

            var resumes = repo.GetAll();

            Assert.Equal(expectedResumes, resumes);
        }
    }
}
