using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DTO;
using Xunit;

namespace TestProject
{
    public class QuizApiTest
    {
        [Fact]
        public async void GetShouldReturnTwoQuizzes()
        {
            //Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            //Act
            var result = await client.GetFromJsonAsync<List<QuizDto>>("/Quizes");

            //Assert
            Assert.Equal(1, result.Count);
        }
        [Fact]
        public async void GetShouldReturnOkStatus()
        {
            //Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            //Act
            var result = await client.GetAsync("/Quizes");

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Contains("application/json", result.Content.Headers.GetValues("Content-Type").First());
        }

    }
}
    
