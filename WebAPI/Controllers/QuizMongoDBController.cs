using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Infrastructure.MongoDB.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
namespace WebAPI.Controllers
{
    public class QuizMongoDBController : Controller
    {
        private readonly QuizUserServiceMongoDB _service;


        public QuizMongoDBController(QuizUserServiceMongoDB userService)
        {
            _service = userService;
        }

        [HttpGet("/api/v2/quizzes")]
        public IEnumerable<Quiz> GetAllQuizzes()
        {
            return _service.FindAllQuizzess();
        }
        [HttpGet("/api/v2/quizbyid/{quizId}")]
        public ActionResult<Quiz> FindQuizById(int id)
        {
            var quiz = _service.FindQuizById(id);
            if (quiz == null)
            {
                return NotFound();
            }

            return Ok(quiz);
        }
    }
}

